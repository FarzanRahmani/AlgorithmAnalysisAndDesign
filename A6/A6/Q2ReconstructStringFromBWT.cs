using System;
using System.Collections.Generic;
using TestCommon;

namespace A6
{
    public class Q2ReconstructStringFromBWT : Processor
    {
        public Q2ReconstructStringFromBWT(string testDataName) 
        : base(testDataName) { }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, String>)Solve);

        /// <summary>
        /// Reconstruct a string from its Burrows–Wheeler transform
        /// </summary>
        /// <param name="bwt"> A string Transform with a single “$” sign </param>
        /// <returns> The string Text such that BWT(Text) = Transform.
        /// (There exists a unique such string.) </returns>
        public string Solve(string bwt)
        {
            return InverseBWT(bwt);
        }

        private static string InverseBWT(string bwt)
        {
            int[] lastToFirst = BuildLastToFirst(bwt);
            int position = 0;
            char[] ans = new char[bwt.Length];
            ans[bwt.Length-1] = '$'; 
            for (int i = bwt.Length - 2; i >= 0; i--)
            {
                ans[i] = bwt[position];
                position = lastToFirst[position];
            }
            return new String(ans);
            // string res = "";
            // foreach (var s in ans)
            // {
            //     res += s;
            // }
            // return res;
        }

        private static int[] BuildLastToFirst(string bwt)
        {
            Dictionary<char,int> counts = new Dictionary<char, int>(5);
            counts['$'] = 0;
            counts['A'] = 0;
            counts['C'] = 0;
            counts['G'] = 0;
            counts['T'] = 0;
            foreach (char c in bwt)
            {
                counts[c]++;
            }
            int tmp = -1;
            Dictionary<char,int> position = new Dictionary<char, int>(5); // position of last occurance of each symbol
            foreach (char c in "$ACGT")
            {
                tmp += counts[c];
                position[c] = tmp; 
            }
            int[] res = new int[bwt.Length];
            for (int i = bwt.Length-1; i >= 0; i--)
            {
                // First-Last Property
                res[i] = position[bwt[i]]; // last A in lastColumn(bwt) is Last A in FirstColumn
                position[bwt[i]]--;
            }
            return res;
        }
    }
}
