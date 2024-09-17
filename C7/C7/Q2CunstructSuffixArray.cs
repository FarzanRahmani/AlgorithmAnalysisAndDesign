using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class Q2CunstructSuffixArray : Processor
    {
        public Q2CunstructSuffixArray(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, long[]>)Solve);

        protected virtual long[] Solve(string text)
        {
            // write your code here        
            return computeSuffixArray(text);
        }

        public static long[] computeSuffixArray(String text) // BuildSuffixArray
        {
            // long[] order = new long[text.Length];
            long[] order = SortCharacters(text); // long --> long
            long[] clas = ComputeCharClasses(text, order);
            long l = 1;
            while (l < text.Length)
            {
                order = SortDoubled(text, l, order, clas);
                clas = UpdateClasses(order, clas, l);
                l = 2 * l;
            }
            return order;
        }

        private static long[] UpdateClasses(long[] newOrder, long[] clas, long l) //
        {
            long n = newOrder.Length; // |text|
            long[] newClas = new long[n];
            newClas[newOrder[0]] = 0;
            for (int i = 1; i < n; i++)
            {
                long cur = newOrder[i], prev = newOrder[i - 1]; // Ci Cj
                long mid = (cur + l) % n, midPrev = (prev + l) % n; // Ci+l Cj+l
                if (clas[cur] != clas[prev] || clas[mid] != clas[midPrev])
                {
                    newClas[cur] = newClas[prev] + 1;
                }
                else
                {
                    newClas[cur] = newClas[prev];
                }
            }
            return newClas;
        }

        private static long[] SortDoubled(string text, long l, long[] order, long[] clas) //
        {
            long[] count = new long[text.Length];
            long[] newOrder = new long[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                count[clas[i]]++; // conut(Ci)
            }
            for (int j = 1; j < text.Length; j++)
            {
                count[j] = count[j] + count[j - 1];
            }
            for (int i = text.Length - 1; i > -1; i--)
            {
                long start = (order[i] - l + text.Length) % text.Length;// Ci-l
                long cl = clas[start];
                count[cl]--;
                newOrder[count[cl]] = start;
            }
            return newOrder;
        }

        private static long[] ComputeCharClasses(string text, long[] order)
        {
            long[] clas = new long[text.Length];
            clas[order[0]] = 0;
            for (int i = 1; i < text.Length; i++)
            {
                if (text[(int)order[i]] != text[(int)order[i - 1]])
                {
                    clas[order[i]] = clas[order[i - 1]] + 1;
                }
                else
                {
                    clas[order[i]] = clas[order[i - 1]];
                }
            }
            return clas;
        }

        private static long[] SortCharacters(string text) // countSort
        {
            long[] order = new long[text.Length];
            Dictionary<char, long> count = new Dictionary<char, long>(5)
            {
                {'A', 0} , {'C', 0} , {'G', 0} , {'T', 0} , {'$', 0}
            };//ACGT$
            for (int i = 0; i < text.Length ; i++)
            {
                count[text[i]]++;
            }
            string alphabet = "$ACGT";
            for (int j = 1; j < 5; j++) // partial sum
            {
                count[alphabet[j]] = count[alphabet[j]] + count[alphabet[j - 1]];
            }
            for (int i = text.Length - 1; i > -1; i--)
            {
                char c = text[i];
                count[c]--;
                order[count[c]] = i;
            }
            return order;
        }
    }
}
