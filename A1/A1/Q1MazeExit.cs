using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A1
{
    public class Q1MazeExit : Processor
    {
        public Q1MazeExit(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, long, long>)Solve);

        public long Solve(long nodeCount, long[][] edges, long StartNode, long EndNode)
        {
            int n = (int)nodeCount; // |V|
            int m = edges.Length; // |E|
            List<int>[] adj = new List<int>[n + 1]; // Adjacency List
            for (int i = 0; i < n + 1; i++)
            {
                adj[i] = new List<int>();
            }
            for (int i = 0; i < m; i++)
            {
                adj[edges[i][0]].Add((int)edges[i][1]); // undirected edge
                adj[edges[i][1]].Add((int)edges[i][0]);
            }
            return reach(adj, (int)StartNode, (int)EndNode);
        }

        private static int reach(List<int>[] adj, int x, int y)
        {
            //write your code here
            bool[] visited = new bool[adj.Length];
            if (Expolre(adj, x, y, visited))
                return 1;
            else
                return 0;
        }

        private static bool Expolre(List<int>[] adj, int x, int y, bool[] vs) // dfs
        {
            vs[x] = true;
            if (x == y)
                return true;
            foreach (int neighbor in adj[x])
            {
                if (!vs[neighbor])
                    if (Expolre(adj, neighbor, y, vs))
                        return true;
            }
            return false;
        }
    }
}
