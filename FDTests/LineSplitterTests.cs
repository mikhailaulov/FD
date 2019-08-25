using System.Collections.Generic;
using FD;
using FluentAssertions;
using Xunit;

namespace FDTests
{
    public class LineSplitterTests
    {
        private readonly LineSplitter _sut;

        public LineSplitterTests()
        {
            _sut = new LineSplitter();
        }


        [Theory]
        [InlineData("line", new[] {"line"})]
        [InlineData(" line ", new[] {"line"})]
        [InlineData("line line", new[] {"line", "line"})]
        [InlineData("line \n line", new[] {"line", "line"})]
        [InlineData("one two", new[] {"one", "two"})]
        [InlineData("one two one two", new[] {"one", "two", "one", "two"})]
        [InlineData("one \ntwo one\n two", new[] {"one", "two", "one", "two"})]
        public void Split_DifferentSets_ShouldSplitStringCorrect(string line, IEnumerable<string> expected)
        {
            var actual = _sut.Split(line);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}