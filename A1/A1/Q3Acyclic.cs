using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A1
{
    public class Q3Acyclic : Processor
    {
        public Q3Acyclic(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges)
        {
            int n = (int)nodeCount; // |V|
            int m = edges.Length; // |E|
            List<int>[] adj = new List<int>[n + 1]; // // Adjacency List
            for (int i = 0; i <= n; i++)
                adj[i] = new List<int>();
            for (int i = 0; i < m; i++)
            {
                int x, y;
                x = (int)edges[i][0];
                y = (int)edges[i][1];
                adj[x].Add(y); // directed edge
            }
            return acyclic(adj);
        }

        private static int acyclic(List<int>[] adj)
        {
            //write your code here
            bool[] MainVisited = new bool[adj.Length];
            for (int i = 1; i < adj.Length; i++)
                if (!MainVisited[i])
                    if (reach(adj, MainVisited, i) == 1)
                        return 1;
            return 0;
            // 0 --> isn't acyclic , 1 is acyclic
        }

        private static int reach(List<int>[] adj, bool[] visited, int x)
        {
            //write your code here
            if (Expolre(adj, x, new HashSet<int>(), visited))
                return 1;
            else
                return 0;
        }

        private static bool Expolre(List<int>[] adj, int x, HashSet<int> pre, bool[] vs) // dfs
        {
            if (pre.Contains(x))
                return true;
            pre.Add(x);
            foreach (int neighbor in adj[x])
            {
                if (!vs[neighbor])
                    if (Expolre(adj, neighbor, pre, vs))
                        return true;
            }
            vs[x] = true;
            return false;
        }
    }
}
