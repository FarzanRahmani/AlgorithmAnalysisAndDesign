using System;
using System.Collections.Generic;
using System.Linq;

public class ShortestPaths
{

    private static void shortestPaths(List<long>[] adj, List<long>[] cost, long s, long[] distance, bool[] reachable, bool[] shortest)
    {
        //write your code here
        // long[] prev
        // for (long i = 0; i < adj.Length; i++)
        // {
        //     // prev[i] = null
        // }
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

    public static void Main(String[] args)
    {
        long[] toks = Console.ReadLine().Split().Select(s => long.Parse(s)).ToArray();
        long n = toks[0]; // |V|
        long m = toks[1]; // |E|
        List<long>[] adj = new List<long>[n + 1]; // Adjacency List
        List<long>[] cost = new List<long>[n + 1]; // Weights
        for (long i = 0; i < n + 1; i++)
        {
            adj[i] = new List<long>();
            cost[i] = new List<long>();
        }
        for (long i = 0; i < m; i++)
        {
            toks = Console.ReadLine().Split().Select(s => long.Parse(s)).ToArray();
            adj[toks[0]].Add(toks[1]); // directed edge
            cost[toks[0]].Add(toks[2]); // cost (weight)
        }
        long start = long.Parse(Console.ReadLine());
        long[] distance = new long[n + 1];
        bool[] reachable = new bool[n + 1];
        bool[] shortest = new bool[n + 1];
        for (long i = 0; i < n + 1; i++)
        {
            distance[i] = long.MaxValue;
            // reachable[i] = false;
            // shortest[i] = false; shortest[i] = 1;
        }
        shortestPaths(adj, cost, start, distance, reachable, shortest);
        for (long i = 1; i < n + 1; i++)
        {
            if (reachable[i] == false)
            {
                System.Console.WriteLine('*');
            }
            else if (shortest[i] == true) // -infinity (reachable frome negative cycle)
            {
                System.Console.WriteLine('-');
            }
            else
            {
                System.Console.WriteLine(distance[i]);
            }
        }
        // Console.ReadKey();
    }

}