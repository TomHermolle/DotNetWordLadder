using System.Collections.Generic;
using DotNetWordLadder.Interfaces;

namespace DotNetWordLadder
{
    internal class WordLadderRunner
    {
        public IWordLadderDictionary Dictionary { get;}
        public IWordLadder WordLadder { get;}
        public IList<LinkedList<string>> Results { get; private set; }
        public IResultsWriter ResultsWriter { get; }
        public string StartWord { get; }
        public string EndWord { get; }

        public WordLadderRunner(string startWord, string endWord,IWordLadderDictionary dictionary, IResultsWriter resultsWriter)
        {
            StartWord = startWord;
            EndWord = endWord;
            Dictionary = dictionary;
            ResultsWriter = resultsWriter;
            WordLadder = new WordLadder(startWord, endWord, Dictionary.GetDictionary());
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
