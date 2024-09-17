using System;
using System.Collections.Generic;

class BurrowsWheelerTransform
    {
        static public void Main(string[] args)
        {
            string bwt = Console.ReadLine();
            string text = InverseBWT(bwt);
            System.Console.WriteLine(text);
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
            string res = "";
            foreach (var s in ans)
            {
                res += s;
            }
            return res;
            // string res = "$";
            // int pos = 0;
            // for (int i = bwt.Length - 1; i > -1; i--)
            // {
            //     res += bwt[pos];
            //     pos = lastToFirst[pos];
            // }
            // char[] ans = res.Reverse().ToArray();
            // string result = "";
            // foreach (var s in ans)
            // {
            //     res += s;
            // }
            // return result;

            // // build first occurance
            // Dictionary<char,int> firstOccurance = new Dictionary<char, int>(5);
            // firstOccurance['$'] = 0;
            // for (int i = 1; i < bwt.Length; i++)
            // {
            //     if (FirstColumn[i] == 'A')
            //     {
            //         firstOccurance['A'] = i;
            //         break;
            //     }
            // }
            // for (int i = 1; i < bwt.Length; i++)
            // {
            //     if (FirstColumn[i] == 'C')
            //     {
            //         firstOccurance['C'] = i;
            //         break;
            //     }
            // }
            // for (int i = 1; i < bwt.Length; i++)
            // {
            //     if (FirstColumn[i] == 'G')
            //     {
            //         firstOccurance['G'] = i;
            //         break;
            //     }
            // }
            // for (int i = 1; i < bwt.Length; i++)
            // {
            //     if (FirstColumn[i] == 'T')
            //     {
            //         firstOccurance['T'] = i;
            //         break;
            //     }
            // }

            // // algorithm
            // char[] ans = new char[bwt.Length];
            // ans[bwt.Length-1] = FirstColumn[0]; // $
            // char curSymbol = bwt[0];
            // int curIndx = 0;//last to first
            // for (int i = bwt.Length - 2; i >= 0; i--)
            // {
            //     ans[i] = curSymbol;
            //     curIndx = firstOccurance[curSymbol] + Count[curSymbol,curIndx]; // last to fisrt
            //     curSymbol = bwt[curIndx];
            // }
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

        // private static int[,] BuildCountArray(string bwt)
        // {
        //     int[,] Count = new int[85,bwt.Length];
        //     for (int i = 1; i < bwt.Length; i++)
        //     {
        //         char currentSymbol = bwt[i-1];
        //         Count['A',i] = Count['A',i-1];
        //         Count['C',i] = Count['C',i-1];
        //         Count['G',i] = Count['G',i-1];
        //         Count['T',i] = Count['T',i-1];
        //         Count['$',i] = Count['$',i-1];
        //         Count[currentSymbol,i]++;
        //     }
        //     return Count;
        // }

    }