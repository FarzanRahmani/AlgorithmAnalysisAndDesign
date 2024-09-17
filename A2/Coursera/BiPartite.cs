using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
public class Bipartite
    {
        private static int bipartite(List<int>[] adj)
        {
            //write your code here

            // bool[] visited = new bool[adj.Length];
            int[] color = new int[adj.Length]; // visited can be removed and -1 can be unvisited
            // for (int i = 0; i < adj.Length; i++)
            //     color[i] = -1;
            
            for (int i = 1; i < adj.Length; i++) // adj.Length = n + 1
                if (color[i] == 0) // not visited
                {
                    if (!BFS(adj,color,i))
                        return 0;
                }

            return 1;
        }
        private static bool BFS(List<int>[] adj,int[] color,int vertex)
        {
            // Queue<int> Q = new Queue<int>(adj.Length);
            Queue<int> Q = new Queue<int>();
            Q.Enqueue(vertex);
            color[vertex] = 1;
            while (Q.Count > 0)
            {
                int u = Q.Dequeue();
                foreach (int v in adj[u])
                {
                    if (u == v) // self-loop
                        return false;

                    // if (!visited[v]) // not visited
                    if (color[v] == 0) // not visited
                    {
                        // color[v] = color[u] ^ 1;
                        // color[v] = 1 - color[u];
                        color[v] = 3 - color[u];
                        Q.Enqueue(v);
                    }
                    else if (color[v] == color[u]) // visited
                        return false;
                }
            }
            return true;
        }

        public static void Main(String[] args)
        {
            int[] toks = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
            int n = toks[0]; // |V|
            int m = toks[1]; // |E|
            List<int>[] adj = new List<int>[n + 1]; // Adjacency List
            for (int i = 0; i < n + 1; i++)
                adj[i] = new List<int>();
            for (int i = 0; i < m; i++)
            {
                toks = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
                adj[toks[0]].Add(toks[1]); // undirected edge
                adj[toks[1]].Add(toks[0]);
            }
            Console.WriteLine(bipartite(adj));
        }
    }
