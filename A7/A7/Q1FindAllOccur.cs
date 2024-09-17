using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class Q1FindAllOccur : Processor
    {
        public Q1FindAllOccur(string testDataName) : base(testDataName)
        {
            this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, String, long[]>)Solve, "\n");

        protected virtual long[] Solve(string text, string pattern)
        {
            // write your code here
            if (text.Length < pattern.Length)
                return new long[1] { -1 };
            List<long> positions = findPattern(pattern, text);
            return positions.ToArray();
        }

        public static List<long> findPattern(String pattern, String text)
        { // FindAllOccurrences
            List<long> result = new List<long>(text.Length);
            // Implement this function yourself
            string transferedString = pattern + "$" + text;
            int[] s = ComputePrefixFunction(transferedString);
            for (int i = pattern.Length; i < transferedString.Length; i++)
            {
                if (s[i] == pattern.Length)
                    result.Add(i - 2 * pattern.Length);
            }
            return result;
        }

        private static int[] ComputePrefixFunction(string p)
        {
            int[] res = new int[p.Length];
            res[0] = 0;
            int border = 0;
            for (int i = 1; i < p.Length; i++)
            {
                while (border > 0 && p[i] != p[border])
                {
                    border = res[border - 1];
                }
                if (p[i] == p[border])
                {
                    border++;
                }
                else
                {
                    border = 0;
                }
                res[i] = border;
            }
            return res;
        }
    }
}
