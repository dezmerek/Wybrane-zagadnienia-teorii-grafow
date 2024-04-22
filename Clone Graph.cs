public class Solution
{
    public Node CloneGraph(Node node)
    {
        if (node == null) return null;

        Dictionary<Node, Node> visited = new Dictionary<Node, Node>();
        Queue<Node> queue = new Queue<Node>();

        visited.Add(node, new Node(node.val));
        queue.Enqueue(node);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            foreach (var neighbor in current.neighbors)
            {
                if (!visited.ContainsKey(neighbor))
                {
                    visited.Add(neighbor, new Node(neighbor.val));
                    queue.Enqueue(neighbor);
                }
                visited[current].neighbors.Add(visited[neighbor]);
            }
        }

        return visited[node];
    }
}