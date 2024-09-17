using System;
using System.Collections.Generic;
using System.Linq;
// page 16 09_graph_decomposition_4_connectivity.pdf
public class ConnectedComponents {
    private static int numberOfComponents(List<int>[] adj) {
        int result = 0;
        //write your code here
        bool[] visited = new bool[adj.Length];
        for (int i = 1; i < adj.Length; i++) // adj.Length = n + 1
            if (!visited[i])
            {
                Expolre(adj,i,visited);
                result++;
            }
        return result;
    }

    private static void Expolre(List<int>[] adj, int x,bool[] vs) // dfs
    {
        vs[x] = true;
        foreach (int neighbor in adj[x])
        {
            if (!vs[neighbor])
                Expolre(adj,neighbor,vs);
        }
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
        Console.WriteLine(numberOfComponents(adj));
        Console.ReadKey();
    }
}