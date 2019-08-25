namespace FD
{
    public class FileExistsValidator : IFileExistsValidator
    {
        private readonly IFileAccessor _fileAccessor;

        public FileExistsValidator(IFileAccessor fileAccessor)
        {
            _fileAccessor = fileAccessor;
        }

        public string Message => "File does not exist";

        public bool IsValid(string arg)
        {
            return _fileAccessor.CheckExists(arg);
        }
    }
}