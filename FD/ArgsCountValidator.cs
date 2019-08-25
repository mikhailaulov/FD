namespace FD
{
    public class ArgsCountValidator : IArgsCountValidator
    {
        public string Message => "Incorrect count of args";

        public bool IsValid(params string[] args)
        {
            return args.Length == 2;
        }
    }
}