using System;
using System.Collections.Generic;
using DotNetWordLadder.Interfaces;

namespace DotNetWordLadder.Execution
{
    internal class WordLadderRunner
    {
        public IList<LinkedList<string>> Results { get; private set; }
        private readonly IResultsWriter _resultsWriter;
        private readonly IWordLadder _wordLadder;

        public WordLadderRunner(IWordLadder wordLadder, IResultsWriter resultsWriter)
        {
            _resultsWriter = resultsWriter;
            _wordLadder = wordLadder;
        }

        public WordLadderRunner Run()
        {
            Results = _wordLadder.GetLadders();
            return this;
        }

        public WordLadderRunner WriteResults()
        {
            _resultsWriter.WriteResults(Results);
            return this;
        }
    }
}
