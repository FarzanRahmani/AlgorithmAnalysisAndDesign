using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Q3GeneralizedMPM : Processor
    {
        public Q3GeneralizedMPM(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, String[], long[]>)Solve);

        public long[] Solve(string text, long n, string[] patterns)
        {
            List<long> answers = Solve(text, patterns);
            if (answers.Count == 0)
                return new long[]{-1};
            return answers.ToArray();
        }

        class TrieAndNodes
        {
            public HashSet<int> endOfpatsNodes;
            public List<Dictionary<char, int>> trie;

            public TrieAndNodes(HashSet<int> endOfpatsNodes, List<Dictionary<char, int>> trie)
            {
                this.endOfpatsNodes = endOfpatsNodes;
                this.trie = trie;
            }
        }

        static List<long> Solve(string text, string[] patterns)
        {
            List<long> ans = new List<long>();
            //write your code here
            TrieAndNodes res = BuildTrie(patterns);
            List<Dictionary<char, int>> trie = res.trie;
            HashSet<int> endOfpatsNodes = res.endOfpatsNodes;
            TrieMatching(text, trie, ans, endOfpatsNodes);
            return ans;
        }

        private static void TrieMatching(string text, List<Dictionary<char, int>> trie, List<long> ans, HashSet<int> endOfpatsNodes)
        {
            for (int i = 0; i < text.Length; i++)
            {
                string tmp = text.Substring(i);
                if (PrefixTrieMatching(tmp, trie, endOfpatsNodes))
                    ans.Add(i);
            }
        }

        private static bool PrefixTrieMatching(string text, List<Dictionary<char, int>> trie, HashSet<int> endOfpatsNodes)
        {
            char symbol = text[0];
            Dictionary<char, int> v = trie[0]; // v --> currentNode
            int vIndex = 0;
            int flag = 1;
            while (true)
            {
                // if (v.Count == 0) // 𝑣 is a leaf in Trie
                // if (v.Count == 0 || endOfpatsNodes.Contains(vIndex)) // 𝑣 is a leaf in Trie
                if (endOfpatsNodes.Contains(vIndex)) // 𝑣 is a leaf in Trie
                {
                    return true;
                }
                else if (v.ContainsKey(symbol)) // there is an edge (𝑣, 𝑤) in Trie labeled by symbol
                {
                    vIndex = v[symbol];
                    v = trie[v[symbol]];
                    if (flag == text.Length)
                        symbol = ' ';
                    else
                        symbol = text[flag];
                }
                else
                {
                    return false;
                }
                flag++;
            }
        }

        static TrieAndNodes BuildTrie(string[] patterns)
        {
            var trie = new List<Dictionary<char, int>>();
            trie.Add(new Dictionary<char, int>()); // root
            // HashSet<int> endOfPatterns = new HashSet<int>(patterns.Count);
            HashSet<int> endOfPatterns = new HashSet<int>();
            foreach (string pattern in patterns)
            {
                Dictionary<char, int> currentNode = trie[0];
                int currentNodeIndex = 0;
                for (int i = 0; i < pattern.Length; i++)
                {
                    char currentSymbol = pattern[i];
                    if (currentNode.ContainsKey(currentSymbol))
                    {
                        currentNodeIndex = currentNode[currentSymbol];
                        currentNode = trie[currentNode[currentSymbol]];
                    }
                    else
                    {
                        trie.Add(new Dictionary<char, int>());
                        currentNode.Add(currentSymbol, trie.Count - 1);
                        currentNode = trie[trie.Count - 1];
                        currentNodeIndex = trie.Count - 1;
                    }

                    if (i == pattern.Length - 1) // whether the path from the root to this node spells a pattern
                    {
                        endOfPatterns.Add(currentNodeIndex);
                    }
                }
            }
            return new TrieAndNodes(endOfPatterns, trie);
        }
    
    }
}
