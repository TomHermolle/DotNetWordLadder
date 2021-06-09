using System.Collections.Generic;
using System.IO;
using DotNetWordLadder.Interfaces;

namespace DotNetWordLadder.IO
{
    internal class ResultsWriter : IResultsWriter
    {
        private readonly string _resultsFile;
        public ResultsWriter(string resultsFile)
        {
            _resultsFile = resultsFile;
        }

        public IList<LinkedList<string>> WriteResults(IList<LinkedList<string>> results)
        {
            using var fileWriter = new StreamWriter(_resultsFile);
            var resultCount = results.Count;
            for (var i = 0; i < resultCount; ++i)
            {
                fileWriter.WriteLine("Solution path {0} of {1}", i + 1, resultCount);
                fileWriter.WriteLine("------------------------");

                foreach (var word in results[i])
                {
                    fileWriter.WriteLine(word);
                }
                fileWriter.WriteLine();
            }

            return results;
        }
    }
}
