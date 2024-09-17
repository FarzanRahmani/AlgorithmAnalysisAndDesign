using TestCommon;

namespace E2;

public class Q2MaxflowVertexCapacity : Processor
{
    public Q2MaxflowVertexCapacity(string testDataName) : base(testDataName)
    {
        // this.ExcludeTestCases(1, 2, 3);
        // this.ExcludeTestCaseRangeInclusive(1, 3);
    }
    public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, long, long[][], long[], long, long, long>)Solve);

    public virtual long Solve(long nodeCount, long edgeCount, long[][] edges, long[] nodeWeight
        , long startNode, long endNode)
    {
        if (edgeCount < 1)
            return 0;
        // write your code here
        long[,] residualGraph = new long[2 * nodeCount + 1, 2 * nodeCount + 1]; // adjancancy matrix 1-based 
        // 0
        // 1..i..N input of node i
        // N+1..i+N..2N output of nod i
        for (long i = 0; i < edgeCount; ++i)
        {
            residualGraph[edges[i][0] + nodeCount, edges[i][1]] += edges[i][2]; // 0-based  u v capacity
        }
        for (int i = 1; i < nodeCount + 1; i++)
        {
            residualGraph[i, i + nodeCount] = nodeWeight[i - 1];
        }
        return maxFlow(residualGraph, nodeCount, startNode, endNode + nodeCount);
    }

    private static long maxFlow(long[,] residualGraph, long numCity, long startNode, long endNode)
    {
        long flow = 0;
        // int[] path = new int[numCity + 1];
        long[] path = new long[2 * numCity + 1]; // predeceddor
        for (int i = 1; i < 2 * numCity + 1; i++)
        {
            path[i] = -1;
        }
        while (FindPath(residualGraph, path, numCity, startNode, endNode))
        {
            long minFlow = int.MaxValue;
            //  long crawl = numCity; // crawl = target
            // long crawl = numCity - 1;
            long crawl = endNode;
            // while (crawl != 0) // 1 -- findMin
            while (crawl != startNode) // 1 -- findMin
            {
                long u = path[crawl]; // predecedor
                minFlow = Math.Min(minFlow, residualGraph[u, crawl]);
                crawl = u;
            }
            // crawl = numCity;
            // crawl = numCity - 1;
            crawl = endNode;
            // while (crawl != 0) // 1  -- update flows and residual graph
            while (crawl != startNode) // 1  -- update flows and residual graph
            {
                long u = path[crawl];
                residualGraph[u, crawl] -= minFlow; // reduce capacity for forward edge
                residualGraph[crawl, u] += minFlow; // increase capacity for backward edge
                crawl = u;
            }
            flow += minFlow;
            // for (int i = 0; i < 2 * numCity + 1; i++)
            // {
            //     path[i] = -1;
            // }
        }
        return flow;
    }

    private static bool FindPath(long[,] Graph, long[] path, long numCity, long startNode, long endNode)
    {
        bool[] visited = new bool[2 * numCity + 1];
        // bool[] visited = new bool[numCity];
        // visited[0] = true;
        visited[startNode] = true;
        Queue<long> BFS = new Queue<long>();
        // BFS.Enqueue(1); 1-based
        // BFS.Enqueue(0);
        BFS.Enqueue(startNode);
        while (BFS.Count > 0)
        {
            long tmp = BFS.Dequeue();
            // if (tmp == numCity) // 1-based
            // if (tmp == numCity - 1) // target
            if (tmp == endNode) // target
                return true;
            for (long i = 1; i < 2 * numCity + 1; i++)
            // for (int i = 0; i < numCity; i++)
            {
                if (!visited[i] && Graph[tmp, i] > 0)
                {
                    BFS.Enqueue(i);
                    visited[i] = true;
                    path[i] = tmp; // ancestor - predecedor
                }
            }
        }
        return visited[endNode];
        // return visited[numCity - 1];
    }
}
