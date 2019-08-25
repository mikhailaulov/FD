using System.Collections.Concurrent;
using System.Collections.Generic;
using FD;
using FluentAssertions;
using Moq;
using Xunit;

namespace FDTests
{
    public class LineAnalyzerTests
    {
        public LineAnalyzerTests()
        {
            _lineSplitter = new Mock<ILineSplitter>();
            _sut = new LineAnalyzer(_lineSplitter.Object);
        }

        private readonly Mock<ILineSplitter> _lineSplitter;
        private readonly LineAnalyzer _sut;

        [Fact]
        public void Analyze_LineHasFewWords_DictShouldHasCorrectCount()
        {
            var dict = new ConcurrentDictionary<string, int>();
            var word = "line";
            var line = $"{word}{word}{word}";
            _lineSplitter.Setup(x => x.Split(line)).Returns(new[] {word, word, word});

            _sut.Analyze(line, dict);

            dict.Should().Contain(new KeyValuePair<string, int>(word, 3));
            _lineSplitter.Verify();
        }

        [Fact]
        public void Analyze_LineHasOneWord_ShouldBeAddedToDict()
        {
            var dict = new ConcurrentDictionary<string, int>();
            var line = "line";
            _lineSplitter.Setup(x => x.Split(line)).Returns(new[] {line});

            _sut.Analyze(line, dict);

            dict.Should().Contain(new KeyValuePair<string, int>(line, 1));
            _lineSplitter.Verify();
        }

        [Fact]
        public void Analyze_LineHasTwoWords_DictShouldHasCorrectCount()
        {
            var dict = new ConcurrentDictionary<string, int>();
            var wordOne = "wordOne";
            var wordTwo = "wordTwo";
            var line = $"{wordOne}{wordTwo}";
            _lineSplitter.Setup(x => x.Split(line)).Returns(new[] {wordOne, wordTwo});

            _sut.Analyze(line, dict);

            dict.Should().Contain(
                new KeyValuePair<string, int>(wordOne, 1),
                new KeyValuePair<string, int>(wordTwo, 1));
            _lineSplitter.Verify();
        }
    }
}