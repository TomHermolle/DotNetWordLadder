using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;

namespace DotNetWordLadder
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            try
            {
                return Parser.Default.ParseArguments<CommandLineOptions>(args)
                    .MapResult(
                        (options) =>
                        {
                            options.ValidateArguments();
                            ExecuteWordLadder(options.StartWord,options.EndWord);
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

        private static void ExecuteWordLadder(string start, string end)
        {
            var results = FindWordLadders(start, end,
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

        private static List<LinkedList<string>> FindWordLadders(string startWord, string endWord, List<string> dictionary)
        {
            var results = new List<LinkedList<string>>();
            var currentLevelNodeLinkedLists = new List<LinkedList<string>> { new (new[] { startWord }) };
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
                        continue;
                    }

                    //From the current word, add to the next level node list the set of dictionary words differing by only one character
                    for (var i = dictionary.Count() - 1; i >= 0; i--)
                    {
                        if (!WordsDifferByOneCharacter(currentLevelNodeLinkedList.Last?.Value, dictionary[i])) continue;

                        var nextLevelNodeLinkedList = new LinkedList<string>(currentLevelNodeLinkedList.ToArray());
                        nextLevelNodeLinkedList.AddLast(dictionary[i]);
                        nextLevelNodeLinkedLists.Add(nextLevelNodeLinkedList);
                        //remove processed words from the dictionary so we don't traverse back "up" the graph next iteration
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
            return word1.Where((character, i) => !character.Equals(word2[i])).Count() == 1;
        }
    }
}
