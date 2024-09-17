using System;
using System.Collections.Generic;
using System.Linq;
public class Evacuation
    {

        public static void Main(String[] args)
        {
            var tokens = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
            int stocks_count = tokens[0]; // cities
            int k = tokens[1]; // 𝑘 time points
            // int[,] residualGraph = new int[vertex_count + 1, vertex_count + 1]; // adjancancy matrix 1-based 
            int[,] residualGraph = new int[stocks_count + stocks_count + 2, stocks_count + stocks_count + 2]; // adjancancy matrix 0-based // [n_city,n_city] adjancy matrix 
            // source = 0 , flights [1..n] , crews [n+1..n+m] , n+m+1 = sink
            for (int i = 1; i < stocks_count + 1; i++) // connect source to left part side of graph(flights)
            {
                residualGraph[0, i] = 1; // source = 0
            }
            for (int i = stocks_count + 1; i < stocks_count + stocks_count + 1; ++i) // connect left part side of graph to sink(crews)
            {
                residualGraph[i, stocks_count + stocks_count + 1] = 1; // stocks_count + stocks_count + 1 = sink
            }
            int[,] data = new int[stocks_count, k];
            for (int i = 0; i < stocks_count; i++) // collecting data
            {
                tokens = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
                for (int j = 0; j < k; j++)
                {
                    data[i, j] = tokens[j];
                }
            }
            for (int i = 0; i < stocks_count; i++) // i = left side
            {
                for (int j = 0; j < stocks_count; j++) // j = right side
                {
                    bool isLower = true;
                    for (int d = 0; d < k; d++)
                    {
                        if (data[i, d] >= data[j, d]) // if all points of ai are less than bi
                        {
                            isLower = false;
                            break;
                        }
                    }
                    if (isLower)
                    {
                        residualGraph[1 + i, 1 + stocks_count + j] = 1;
                    }
                }
            }
            Console.WriteLine(stocks_count - maxFlow(residualGraph, stocks_count + stocks_count + 2));
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