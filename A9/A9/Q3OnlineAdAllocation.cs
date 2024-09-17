using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A9
{
    public class Q3OnlineAdAllocation : Processor
    {

        public Q3OnlineAdAllocation(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, double[,], String>)Solve);

        public string Solve(int c1, int v, double[,] matrix1)
        {
            // Comment the line below and write your code here
            // throw new NotImplementedException();
            // int[] toks = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
            // int n = toks[0]; // number of inequalities
            int n = c1; // number of inequalities
            // int m = toks[1]; // number of variables
            int m = v; // number of variables
            List<List<double>> A = new List<List<double>>(n); // matrix zarayeb
            for (int i = 0; i < n; i++)
            {
                // A.Add(Console.ReadLine().Split().Select(s => double.Parse(s)).ToList());
                A.Add(new List<double>(n + m + 2));
                for (int j = 0; j < m; j++)
                {
                    A[i].Add(matrix1[i, j]);
                }
            }
            // List<double> b = Console.ReadLine().Split().Select(s => double.Parse(s)).ToList(); // Ax >= b
            // List<double> b = Console.ReadLine().Split().Select(s => double.Parse(s)).ToList(); // Ax >= b
            List<double> b = new List<double>(n + m + 2);
            for (int j = 0; j < n; j++)
            {
                b.Add(matrix1[j, m]);
            }
            // List<double> c = Console.ReadLine().Split().Select(s => double.Parse(s)).ToList(); // costs (object function)
            List<double> c = new List<double>(n + m + 2);
            for (int j = 0; j < m; j++)
            {
                c.Add(matrix1[n, j]);
            }

            b.Add(0);
            List<int> arti = new List<int>(n + 1);
            for (int i = 0; i < n + 1; i++)
            {
                if (b[i] < 0)
                    arti.Add(i);
            }
            int s = arti.Count;
            List<int> basis = new List<int>(n + m + 2); // Basic Variables
            List<int> artiVar = new List<int>(n + m + 2); // artificail variables
            int count = 0;
            List<double> tmp;
            for (int i = 0; i < n; i++)
            {
                double[] lst = new double[n + s + 2];
                lst[i] = 1;
                if (b[i] >= 0)
                {
                    A[i].AddRange(lst);
                    basis.Add(m + i);
                }
                else
                {
                    b[i] = -b[i];
                    // List<int> tmp = new List<int>(A[i].Count + lst.Length);
                    tmp = new List<double>(A[i].Count + lst.Length);
                    foreach (var e in A[i])
                    {
                        tmp.Add(-e);
                    }
                    foreach (var e in lst)
                    {
                        tmp.Add(-e);
                    }

                    tmp[n + m + count] = 1; /////////////////////
                                            // tmp.Add(1);

                    basis.Add(n + m + count);
                    artiVar.Add(n + m + count);
                    count++;
                    A[i] = tmp;
                }
            }
            tmp = new List<double>(c.Count + n + s + 2);
            foreach (var e in c)
            {
                tmp.Add(-e);
            }
            for (int i = 0; i < n + s + 2; i++)
            {
                tmp.Add(0);
            }

            tmp[tmp.Count - 2] = 1;

            A.Add(tmp); // check
                        // w
            double a;
            tmp = new List<double>(m + n + s + 2);
            for (int j = 0; j < m + n; j++)
            {
                a = 0;
                foreach (int e in arti)
                {
                    a += A[e][j];
                }
                tmp.Add(-a);
            }
            for (int i = 0; i < s + 2; i++)
            {
                tmp.Add(0);
            }
            // tmp[-1] = 1;/////////////
            tmp[tmp.Count - 1] = 1;

            A.Add(tmp); // check
            a = 0;
            foreach (int e in arti)
            {
                a += b[e];
            }
            b.Add(-a);
            return TwoPhaseSimplex(n, m, s, A, b, basis, artiVar);
            // A.AddRange(tmp);
            // System.Console.Write(column[raw].ToString("0.000000") + " ");
        }

        private static string TwoPhaseSimplex(int n, int m, int s, List<List<double>> A, List<double> b, List<int> basis, List<int> artiVar)
        {
            double w = PhaseOne(n, m, s, A, b, basis);
            if (w < -0.001)
            {
                // System.Console.WriteLine("No solution");
                return "No Solution";
            }
            else
            {
                Transition(n, m, s, A, b, basis, artiVar);
                int ans = PhaseTwo(n, m, s, A, b, basis);
                if (ans == 1)
                {
                    // System.Console.WriteLine("Infinity");
                    return "Infinity";
                }
                else
                {
                    double[] solution = new double[m + n];
                    for (int i = 0; i < n; i++)
                    {
                        solution[basis[i]] = b[i];
                    }

                    // roundation
                    for (int i = 0; i < m; i++)
                    {
                        if (solution[i] >= 0)
                        {
                            double x = solution[i] - (int)solution[i];
                            if (x < 0.25)
                                solution[i] = solution[i] - x;
                            else if (x >= 0.75)
                                solution[i] = solution[i] - x + 1;
                            else
                                solution[i] = solution[i] - x + 0.5;
                        }
                        else
                        {
                            double x = (solution[i] - (int)solution[i]) * -1;
                            if (x < 0.25)
                                solution[i] = solution[i] + x;
                            else if (x >= 0.75)
                                solution[i] = solution[i] + x - 1;
                            else
                                solution[i] = solution[i] + x - 0.5;
                        }
                    }

                    // System.Console.WriteLine("Bounded solution");
                    string res = "Bounded Solution\n";
                    for (int k = 0; k < m; k++)
                    {
                        // System.Console.Write(solution[k].ToString("0.000000000000000") + " "); // 18 ta ham mishe
                        res += solution[k];
                        if (k != m - 1)
                            res += " ";
                    }
                    return res;
                }
            }
        }

        private static int PhaseTwo(int n, int m, int s, List<List<double>> A, List<double> b, List<int> basis)
        {
            int ans = 0;
            A.RemoveAt(A.Count - 1);
            b.RemoveAt(b.Count - 1);
            int enter = ChoosePivotColumn(n, m, -2, A);
            while (enter != -1)
            {
                // choose pivot
                int depart = -1; // depart row
                double minRatio = double.MaxValue;
                for (int i = 0; i < n; i++)
                {
                    if (A[i][enter] > 0)//smallest non negative
                    {
                        // float ratio = (float)b[i]/(float)A[i][enter];
                        double ratio = b[i] / A[i][enter];
                        if (ratio < minRatio)
                        {
                            minRatio = ratio;
                            depart = i; // pivot row
                        }
                    }
                }
                if (depart == -1)
                {
                    ans = 1;// infinity
                    break;
                }
                else
                {
                    basis[depart] = enter;
                    Pivoting(depart, enter, n, m, s, A, b);
                    enter = ChoosePivotColumn(n, m, -2, A);
                }
            }
            return ans;
        }

        private static void Transition(int n, int m, int s, List<List<double>> A, List<double> b, List<int> basis, List<int> artiVar)
        {
            int enter = -1;
            for (int i = 0; i < n; i++)
            {
                if (artiVar.Contains(basis[i]))
                {
                    for (int j = 0; j < n + m; j++)
                    {
                        if (A[i][j] != 0)
                        {
                            enter = j;
                            basis[i] = j;
                            break;
                        }
                    }
                    Pivoting(i, enter, n, m, s, A, b);
                }
            }
        }

        private static double PhaseOne(int n, int m, int s, List<List<double>> A, List<double> b, List<int> basis)
        {
            int enter = ChoosePivotColumn(n, m, s, A); // pivot column
            while (enter != -1)
            {
                // choose pivot 
                int depart = -1; // depart row
                double minRatio = float.MaxValue;
                for (int i = 0; i < n; i++)
                {
                    if (A[i][enter] > 0)//smallest non negative
                    {
                        // float ratio = (float)b[i]/(float)A[i][enter];
                        double ratio = b[i] / A[i][enter];
                        if (ratio < minRatio)
                        {
                            minRatio = ratio;
                            depart = i; // pivot row
                        }
                    }
                }
                basis[depart] = enter;
                Pivoting(depart, enter, n, m, s, A, b);
                enter = ChoosePivotColumn(n, m, s, A);
            }
            // return b[-1];
            return b[b.Count - 1];
        }

        private static void Pivoting(int depart, int enter, int n, int m, int s, List<List<double>> A, List<double> b)
        {
            // Gaussian elimination
            double pivot = A[depart][enter];
            if (pivot != 1)
            {
                for (int k = 0; k < n + m + s + 2; k++)
                {
                    A[depart][k] /= pivot;
                }
                b[depart] /= pivot;
            }
            for (int i = 0; i < A.Count; i++)
            {
                if (i != depart)
                {
                    double a = A[i][enter];
                    for (int j = 0; j < n + m + s + 2; j++)
                    {
                        A[i][j] -= A[depart][j] * a; //////////////////
                    }
                    b[i] -= b[depart] * a;
                }
            }
        }

        private static int ChoosePivotColumn(int n, int m, int s, List<List<double>> A)
        {
            double min = -0.001;
            int enter = -1;
            // choose pivot column
            for (int j = 0; j < n + m + s + 2; j++)
            {
                // if (A[-1][j] < min) // most negative in bottom row ///////////////
                if (A[A.Count - 1][j] < min) // most negative in bottom row
                {
                    // min = A[-1][j];////////
                    min = A[A.Count - 1][j];
                    enter = j; // pivot column
                }
            }
            return enter;
        }
    }
}
