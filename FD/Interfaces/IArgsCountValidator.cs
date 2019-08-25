namespace FD
{
    public interface IArgsCountValidator
    {
        string Message { get; }
        bool IsValid(params string[] args);
    }
}