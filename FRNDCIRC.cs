using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

public interface IUnionFind
{
    string Find(string item);
    int Union(string item1, string item2);
}

public class DisjointSet : IUnionFind
{
    private Dictionary<string, string> leader;
    private Dictionary<string, int> rank;
    private Dictionary<string, int> groupSize;

    public DisjointSet()
    {
        leader = new Dictionary<string, string>();
        rank = new Dictionary<string, int>();
        groupSize = new Dictionary<string, int>();
    }

    public string Find(string item)
    {
        if (leader[item] != item)
        {
            leader[item] = Find(leader[item]);
        }
        return leader[item];
    }

    public int Union(string item1, string item2)
    {
        if (!leader.ContainsKey(item1))
        {
            leader[item1] = item1;
            rank[item1] = 0;
            groupSize[item1] = 1;
        }
        if (!leader.ContainsKey(item2))
        {
            leader[item2] = item2;
            rank[item2] = 0;
            groupSize[item2] = 1;
        }

        string root1 = Find(item1);
        string root2 = Find(item2);

        if (root1 != root2)
        {
            if (rank[root1] > rank[root2])
            {
                leader[root2] = root1;
                groupSize[root1] += groupSize[root2];
                return groupSize[root1];
            }
            else if (rank[root1] < rank[root2])
            {
                leader[root1] = root2;
                groupSize[root2] += groupSize[root1];
                return groupSize[root2];
            }
            else
            {
                leader[root2] = root1;
                groupSize[root1] += groupSize[root2];
                rank[root1]++;
                return groupSize[root1];
            }
        }
        return groupSize[root1];
    }
}

public class InputHandler
{
    public static List<Tuple<int, List<Tuple<string, string>>>> ParseInput(StreamReader reader)
    {
        int testCases = int.Parse(reader.ReadLine());
        var inputData = new List<Tuple<int, List<Tuple<string, string>>>>();

        for (int t = 0; t < testCases; t++)
        {
            int numberOfPairs = int.Parse(reader.ReadLine());
            var pairs = new List<Tuple<string, string>>();

            for (int n = 0; n < numberOfPairs; n++)
            {
                var names = reader.ReadLine().Split();
                pairs.Add(new Tuple<string, string>(names[0], names[1]));
            }

            inputData.Add(new Tuple<int, List<Tuple<string, string>>>(numberOfPairs, pairs));
        }

        return inputData;
    }
}

public class OutputHandler
{
    public static void WriteOutput(StreamWriter writer, List<string> output)
    {
        foreach (var line in output)
        {
            writer.WriteLine(line);
        }
    }
}

public class FriendCircleApp
{
    public static void Main()
    {
        using var reader = new StreamReader(Console.OpenStandardInput());
        using var writer = new StreamWriter(Console.OpenStandardOutput());

        var inputData = InputHandler.ParseInput(reader);
        var results = new List<string>();

        foreach (var data in inputData)
        {
            var disjointSet = new DisjointSet();
            var pairs = data.Item2;

            foreach (var pair in pairs)
            {
                int size = disjointSet.Union(pair.Item1, pair.Item2);
                results.Add(size.ToString());
            }
        }

        OutputHandler.WriteOutput(writer, results);
    }
}
