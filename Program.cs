using System;
using CommandLine;

namespace DotNetWordLadder
{
    internal class Program
    {
        /// <summary>
        /// Driver method to parse and validate input arguments, execute a traversal through an input dictionary, and output a file of word ladder results.
        /// </summary>
        /// <param name="args">Four arguments expected as specified in the properties of the CommandLineOptions class</param>
        /// <returns>Integer return code, zero on successful completion</returns>
        private static int Main(string[] args)
        {
            try
            {
                return Parser.Default.ParseArguments<CommandLineOptions>(args)
                    .MapResult(
                        (options) =>
                        {
                            options.ValidateArguments();

                            ExecuteWordLadder(options.StartWord,options.EndWord,options.DictionaryFile,options.ResultFile,options.WordLength);
                            return 0;
                        },
                        errors => 1);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Invalid arguments detected: {0}", ex);
                return 1;
            }
        }


        private static void ExecuteWordLadder(string startWord, string endWord, string dictionaryFile, string resultFile, int wordLength)
        {
            var cleanDictionary = DictionaryFactory.GetDictionary(dictionaryFile, wordLength);
            var wordLadder = new WordLadder(startWord, endWord, cleanDictionary);
            var results = wordLadder.GetLadders();
            
            //var results = FindWordLadders(start, end,
            //    new List<string> { "sail", "mail", "rain", "nail", "pain", "main", "rail", "pail", "ruin" ,"main","seal","meal","real","peal","mall","pall","said","maid","paid","raid"});

            var resultCount = results.Count;
            for (var i = 0; i < resultCount; ++i)
            {
                Console.WriteLine("Solution path {0} of {1}", i+1, resultCount);
                Console.WriteLine("------------------------");

                foreach (var word in results[i])
                {
                    Console.WriteLine(word);
                }
                Console.WriteLine();
            }
        }


    }
}
