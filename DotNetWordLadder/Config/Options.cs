using System;
using System.IO;
using CommandLine;

namespace DotNetWordLadder.Config
{
    /// <summary>
    /// Class to validate command line arguments and transform into Options using the CommandLineParser nuget package. Properties are mapped to OptionAttributes
    /// which specify argument shortName, longName, Required, and HelpText.
    /// </summary>
    public class Options 
    {
        [Option('d', "DictionaryFile", Required = true, HelpText = "Path to dictionary file.")]
        public virtual string DictionaryFile { get; set; }

        [Option('s', "StartWord", Required = true, HelpText = "Four-letter word from which to start the dictionary traversal.")]
        public virtual string StartWord { get; set; }

        [Option('e', "EndWord", Required = true, HelpText = "Four-letter end (target) word at which the dictionary traversal has completed.")]
        public virtual string EndWord { get; set; }

        [Option('r', "ResultFile", Required = true, HelpText = "Path to output file containing solution word ladder(s).")]
        public virtual string ResultFile { get; set; }

        [Option(Hidden=true)]
        public virtual int WordLength { get; set; }

        /// <summary>
        /// Semantic validation of arguments to run after initial argument syntax parsing has succeeded
        /// </summary>
        public void Validate()
        {
            if (!File.Exists(DictionaryFile)) throw new ArgumentException($"DictionaryFile '{DictionaryFile}' does not exist");
            if (Path.GetExtension(ResultFile) != ".txt") throw new ArgumentException($"ResultFile '{ResultFile}' must have a file extension of '.txt'");
            if (!Directory.Exists(Path.GetDirectoryName(ResultFile))) throw new ArgumentException($"ResultFile directory '{Path.GetDirectoryName(ResultFile)}' does not exist, path must be specified");
            if (StartWord.Length < 4) throw new ArgumentException($"StartWord '{StartWord}' must be at least 4 characters long");
            if (EndWord.Length < 4) throw new ArgumentException($"EndWord '{EndWord}' must be at least 4 characters long");
            if (StartWord.Length != EndWord.Length) throw new ArgumentException($"StartWord '{StartWord}' and EndWord '{EndWord}' must be the same length");
            //if (WordLength < 4) throw new ArgumentException($"WordLength '{WordLength}' must be >= 4");
            WordLength = StartWord.Length;
        }
    }
}
