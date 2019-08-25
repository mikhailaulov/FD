using System.Collections.Generic;

namespace FD
{
    public interface IArgsValidatorFacade
    {
        string Validate(string[] args);
    }
}