using TestCommon;

namespace E2
{
    public class Q1LatinSquareSAT : Processor
    {
        public Q1LatinSquareSAT(string testDataName) : base(testDataName)
        {
            // this.ExcludeTestCases(1, 2, 3);
            // this.ExcludeTestCaseRangeInclusive(1, 3);
            this.ExcludeTestCaseRangeInclusive(40, 54);
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int?[,], string>)Solve);

        public override Action<string, string> Verifier =>
            TestTools.SatVerifier;


        public virtual string Solve(int dim, int?[,] square)
        {
            // for each cell Xij we have dim variables
            // 0,1,...,n-1 for X00
            // (i*dim + j)*dim + 1 --> start variable of cell Xij
            int dim2 = dim * dim;
            int t = 1 + (dim) * (dim - 1) / 2;
            // List<string> ans = new List<string>(dim2 + 3*(dim2)*((dim + dim2) / 2 ) + dim);
            string sAns = "";
            int clsCNt = 0;
            // ans.Add(""); // num of clauses and variables
            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    if (square[i, j].HasValue)
                    {
                        int start = GetStart(i, j, dim) + (int)square[i, j];
                        // ans.Add($"{start} 0");
                        // sAns += $"{start} 0";
                        sAns += $"{start} 0\n";
                        clsCNt++;
                    }
                    else
                    {
                        int[] tmp = new int[dim];
                        int start = GetStart(i, j, dim);
                        for (int k = 0; k < dim; k++)
                        {
                            // tmp[k] = start + k;
                            tmp[k] = start++;
                        }
                        // ExactlyOneOf(tmp, ans);
                        ExactlyOneOf(tmp, ref sAns);
                        // clsCNt += 1 + (dim)*(dim - 1) / 2;
                        clsCNt += t;
                    }
                }
            }

            // ExactlyOneOf each cell
            // clsCNt += dim2*t;
            // for (int i = 0; i < dim; i++) // row
            // {
            //     for (int j = 0; j < dim; j++) // col
            //     {
            //         int[] tmp = new int[dim];
            //         int start = GetStart(i, j, dim);
            //         for (int k = 0; k < dim; k++)
            //         {
            //             // tmp[k] = start + k;
            //             tmp[k] = start++;
            //         }
            //         // ExactlyOneOf(tmp, ans);
            //         ExactlyOneOf(tmp,ref sAns);
            //         // clsCNt += 1 + (dim)*(dim - 1) / 2;
            //         // clsCNt += t;
            //     }
            // }

            // ExactlyOneOf each row
            clsCNt += dim2 * t;
            for (int i = 0; i < dim; i++) // row
            {
                for (int k = 0; k < dim; k++)
                {
                    int[] tmp = new int[dim];
                    // for (int j = 0; j < dim; j++)
                    // {
                    //     tmp[j] = GetStart(i, j, dim) + k;
                    // }
                    tmp[0] = GetStart(i, 0, dim) + k;
                    for (int j = 1; j < dim; j++)
                    {
                        tmp[j] = tmp[j - 1] + dim;
                    }

                    // ExactlyOneOf(tmp, ans);
                    ExactlyOneOf(tmp, ref sAns);
                    // clsCNt += 1 + (dim)*(dim - 1) / 2;
                    // clsCNt += t;
                }
            }

            // ExactlyOneOf each col
            clsCNt += dim2 * t;
            for (int j = 0; j < dim; j++) // col
            {
                for (int k = 0; k < dim; k++)
                {
                    int[] tmp = new int[dim];
                    // for (int i = 0; i < dim; i++)
                    // {
                    //     tmp[i] = GetStart(i, j, dim) + k;
                    // }
                    tmp[0] = GetStart(0, j, dim) + k;
                    for (int i = 1; i < dim; i++)
                    {
                        tmp[i] = tmp[i - 1] + dim2;
                    }

                    // ExactlyOneOf(tmp,ans);
                    ExactlyOneOf(tmp, ref sAns);
                    // clsCNt += 1 + (dim)*(dim - 1) / 2;
                    // clsCNt += t;
                }
            }

            // string res = $"{ans.Count} {dim2 * dim}\n";
            // foreach (string s in ans)
            // {
            //     res += s + "\n";
            // }
            // return res;

            return $"{clsCNt} {dim2 * dim}\n" + sAns;

            // // for each cell Xij we have dim variables
            // // 0,1,...,n-1 for X00
            // // (i*dim + j)*dim + 1 --> start variable of cell Xij
            // int dim2 = dim * dim;
            // int t = 1 + (dim) * (dim - 1) / 2;
            // List<string> ans = new List<string>(dim2 + 3*(dim2)*((dim + dim2) / 2 ) + dim);
            // // string sAns = "";
            // // int clsCNt = 0;
            // // ans.Add(""); // num of clauses and variables
            // for (int i = 0; i < dim; i++)
            // {
            //     for (int j = 0; j < dim; j++)
            //     {
            //         if (square[i, j].HasValue)
            //         {
            //             int start = GetStart(i, j, dim) + (int)square[i, j];
            //             ans.Add($"{start} 0");
            //             // sAns += $"{start} 0\n";
            //             // clsCNt++;
            //         }
            //         else
            //         {
            //             int[] tmp = new int[dim];
            //             int start = GetStart(i, j, dim);
            //             for (int k = 0; k < dim; k++)
            //             {
            //                 tmp[k] = start + k;
            //                 // tmp[k] = start++;
            //             }
            //             ExactlyOneOf(tmp, ans);
            //             // ExactlyOneOf(tmp, ref sAns);
            //             // // clsCNt += 1 + (dim)*(dim - 1) / 2;
            //             // clsCNt += t;
            //         }
            //     }
            // }

            // // ExactlyOneOf each row
            // // clsCNt += dim2 * t;
            // for (int i = 0; i < dim; i++) // row
            // {
            //     for (int k = 0; k < dim; k++)
            //     {
            //         List<int> tmp = new List<int>(dim);
            //         for (int j = 0; j < dim; j++)
            //         {
            //             if (!square[i, j].HasValue)
            //                 tmp.Add(GetStart(i, j, dim) + k);
            //         }
            //         if (tmp.Count != 0)
            //         {
            //             ExactlyOneOf(tmp, ans);
            //             // ExactlyOneOf(tmp, ref sAns);
            //             // clsCNt += 1 + (tmp.Count) * (tmp.Count - 1) / 2;
            //             // clsCNt += t;
            //         }
            //     }
            // }

            // // ExactlyOneOf each col
            // // clsCNt += dim2 * t;
            // for (int j = 0; j < dim; j++) // col
            // {
            //     for (int k = 0; k < dim; k++)
            //     {
            //         List<int> tmp = new List<int>(dim);
            //         for (int i = 0; i < dim; i++)
            //         {
            //             if (!square[i, j].HasValue)
            //                 tmp.Add(GetStart(i, j, dim) + k);
            //         }
            //         if (tmp.Count != 0)
            //         {
            //             ExactlyOneOf(tmp,ans);
            //             // ExactlyOneOf(tmp, ref sAns);
            //             // clsCNt += 1 + (tmp.Count) * (tmp.Count - 1) / 2;
            //             // // clsCNt += t;
            //         }
            //     }
            // }

            // string res = $"{ans.Count} {dim2 * dim}\n";
            // foreach (string s in ans)
            // {
            //     res += s + "\n";
            // }
            // return res;

            // // return $"{clsCNt} {dim2 * dim}\n" + sAns;
        }

        private int GetStart(int i, int j, int dim)
        {
            return (i * dim + j) * dim + 1;
        }

        static IEnumerable<IEnumerable<T>>
        GetCombinations<T>(IEnumerable<T> list, int length) where T : IComparable
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetCombinations(list, length - 1)
                .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) > 0),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        public static void ExactlyOneOf(IEnumerable<int> literals, List<string> ans)
        {
            // num of clauses = 1 + (literals.Count)(literals.Count - 1) / 2
            string tmp = "";
            foreach (int t in literals)
            {
                tmp += t;
                tmp += " ";
            }
            tmp += "0";
            // System.Console.WriteLine(tmp);
            ans.Add(tmp);
            var c = GetCombinations(literals, 2);
            foreach (var pair in c)
            {
                var pL = pair.ToArray();
                tmp = (-pL[0]).ToString() + " " + (-pL[1]).ToString();
                tmp += " 0";
                // System.Console.WriteLine(tmp);
                ans.Add(tmp);
            }
        }

        public static void ExactlyOneOf(IEnumerable<int> literals, ref string ans)
        {
            // num of clauses = 1 + (literals.Count)(literals.Count - 1) / 2
            string tmp = "";
            foreach (int t in literals)
            {
                tmp += t;
                tmp += " ";
            }
            // tmp += "0";
            // System.Console.WriteLine(tmp);
            // ans.Add(tmp);
            tmp += "0\n";
            ans += tmp;

            var c = GetCombinations(literals, 2);
            foreach (var pair in c)
            {
                var pL = pair.ToArray();
                tmp = (-pL[0]).ToString() + " " + (-pL[1]).ToString();
                // tmp += " 0";
                // System.Console.WriteLine(tmp);
                // ans.Add(tmp);
                tmp += " 0\n";
                ans += tmp;
            }
        }
    }
}
