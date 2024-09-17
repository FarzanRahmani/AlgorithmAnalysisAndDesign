using System;
using System.Collections.Generic;
using System.Linq;

public class Toposort {
    private static List<int> toposort(List<int>[] adj) {
        bool[] used = new bool[adj.Length]; // used = visited
        List<int> order = new List<int>(adj.Length);
        for (int i = 1; i < adj.Length; i++)
            if (!used[i])
                    dfs(adj,used,order,i); // adj = Graph
        order.Reverse();
        return order;
    }

    private static void dfs(List<int>[] adj, bool[] used, List<int> order , int s) {
        used[s] = true;
        foreach (int neighbor in adj[s])
        {
            if (!used[neighbor])
                dfs(adj,used,order,neighbor);
        }
        order.Add(s);
    }

    public static void Main(String[] args) {
        var toks = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
        int n = toks[0]; // |V|
        int m = toks[1]; // |E|
        List<int>[] adj = new List<int>[n+1]; // Adjacency List
        for (int i = 0; i <= n; i++) {
            adj[i] = new List<int>();
        }
        for (int i = 0; i < m; i++) {
            toks = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
            adj[toks[0]].Add(toks[1]);
        }
        List<int> order = toposort(adj);
        foreach (int x in order)
        {
            System.Console.Write(x + " ");
        }
    }
}