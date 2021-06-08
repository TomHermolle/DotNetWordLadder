using System.Collections.Generic;

namespace DotNetWordLadder.Interfaces
{
    internal interface IWordLadderDictionary
    {
        IList<string> GetDictionary();
    }
}