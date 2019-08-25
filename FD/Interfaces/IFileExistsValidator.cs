namespace FD
{
    public interface IFileExistsValidator
    {
        string Message { get; }
        bool IsValid(string arg);
    }
}