using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A2
{
    public class Q1ShortestPath : Processor
    {
        public Q1ShortestPath(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, long, long>)Solve);


        public long Solve(long NodeCount, long[][] edges,
                          long StartNode, long EndNode)
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
            return distance(adj, StartNode, EndNode);
        }

        private static long distance(List<long>[] adj, long s, long t) // adj --> Graph
        {
            //write your code here
            long[] dist = new long[adj.Length];
            for (long i = 0; i < adj.Length; i++)
                dist[i] = -1; // inflongy
            Queue<long> Q = new Queue<long>(adj.Length);
            Q.Enqueue(s);
            dist[s] = 0;
            while (Q.Count > 0)
            {
                long u = Q.Dequeue();
                foreach (long v in adj[u])
                    if (dist[v] == -1)
                    {
                        dist[v] = dist[u] + 1;
                        Q.Enqueue(v);
                    }
            }
            return dist[t];
        }
    }
}
