using System;
using System.Linq;
public class GSMNetwork
    {

        public static void Main(String[] args)
        {
            var toks = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
            int n = toks[0];
            int m = toks[1];
            System.Console.WriteLine((n * 4 + m * 3).ToString() + " " + (n * 3).ToString());
            // "1 -2 0\n"
            // vertex : 1 , 2 , 3 , i , n
            // color ; 1 2 3 , 4 5 6 , 7 8 9 , i*3-2 i*3-1 i*3 , ... 
            for (int i = 1; i <= n; i++)
            {
                // System.Console.WriteLine($"{i*3-2} {i*3-1} {i*3} 0");
                // System.Console.WriteLine($"-{i*3-2} -{i*3-1} 0");
                // System.Console.WriteLine($"-{i*3-2} -{i*3} 0");
                // System.Console.WriteLine($"-{i*3-1} -{i*3} 0"); 
                string tmp = (i * 3 - 2).ToString() + " " + (i * 3 - 1).ToString() + " " + (i * 3).ToString() + " " + "0";
                System.Console.WriteLine(tmp);
                tmp = (-(i * 3 - 2)).ToString() + " " + (-(i * 3 - 1)).ToString() + " " + "0";
                System.Console.WriteLine(tmp);
                tmp = (-(i * 3 - 2)).ToString() + " " + (-(i * 3)).ToString() + " " + "0";
                System.Console.WriteLine(tmp);
                tmp = (-(i * 3 - 1)).ToString() + " " + (-(i * 3)).ToString() + " " + "0";
                System.Console.WriteLine(tmp);
            }
            for (int i = 0; i < m; i++)
            {
                // toks = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
                // System.Console.WriteLine($"-{toks[0]*3-2} -{toks[1]*3-2} 0");
                // System.Console.WriteLine($"-{toks[0]*3-1} -{toks[1]*3-1} 0");
                // System.Console.WriteLine($"-{toks[0]*3} -{toks[1]*3} 0");
                toks = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
                string tmp = (-(toks[0] * 3 - 2)).ToString() + " " + (-(toks[1] * 3 - 2)).ToString() + " " + "0";
                System.Console.WriteLine(tmp);
                tmp = (-(toks[0] * 3 - 1)).ToString() + " " + (-(toks[1] * 3 - 1)).ToString() + " " + "0";
                System.Console.WriteLine(tmp);
                tmp = (-(toks[0] * 3)).ToString() + " " + (-(toks[1] * 3)).ToString() + " " + "0";
                System.Console.WriteLine(tmp);
            }
            Console.ReadKey();
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

    }