using System.Collections.Generic;
using DotNetWordLadder.Config;
using DotNetWordLadder.Interfaces;

namespace DotNetWordLadder.Execution
{
    internal class WordLadderRunner
    {
        public IWordLadder WordLadder { get;}
        public IList<LinkedList<string>> Results { get; private set; }
        public IResultsWriter ResultsWriter { get; }

        public WordLadderRunner(WordLadderConfig config, IResultsWriter resultsWriter)
        {
            ResultsWriter = resultsWriter;
            WordLadder = new WordLadder(config);
        }

        public WordLadderRunner Run()
        {
            Results = WordLadder.GetLadders();
            return this;
        }

        public WordLadderRunner WriteResults()
        {
            ResultsWriter.WriteResults(Results);
            return this;
        }
    }
}
