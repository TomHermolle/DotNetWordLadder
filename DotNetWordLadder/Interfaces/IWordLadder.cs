using System.Collections.Generic;

namespace DotNetWordLadder.Interfaces
{
    public interface IWordLadder
    {
        public IList<LinkedList<string>> GetLadders();
    }
}