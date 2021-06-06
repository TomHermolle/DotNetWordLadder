using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;

namespace DotNetWordLadder
{
    class Program
    {
        public class CommandLineOptions
        {
            [Option('d',"DictionaryFile",Required = true,HelpText = "Path to dictionary file.")]
            public string DictionaryFile { get; set; }

            [Option('s', "StartWord", Required = true, HelpText = "Four-letter word from which to start the dictionary traversal.")]
            public string StartWord { get; set; }

            [Option('e', "EndWord", Required = true, HelpText = "Four-letter end (target) word at which the dictionary traversal has completed.")]
            public string EndWord { get; set; }

            [Option('r', "ResultFile", Required = true, HelpText = "Path to output file containing solution word ladder(s).")]
            public string ResultFile { get; set; }
        }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed(o => ExecuteWordLadder(o.StartWord, o.EndWord));
        }


        private static void ExecuteWordLadder(string start, string end)
        {
            var results = GetResults(start, end,
                new List<string> { "sail", "mail", "rain", "nail", "pain", "main", "rail", "pail", "ruin" ,"main","seal","meal","real","peal","mall","pall","said","maid","paid","raid"});

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

        private static List<LinkedList<string>> GetResults(string startWord, string endWord, List<string> dictionary)
        {
            var results = new List<LinkedList<string>>();
            var startWordList = new LinkedList<string>();
            startWordList.AddFirst(startWord);
            var currentLevelNodeLinkedLists = new List<LinkedList<string>> { startWordList };
            var endWordReached = false;

            while (currentLevelNodeLinkedLists.Count != 0 && !endWordReached)
            {
                var nextLevelNodeLinkedLists = new List<LinkedList<string>>();
                foreach (var currentLevelNodeLinkedList in currentLevelNodeLinkedLists)
                {
                    //If we've found a word that is only one step from the end word, link the nodes and add to the results list
                    if (WordsDifferByOneCharacter(endWord, currentLevelNodeLinkedList.Last?.Value))
                    {
                        currentLevelNodeLinkedList.AddLast(endWord);
                        results.Add(currentLevelNodeLinkedList);
                        //the end word has been reached so once we've checked all nodes at this level we can finish processing
                        endWordReached = true;
                    }

                    //From the current word, add to the next level node list the set of dictionary words differing by only one character
                    for (var i = dictionary.Count() - 1; i >= 0; i--)
                    {
                        if (!WordsDifferByOneCharacter(currentLevelNodeLinkedList.Last?.Value, dictionary[i]))
                        {
                            continue;
                        }

                        var nextLevelNodeLinkedList = new LinkedList<string>(currentLevelNodeLinkedList.ToArray());
                        nextLevelNodeLinkedList.AddLast(dictionary[i]);
                        nextLevelNodeLinkedLists.Add(nextLevelNodeLinkedList);
                        dictionary.RemoveAt(i);
                    }
                }

                //Shift to the next level of nodes to traverse 
                currentLevelNodeLinkedLists = nextLevelNodeLinkedLists;
            }

            return results;
        }

        private static bool WordsDifferByOneCharacter(string word1, string word2)
        {
            // all string have same length
            return word1.Where((character, i) => !character.Equals(word2[i])).Count() == 1;
        }
    }
}
