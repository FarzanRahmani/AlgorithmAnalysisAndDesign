using System;
using System.Collections.Generic;
using System.Linq;

namespace trie
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<string> patterns = new List<string>(n);
            for (int i = 0; i < n; i++)
            {
                string s = Console.ReadLine();
                patterns.Add(s);
            }

            var trie = BuildTrie(patterns);

            for (int i = 0; i < trie.Count(); i++) // 0 --> root
            {
                foreach (var edge in trie[i]) // adjacancy list
                {
                    Console.WriteLine("{0}->{1}:{2}", i, edge.Value.ToString(), edge.Key);
                }
            }
        }

        static List<Dictionary<char, int>> BuildTrie(List<string> patterns)
        {
            var trie = new List<Dictionary<char, int>>();
            //write your code here

            trie.Add(new Dictionary<char, int>()); // root
            foreach (string pattern in patterns)
            {
                Dictionary<char, int> currentNode = trie[0];
                for (int i = 0; i < pattern.Length; i++)
                {
                    char currentSymbol = pattern[i];
                    if (currentNode.ContainsKey(currentSymbol))
                    {
                        currentNode = trie[ currentNode[currentSymbol] ];
                    }
                    else
                    {
                        trie.Add(new Dictionary<char, int>());
                        currentNode.Add(currentSymbol,trie.Count - 1);
                        currentNode = trie[trie.Count - 1];
                    }
                }
            }

            return trie;
        }
    }
}