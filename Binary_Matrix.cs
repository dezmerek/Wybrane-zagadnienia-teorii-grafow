using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string[] dimensions = Console.ReadLine().Split();
        int m = int.Parse(dimensions[0]);
        int n = int.Parse(dimensions[1]);

        int[] rowSums = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int[] colSums = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

        int totalRowSum = 0;
        foreach (int r in rowSums) totalRowSum += r;

        int totalColSum = 0;
        foreach (int c in colSums) totalColSum += c;

        if (totalRowSum != totalColSum)
        {
            Console.WriteLine("NIE");
            return;
        }

        int totalNodes = m + n + 2;
        int source = 0;
        int sink = totalNodes - 1;
        var capacity = new int[totalNodes, totalNodes];

        for (int i = 0; i < m; i++)
        {
            capacity[source, i + 1] = rowSums[i];
        }

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                capacity[i + 1, m + j + 1] = 1;
            }
        }

        for (int j = 0; j < n; j++)
        {
            capacity[m + j + 1, sink] = colSums[j];
        }

        int maxFlow = MaxFlow(capacity, source, sink);

        if (maxFlow == totalRowSum)
        {
            Console.WriteLine("TAK");
        }
        else
        {
            Console.WriteLine("NIE");
        }
    }

    static int MaxFlow(int[,] capacity, int source, int sink)
    {
        int n = capacity.GetLength(0);
        int[,] residualCapacity = (int[,])capacity.Clone();
        int[] parent = new int[n];
        int maxFlow = 0;

        while (BFS(residualCapacity, source, sink, parent))
        {
            int flow = int.MaxValue;

            for (int v = sink; v != source; v = parent[v])
            {
                int u = parent[v];
                flow = Math.Min(flow, residualCapacity[u, v]);
            }

            for (int v = sink; v != source; v = parent[v])
            {
                int u = parent[v];
                residualCapacity[u, v] -= flow;
                residualCapacity[v, u] += flow;
            }

            maxFlow += flow;
        }

        return maxFlow;
    }

    static bool BFS(int[,] residualCapacity, int source, int sink, int[] parent)
    {
        int n = residualCapacity.GetLength(0);
        bool[] visited = new bool[n];
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(source);
        visited[source] = true;
        parent[source] = -1;

        while (queue.Count > 0)
        {
            int u = queue.Dequeue();

            for (int v = 0; v < n; v++)
            {
                if (!visited[v] && residualCapacity[u, v] > 0)
                {
                    queue.Enqueue(v);
                    parent[v] = u;
                    visited[v] = true;

                    if (v == sink)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}
