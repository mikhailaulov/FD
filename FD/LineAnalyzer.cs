using System.Collections.Concurrent;

namespace FD
{
    public class LineAnalyzer : ILineAnalyzer
    {
        private readonly ILineSplitter _lineSplitter;

        public LineAnalyzer(ILineSplitter lineSplitter)
        {
            _lineSplitter = lineSplitter;
        }

        public void Analyze(string line, ConcurrentDictionary<string, int> words)
        {
            foreach (var word in _lineSplitter.Split(line))
                words.AddOrUpdate(word, 1, (key, value) => value + 1);
        }
    }
}