using System;
using System.Collections.Generic;
using TestCommon;

namespace A3
{
    public class Q4FriendSuggestion : Processor
    {
        public Q4FriendSuggestion(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][],
            long, long[][], long[]>)Solve);

        public long[] Solve(long nodeCount, long edgeCount,
                              long[][] edges, long queriesCount,
                              long[][] queries)
        {
            // long n = nodeCount; // |V|
            long m = edges.Length; // |E|
            List<long>[] adj = new List<long>[nodeCount + 1]; // Adjacency List
            List<long>[] cost = new List<long>[nodeCount + 1];
            List<long>[] adjRev = new List<long>[nodeCount + 1]; // Adjacency List
            List<long>[] costRev = new List<long>[nodeCount + 1];
            for (long i = 0; i < nodeCount + 1; i++)
            {
                adj[i] = new List<long>();
                cost[i] = new List<long>();
                adjRev[i] = new List<long>();
                costRev[i] = new List<long>();
            }
            for (long i = 0; i < m; i++)
            {
                adj[edges[i][0]].Add(edges[i][1]); // directed edge
                cost[edges[i][0]].Add(edges[i][2]); // cost (weight)

                adjRev[edges[i][1]].Add(edges[i][0]); // directed edge
                costRev[edges[i][1]].Add(edges[i][2]); // cost (weight)
            }
            long[] ans = new long[queriesCount];
            for (long i = 0; i < queriesCount; i++)
            {
                long start = queries[i][0];
                long target = queries[i][1];
                if (start == target)
                    ans[i] = 0;
                // else if (reach(adj, start, target) == 0)
                else if (!Expolre(adj, start, target, new bool[adj.Length]))
                    ans[i] = -1;
                else
                    ans[i] = distance(adj, cost, adjRev, costRev, start, target);
            }
            return ans;
        }

        private static bool Expolre(List<long>[] adj, long x, long y, bool[] vs) // dfs
        {
            vs[x] = true;
            if (x == y)
                return true;
            foreach (long neighbor in adj[x])
            {
                if (!vs[neighbor])
                    if (Expolre(adj, neighbor, y, vs))
                        return true;
            }
            return false;
        }

        private static long distance(List<long>[] adj, List<long>[] cost, List<long>[] adjRev, List<long>[] costRev, long s, long t)
        {
            long[] dist = new long[adj.Length]; // long[] prev
            long[] distRev = new long[adj.Length];
            for (long i = 0; i < adj.Length; i++)
            {
                dist[i] = long.MaxValue - 1; // prev[i] = null
                distRev[i] = long.MaxValue - 1;
            }
            dist[s] = 0;
            distRev[t] = 0;

            HashSet<long> proc = new HashSet<long>();
            HashSet<long> procRev = new HashSet<long>();

            long[] priority = new long[adj.Length];
            long[] priorityRev = new long[adj.Length];
            for (long i = 0; i < adj.Length; i++)
            {
                priority[i] = dist[i];
                priorityRev[i] = distRev[i];
            }
            priority[0] = long.MaxValue; // long.MaxValue : is in visited region
            priorityRev[0] = long.MaxValue;
            while (proc.Count < adj.Length) // adj.Length / 2
            {
                long u = MinIndex(priority);
                priority[u] = long.MaxValue;
                proc.Add(u); // Process and Relax
                int i = 0;
                if (dist[u] != long.MaxValue - 1) // isReachable?
                    foreach (long v in adj[u])
                    {
                        if (dist[v] > (dist[u] + cost[u][i])) // Relax
                        {
                            dist[v] = (dist[u] + cost[u][i]); // prev[v] = u
                            priority[v] = dist[v];
                        }
                        i++;
                    }
                if (procRev.Contains(u))
                    return ShortestPath(s, dist, proc, t, distRev, procRev);

                u = MinIndex(priorityRev);
                priorityRev[u] = long.MaxValue;
                procRev.Add(u);
                i = 0;
                if (distRev[u] != long.MaxValue - 1) // isReachable?
                    foreach (long v in adjRev[u])
                    {
                        if (distRev[v] > (distRev[u] + costRev[u][i]))// Relax
                        {
                            distRev[v] = (distRev[u] + costRev[u][i]); // prev[v] = u
                            priorityRev[v] = distRev[v];
                        }
                        i++;
                    }
                if (proc.Contains(u))
                {
                    return ShortestPath(s, dist, proc, t, distRev, procRev);
                }
            }

            if (dist[t] == long.MaxValue - 1)
                return -1;
            return dist[t];
        }

        private static long ShortestPath(long s, long[] dist, HashSet<long> proc, long t, long[] distRev, HashSet<long> procRev)
        {
            long distance = long.MaxValue;
            proc.UnionWith(procRev);
            foreach (long u in proc)
            {
                if (dist[u] < long.MaxValue - 1 && distRev[u] < long.MaxValue - 1)
                    if (dist[u] + distRev[u] < distance)
                        distance = dist[u] + distRev[u];
            }
            return distance;
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
