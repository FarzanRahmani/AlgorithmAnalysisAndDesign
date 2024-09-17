using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
public class FriendSuggestion
{

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

    private static long distance(List<int>[] adj, List<int>[] cost, List<int>[] adjRev, List<int>[] costRev, int s, int t)
    {
        long[] dist = new long[adj.Length]; // int[] prev
        long[] distRev = new long[adj.Length];
        for (int i = 0; i < adj.Length; i++)
        {
            dist[i] = long.MaxValue - 1; // prev[i] = null
            distRev[i] = long.MaxValue - 1;
        }
        dist[s] = 0;
        distRev[t] = 0;
        HashSet<int> proc = new HashSet<int>();// bool[] isInVisRegion = new bool[adj.Length]; 
        HashSet<int> procRev = new HashSet<int>();// bool[] isInVisRegionRev = new bool[adj.Length];

        long[] priority = new long[adj.Length];
        long[] priorityRev = new long[adj.Length];
        for (int i = 0; i < adj.Length; i++)
        {
            priority[i] = dist[i];
            priorityRev[i] = distRev[i];
        }
        priority[0] = long.MaxValue; // long.MaxValue : is in visited region
        priorityRev[0] = long.MaxValue;
        while (proc.Count < adj.Length) // adj.Length / 2
        {
            int u = MinIndex(priority);
            priority[u] = int.MaxValue;// isInVisRegion[u] = true;
            proc.Add(u); // Process and Relax
            int i = 0;
            if (dist[u] != long.MaxValue - 1) // isReachable?
                foreach (int v in adj[u])
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
                foreach (int v in adjRev[u])
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

    private static long ShortestPath(int s, long[] dist, HashSet<int> proc, int t, long[] distRev, HashSet<int> procRev)
    {
        long distance = long.MaxValue;
        proc.UnionWith(procRev);
        foreach (int u in proc)
        {
            if (dist[u] < long.MaxValue - 1 && distRev[u] < long.MaxValue - 1)
                if (dist[u] + distRev[u] < distance)
                    distance = dist[u] + distRev[u];
        }
        return distance;
    }

    private static int MinIndex(long[] nums) // priority queue for better time
    {
        int res = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[res] > nums[i])
                res = i;
        }
        return res;
    }

    public static void Main(string[] args)
    {
        int[] toks = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
        int n = toks[0]; // |V|
        int m = toks[1]; // |E|
        List<int>[] adj = new List<int>[n + 1]; // Adjacency List
        List<int>[] cost = new List<int>[n + 1];

        List<int>[] adjRev = new List<int>[n + 1]; // Adjacency List
        List<int>[] costRev = new List<int>[n + 1];
        for (int i = 0; i < n + 1; i++)
        {
            adj[i] = new List<int>();
            cost[i] = new List<int>();

            adjRev[i] = new List<int>();
            costRev[i] = new List<int>();
        }
        for (int i = 0; i < m; i++)
        {
            toks = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
            adj[toks[0]].Add(toks[1]); // directed edge
            cost[toks[0]].Add(toks[2]); // cost (weight)

            adjRev[toks[1]].Add(toks[0]); // directed edge
            costRev[toks[1]].Add(toks[2]); // cost (weight)
        }
        int q = int.Parse(Console.ReadLine());
        for (int i = 0; i < q; i++)
        {
            toks = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
            int start = toks[0];
            int target = toks[1];
            if (start == target)
                System.Console.WriteLine(0);
            else if (!Expolre(adj, start, target, new bool[adj.Length])) // if was undirected just 1 time find connected components
                System.Console.WriteLine(-1);
            else
                System.Console.WriteLine(distance(adj, cost, adjRev, costRev, start, target));
            // else
            // {
            //     long res = distance(adj, cost, adjRev, costRev, start, target);
            //     if (res == long.MaxValue - 1)
            //         System.Console.WriteLine(-1);
            //     else
            //         System.Console.WriteLine(res);
            // }
        }
    }


}