using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FD
{
    public class FileAccessor : IFileAccessor
    {
        public IEnumerable<string> ReadFile(string path, Encoding encoding)
        {
            return File.ReadLines(path, encoding);
        }

        public void WriteFile(string path, IEnumerable<string> lines, Encoding encoding)
        {
            File.WriteAllLinesAsync(path, lines, encoding);
        }

        public bool CheckExists(string path)
        {
            return File.Exists(path);
        }
    }
}