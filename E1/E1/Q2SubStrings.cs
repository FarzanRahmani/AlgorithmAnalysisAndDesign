using System;
using System.Collections.Generic;
using TestCommon;
using System.Linq;

namespace E1
{
    public class Q2SubStrings : Processor
    {
        public Q2SubStrings(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
        E1Processors.ProcessQ3SubStrings(inStr, Solve);

        public virtual long Solve(long n, String text)
        {
            // int maxSubs = (int)(n * (n+1))/2;
            // HashSet<string> subStrs = new HashSet<string>(maxSubs);
            // for (int len = 1; len <= n; len++)
            // {
            //     for (int i = 0; i <= n - len; i++)
            //     {
            //         subStrs.Add(text.Substring(i,len));
            //     }
            // }
            // return subStrs.Count;

            int[] suffix_array = computeSuffixArray(text);
            int[] lcp_array = computeLCPArray(text, suffix_array);
            long res = 0;
            for (int i = 1; i < suffix_array.Length; i++) // i =0 --> "$"
            {
                int suffixLen = text.Length - suffix_array[i];
                int NotComputed = suffixLen - lcp_array[i - 1];
                res += NotComputed - 1; // $ akharesh nabayad shemorde she
            }
            return res;
        }

        private int[] computeLCPArray(string text, int[] suffix_array)
        {
            int[] lcpArray = new int[text.Length - 1];
            int lcp = 0;
            int[] posInOrder = InvertSuffixxArray(suffix_array);
            int suffix = suffix_array[0];
            for (int i = 0; i < text.Length; i++) // 0 to |S| − 1
            {
                int orderIndex = posInOrder[suffix];
                if (orderIndex == text.Length - 1)
                {
                    lcp = 0;
                    suffix = (suffix + 1) % text.Length;
                    continue;
                }
                int nextSuffix = suffix_array[orderIndex + 1];
                lcp = LCPOfSuffixes(text, suffix, nextSuffix, lcp - 1);
                lcpArray[orderIndex] = lcp;
                suffix = (suffix + 1) % text.Length;
            }
            return lcpArray;
        }

        private int LCPOfSuffixes(string s, int i, int j, int equal)
        {
            int lcp = Math.Max(0, equal);
            while ((i + lcp < s.Length) && (j + lcp < s.Length))
            {
                if (s[i+lcp] == s[j+lcp])
                {
                    lcp++;
                }
                else
                {
                    break;
                }
            }
            return lcp;
        }

        private int[] InvertSuffixxArray(int[] suffix_array)
        {
            int[] pos = new int[suffix_array.Length];
            for (int i = 0; i < pos.Length; i++)
            {
                pos[suffix_array[i]] = i;
            }
            return pos;
        }

        public static int[] computeSuffixArray(String text) // BuildSuffixArray
        {
            // int[] order = new int[text.Length];
            int[] order = SortCharacters(text); // int --> long
            int[] clas = ComputeCharClasses(text, order);
            int l = 1;
            while (l < text.Length)
            {
                order = SortDoubled(text, l, order, clas);
                clas = UpdateClasses(order, clas, l);
                l = 2 * l;
            }
            return order;
        }

        private static int[] UpdateClasses(int[] newOrder, int[] clas, int l) //
        {
            int n = newOrder.Length; // |text|
            int[] newClas = new int[n];
            newClas[newOrder[0]] = 0;
            for (int i = 1; i < n; i++)
            {
                int cur = newOrder[i], prev = newOrder[i - 1]; // Ci Cj
                int mid = (cur + l) % n, midPrev = (prev + l) % n; // Ci+l Cj+l
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

        private static int[] SortDoubled(string text, int l, int[] order, int[] clas) //
        {
            int[] count = new int[text.Length];
            int[] newOrder = new int[text.Length];
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
                int start = (order[i] - l + text.Length) % text.Length;// Ci-l
                int cl = clas[start];
                count[cl]--;
                newOrder[count[cl]] = start;
            }
            return newOrder;
        }

        private static int[] ComputeCharClasses(string text, int[] order)
        {
            int[] clas = new int[text.Length];
            clas[order[0]] = 0;
            for (int i = 1; i < text.Length; i++)
            {
                if (text[order[i]] != text[order[i - 1]])
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

        private static int[] SortCharacters(string text) // countSort
        {
            int[] order = new int[text.Length];
            Dictionary<char, int> count = new Dictionary<char, int>(5)
            {
                {'A', 0} , {'C', 0} , {'G', 0} , {'T', 0} , {'$', 0}
            };//ACGT$
            for (int i = 0; i < text.Length; i++)
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
