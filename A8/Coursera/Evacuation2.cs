using System;
using System.Collections.Generic;
using System.Linq;
public class Evacuation
    {

        public static void Main(String[] args)
        {
            var tokens = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
            int vertex_count = tokens[0]; // cities
            int edge_count = tokens[1];
            // int[,] residualGraph = new int[vertex_count + 1, vertex_count + 1]; // adjancancy matrix 1-based 
            int[,] residualGraph = new int[vertex_count, vertex_count]; // adjancancy matrix 0-based // [n_city,n_city] adjancy matrix 
            for (int i = 0; i < edge_count; ++i)
            {
                tokens = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
                residualGraph[tokens[0] - 1, tokens[1] - 1] += tokens[2]; // 0-based  u v capacity
                // residualGraph[tokens[0], tokens[1]] = tokens[2]; // 1-based  u v capacity
            }
            System.Console.WriteLine(maxFlow(residualGraph, vertex_count));
            Console.ReadKey();
        }

        private static int maxFlow(int[,] residualGraph, int numCity)
        {
            int flow = 0;
            // int[] path = new int[numCity + 1];
            int[] path = new int[numCity]; // predeceddor
            for (int i = 0; i < numCity; i++)
            {   
                path[i] = -1;
            }
            while (FindPath(residualGraph, path, numCity))
            {
                int minFlow = int.MaxValue;
                // int crawl = numCity; // crawl = target
                int crawl = numCity - 1;
                while (crawl != 0) // 1 -- findMin
                {
                    int u = path[crawl]; // predecedor
                    minFlow = Math.Min(minFlow, residualGraph[u, crawl]);
                    crawl = u;
                }
                // crawl = numCity;
                crawl = numCity - 1;
                while (crawl != 0) // 1  -- update flows and residual graph
                {
                    int u = path[crawl];
                    residualGraph[u, crawl] -= minFlow; // reduce capacity for forward edge
                    residualGraph[crawl, u] += minFlow; // increase capacity for backward edge
                    crawl = u;
                }
                flow += minFlow;
            }
            return flow;
        }

        private static bool FindPath(int[,] Graph, int[] path, int numCity)
        {
            // bool[] visited = new bool[numCity + 1];
            bool[] visited = new bool[numCity];
            visited[0] = true;
            Queue<int> BFS = new Queue<int>();
            // BFS.Enqueue(1); 1-based
            BFS.Enqueue(0);
            while (BFS.Count > 0)
            {
                int tmp = BFS.Dequeue();
                // if (tmp == numCity) // 1-based
                if (tmp == numCity - 1) // target
                    return true;
                // for (int i = 1; i < numCity + 1; i++)
                for (int i = 0; i < numCity; i++)
                {
                    if (!visited[i] && Graph[tmp, i] > 0)
                    {
                        BFS.Enqueue(i);
                        visited[i] = true;
                        path[i] = tmp; // ancestor - predecedor
                    }
                }
            }
            // return visited[numCity];
            return visited[numCity - 1];
        }

    }