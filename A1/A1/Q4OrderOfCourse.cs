using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TestCommon;

namespace A1
{
    public class Q4OrderOfCourse : Processor
    {
        public Q4OrderOfCourse(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long[]>)Solve);

        public long[] Solve(long nodeCount, long[][] edges)
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
            List<long> order = toposort(adj);
            return order.ToArray();
        }

        private static List<long> toposort(List<int>[] adj)
        {
            bool[] used = new bool[adj.Length]; // used = visited
            List<long> order = new List<long>(adj.Length);
            for (int i = 1; i < adj.Length; i++)
                if (!used[i])
                    dfs(adj, used, order, i); // adj = Graph
            order.Reverse();
            return order;
        }

        private static void dfs(List<int>[] adj, bool[] used, List<long> order, int s)
        {
            used[s] = true;
            foreach (int neighbor in adj[s])
            {
                if (!used[neighbor])
                    dfs(adj, used, order, neighbor);
            }
            order.Add(s);
        }

        public override Action<string, string> Verifier { get; set; } = TopSortVerifier;

        public static void TopSortVerifier(string inFileName, string strResult)
        {
            long[] topOrder = strResult.Split(TestTools.IgnoreChars)
                .Select(x => long.Parse(x)).ToArray();

            long count;
            long[][] edges;
            TestTools.ParseGraph(File.ReadAllText(inFileName), out count, out edges);

            // Build an array for looking up the position of each node in topological order
            // for example if topological order is 2 3 4 1, topOrderPositions[2] = 0, 
            // because 2 is first in topological order.
            long[] topOrderPositions = new long[count];
            for (int i = 0; i < topOrder.Length; i++)
                topOrderPositions[topOrder[i] - 1] = i;
            // Top Order nodes is 1 based (not zero based).

            // Make sure all direct depedencies (edges) of the graph are met:
            //   For all directed edges u -> v, u appears before v in the list
            foreach (var edge in edges)
                if (topOrderPositions[edge[0] - 1] >= topOrderPositions[edge[1] - 1])
                    throw new InvalidDataException(
                        $"{Path.GetFileName(inFileName)}: " +
                        $"Edge dependency violoation: {edge[0]}->{edge[1]}");

        }
    }
}
