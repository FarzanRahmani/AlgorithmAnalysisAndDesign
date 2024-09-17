using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A10
{
    public class Q1FrequencyAssignment : Processor
    {
        public Q1FrequencyAssignment(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, long[,], string[]>)Solve);


        public String[] Solve(int V, int E, long[,] matrix)
        {
            int n = V;
            int m = E;
            List<string> ans = new List<string>(n * 4 + m * 3);
            // System.Console.WriteLine((n * 4 + m * 3).ToString() + " " + (n * 3).ToString());
            ans.Add((m * 3 + n).ToString() + " " + (n * 3).ToString());
            // ans.Add((n * 4 + m * 3).ToString() + " " + (n * 3).ToString());
            // "1 -2 0\n"
            // vertex : 1 , 2 , 3 , i , n
            // color ; 1 2 3 , 4 5 6 , 7 8 9 , i*3-2 i*3-1 i*3 , ... 

            for (int i = 1; i <= n; i++)
            {
                ans.Add($"{i*3-2} {i*3-1} {i*3} 0");
                // ans.Add($"-{i*3-2} -{i*3-1} 0");
                // ans.Add($"-{i*3-2} -{i*3} 0");
                // ans.Add($"-{i*3-1} -{i*3} 0"); 
                // 
                // string tmp = (i * 3 - 2).ToString() + " " + (i * 3 - 1).ToString() + " " + (i * 3).ToString() + " " + "0";
                // // System.Console.WriteLine(tmp);
                // ans.Add(tmp);
                // tmp = (-(i * 3 - 2)).ToString() + " " + (-(i * 3 - 1)).ToString() + " " + "0";
                // // System.Console.WriteLine(tmp);
                // ans.Add(tmp);
                // tmp = (-(i * 3 - 2)).ToString() + " " + (-(i * 3)).ToString() + " " + "0";
                // // System.Console.WriteLine(tmp);
                // ans.Add(tmp);
                // tmp = (-(i * 3 - 1)).ToString() + " " + (-(i * 3)).ToString() + " " + "0";
                // // System.Console.WriteLine(tmp);
                // ans.Add(tmp);
            }

            for (int i = 0; i < m; i++)
            {
                // toks = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
                ans.Add($"-{matrix[i,0]*3-2} -{matrix[i,1]*3-2} 0");
                ans.Add($"-{matrix[i,0]*3-1} -{matrix[i,1]*3-1} 0");
                ans.Add($"-{matrix[i,0]*3} -{matrix[i,1]*3} 0");

                // // toks = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
                // string tmp = (-(matrix[i,0] * 3 - 2)).ToString() + " " + (-(matrix[i,1] * 3 - 2)).ToString() + " " + "0";
                // // System.Console.WriteLine(tmp);
                // ans.Add(tmp);
                // tmp = (-(matrix[i,0] * 3 - 1)).ToString() + " " + (-(matrix[i,1] * 3 - 1)).ToString() + " " + "0";
                // // System.Console.WriteLine(tmp);
                // ans.Add(tmp);
                // tmp = (-(matrix[i,0] * 3)).ToString() + " " + (-(matrix[i,1] * 3)).ToString() + " " + "0";
                // // System.Console.WriteLine(tmp);
                // ans.Add(tmp);
            }
            return ans.ToArray();
        }

        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;


    }
}
