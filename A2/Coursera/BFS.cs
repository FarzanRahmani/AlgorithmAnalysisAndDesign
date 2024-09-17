using System;
using System.Collections.Generic;
using System.Linq;

public class BFS
{
    private static int distance(List<int>[] adj, int s, int t) // adj --> Graph
    {
        //write your code here
        int[] dist = new int[adj.Length];
        for (int i = 0; i < adj.Length; i++)
        {
            dist[i] = -1; // infinty
        }
        Queue<int> Q = new Queue<int>(adj.Length);
        Q.Enqueue(s);
        dist[s] = 0;
        while (Q.Count > 0)
        {
            int u = Q.Dequeue();
            foreach (int v in adj[u])
            {
                if (dist[v] == -1)
                {
                    dist[v] = dist[u] + 1;
                    Q.Enqueue(v);
                }
            }
        }
        return dist[t];
    }

    public static void Main(String[] args)
    {
        int[] toks = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
        int n = toks[0]; // |V|
        int m = toks[1]; // |E|
        List<int>[] adj = new List<int>[n + 1]; // Adjacency List
        for (int i = 0; i < n + 1; i++)
        {
            adj[i] = new List<int>();
        }
        for (int i = 0; i < m; i++)
        {
            toks = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
            adj[toks[0]].Add(toks[1]); // undirected edge
            adj[toks[1]].Add(toks[0]);

            // int x, y; // 0-Based
            // x = toks[0];
            // y = toks[1];
            // adj[x - 1].Add(y - 1);
            // adj[y - 1].Add(x - 1);
        }
        toks = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
        int x = toks[0];
        int y = toks[1];
        Console.WriteLine(distance(adj, x, y));
        // Console.ReadKey();
    }
}
