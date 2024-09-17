using System;
using System.Collections.Generic;
using System.Linq;
public class Evacuation
    {

        public static void Main(String[] args)
        {
            var tokens = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
            int flight_count = tokens[0]; // cities
            int crew_count = tokens[1];
            // int[,] residualGraph = new int[vertex_count + 1, vertex_count + 1]; // adjancancy matrix 1-based 
            int[,] residualGraph = new int[flight_count + crew_count + 2, flight_count + crew_count + 2]; // adjancancy matrix 0-based // [n_city,n_city] adjancy matrix 
            // source = 0 , flights [1..n] , crews [n+1..n+m] , n+m+1 = sink
            for (int i = 1; i < flight_count + 1; i++) // connect source to left part side of graph(flights)
            {
                residualGraph[0, i] = 1;
            }
            for (int i = flight_count + 1; i < flight_count + crew_count + 1; ++i) // connect left part side of graph to sink(crews)
            {
                residualGraph[i, flight_count + crew_count + 1] = 1;
            }
            for (int i = 0; i < flight_count; ++i) // left to right edges
            {
                tokens = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
                for (int j = 0; j < crew_count; j++)
                {
                    residualGraph[i + 1, flight_count + 1 + j] = tokens[j];
                }
            }
            maxFlow(residualGraph, flight_count + crew_count + 2);
            int[] ans = new int[flight_count]; // matching
            for (int i = 0; i < flight_count; i++)
            {
                ans[i] = -1;
            }
            for (int i = 1 + flight_count; i < 1 + flight_count + crew_count; i++)
            {
                for (int j = 1; j < flight_count + 1; j++)
                {
                    if (residualGraph[i,j] == 1)
                    {
                        ans[j-1] = i - flight_count; // backwards edge which has capacity 1 has flow in forward side
                    }
                }
            }
            foreach (int a in ans)
            {
                Console.Write(a +" ");
            }
            System.Console.WriteLine();
            // Console.ReadKey();
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
                while (crawl != 0) // 
                {
                    int u = path[crawl]; // predecedor
                    minFlow = Math.Min(minFlow, residualGraph[u, crawl]);
                    crawl = u;
                }
                // crawl = numCity;
                crawl = numCity - 1;
                while (crawl != 0) // 1
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