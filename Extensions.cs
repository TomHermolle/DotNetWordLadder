using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetWordLadder
{
    public static class Extensions
    {
        /// <summary>
        /// List extension method to allow a delimited write to the console. Strictly speaking a list shouldn't have
        /// any knowledge of the console, but it's such a low-level System framework object that the utility of this method
        /// outweighs single responsibility purism.
        /// </summary>
        /// <typeparam name="T">Any IList</typeparam>
        /// <param name="list">The instance of the list to write to console</param>
        /// <param name="delimiter">Output delimiter, defaulted to "\n"</param>
        public static void WriteToConsole<T>(this IList<T> list, string delimiter = "\n")
        {
            list.FastForEach(item => Console.Write("{0}{1}", item.ToString(), delimiter));
        }

        /// <summary>
        /// The most performant method of iterating through a list is a for loop with a cached count, but it is verbose code
        /// and ugly to read, so I've abstracted it into this extension.
        /// </summary>
        /// <typeparam name="T">Any IList</typeparam>
        /// <param name="list">The instance of the list to loop through</param>
        /// <param name="listProcessingAction">Method to act upon the list at each iteration</param>
        public static void FastForEach<T>(this IList<T> list, Action<T> listProcessingAction)
        {
            var count = list.Count();
            for (var i = 0; i < count; ++i)
            {
                listProcessingAction(list[i]);
            }
            //Console.WriteLine();
        }
    }
}
