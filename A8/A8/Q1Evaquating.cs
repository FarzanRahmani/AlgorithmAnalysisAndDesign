using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class Q1Evaquating : Processor
    {
        public Q1Evaquating(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long>)Solve);

        public virtual long Solve(long nodeCount, long edgeCount, long[][] edges)
        {
            if (edgeCount < 1)
                return 0;
            // write your code here
            // int[,] residualGraph = new int[vertex_count + 1, vertex_count + 1]; // adjancancy matrix 1-based 
            long[,] residualGraph = new long[nodeCount, nodeCount]; // adjancancy matrix 0-based // [n_city,n_city] adjancy matrix 
            for (long i = 0; i < edgeCount; ++i)
            {
                residualGraph[edges[i][0] - 1, edges[i][1] - 1] += edges[i][2]; // 0-based  u v capacity
                // residualGraph[tokens[0], tokens[1]] = tokens[2]; // 1-based  u v capacity
            }
            return maxFlow(residualGraph, nodeCount);
        }

        private static long maxFlow(long[,] residualGraph, long numCity)
        {
            long flow = 0;
            // int[] path = new int[numCity + 1];
            int[] path = new int[numCity]; // predeceddor
            for (int i = 0; i < numCity; i++)
            {
                path[i] = -1;
            }
            while (FindPath(residualGraph, path, numCity))
            {
                long minFlow = int.MaxValue;
                //  long crawl = numCity; // crawl = target
                long crawl = numCity - 1;
                while (crawl != 0) // 1 -- findMin
                {
                    long u = path[crawl]; // predecedor
                    minFlow = Math.Min(minFlow, residualGraph[u, crawl]);
                    crawl = u;
                }
                // crawl = numCity;
                crawl = numCity - 1;
                while (crawl != 0) // 1  -- update flows and residual graph
                {
                    long u = path[crawl];
                    residualGraph[u, crawl] -= minFlow; // reduce capacity for forward edge
                    residualGraph[crawl, u] += minFlow; // increase capacity for backward edge
                    crawl = u;
                }
                flow += minFlow;
            }
            return flow;
        }

        private static bool FindPath(long[,] Graph, int[] path, long numCity)
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
                // for ( long i = 1; i < numCity + 1; i++)
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
}
