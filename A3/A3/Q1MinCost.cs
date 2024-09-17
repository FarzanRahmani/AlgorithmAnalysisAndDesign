using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// using Priority_Queue;
using TestCommon;

namespace A3
{
    public class Q1MinCost : Processor
    {
        public Q1MinCost(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, long, long>)Solve);


        public long Solve(long nodeCount, long[][] edges, long startNode, long endNode)
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
            return distance(adj, cost, startNode, endNode);
        }

        private static long distance(List<long>[] adj, List<long>[] cost, long s, long t)
        {
            long[] dist = new long[adj.Length]; // long[] prev
            for (long i = 0; i < adj.Length; i++)
            {
                dist[i] = long.MaxValue - 1; // prev[i] = null
            }
            dist[s] = 0;
            bool[] isInVisRegion = new bool[adj.Length];
            isInVisRegion[0] = true;
            long[] priority = new long[adj.Length];
            for (long i = 0; i < adj.Length; i++)
            {
                priority[i] = dist[i];
            }
            // priority[s] = 0;
            priority[0] = long.MaxValue; // long.MaxValue : is in visited region
            while (isInVisRegion.Contains(false))
            {
                long u = MinIndex(priority);
                priority[u] = long.MaxValue;
                isInVisRegion[u] = true;
                int i = 0;
                if (dist[u] != long.MaxValue - 1) // isReachable?
                    foreach (long v in adj[u])
                    {
                        // Relax
                        if (dist[v] > (dist[u] + cost[u][i]))
                        {
                            dist[v] = (dist[u] + cost[u][i]); // prev[v] = u
                            priority[v] = dist[v];
                        }
                        i++;
                    }
            }
            if (dist[t] == long.MaxValue - 1)
                return -1;
            return dist[t];
        }

        private static long MinIndex(long[] nums)
        {
            long res = 0;
            for (long i = 0; i < nums.Length; i++)
            {
                if (nums[res] > nums[i])
                    res = i;
            }
            return res;
        }
    }
}
