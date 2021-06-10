using System;
using System.Diagnostics;
using CommandLine;
using DotNetWordLadder.Config;
using DotNetWordLadder.Execution;

namespace DotNetWordLadder
{
    internal class Program
    {
        /// <summary>
        /// Driver method to parse and validate input arguments, execute a traversal through an input dictionary, and output a file of word ladder results.
        /// Uses the CommandLineParser nuget package for argument syntax parsing: https://github.com/commandlineparser/commandline.
        /// </summary>
        /// <param name="args">Four arguments expected as specified in the properties of the Options class</param>
        /// <returns>Integer return code, zero on successful completion</returns>
        private static int Main(string[] args)
        {
            try
            {
                return Parser.Default.ParseArguments<Options>(args)
                    .MapResult(options =>
                        {
                            options.Validate();

                            var stopWatch = new Stopwatch();
                            stopWatch.Start();

                            var runner = new WordLadderRunner(
                                    new WordLadder(new WordLadderConfig(options)),
                                    new ResultsWriter(options.ResultFile))
                                .Run()
                                .WriteResults();

                            stopWatch.Stop();

                            Console.WriteLine("Result lists; {0} | Ladder length; {1} | Processing time (ms); {2}",runner.Results.Count,runner.Results.Count>0 ? runner.Results[0].Count : 0, stopWatch.Elapsed.Milliseconds);

                            return 0;
                        },
                        _ => 1);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Invalid arguments detected: {0}", ex);
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error: {0}", ex);
                return 2;
            }
        }
    }
}
