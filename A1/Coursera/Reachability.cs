using System;
using System.Collections.Generic;
using System.Linq;
// page 18 09_graph_decomposition_3_explore.pdf
public class Reachability {
    private static int reach(List<int>[] adj, int x, int y) {
        //write your code here
        bool[] visited = new bool[adj.Length];
        if (Expolre(adj,x,y,visited))
            return 1;
        else
            return 0;
    }

    private static bool Expolre(List<int>[] adj, int x, int y,bool[] vs) // dfs
    {
        vs[x] = true;
        if(x == y)
            return true;
        foreach (int neighbor in adj[x])
        {
            if (!vs[neighbor])
                if (Expolre(adj,neighbor,y,vs))
                    return true;
        }
        return false;
    }

    public static void Main(string[] args) {
        int[] toks = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
        int n = toks[0]; // |V|
        int m = toks[1]; // |E|
        List<int>[] adj = new List<int>[n+1]; // Adjacency List
        for (int i = 0; i < n+1; i++) {
            adj[i] = new List<int>();
        }
        for (int i = 0; i < m; i++) {
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
        Console.WriteLine(reach(adj, x, y));
        Console.ReadKey();
    }
}