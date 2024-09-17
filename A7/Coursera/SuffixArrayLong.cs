using System;
using System.Collections.Generic;
public class Suffix
    {
        // Build suffix array of the string text and
        // return an int[] result of the same length as the text
        // such that the value result[i] is the index (0-based)
        // in text where the i-th lexicographically smallest
        // suffix of text starts.
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

        static public void Main(String[] args)
        {
            String text = Console.ReadLine();
            int[] suffix_array = computeSuffixArray(text);
            foreach (var i in suffix_array)
            {
                Console.Write(i + " ");
            }
            System.Console.WriteLine();
            // Console.ReadKey();
        }

    }
