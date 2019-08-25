using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FD
{
    internal class Program
    {
        private const string CodePage = "windows-1251";
        private const int MaxDegreeOfParallelism = 10;
        private static ServiceProvider _serviceProvider;

        private static void Main(string[] args)
        {
            RegisterServices();

            var logger = _serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();
            var lineAnalyzer = _serviceProvider.GetService<ILineAnalyzer>();
            var fileAccessor = _serviceProvider.GetService<IFileAccessor>();
            var validatorFacade = _serviceProvider.GetService<IArgsValidatorFacade>();
            var words = new ConcurrentDictionary<string, int>();

            logger.LogInformation("Starting application");
            var validationError = validatorFacade.Validate(args);
            if (!string.IsNullOrEmpty(validationError))
            {
                logger.LogInformation("Incorrect input parameters please check it out");
                logger.LogInformation(validationError);
                return;
            }

            var opt = new AppOptions(args[0], args[1]);

            fileAccessor
                .ReadFile(opt.SourceFilePath, Encoding.GetEncoding(CodePage))
                .AsParallel()
                .WithDegreeOfParallelism(MaxDegreeOfParallelism)
                .ForAll(x => lineAnalyzer.Analyze(x, words));

            var lines = words
                .OrderByDescending(x => x.Value)
                .Select(x => $"{x.Key},{x.Value}\n");
            fileAccessor.WriteFile(opt.DestinationFilePath, lines, Encoding.GetEncoding(CodePage));

            logger.LogInformation("Done");
        }


        private static void RegisterServices()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _serviceProvider = new ServiceCollection()
                .AddSingleton<IFileAccessor, FileAccessor>()
                .AddSingleton<ILineAnalyzer, LineAnalyzer>()
                .AddSingleton<ILineSplitter, LineSplitter>()
                .AddSingleton<IFileExistsValidator, FileExistsValidator>()
                .AddSingleton<IArgsCountValidator, ArgsCountValidator>()
                .AddSingleton<IArgsValidatorFacade, ArgsValidatorFacade>()
                .AddLogging(configure => configure.AddConsole())
                .BuildServiceProvider();
        }
    }
}