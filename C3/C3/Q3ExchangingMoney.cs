using System;
using System.Collections.Generic;
using TestCommon;
namespace C3
{
    public class Q3ExchangingMoney : Processor
    {
        public Q3ExchangingMoney(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, string[]>)Solve);


        public string[] Solve(long nodeCount, long[][] edges, long startNode)
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
            long[] distance = new long[nodeCount + 1];
            bool[] reachable = new bool[nodeCount + 1];
            bool[] shortest = new bool[nodeCount + 1];
            for (long i = 0; i < nodeCount + 1; i++)
            {
                distance[i] = long.MaxValue;
                // reachable[i] = false;
                // shortest[i] = false; shortest[i] = 1;
            }
            shortestPaths(adj, cost, startNode, distance, reachable, shortest);
            string[] ans = new string[nodeCount];
            for (long i = 1; i < nodeCount + 1; i++)
            {
                if (reachable[i] == false)
                {
                    ans[i - 1] = "*";
                }
                else if (shortest[i] == true) // -infinity (reachable frome negative cycle)
                {
                    ans[i - 1] = "-";
                }
                else
                {
                    ans[i - 1] = distance[i].ToString();
                }
            }
            return ans;
        }

        private static void shortestPaths(List<long>[] adj, List<long>[] cost, long s, long[] distance, bool[] reachable, bool[] shortest)
        {
            distance[s] = 0;
            for (long i = 0; i < adj.Length - 2; i++) // |V| - 1 times
            {
                // for all edges
                for (long u = 1; u < adj.Length; u++)
                {
                    int flag = 0;
                    if (distance[u] != long.MaxValue)
                        foreach (long v in adj[u])
                        {
                            Relax(u, v, distance, cost[u][flag]);
                            flag++;
                        }
                }
            }

            Queue<long> a = new Queue<long>();
            // |V|-th time
            for (long u = 1; u < adj.Length; u++)
            {
                int flag = 0;
                if (distance[u] != long.MaxValue) // ***
                    foreach (long v in adj[u])
                    {
                        if (distance[v] > distance[u] + cost[u][flag])
                            a.Enqueue(v);
                        flag++;
                    }
            }
            // bfs
            bool[] visited = new bool[adj.Length];
            while (a.Count > 0)
            {
                long vertex = a.Dequeue();
                visited[vertex] = true;
                shortest[vertex] = true;
                foreach (long neighbor in adj[vertex])
                {
                    if (!visited[neighbor])
                        a.Enqueue(neighbor);
                }
            }

            for (long i = 1; i < adj.Length; i++) // reachability
            {
                if (distance[i] != long.MaxValue)
                    reachable[i] = true;
            }
        }

        private static void Relax(long u, long v, long[] dist, long c)
        {
            if (dist[v] > dist[u] + c)
                dist[v] = dist[u] + c;
            // prev[v] = u 
        }

    }
}
