using System;
using System.Collections.Generic;
using System.Linq;

namespace FD
{
    public class ArgsValidatorFacade : IArgsValidatorFacade
    {
        private readonly IArgsCountValidator _argsCountValidator;
        private readonly IFileExistsValidator _existsValidator;

        public ArgsValidatorFacade(IFileExistsValidator existsValidator, IArgsCountValidator argsCountValidator)
        {
            _existsValidator = existsValidator;
            _argsCountValidator = argsCountValidator;
        }

        public string Validate(string[] args)
        {
            if (!_argsCountValidator.IsValid(args)) 
                 return _argsCountValidator.Message;

            if (!_existsValidator.IsValid(args.FirstOrDefault())) 
                 return _existsValidator.Message;
            
            return String.Empty;
        }
    }
}