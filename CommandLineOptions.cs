﻿using System;
using System.IO;
using CommandLine;

namespace DotNetWordLadder
{
    internal class CommandLineOptions
    {
        [Option('d', "DictionaryFile", Required = true, HelpText = "Path to dictionary file.")]
        public string DictionaryFile { get; set; }

        [Option('s', "StartWord", Required = true, HelpText = "Four-letter word from which to start the dictionary traversal.")]
        public string StartWord { get; set; }

        [Option('e', "EndWord", Required = true, HelpText = "Four-letter end (target) word at which the dictionary traversal has completed.")]
        public string EndWord { get; set; }

        [Option('r', "ResultFile", Required = true, HelpText = "Path to output file containing solution word ladder(s).")]
        public string ResultFile { get; set; }

        public void ValidateArguments()
        {
            if (!File.Exists(DictionaryFile)) throw new ArgumentException($"DictionaryFile '{DictionaryFile}' does not exist");
            if (Path.GetExtension(ResultFile) != ".txt") throw new ArgumentException($"ResultFile '{ResultFile}' must have a file extension of '.txt'");
            if (!Directory.Exists(Path.GetDirectoryName(ResultFile))) throw new ArgumentException($"ResultFile directory '{Path.GetDirectoryName(ResultFile)}' does not exist");
            if (StartWord.Length != 4) throw new ArgumentException($"StartWord '{StartWord}' is not 4 characters long");
            if (EndWord.Length != 4) throw new ArgumentException($"EndWord '{EndWord}' is not 4 characters long");
        }
    }
}