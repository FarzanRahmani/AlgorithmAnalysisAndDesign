using System;
using System.Collections.Generic;
using System.Linq;
public class BudgetAllocation
{
    public static void Main(String[] args)
    {
        var toks = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
        int n = toks[0];
        int m = toks[1];

        int[,] A = new int[n, m];
        for (int i = 0; i < n; i++)
        {
            toks = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
            for (int j = 0; j < m; j++)
            {
                A[i, j] = toks[j];
            }
        }

        int[] b = new int[n];
        toks = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
        for (int i = 0; i < n; i++)
        {
            b[i] = toks[i];
        }

        List<string> res = new List<string>(n * 8 + 1);
        // Variables : X1 X2 X3 ... Xm
        string tmp = "";
        // for (int i = 1; i <= m; i++)
        // {
        //     tmp += i.ToString() + " ";
        // }
        // tmp += "0";
        // res.Add(tmp);
        // tmp = "";
        for (int i = 0; i < n; i++)
        {
            List<int> row = new List<int>(3); //  non-zero coefficients in a row
            for (int j = 0; j < m; j++)
            {
                if (A[i, j] != 0)
                {
                    row.Add(j + 1);
                }
            }
            if (row.Count == 3)
            {
                if (A[i, row[0] - 1] + A[i, row[1] - 1] + A[i, row[2] - 1] > b[i])// Xa Xb Xc
                {
                    tmp = "";
                    tmp += (-row[0]).ToString() + " " + (-row[1]).ToString() + " " + (-row[2]).ToString() + " 0";
                    res.Add(tmp);
                }
                if (A[i, row[1] - 1] + A[i, row[2] - 1] > b[i])// -Xa Xb Xc
                {
                    tmp = "";
                    tmp += (row[0]).ToString() + " " + (-row[1]).ToString() + " " + (-row[2]).ToString() + " 0";
                    res.Add(tmp);
                }
                if (A[i, row[0] - 1] + A[i, row[2] - 1] > b[i])// Xa -Xb Xc
                {
                    tmp = "";
                    tmp += (-row[0]).ToString() + " " + (row[1]).ToString() + " " + (-row[2]).ToString() + " 0";
                    res.Add(tmp);
                }
                if (A[i, row[0] - 1] + A[i, row[1] - 1] > b[i])// Xa Xb -Xc
                {
                    tmp = "";
                    tmp += (-row[0]).ToString() + " " + (-row[1]).ToString() + " " + (row[2]).ToString() + " 0";
                    res.Add(tmp);
                }
                if (A[i, row[2] - 1] > b[i])// -Xa -Xb Xc
                {
                    tmp = "";
                    tmp += (row[0]).ToString() + " " + (row[1]).ToString() + " " + (-row[2]).ToString() + " 0";
                    res.Add(tmp);
                }
                if (A[i, row[1] - 1] > b[i])// -Xa Xb -Xc
                {
                    tmp = "";
                    tmp += (row[0]).ToString() + " " + (-row[1]).ToString() + " " + (row[2]).ToString() + " 0";
                    res.Add(tmp);
                }
                if (A[i, row[0] - 1] > b[i])// Xa -Xb -Xc
                {
                    tmp = "";
                    tmp += (-row[0]).ToString() + " " + (row[1]).ToString() + " " + (row[2]).ToString() + " 0";
                    res.Add(tmp);
                }
                if (0 > b[i])// -Xa -Xb -Xc
                {
                    tmp = "";
                    tmp += (row[0]).ToString() + " " + (row[1]).ToString() + " " + (row[2]).ToString() + " 0";
                    res.Add(tmp);
                }
            }
            else if (row.Count == 2)
            {
                if (A[i, row[0] - 1] + A[i, row[1] - 1] > b[i])// Xa Xb
                {
                    tmp = "";
                    tmp += (-row[0]).ToString() + " " + (-row[1]).ToString() + " 0";
                    res.Add(tmp);
                }
                if (A[i, row[1] - 1] > b[i])// -Xa Xb
                {
                    tmp = "";
                    tmp += (row[0]).ToString() + " " + (-row[1]).ToString() + " 0";
                    res.Add(tmp);
                }
                if (A[i, row[0] - 1] > b[i])// Xa -Xb
                {
                    tmp = "";
                    tmp += (-row[0]).ToString() + " " + (row[1]).ToString() + " 0";
                    res.Add(tmp);
                }
                if (0 > b[i])// -Xa -Xb
                {
                    tmp = "";
                    tmp += (row[0]).ToString() + " " + (row[1]).ToString() + " 0";
                    res.Add(tmp);
                }
            }
            else if (row.Count == 1)
            {
                if (A[i, row[0] - 1] > b[i])// Xa
                {
                    tmp = "";
                    tmp += (-row[0]).ToString() + " 0";
                    res.Add(tmp);
                }
                if (0 > b[i])// -Xa
                {
                    tmp = "";
                    tmp += (row[0]).ToString() + " 0";
                    res.Add(tmp);
                }
            }
            else if (row.Count == 0)
            {
                if (0 > b[i])
                {
                    // System.Console.WriteLine("2 1");
                    // System.Console.WriteLine("1 0");
                    // System.Console.WriteLine("-1 0");
                    // return;
                    res = new List<string>() { "2 1", "1 0", "-1 0" };
                    break;
                }
            }
        }

        if (res.Count == 0)
        {
            System.Console.WriteLine("1 1");
            System.Console.WriteLine("1 -1 0");
        }
        // elif clauses == [[1], [-1]]:
        //     n_variables = 1
        else
        {
            System.Console.WriteLine(res.Count.ToString() + " " + m.ToString());
            foreach (string s in res)
            {
                System.Console.WriteLine(s);
            }
        }
        // Console.ReadKey();
    }


}