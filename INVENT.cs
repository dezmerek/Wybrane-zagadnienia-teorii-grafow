using System;
using System.Collections.Generic;

class Edge : IComparable<Edge>
{
    public int u, v, weight;

    public Edge(int U, int V, int Weight)
    {
        u = U;
        v = V;
        weight = Weight;
    }

    public int CompareTo(Edge other)
    {
        return weight.CompareTo(other.weight);
    }
}

class Program
{
    static int[] parent;
    static int[] size;

    static int Find(int p)
    {
        while (p != parent[p])
        {
            parent[p] = parent[parent[p]];
            p = parent[p];
        }
        return p;
    }

    static void Union(int p, int q)
    {
        int rootP = Find(p);
        int rootQ = Find(q);
        if (rootP == rootQ) return;

        if (size[rootP] > size[rootQ])
        {
            parent[rootQ] = rootP;
            size[rootP] += size[rootQ];
        }
        else
        {
            parent[rootP] = rootQ;
            size[rootQ] += size[rootP];
        }
    }

    static void Main(string[] args)
    {
        int T = int.Parse(Console.ReadLine());
        for (int test = 1; test <= T; test++)
        {
            Console.ReadLine(); // Read and ignore the empty line

            int N = int.Parse(Console.ReadLine());

            parent = new int[N];
            size = new int[N];
            for (int i = 0; i < N; i++)
            {
                parent[i] = i;
                size[i] = 1;
            }

            List<Edge> edges = new List<Edge>();
            for (int i = 0; i < N - 1; i++)
            {
                string[] parts = Console.ReadLine().Split();
                int u = int.Parse(parts[0]) - 1;
                int v = int.Parse(parts[1]) - 1;
                int weight = int.Parse(parts[2]);
                edges.Add(new Edge(u, v, weight));
            }

            edges.Sort();

            long totalCost = 0;
            foreach (Edge edge in edges)
            {
                int u = edge.u;
                int v = edge.v;
                int rootU = Find(u);
                int rootV = Find(v);

                if (rootU != rootV)
                {
                    totalCost += ((long)size[rootU] * size[rootV] - 1) * (edge.weight + 1) + edge.weight;
                    Union(rootU, rootV);
                }
            }

            Console.WriteLine(totalCost);
        }
    }
}
