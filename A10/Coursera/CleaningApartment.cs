using System;
using System.Collections.Generic;
using System.Linq;
public struct pair
{
    public pair(long a, long b)
    {
        v1 = a;
        v2 = b;
    }
    public long v1;
    public long v2;

    public override bool Equals(object obj)
    {
        return obj is pair pair &&
               v1 == pair.v1 &&
               v2 == pair.v2;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(v1, v2);
    }
}
public class CleaningApartment
{
    public static void Main(String[] args)
    {
        int n = V; // vertex
        int m = E; // edges
                   // System.Console.WriteLine((n * 4 + m * 3).ToString() + " " + (n * 3).ToString());
                   // "1 -2 0\n"
                   // vertex : 1 , 2 , 3 , i , n
                   // color : 1 2 3 ... n, n+1 n+2 ... n+n, 2n+1 2n+2 ... 2n+n , (i-1)*n+1 (i-2)*n+1 ... (i-1)*n + n , ... 

        // System.Console.Write(2 * (n * (n * (n - 1) / 2 + 1)) + ((n * (n - 1)) - 2 * m) * (n - 1)); // num of clauses
        // System.Console.Write(" ");
        // System.Console.WriteLine(n * n); // num of variables
        // exactlyOne for vertexes 1,2,3,...,n  for(i=vertex in 1 to n){(i-1)*n+1 (i-2)*n+1 ... (i-1)*n + n}
        List<string> ans = new List<string>(5 * n * n * n * (n * n - m));
        ans.Add(""); // num of clauses and variables
        int[] temp = new int[n];
        for (int v = 1; v <= n; v++) // n*(n*(n-1)/2 + 1)
        {
            for (int j = 1; j <= n; j++)
            {
                temp[j - 1] = (v - 1) * n + j;
            }
            ExactlyOneOf(temp, ans);
        }
        // exactlyOne for positions in path for(p in 1 to n) (for position 1=p : 1=p , n+1=p , 2n+1=p , (i-1)*n+1=p  i:[1,n])
        // int[] temp = new int[n];
        for (int p = 1; p <= n; p++) // n*(n*(n-1)/2 + 1)
        {
            for (int i = 0; i < n; i++)
            {
                temp[i] = i * n + p;
            }
            ExactlyOneOf(temp, ans);
        }
        // successive vertices
        HashSet<pair> edges = new HashSet<pair>(m);
        for (int i = 0; i < m; i++)
        {
            // toks = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
            // edges.Add(new pair(toks[0], toks[1]));
            edges.Add(new pair(matrix[i, 0], matrix[i, 1]));
        }
        // for (int v1 = 1; v1 <= n; v1++) // ((n*(n - 1)) - 2*m )*(n-1) ?= 27
        // {
        //     // for (int v2 = 1; v2 <= n; v2++)
        //     for (int v2 = 1; v2 <= n; v2++)
        //     {
        //         if (v1 == v2)
        //             continue;
        //         if (!edges.Contains(new pair(v1, v2)) && !edges.Contains(new pair(v2, v1))) ///////////
        //         {
        //             for (int k = 1; k < n; k++)
        //             {
        //                 string t = "";
        //                 // -xik | - xj(k+1)
        //                 t += (-((v1 - 1) * n + k)).ToString();
        //                 t += " ";
        //                 t += (-((v2 - 1) * n + k + 1)).ToString();
        //                 t += " 0";
        //                 // System.Console.WriteLine(t);
        //                 ans.Add(t);
        //             }
        //         }
        //     }
        // }
        for (int v1 = 1; v1 <= n; v1++) // ((n*(n - 1)) - 2*m )*(n-1) ?= 27
        {
            for (int v2 = v1 + 1; v2 <= n; v2++)
            {
                if (!edges.Contains(new pair(v1, v2))) ///////////
                    if (!edges.Contains(new pair(v2, v1)))
                    {
                        for (int k = 1; k < n; k++)
                        {
                            string t = "";
                            // -xik | - xj(k+1)
                            t += (-((v1 - 1) * n + k)).ToString();
                            t += " ";
                            t += (-((v2 - 1) * n + k + 1)).ToString();
                            t += " 0";
                            // System.Console.WriteLine(t);
                            ans.Add(t);
                            // -xi(k+1) | - xjk
                            t = "";
                            t += (-((v1 - 1) * n + k + 1)).ToString();
                            t += " ";
                            t += (-((v2 - 1) * n + k)).ToString();
                            t += " 0";
                            // System.Console.WriteLine(t);
                            ans.Add(t);
                        }
                    }
            }
        }
        string res = $"{n * n} {ans.Count - 1}";
        ans[0] = res;
        return ans.ToArray();
    }
}