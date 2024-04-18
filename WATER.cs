class Program
{
    static int[] dx = { -1, 1, 0, 0 };
    static int[] dy = { 0, 0, -1, 1 };

    static void Main()
    {
        int t = int.Parse(Console.ReadLine().Trim());
        for (int k = 0; k < t; k++)
        {
            if (k > 0)
                Console.ReadLine();

            string[] dimensions = Console.ReadLine().Split();
            int n = int.Parse(dimensions[0]);
            int m = int.Parse(dimensions[1]);

            int[,] heights = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                string[] row = Console.ReadLine().Split();
                for (int j = 0; j < m; j++)
                {
                    heights[i, j] = int.Parse(row[j]);
                }
            }

            Console.WriteLine(CalculateMaxWaterVolume(heights, n, m));
        }
    }

    static int CalculateMaxWaterVolume(int[,] heights, int n, int m)
    {
        bool[,] visited = new bool[n, m];
        PriorityQueue<(int height, int x, int y)> pq = new PriorityQueue<(int height, int x, int y)>();

        InitializeBorder(heights, n, m, pq, visited);

        int waterTrapped = 0;

        while (pq.Count > 0)
        {
            var current = pq.Dequeue();

            for (int i = 0; i < 4; i++)
            {
                int nx = current.x + dx[i];
                int ny = current.y + dy[i];

                if (nx >= 0 && nx < n && ny >= 0 && ny < m && !visited[nx, ny])
                {
                    visited[nx, ny] = true;
                    int newHeight = Math.Max(current.height, heights[nx, ny]);
                    waterTrapped += Math.Max(0, current.height - heights[nx, ny]);
                    pq.Enqueue((newHeight, nx, ny));
                }
            }
        }

        return waterTrapped;
    }

    static void InitializeBorder(int[,] heights, int n, int m, PriorityQueue<(int height, int x, int y)> pq, bool[,] visited)
    {
        for (int i = 0; i < n; i++)
        {
            EnqueueAndUpdate(heights, i, 0, pq, visited);
            EnqueueAndUpdate(heights, i, m - 1, pq, visited);
        }

        for (int j = 1; j < m - 1; j++)
        {
            EnqueueAndUpdate(heights, 0, j, pq, visited);
            EnqueueAndUpdate(heights, n - 1, j, pq, visited);
        }
    }

    static void EnqueueAndUpdate(int[,] heights, int x, int y, PriorityQueue<(int height, int x, int y)> pq, bool[,] visited)
    {
        pq.Enqueue((heights[x, y], x, y));
        visited[x, y] = true;
    }
}

class PriorityQueue<T>
{
    private SortedSet<T> set = new SortedSet<T>();
    public int Count => set.Count;

    public void Enqueue(T item)
    {
        set.Add(item);
    }

    public T Dequeue()
    {
        var item = set.Min;
        set.Remove(item);
        return item;
    }
}
