using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace DotNetWordLadder
{
    internal static class DictionaryFactory
    {
        /// <summary>
        /// Returns a list of lower case dictionary entries of a specified length found in a specified file,
        /// ignoring abbreviations, acronyms, special characters, numbers, and names.
        /// </summary>
        /// <param name="dictionaryPath">A valid file path to an existing text file containing a list of words</param>
        /// <param name="wordLength">Length of the words to extract</param>
        /// <returns></returns>
        public static IList<string> GetDictionary(string dictionaryPath,int wordLength)
        {
            var cleanDictionary = new List<string>();
            var regexPattern = "^[a-z]{" + wordLength + "}$";
            var validEntryRegex = new Regex(regexPattern);

            using var fileReader = new StreamReader(File.Open(dictionaryPath, FileMode.Open));
            while (fileReader.Peek() >= 0)
            {
                var dictionaryEntry = fileReader.ReadLine() ?? string.Empty;
                if (validEntryRegex.IsMatch(dictionaryEntry)) cleanDictionary.Add(dictionaryEntry);
            }

            return cleanDictionary;
        }
    }
}