using System.Collections.Generic;

namespace DotNetWordLadder.Interfaces
{
    internal interface IWordLadder
    {
        public IList<LinkedList<string>> GetLadders();
    }
}