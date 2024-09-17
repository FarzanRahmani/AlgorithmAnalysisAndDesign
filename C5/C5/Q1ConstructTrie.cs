using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Q1ConstructTrie : Processor
    {
        public Q1ConstructTrie(string testDataName) : base(testDataName)
        {
            this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, String[], String[]>) Solve);

        public string[] Solve(long n, string[] patterns)
        {
            var trie = BuildTrie(patterns);
            List<string> ans = new List<string>(trie.Count * 4);
            for (int i = 0; i < trie.Count(); i++) // 0 --> root
            {
                foreach (var edge in trie[i]) // adjacancy list
                {
                    // Console.WriteLine("{0}->{1}:{2}", i, edge.Value.ToString(), edge.Key);
                    ans.Add( i.ToString() + "->" + edge.Value.ToString() + ":" + edge.Key );
                }
            }
            return ans.ToArray();
        }

        static List<Dictionary<char, int>> BuildTrie(string[] patterns)
        {
            var trie = new List<Dictionary<char, int>>();
            trie.Add(new Dictionary<char, int>()); // root
            foreach (string pattern in patterns)
            {
                Dictionary<char, int> currentNode = trie[0];
                for (int i = 0; i < pattern.Length; i++)
                {
                    char currentSymbol = pattern[i];
                    if (currentNode.ContainsKey(currentSymbol))
                    {
                        currentNode = trie[currentNode[currentSymbol]];
                    }
                    else
                    {
                        trie.Add(new Dictionary<char, int>());
                        currentNode.Add(currentSymbol, trie.Count - 1);
                        currentNode = trie[trie.Count - 1];
                    }
                }
            }

            return trie;
        }
    }
}
