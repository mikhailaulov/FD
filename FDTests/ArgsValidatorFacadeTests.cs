using FD;
using FluentAssertions;
using Moq;
using Xunit;

namespace FDTests
{
    public class ArgsValidatorFacadeTests
    {
        public ArgsValidatorFacadeTests()
        {
            _fileExistsValidator = new Mock<IFileExistsValidator>();
            _argsCountValidator = new Mock<IArgsCountValidator>();
            _sut = new ArgsValidatorFacade(_fileExistsValidator.Object, _argsCountValidator.Object);
        }

        private readonly ArgsValidatorFacade _sut;
        private readonly Mock<IFileExistsValidator> _fileExistsValidator;
        private readonly Mock<IArgsCountValidator> _argsCountValidator;

        [Fact]
        public void Validate_FileDoestExist_ShouldReturnError()
        {
            var args = new[] {"arg1", "arg2"};
            var mess = "mess";
            _argsCountValidator.Setup(x => x.IsValid(args)).Returns(true);
            _fileExistsValidator.Setup(x => x.IsValid(args[1])).Returns(false);
            _fileExistsValidator.Setup(x => x.Message).Returns(mess);

            var actual = _sut.Validate(args);

            actual.Should().BeEquivalentTo(mess);
            VerifyAll();
        }

        [Fact]
        public void Validate_HasNoArg_ShouldReturnError()
        {
            var args = new[] {"arg1"};
            var mess = "mess";
            _argsCountValidator.Setup(x => x.IsValid(args)).Returns(false);
            _argsCountValidator.Setup(x => x.Message).Returns(mess);

            var actual = _sut.Validate(args);

            actual.Should().BeEquivalentTo(mess);
            VerifyAll();
        }

        [Fact]
        public void Validate_NoErrors_ShouldReturnNull()
        {
            var args = new[] {"arg1", "arg2"};
            _argsCountValidator.Setup(x => x.IsValid(args)).Returns(true);
            _fileExistsValidator.Setup(x => x.IsValid(args[1])).Returns(true);

            var actual = _sut.Validate(args);

            actual.Should().BeNullOrEmpty();
            VerifyAll();
        }

        private void VerifyAll()
        {
            _argsCountValidator.Verify();
            _fileExistsValidator.Verify();
        }
    }
}