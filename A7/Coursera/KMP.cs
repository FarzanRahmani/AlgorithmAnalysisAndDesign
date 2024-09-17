using System;
using System.Collections.Generic;
public class KnuthMorrisPratt {
    // Find all the occurrences of the pattern in the text and return
    // a list of all positions in the text (starting from 0) where
    // the pattern starts in the text.
    public static List<int> findPattern(String pattern, String text) { // FindAllOccurrences
        List<int> result = new List<int>(text.Length);
        // Implement this function yourself
        string transferedString = pattern + "$" + text;
        int[] s = ComputePrefixFunction(transferedString);
        for (int i = pattern.Length; i < transferedString.Length; i++)
        {
            if (s[i] == pattern.Length)
                result.Add(i - 2*pattern.Length);
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

        static public void Main(String[] args)  {
        String pattern = Console.ReadLine();
        String text = Console.ReadLine();
        List<int> positions = findPattern(pattern, text);
        foreach (var p in positions)
        {
            Console.Write(p + " ");
        }
        System.Console.WriteLine();
        // Console.ReadKey();
    }
}