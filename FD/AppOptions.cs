namespace FD
{
    public class AppOptions
    {
        public AppOptions(string sourceFilePath, string destinationFilePath)
        {
            SourceFilePath = sourceFilePath;
            DestinationFilePath = destinationFilePath;
        }

        public string SourceFilePath { get; }
        public string DestinationFilePath { get; }
    }
}