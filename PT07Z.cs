using System;
using System.Collections.Generic;

class Program
{
    static List<int>[] tree;
    static bool[] visited;
    static int maxDistance;
    static int farthestNode;

    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());

        tree = new List<int>[n + 1];
        for (int i = 0; i <= n; i++)
        {
            tree[i] = new List<int>();
        }

        for (int i = 0; i < n - 1; i++)
        {
            var edge = Console.ReadLine().Split();
            int u = int.Parse(edge[0]);
            int v = int.Parse(edge[1]);
            tree[u].Add(v);
            tree[v].Add(u);
        }

        visited = new bool[n + 1];
        maxDistance = -1;
        DFS(1, 0);
        int startNode = farthestNode;

        visited = new bool[n + 1];
        maxDistance = -1;
        DFS(startNode, 0);

        Console.WriteLine(maxDistance);
    }

    static void DFS(int node, int distance)
    {
        visited[node] = true;

        if (distance > maxDistance)
        {
            maxDistance = distance;
            farthestNode = node;
        }

        foreach (var neighbor in tree[node])
        {
            if (!visited[neighbor])
            {
                DFS(neighbor, distance + 1);
            }
        }
    }
}
