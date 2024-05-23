using System;
using System.Collections.Generic;

public class PathFinder
{
    public static void Main()
    {
        string[] lines = new string[]
        {
            "..###.....#####.....#####",
            "..###.....#####.....#####",
            "..###.....#####.....#####",
            "...........###......#####",
            "......###...........#####",
            "...#..###..###......#####",
            "###############.#######..",
            "###############.#######.#",
            "###############.#######..",
            "................########.",
            "................#######..",
            "...#######..##.......##.#",
            "...#######..##.......##..",
            "...##...........########.",
            "#####...........#######..",
            "#####...##..##..#...#...#",
            "#####...##..##....#...#.."
        };

        var map = ConvertToCharArray(lines);
        var path = FindShortestPath(map);

        if (path == null)
        {
            Console.WriteLine("No path found.");
        }
        else
        {
            foreach (var (x, y) in path)
            {
                map[x, y] = 'o';
            }

            PrintMap(map);
        }
    }

    static char[,] ConvertToCharArray(string[] lines)
    {
        int rows = lines.Length;
        int cols = lines[0].Length;
        char[,] map = new char[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                map[i, j] = lines[i][j];
            }
        }
        return map;
    }

    static List<(int, int)> FindShortestPath(char[,] map)
    {
        int rows = map.GetLength(0);
        int cols = map.GetLength(1);
        var start = (x: 0, y: 0);
        var goal = (x: rows - 1, y: cols - 1);
        var directions = new List<(int, int)> { (1, 0), (0, 1), (-1, 0), (0, -1) };
        var queue = new Queue<(int x, int y, List<(int, int)> path)>();
        var visited = new bool[rows, cols];
        queue.Enqueue((start.x, start.y, new List<(int, int)> { start }));
        visited[start.x, start.y] = true;

        while (queue.Count > 0)
        {
            var (x, y, path) = queue.Dequeue();

            foreach (var (dx, dy) in directions)
            {
                int newX = x + dx, newY = y + dy;
                if (newX >= 0 && newX < rows && newY >= 0 && newY < cols && !visited[newX, newY] && map[newX, newY] != '#')
                {
                    var newPath = new List<(int, int)>(path) { (newX, newY) };
                    if (newX == goal.x && newY == goal.y)
                    {
                        return newPath;
                    }
                    queue.Enqueue((newX, newY, newPath));
                    visited[newX, newY] = true;
                }
            }
        }

        return null;
    }

    static void PrintMap(char[,] map)
    {
        int rows = map.GetLength(0);
        int cols = map.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(map[i, j]);
            }
            Console.WriteLine();
        }
    }
}
