using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using DotNetWordLadder.Interfaces;

namespace DotNetWordLadder
{
    internal class WordLadderDictionary : IWordLadderDictionary
    {
        private readonly string _dictionaryFile;
        private readonly int _wordLength;

        internal WordLadderDictionary(string dictionaryFile,int wordLength)
        {
            _dictionaryFile = dictionaryFile;
            _wordLength = wordLength;
        }

        /// <summary>
        /// Returns a list of lower case dictionary entries of a specified length found in a specified file,
        /// ignoring abbreviations, acronyms, special characters, numbers, and names.
        /// </summary>
        public IList<string> GetDictionary()
        {
            var cleanDictionary = new List<string>();
            var regexPattern = "^[a-z]{" + _wordLength + "}$";
            var validEntryRegex = new Regex(regexPattern);

            using var fileReader = new StreamReader(File.Open(_dictionaryFile, FileMode.Open));
            while (fileReader.Peek() >= 0)
            {
                var dictionaryEntry = fileReader.ReadLine() ?? string.Empty;
                if (validEntryRegex.IsMatch(dictionaryEntry)) cleanDictionary.Add(dictionaryEntry);
            }

            return cleanDictionary;
        }
    }
}