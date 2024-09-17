using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;


namespace E1
{
    public class Q1SecondMST : Processor
    {
        public Q1SecondMST(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);


        public long Solve(long nodeCount, long[][] edges) // long[nodeCount][3]
        {
            // long[] x = new long[pointCount];
            // long[] y = new long[pointCount];
            // for (int i = 0; i < pointCount; i++)
            // {
            //     x[i] = points[i][0];
            //     y[i] = points[i][1];
            // }
            // return minimumDistance(x, y);
            return minimumDistance(nodeCount, edges);

        }

        public struct VandE
        {
            public long v1;
            public long v2;
            public long edge;

            public VandE(long v1, long v2, long edge)
            {
                this.v1 = v1;
                this.v2 = v2;
                this.edge = edge;
            }
        }

        public class Node
        {
            public Node parent;
            public long rank; // union by rank
            public long v;

            public Node(int V)
            { // makeSet
                this.v = V;
                rank = 0;
                parent = this;
            }
            public Node getParent()
            { // find
              // find super parent and compress path
                if (this.parent != this)
                    this.parent = this.parent.getParent();

                return parent;
            }
        }

        public static void merge(Node destination, Node source)
        { // union
            Node realDestination = destination.getParent();
            Node realSource = source.getParent();
            if (realDestination == realSource)
                return;

            if (realDestination.rank > realSource.rank)
            {
                realSource.parent = realDestination;
            }
            else
            {
                realDestination.parent = realSource.parent;

                if (realDestination.rank == realSource.rank)
                    realSource.rank++;
            }
        }

        // private static double minimumDistance(long[] x, long[] y)
        private static long minimumDistance(long nodeCount, long[][] edges)
        {
            // long numOfEdges = (x.Length * (x.Length - 1)) / 2;
            // throw new NotImplementedException();
            VandE[] Graph = new VandE[edges.Length];
            // long flag = 0;
            // for (int i = 0; i < x.Length; i++)
            // {
            //     for (int j = i + 1; j < x.Length; j++) // j = 0
            //     {
            //         Graph[flag] = new VandE(i, j, Math.Sqrt(Math.Pow(x[i] - x[j], 2) + Math.Pow(y[i] - y[j], 2)));
            //         flag++;
            //     }
            // }

            // long flag = 0;
            // for (int i = 0; i < edges.Length; i++)
            // {
            //     for (int j = 0; j < edges[i].Length; j++) // j = 0
            //     {
            //         Graph[flag] = new VandE(i, j, edges[i][j]);
            //         flag++;
            //     }
            // }
            for (int i = 0; i < edges.Length; i++)
            {
                // Graph[i] = new VandE(edges[i][0], edges[i][1], edges[i][2]); // 1-based
                Graph[i] = new VandE(edges[i][0] - 1, edges[i][1] - 1, edges[i][2]); // 0-based
            }

            Node[] vertices = new Node[nodeCount]; // 0-based
            for (int i = 0; i < nodeCount; i++)
            {
                vertices[i] = new Node(i);// vertices[i] = new Node(i+1);
            }
            // Node[] tmpVertices = new Node[nodeCount]; // 0-based
            // for (int i = 0; i < nodeCount; i++)
            // {
            //     tmpVertices[i] = new Node(i);// vertices[i] = new Node(i+1);
            // }

            Graph = Graph.OrderBy(t => t.edge).ToArray(); // Sort
            long num = 0;
            // long res = 0;
            List<long> optimumEdges = new List<long>((int)nodeCount);
            // foreach (var e in Graph)
            // {
            //     // get parent == find
            //     if (vertices[e.v1].getParent() != vertices[e.v2].getParent()) // max |V|-1 time come here (break after |V|-1 times)
            //     {
            //         // res += e.edge;
            //         merge(vertices[e.v1], vertices[e.v2]); // union (dsu)
            //         num++;
            //     }
            //     if (num == nodeCount - 1)
            //         break; // return
            // }
            for (int i = 0; i < Graph.Length; i++)
            {
                // get parent == find
                var e = Graph[i];
                if (vertices[e.v1].getParent() != vertices[e.v2].getParent()) // max |V|-1 time come here (break after |V|-1 times)
                {
                    // res += e.edge;
                    optimumEdges.Add(i);
                    merge(vertices[e.v1], vertices[e.v2]); // union (dsu)
                    num++;
                }
                if (num == nodeCount - 1)
                    break; // return
            }
            long res = long.MaxValue;
            long tmp = 0;
            for (int z = 0; z < optimumEdges.Count; z++)
            {
                tmp = 0;
                num = 0;
                // vertices = tmpVertices;
                for (int i = 0; i < nodeCount; i++)
                {
                    vertices[i] = new Node(i);// vertices[i] = new Node(i+1);
                }
                for (int i = 0; i < Graph.Length; i++)
                {
                    // get parent == find
                    if (i == optimumEdges[z])
                        continue;
                    var e = Graph[i];
                    if (vertices[e.v1].getParent() != vertices[e.v2].getParent()) // max |V|-1 time come here (break after |V|-1 times)
                    {
                        tmp += e.edge;
                        merge(vertices[e.v1], vertices[e.v2]); // union (dsu)
                        num++;
                    }
                    if (num == nodeCount - 1)
                    {
                        res = Math.Min(tmp, res);
                        break; // return
                    }
                }
            }
            if (res == long.MaxValue)
            {
                return -1;
            }
            else
                return res;
        }
    }


}
