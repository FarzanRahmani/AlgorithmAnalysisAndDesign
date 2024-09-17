using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A2
{
    public class Q2BipartiteGraph : Processor
    {
        public Q2BipartiteGraph(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);


        public long Solve(long NodeCount, long[][] edges)
        {
            long n = NodeCount; // |V|
            long m = edges.Length; // |E|
            List<long>[] adj = new List<long>[n + 1]; // Adjacency List
            for (long i = 0; i < n + 1; i++)
                adj[i] = new List<long>();
            for (long i = 0; i < m; i++)
            {
                adj[edges[i][0]].Add(edges[i][1]); // undirected edge
                adj[edges[i][1]].Add(edges[i][0]);
            }
            return bipartite(adj);
        }

        private static long bipartite(List<long>[] adj)
        {
            long[] color = new long[adj.Length]; // visited can be removed and -1 can be unvisited
            for (long i = 1; i < adj.Length; i++) // adj.Length = n + 1
                if (color[i] == 0) // not visited
                {
                    if (!BFS(adj, color, i))
                        return 0;
                }
            return 1;
        }
        private static bool BFS(List<long>[] adj, long[] color, long vertex)
        {
            // Queue<long> Q = new Queue<long>(adj.Length);
            Queue<long> Q = new Queue<long>();
            Q.Enqueue(vertex);
            color[vertex] = 1;
            while (Q.Count > 0)
            {
                long u = Q.Dequeue();
                foreach (long v in adj[u])
                {
                    // if (u == v) // self-loop
                    //     return false;
                    if (color[v] == 0) // not visited
                    {
                        color[v] = 3 - color[u];
                        Q.Enqueue(v);
                    }
                    else if (color[v] == color[u]) // visited
                        return false;
                }
            }
            return true;
        }

    }
}
