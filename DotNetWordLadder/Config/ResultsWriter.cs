using System.Collections.Generic;
using System.IO;
using DotNetWordLadder.Interfaces;

namespace DotNetWordLadder.Config
{
    public class ResultsWriter : IResultsWriter
    {
        private readonly string _resultsFile;
        public ResultsWriter(string resultsFile)
        {
            _resultsFile = resultsFile;
        }

        public void WriteResults(IList<LinkedList<string>> results)
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
        }
    }
}
