using System.Collections.Concurrent;

namespace FD
{
    public interface ILineAnalyzer
    {
        void Analyze(string line, ConcurrentDictionary<string, int> words);
    }
}