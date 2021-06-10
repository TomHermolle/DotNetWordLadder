# DotNetWordLadder

## Introduction

.Net 5 C# console app to traverse an input word graph (dictionary) from a start to an end word in the shortest number of hops, changing only one letter at a time.

## Inputs

Argument | Description
-------- | -----------
`--StartWord \| -s` | Word in the dictionary from which to start the search
`--EndWord \| -e` | Word in the dictionary at which the search should stop
`--DictionaryFile \| -d` | Path to file containing dictionary of words
`--ResultFile \| -r` | Path to output file containing matched word ladders

The solution contains a dictionary file called `words-english.txt` in the Resources folder.

StartWord and EndWord must be of the same length, and must be at least 4 characters long.

## Algorithm

The first choice to make is between a Breadth-First Search (BFS) or Depth-First Search (DFS). 

BFS involves traversing all the nodes at a particular level of the graph, before finding all child nodes at the next level, then traversing those, etc. BFS should be faster when the graph is not expected to be particularly wide and/or is expected to be deep, and when more than one solution is possible.

DFS traverses each branch of the graph from top to bottom, before back-tracking to search the next branch, etc. DFS is expected to be faster for solving puzzles with only one solution, where the target node is expected to be deep in the graph, or where the graph is very broad.

In this case it is clear that more than one solution may often exist; and also that the graph will not be particularly wide, there being only a small number of possible actual words that can be formed from a given word by changing only one letter. 

So the chosen algorithm is BFS, although interesting future work would be to experiment with the performance effects of DFS vs BFS for word nodes of different lengths and dictionaries of different sizes. 

The time complexity of both searches is `O(N + E)` where `N` is the number of nodes and `E` is the number of edges (connections) in the graph, but the space complexity is dependent on the shape of the graph.

### Pseudo-code

```
    Loop from StartWord while dictionary words exist and not EndWordFound
    {
        ForEach CurrentWord at current level in graph
        {
            If EndWord is only one letter different to CurrentWord
            {
                Add path from StartWord through CurrentWord to EndWord to Results
                EndWordFound = true
                Skip to next word at this level
            }

            ForEach remaining DictionaryWord 
            {
                If EndWord is only one letter different to DictionaryWord
                {
                    Add DictionaryWord to list of next level words to process
                    Remove DictionaryWord from dictionary
                }
            }
        }
        Move to next level in graph
    }
    
    Return Results
```

### Implementation Notes

A `LinkedList<string>` object is used to store each possible solution path through the graph, so that the solution can be output to the final results file in linked node order.

Multiple results are possible so the results are stored as `IList<LinkedList<string>>`.

The dictionary file contains acronyms, names, special characters, etc. These are filtered out using the Regex pattern matcher, which has had [multiple performance enhancements](https://devblogs.microsoft.com/dotnet/regex-performance-improvements-in-net-5/) implemented in .Net 5.0.

The filtered dictionary words are stored in a `List<string>`. Iterating through the dictionary would be faster if this was a `HashSet<string>`; however, removing each visited word node from the dictionary would be more problematic because the `HashSet` object has no indexer; we would need to copy each node to be deleted into a separate list and then delete in bulk once each iteration was complete. It is considered that this would outweigh any performance gain made by using the `HashSet`, however this has not been tested.