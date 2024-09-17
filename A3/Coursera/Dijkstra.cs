using System;
using System.Collections.Generic;
using System.Linq;

public class Dijkstra
{
    private static long distance(List<int>[] adj, List<int>[] cost, int s, int t)
    {
        int[] dist = new int[adj.Length]; // int[] prev
        for (int i = 0; i < adj.Length; i++)
        {
            dist[i] = int.MaxValue - 1; // prev[i] = null
        }
        dist[s] = 0;
        bool[] isInVisRegion = new bool[adj.Length];
        isInVisRegion[0] = true;
        int[] priority = new int[adj.Length];
        for (int i = 0; i < adj.Length; i++)
        {
            priority[i] = dist[i];
        }
        // priority[s] = 0;
        priority[0] = int.MaxValue; // int.MaxValue : is in visited region
        while (isInVisRegion.Contains(false))
        {
            int u = MinIndex(priority);
            priority[u] = int.MaxValue;
            isInVisRegion[u] = true;
            int i = 0;
            if (dist[u] != int.MaxValue - 1) // isReachable?
                foreach (int v in adj[u])
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
        if (dist[t] == int.MaxValue - 1)
            return -1;
        return dist[t];
    }

    private static int MinIndex(int[] nums)
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
        for (int i = 0; i < n + 1; i++)
        {
            adj[i] = new List<int>();
            cost[i] = new List<int>();
        }
        for (int i = 0; i < m; i++)
        {
            toks = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
            adj[toks[0]].Add(toks[1]); // directed edge
            cost[toks[0]].Add(toks[2]); // cost (weight)
        }
        toks = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
        int x = toks[0];
        int y = toks[1];
        System.Console.WriteLine(distance(adj, cost, x, y));
    }
}