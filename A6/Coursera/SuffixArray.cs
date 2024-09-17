using System;
using System.Linq;

public struct SuffixAndIndex
    {
        public int index;
        public string suffix;

        public SuffixAndIndex(int index, string suffix)
        {
            this.index = index;
            this.suffix = suffix;
        }
    }

    public class SuffixArray
    {
        // Build suffix array of the string text and
        // return an int[] result of the same length as the text
        // such that the value result[i] is the index (0-based)
        // in text where the i-th lexicographically smallest
        // suffix of text starts.
        public static int[] computeSuffixArray(String text)
        { // GAGAGAGA$
          // write your code here
            SuffixAndIndex[] suffixes = new SuffixAndIndex[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                suffixes[i] = new SuffixAndIndex(i, text.Substring(i));
            }
            int[] res = suffixes.OrderBy(si => si.suffix).Select(si => si.index).ToArray();
            return res;
        }


        static public void Main(String[] args)
        {
            String text = Console.ReadLine();
            int[] SuffixArray = computeSuffixArray(text);
            foreach (var a in SuffixArray)
            {
                Console.Write(a + " ");
            }
            Console.WriteLine();
            // Console.ReadKey();
        }
    }