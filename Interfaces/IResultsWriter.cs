using System.Collections.Generic;

namespace DotNetWordLadder.Interfaces
{
    internal interface IResultsWriter
    {
        IList<LinkedList<string>> WriteResults(IList<LinkedList<string>> results);
    }
}