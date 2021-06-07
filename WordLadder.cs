using System.Collections.Generic;
using System.Linq;

namespace DotNetWordLadder
{
    internal class WordLadder : IWordLadder<LinkedList<string>>
    {
        private readonly string _startWord;
        private readonly string _endWord;
        private readonly IList<string> _dictionary;

        public WordLadder(string startWord, string endWord, IList<string> dictionary)
        {
            _startWord = startWord;
            _endWord = endWord;
            _dictionary = dictionary;
        }

        public IList<LinkedList<string>> GetLadders()
        {
            var results = new List<LinkedList<string>>();
            var currentLevelNodeLinkedLists = new List<LinkedList<string>> { new(new[] { _startWord }) };
            var endWordReached = false;

            while (currentLevelNodeLinkedLists.Count != 0 && !endWordReached)
            {
                var nextLevelNodeLinkedLists = new List<LinkedList<string>>();
                foreach (var currentLevelNodeLinkedList in currentLevelNodeLinkedLists)
                {
                    //If we've found a word that is only one step from the end word, link the nodes and add to the results list
                    if (WordsDifferByOneCharacter(_endWord, currentLevelNodeLinkedList.Last?.Value))
                    {
                        currentLevelNodeLinkedList.AddLast(_endWord);
                        results.Add(currentLevelNodeLinkedList);
                        //the end word has been reached so once we've checked all nodes at this level we can finish processing
                        endWordReached = true;
                        continue;
                    }

                    //From the current word, add to the next level node list the set of dictionary words differing by only one character
                    for (var i = _dictionary.Count() - 1; i >= 0; i--)
                    {
                        if (!WordsDifferByOneCharacter(currentLevelNodeLinkedList.Last?.Value, _dictionary[i])) continue;

                        var nextLevelNodeLinkedList = new LinkedList<string>(currentLevelNodeLinkedList.ToArray());
                        nextLevelNodeLinkedList.AddLast(_dictionary[i]);
                        nextLevelNodeLinkedLists.Add(nextLevelNodeLinkedList);
                        //remove processed words from the dictionary so we don't traverse back "up" the graph next iteration
                        _dictionary.RemoveAt(i);
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
