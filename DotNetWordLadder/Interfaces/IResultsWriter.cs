using System.Collections.Generic;

namespace DotNetWordLadder.Interfaces
{
    public interface IResultsWriter
    {
        IList<LinkedList<string>> WriteResults(IList<LinkedList<string>> results);
    }
}