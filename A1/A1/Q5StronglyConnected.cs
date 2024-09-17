using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A1
{
    public class Q5StronglyConnected : Processor
    {
        public Q5StronglyConnected(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges)
        {
            int n = (int)nodeCount; // |V|
            int m = edges.Length; // |E|
            List<int>[] adj = new List<int>[n + 1]; // // Adjacency List
            List<int>[] adjReverse = new List<int>[n + 1];
            for (int i = 0; i <= n; i++)
            {
                adj[i] = new List<int>();
                adjReverse[i] = new List<int>();
            }
            for (int i = 0; i < m; i++)
            {
                int x, y;
                x = (int)edges[i][0];
                y = (int)edges[i][1];
                adj[x].Add(y); // directed edge
                adjReverse[y].Add(x);
            }
            return numberOfStronglyConnectedComponents(adj, adjReverse);
        }

        private static int numberOfStronglyConnectedComponents(List<int>[] adj, List<int>[] adjReverse)
        {
            int adjLen = adj.Length;
            Dictionary<int, int> postNums = new Dictionary<int, int>(adjLen);
            bool[] visited = new bool[adjLen];
            int CLK = 0;
            for (int i = 1; i < adjLen; i++)
                if (!visited[i])
                    DFS(i, adjReverse, postNums, visited, ref CLK);

            visited = new bool[adjLen];
            int ans = 0;
            // CLK = |V| (largest post number)
            for (int i = CLK; i > 0; i--)
            {
                int lastPost = postNums[i];
                if (!visited[lastPost])
                {
                    ans++;
                    Explore(lastPost, visited, adj); // as input : SCC number as input which start from 1 and ++ in each block 
                }
            }
            return ans;
        }

        private static void Explore(int i, bool[] visited, List<int>[] adj)
        {
            visited[i] = true;
            foreach (int n in adj[i])
                if (!visited[n])
                    Explore(n, visited, adj);
        }

        private static void DFS(int i, List<int>[] adjReverse, Dictionary<int, int> postNumbers, bool[] visited, ref int clk)
        {
            visited[i] = true;
            foreach (int n in adjReverse[i])
                if (!visited[n])
                    DFS(n, adjReverse, postNumbers, visited, ref clk);
            postNumbers[++clk] = i;
        }
    }

}

