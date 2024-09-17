using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace C4
{
    public class Q1BuildingRoads : Processor
    {
        public Q1BuildingRoads(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], double>)Solve);

        public double Solve(long pointCount, long[][] points)
        {
            long[] x = new long[pointCount];
            long[] y = new long[pointCount];
            for (int i = 0; i < pointCount; i++)
            {
                x[i] = points[i][0];
                y[i] = points[i][1];
            }
            return minimumDistance(x, y);
        }

        public struct VandE
        {
            public long v1;
            public long v2;
            public double edge;

            public VandE(int v1, long v2, double edge)
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

        private static double minimumDistance(long[] x, long[] y)
        {
            double result = 0.0;
            // write your code here
            // long numOfEdges = x.Length * (x.Length - 1);
            long numOfEdges = (x.Length * (x.Length - 1))/2;
            VandE[] Graph = new VandE[numOfEdges];
            long flag = 0;
            for (int i = 0; i < x.Length; i++)
            {
                for (int j = i + 1; j < x.Length; j++) // j = 0
                {
                    Graph[flag] = new VandE(i, j, Math.Sqrt(Math.Pow(x[i] - x[j], 2) + Math.Pow(y[i] - y[j], 2)));
                    flag++;
                }
            }

            Node[] vertices = new Node[x.Length]; // 0-based
            for (int i = 0; i < x.Length; i++)
            {
                vertices[i] = new Node(i);
            }
            Graph = Graph.OrderBy(t => t.edge).ToArray(); // Sort
            long num = 0;
            foreach (var e in Graph)
            {
                // get parent == find
                if (vertices[e.v1].getParent() != vertices[e.v2].getParent()) // max |V|-1 time come here (break after |V|-1 times)
                {
                    result += e.edge;
                    merge(vertices[e.v1], vertices[e.v2]); // union (dsu)
                    num++;
                }
                if (num == x.Length - 1)
                    break; // return
            }
            
            return double.Parse(result.ToString("f6"));
        }


    }
}