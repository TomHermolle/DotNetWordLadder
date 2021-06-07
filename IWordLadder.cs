using System.Collections.Generic;

namespace DotNetWordLadder
{
    internal interface IWordLadder<T> where T : ICollection<string>
    {
        public IList<T> GetLadders();
    }
}