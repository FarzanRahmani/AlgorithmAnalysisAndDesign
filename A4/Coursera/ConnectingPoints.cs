using System;
using System.Linq;

public struct VandE
{
    public int v1;
    public int v2;
    public double edge;

    public VandE(int v1, int v2, double edge)
    {
        this.v1 = v1;
        this.v2 = v2;
        this.edge = edge;
    }
}


public class ConnectingPoints
{
    public class Node
    {
        public Node parent;
        public int rank; // union by rank
        public int v;

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

    private static double minimumDistance(int[] x, int[] y)
    {
        double result = 0.0;
        // write your code here
        int numOfEdges = x.Length * (x.Length - 1);
        VandE[] Graph = new VandE[numOfEdges];
        int flag = 0;
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
        int num = 0;
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

        return result;
    }

    public static void Main(String[] args)
    {
        int n = int.Parse(Console.ReadLine());
        int[] x = new int[n];
        int[] y = new int[n];
        for (int i = 0; i < n; i++)
        {
            int[] toks = Console.ReadLine().Split().Select(s => int.Parse(s)).ToArray();
            x[i] = toks[0];
            y[i] = toks[1];
        }
        Console.WriteLine(minimumDistance(x, y));
    }
}
