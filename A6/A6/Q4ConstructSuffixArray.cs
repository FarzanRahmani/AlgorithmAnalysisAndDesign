using System;
using System.Linq;
using TestCommon;

namespace A6
{
    public class Q4ConstructSuffixArray : Processor
    {
        public Q4ConstructSuffixArray(string testDataName)
        : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, long[]>)Solve);

        /// <summary>
        /// Construct the suffix array of a string
        /// </summary>
        /// <param name="text"> A string Text ending with a “$” symbol </param>
        /// <returns> SuffixArray(Text), that is, the list of starting positions
        /// (0-based) of sorted suffixes separated by spaces </returns>
        public long[] Solve(string text)
        {
            return computeSuffixArray(text);
        }

        public static long[] computeSuffixArray(String text)
        { // GAGAGAGA$
          // write your code here
            SuffixAndIndex[] suffixes = new SuffixAndIndex[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                suffixes[i] = new SuffixAndIndex(i, text.Substring(i));
            }
            long[] res = suffixes.OrderBy(si => si.suffix).Select(si => si.index).ToArray();
            return res;
        }
    }

    public struct SuffixAndIndex
    {
        public long index;
        public string suffix;

        public SuffixAndIndex(int index, string suffix)
        {
            this.index = index;
            this.suffix = suffix;
        }
    }
}
