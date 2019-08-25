using System.Collections.Generic;
using System.Text;

namespace FD
{
    public interface IFileAccessor
    {
        IEnumerable<string> ReadFile(string path, Encoding encoding);

        void WriteFile(string path, IEnumerable<string> lines, Encoding encoding);

        bool CheckExists(string path);
    }
}