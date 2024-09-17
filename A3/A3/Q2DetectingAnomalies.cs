using System;
using System.Collections.Generic;
using TestCommon;
namespace A3
{
    public class Q2DetectingAnomalies : Processor
    {
        public Q2DetectingAnomalies(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);


        public long Solve(long nodeCount, long[][] edges)
        {
            // long n = nodeCount; // |V|
            long m = edges.Length; // |E|
            List<long>[] adj = new List<long>[nodeCount + 1]; // Adjacency List
            List<long>[] cost = new List<long>[nodeCount + 1];
            for (long i = 0; i < nodeCount + 1; i++)
            {
                adj[i] = new List<long>();
                cost[i] = new List<long>();
            }
            for (long i = 0; i < m; i++)
            {
                adj[edges[i][0]].Add(edges[i][1]); // directed edge
                cost[edges[i][0]].Add(edges[i][2]); // cost (weight)
            }
            return negativeCycle(adj, cost);
        }

        private static long negativeCycle(List<long>[] adj, List<long>[] cost)
        {
            long[] dist = new long[adj.Length];

            for (long i = 0; i < adj.Length - 2; i++) // |V| - 1 times
            {
                // for all edges
                for (long u = 1; u < adj.Length; u++)
                {
                    int flag = 0;
                    // if (dist[u] != long.MaxValue)
                    foreach (long v in adj[u])
                    {
                        Relax(u, v, dist, cost[u][flag]);
                        flag++;
                    }
                }
            }

            // |V|-th time
            for (long u = 1; u < adj.Length; u++)
            {
                int flag = 0;
                foreach (long v in adj[u])
                {
                    if (dist[v] > dist[u] + cost[u][flag])
                        return 1;
                    flag++;
                }
            }
            return 0;
        }

        private static void Relax(long u, long v, long[] dist, long c)
        {
            if (dist[v] > dist[u] + c)
                dist[v] = dist[u] + c;
            // prev[v] = u 
        }

    }
}
