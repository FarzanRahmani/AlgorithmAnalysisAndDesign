using System;
using System.Collections.Generic;
using System.Linq;
public class BWMatching
{
    // Preprocess the Burrows-Wheeler Transform bwt of some text
    // and compute as a result:
    //   * FirstOccurrences - for each character C in bwt, starts[C] is the first position
    //       of this character in the sorted array of
    //       all characters of the text.
    //   * occ_count_before - for each character C in bwt and each position P in bwt,
    //       occ_count_before[C][P] is the number of occurrences of character C in bwt
    //       from position 0 to position P inclusive.
    public static void PreprocessBWT(String bwt, Dictionary<char, int> firstOccurance, Dictionary<char, int[]> occ_counts_before)
    {
        // Implement this function yourself
        // Dictionary<char, int> count = new Dictionary<char, int>(5)
        //     {
        //         {'A', 0} , {'C', 0} , {'G', 0} , {'T', 0} , {'$', 0}
        //     };
        //     foreach (char c in bwt)
        //     {
        //         count[c]++;
        //     }
        //     char[] alphabet = new char[5]{'$', 'A', 'C', 'G', 'T'};
        //     firstOccurance['$'] = 0;
        //     for (int i = 1; i < 4; i++)
        //     {
        //         firstOccurance[alphabet[i]] = firstOccurance[alphabet[i-1]] + count[alphabet[i-1]];
        //     }
        ////////////////
        char[] FirstColumn = bwt.OrderBy(c => c).ToArray();
        firstOccurance['$'] = 0;
        for (int i = 1; i < bwt.Length; i++)
        {
            if (FirstColumn[i] == 'A')
            {
                firstOccurance['A'] = i;
                break;
            }
        }
        for (int i = 1; i < bwt.Length; i++)
        {
            if (FirstColumn[i] == 'C')
            {
                firstOccurance['C'] = i;
                break;
            }
        }
        for (int i = 1; i < bwt.Length; i++)
        {
            if (FirstColumn[i] == 'G')
            {
                firstOccurance['G'] = i;
                break;
            }
        }
        for (int i = 1; i < bwt.Length; i++)
        {
            if (FirstColumn[i] == 'T')
            {
                firstOccurance['T'] = i;
                break;
            }
        }

        occ_counts_before['A'] = new int[bwt.Length + 1];
        occ_counts_before['C'] = new int[bwt.Length + 1];
        occ_counts_before['G'] = new int[bwt.Length + 1];
        occ_counts_before['T'] = new int[bwt.Length + 1];
        occ_counts_before['$'] = new int[bwt.Length + 1];
        for (int i = 1; i <= bwt.Length; i++)
        {
            char currentSymbol = bwt[i - 1];
            // Count['A',i] = Count['A',i-1];
            occ_counts_before['A'][i] = occ_counts_before['A'][i - 1];
            // Count['C',i] = Count['C',i-1];
            occ_counts_before['C'][i] = occ_counts_before['C'][i - 1];
            // Count['G',i] = Count['G',i-1];
            occ_counts_before['G'][i] = occ_counts_before['G'][i - 1];
            // Count['T',i] = Count['T',i-1];
            occ_counts_before['T'][i] = occ_counts_before['T'][i - 1];
            // Count['$',i] = Count['$',i-1];
            occ_counts_before['$'][i] = occ_counts_before['$'][i - 1];
            // Count[currentSymbol,i]++;
            occ_counts_before[currentSymbol][i]++;
        }
    }

    // Compute the number of occurrences of string pattern in the text
    // given only Burrows-Wheeler Transform bwt of the text and additional
    // information we get from the preprocessing stage - starts and occ_counts_before.
    public static int CountOccurrences(String pattern, String bwt, Dictionary<char, int> firstOccurance, Dictionary<char, int[]> occ_counts_before) // betterBWMatching
    {
        int top = 0;
        int bottom = bwt.Length - 1;
        while (top <= bottom)
        {
            if (pattern.Length > 0)
            {
                char symbol = pattern[pattern.Length - 1];
                pattern = pattern.Substring(0, pattern.Length - 1);
                if (firstOccurance.ContainsKey(symbol))
                {
                    top = firstOccurance[symbol] + occ_counts_before[symbol][top];
                    bottom = firstOccurance[symbol] + occ_counts_before[symbol][bottom + 1] - 1;
                }
                else
                    return 0;
            }
            else
            {
                return bottom - top + 1;
            }
        }
        return 0;
    }

    static public void Main(String[] args)
    {
        String bwt = Console.ReadLine(); // LastColumn
                                         // Start of each character in the sorted list of characters of bwt,
                                         // see the description in the comment about function PreprocessBWT
        Dictionary<char, int> FirstOccurrences = new Dictionary<char, int>(5); // starts
                                                                               // Occurrence counts for each character and each position in bwt,
                                                                               // see the description in the comment about function PreprocessBWT
        Dictionary<char, int[]> occ_counts_before = new Dictionary<char, int[]>(5);
        // Preprocess the BWT once to get starts and occ_count_before.
        // For each pattern, we will then use these precomputed values and
        // spend only O(|pattern|) to find all occurrences of the pattern
        // in the text instead of O(|pattern| + |text|).
        PreprocessBWT(bwt, FirstOccurrences, occ_counts_before);
        int patternCount = int.Parse(Console.ReadLine());
        int[] result = new int[patternCount];
        string[] patterns = Console.ReadLine().Split();
        for (int i = 0; i < patternCount; ++i)
        {
            result[i] = CountOccurrences(patterns[i], bwt, FirstOccurrences, occ_counts_before);
        }
        foreach (var a in result)
        {
            Console.Write(a + " ");
        }
        Console.WriteLine();
    }
}