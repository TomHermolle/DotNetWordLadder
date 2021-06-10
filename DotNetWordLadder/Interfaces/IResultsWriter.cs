using System.Collections.Generic;

namespace DotNetWordLadder.Interfaces
{
    public interface IResultsWriter
    {
        void WriteResults(IList<LinkedList<string>> results);
    }
}