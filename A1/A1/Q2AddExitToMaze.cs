using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A1
{
    public class Q2AddExitToMaze : Processor
    {
        public Q2AddExitToMaze(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges)
        {
            long n = nodeCount; // |V|
            long m = edges.Length; // |E|
            List<long>[] adj = new List<long>[n + 1]; // Adjacency List
            for (long i = 0; i < n + 1; i++)
            {
                adj[i] = new List<long>();
            }
            for (long i = 0; i < m; i++)
            {
                adj[edges[i][0]].Add(edges[i][1]); // undirected edge
                adj[edges[i][1]].Add(edges[i][0]);
            }
            return numberOfComponents(adj);
        }

        private static long numberOfComponents(List<long>[] adj)
        {
            long result = 0;
            bool[] visited = new bool[adj.Length];
            for (long i = 1; i < adj.Length; i++) // adj.Length = n + 1
                if (!visited[i])
                {
                    Expolre(adj, i, visited);
                    result++;
                }
            return result;
        }

        // private static void Expolre(List<long>[] adj, long x, bool[] vs) // dfs recursive
        // {
        //     vs[x] = true;
        //     foreach (long neighbor in adj[x])
        //     {
        //         if (!vs[neighbor])
        //             Expolre(adj, neighbor, vs);
        //     }
        // }
        private static void Expolre(List<long>[] adj, long x, bool[] vs) // dfs iterative
        {
            vs[x] = true;
            Stack<long> s = new Stack<long>();
            vs[x] = true;
            s.Push(x);
            long tmp = 0;
            while (s.Count > 0)
            {
                tmp = s.Pop();
                foreach (long neighbor in adj[tmp])
                {
                    if (!vs[neighbor])
                    {
                        vs[neighbor] = true;
                        s.Push(neighbor);
                    }
                }
            }
        }
    }
}
