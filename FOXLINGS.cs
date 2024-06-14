using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Foxlings
{
    static int GetRoot(int node, Dictionary<int, int> parents)
    {
        if (parents[node] != node)
            parents[node] = GetRoot(parents[node], parents);
        return parents[node];
    }

    static void Connect(int node1, int node2, Dictionary<int, int> parents, Dictionary<int, int> groupSizes)
    {
        int root1 = GetRoot(node1, parents);
        int root2 = GetRoot(node2, parents);

        if (root1 != root2)
        {
            if (groupSizes[root1] > groupSizes[root2])
            {
                parents[root2] = root1;
                groupSizes[root1] += groupSizes[root2];
            }
            else
            {
                parents[root1] = root2;
                groupSizes[root2] += groupSizes[root1];
            }
        }
    }

    static void Main()
    {
        FastIO io = new FastIO();

        var inputs = io.NextLine().Split();
        int totalNodes = int.Parse(inputs[0]);
        int totalEdges = int.Parse(inputs[1]);

        var parents = new Dictionary<int, int>();
        var groupSizes = new Dictionary<int, int>();
        int uniqueNodes = 0;

        for (int i = 0; i < totalEdges; i++)
        {
            var edge = io.NextLine().Split();
            int node1 = int.Parse(edge[0]);
            int node2 = int.Parse(edge[1]);

            if (!parents.ContainsKey(node1))
            {
                uniqueNodes++;
                parents[node1] = node1;
                groupSizes[node1] = 1;
            }

            if (!parents.ContainsKey(node2))
            {
                uniqueNodes++;
                parents[node2] = node2;
                groupSizes[node2] = 1;
            }

            Connect(node1, node2, parents, groupSizes);
        }

        int distinctGroups = 0;
        var rootsSeen = new HashSet<int>();

        foreach (var node in parents.Keys)
        {
            int root = GetRoot(node, parents);
            if (!rootsSeen.Contains(root))
            {
                distinctGroups++;
                rootsSeen.Add(root);
            }
        }

        io.Write(distinctGroups + (totalNodes - uniqueNodes));
        io.Flush();
    }
}

class FastIO
{
    private BufferedStream input;
    private StreamWriter output;
    private byte[] buffer;
    private int bytesRead, bufferPosition;
    private StringBuilder sb;

    public FastIO()
    {
        input = new BufferedStream(Console.OpenStandardInput());
        output = new StreamWriter(Console.OpenStandardOutput());
        buffer = new byte[1 << 16];
        bytesRead = 0;
        bufferPosition = 0;
        sb = new StringBuilder();
    }

    public string NextLine()
    {
        sb.Clear();
        while (true)
        {
            if (bufferPosition == bytesRead)
            {
                bytesRead = input.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0) break;
                bufferPosition = 0;
            }
            char c = (char)buffer[bufferPosition++];
            if (c == '\n') break;
            sb.Append(c);
        }
        return sb.ToString();
    }

    public void Write(object obj)
    {
        output.Write(obj);
    }

    public void Flush()
    {
        output.Flush();
    }
}
