using System;
using System.Collections.Generic;
using System.Linq;

public class Acyclicity {
    private static int acyclic(List<int>[] adj) {
        //write your code here
        bool[] MainVisited = new bool[adj.Length];
        for (int i = 1; i < adj.Length; i++)
            if(!MainVisited[i])
                if(reach(adj,MainVisited,i) == 1)
                    return 1;
        return 0; 
        // 0 --> isn't acyclic , 1 is acyclic
    }

    private static int reach(List<int>[] adj,bool[] visited , int x) {
        //write your code here
        if (Expolre(adj,x,new HashSet<int>(),visited))
            return 1;
        else
            return 0;
    }

    private static bool Expolre(List<int>[] adj, int x, HashSet<int> pre,bool[] vs) // dfs
    {
        if(pre.Contains(x))
            return true;
        pre.Add(x);
        foreach (int neighbor in adj[x])
        {
            if (!vs[neighbor])
                if (Expolre(adj,neighbor,pre,vs))
                    return true;
        }
        vs[x] = true;
        return false;
    }

    public static void Main(String[] args) {
        var toks = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
        int n = toks[0]; // |V|
        int m = toks[1]; // |E|
        List<int>[] adj = new List<int>[n+1]; // // Adjacency List
        for (int i = 0; i <= n; i++) 
            adj[i] = new List<int>();
        for (int i = 0; i < m; i++) {
            toks = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
            int x, y;
            x = toks[0];
            y = toks[1];
            adj[x].Add(y); // directed edge
        }
        System.Console.WriteLine(acyclic(adj));
        Console.ReadKey();
    }
}