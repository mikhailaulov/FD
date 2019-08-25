using System;
using System.Collections.Generic;

namespace FD
{
    public class LineSplitter : ILineSplitter
    {
        private static readonly char[] Separators = {' ', '\n'};

        public IEnumerable<string> Split(string line)
        {
            return line.Split(Separators, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}