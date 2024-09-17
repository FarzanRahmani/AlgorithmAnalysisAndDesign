using System;
using System.Collections.Generic;
using System.Linq;

public class NegativeCycle
{
    private static int negativeCycle(List<int>[] adj, List<int>[] cost)
    {
        int[] dist = new int[adj.Length];
        // int[] prev
        // for (int i = 0; i < adj.Length; i++)
        // {
        //     dist[i] = int.MaxValue;
        //     // prev[i] = null
        // }
        // dist[1] = 0; // if 1 connected component we have 

        for (int i = 0; i < adj.Length - 2; i++) // |V| - 1 times
        {
            // for all edges
            for (int u = 1; u < adj.Length; u++)
            {
                int flag = 0;
                // if (dist[u] != int.MaxValue)
                foreach (int v in adj[u])
                {
                    Relax(u, v, dist, cost[u][flag]);
                    flag++;
                }
            }
        }

        // |V|-th time
        for (int u = 1; u < adj.Length; u++)
        {
            int flag = 0;
            foreach (int v in adj[u])
            {
                if (dist[v] > dist[u] + cost[u][flag])
                    return 1;
                flag++;
            }
        }
        return 0;
    }

    private static void Relax(int u, int v, int[] dist, int c)
    {
        if (dist[v] > dist[u] + c)
            dist[v] = dist[u] + c;
        // prev[v] = u 
    }

    public static void Main(String[] args)
    {
        int[] toks = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
        int n = toks[0]; // |V|
        int m = toks[1]; // |E|
        List<int>[] adj = new List<int>[n + 1]; // Adjacency List
        List<int>[] cost = new List<int>[n + 1]; // Weights
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
        System.Console.WriteLine(negativeCycle(adj, cost));
        Console.ReadKey();
    }

}