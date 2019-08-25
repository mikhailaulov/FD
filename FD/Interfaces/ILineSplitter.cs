using System.Collections.Generic;

namespace FD
{
    public interface ILineSplitter
    {
        IEnumerable<string> Split(string line);
    }
}