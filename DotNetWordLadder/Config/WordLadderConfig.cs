using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace DotNetWordLadder.Config
{
    public class WordLadderConfig
    {
        public virtual Options Options { get; }

        public WordLadderConfig()
        {
        }

        public WordLadderConfig(Options options)
        {
            Options = options;
        }

        public virtual IList<string> Dictionary => GetDictionary();

        /// <summary>
        /// Returns a list of lower case dictionary entries of a specified length found in a specified file,
        /// ignoring abbreviations, acronyms, special characters, numbers, and names.
        /// </summary>
        private IList<string> GetDictionary()
        {
            var cleanDictionary = new List<string>();
            var regexPattern = "^[a-z]{" + Options.WordLength + "}$";
            var validEntryRegex = new Regex(regexPattern);

            using var fileReader = new StreamReader(File.Open(Options.DictionaryFile, FileMode.Open));
            while (fileReader.Peek() >= 0)
            {
                var dictionaryEntry = fileReader.ReadLine() ?? string.Empty;
                if (validEntryRegex.IsMatch(dictionaryEntry)) cleanDictionary.Add(dictionaryEntry);
            }

            return cleanDictionary;
        }
    }
}