using System;
using System.Collections.Generic;

class Graph
{
    private int vertices;
    private List<int>[] adj;

    public Graph(int v)
    {
        vertices = v;
        adj = new List<int>[v];

        for (int i = 0; i < v; i++)
        {
            adj[i] = new List<int>();
        }
    }

    // Add friendship (Undirected Graph)
    public void AddEdge(int u, int v)
    {
        adj[u].Add(v);
        adj[v].Add(u);
    }

    // Task 1: Find all friends of a user
    public void FindFriends(int user)
    {
        Console.Write("Friends of User " + user + ": ");
        foreach (int friend in adj[user])
        {
            Console.Write(friend + " ");
        }
        Console.WriteLine();
    }

    // Task 2: Check if two users are connected
    public bool AreConnected(int source, int destination)
    {
        bool[] visited = new bool[vertices];
        Queue<int> queue = new Queue<int>();

        visited[source] = true;
        queue.Enqueue(source);

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();

            if (current == destination)
                return true;

            foreach (int neighbor in adj[current])
            {
                if (!visited[neighbor])
                {
                    visited[neighbor] = true;
                    queue.Enqueue(neighbor);
                }
            }
        }

        return false;
    }

    // Task 3: Shortest Path
    public void ShortestPath(int source, int destination)
    {
        bool[] visited = new bool[vertices];
        int[] parent = new int[vertices];

        for (int i = 0; i < vertices; i++)
            parent[i] = -1;

        Queue<int> queue = new Queue<int>();

        visited[source] = true;
        queue.Enqueue(source);

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();

            foreach (int neighbor in adj[current])
            {
                if (!visited[neighbor])
                {
                    visited[neighbor] = true;
                    parent[neighbor] = current;
                    queue.Enqueue(neighbor);
                }
            }
        }

        if (!visited[destination])
        {
            Console.WriteLine("No Path Found");
            return;
        }

        Stack<int> path = new Stack<int>();
        int temp = destination;

        while (temp != -1)
        {
            path.Push(temp);
            temp = parent[temp];
        }

        Console.Write("Shortest Path: ");
        while (path.Count > 0)
        {
            Console.Write(path.Pop());

            if (path.Count > 0)
                Console.Write(" -> ");
        }

        Console.WriteLine();
    }

    // Task 4: Users at Distance 2
    public void UsersAtDistanceTwo(int source)
    {
        bool[] visited = new bool[vertices];
        int[] distance = new int[vertices];

        Queue<int> queue = new Queue<int>();

        visited[source] = true;
        queue.Enqueue(source);

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();

            foreach (int neighbor in adj[current])
            {
                if (!visited[neighbor])
                {
                    visited[neighbor] = true;
                    distance[neighbor] = distance[current] + 1;
                    queue.Enqueue(neighbor);
                }
            }
        }

        Console.Write("Users at Distance 2 from User " + source + ": ");

        for (int i = 0; i < vertices; i++)
        {
            if (distance[i] == 2)
                Console.Write(i + " ");
        }

        Console.WriteLine();
    }

    // Task 5: Detect Cycle
    private bool CycleDFS(int current, bool[] visited, int parent)
    {
        visited[current] = true;

        foreach (int neighbor in adj[current])
        {
            if (!visited[neighbor])
            {
                if (CycleDFS(neighbor, visited, current))
                    return true;
            }
            else if (neighbor != parent)
            {
                return true;
            }
        }

        return false;
    }

    public bool HasCycle()
    {
        bool[] visited = new bool[vertices];

        for (int i = 0; i < vertices; i++)
        {
            if (!visited[i])
            {
                if (CycleDFS(i, visited, -1))
                    return true;
            }
        }

        return false;
    }

    // Task 6: Connected Components
    private void DFS(int vertex, bool[] visited)
    {
        visited[vertex] = true;
        Console.Write(vertex + " ");

        foreach (int neighbor in adj[vertex])
        {
            if (!visited[neighbor])
                DFS(neighbor, visited);
        }
    }

    public void ConnectedComponents()
    {
        bool[] visited = new bool[vertices];
        int count = 1;

        for (int i = 0; i < vertices; i++)
        {
            if (!visited[i])
            {
                Console.Write("Friend Group " + count + ": ");
                DFS(i, visited);
                Console.WriteLine();
                count++;
            }
        }
    }
}

class Program
{
    static void Main()
    {
        Graph graph = new Graph(6);

        // Add Friendships
        graph.AddEdge(0, 1);
        graph.AddEdge(0, 2);
        graph.AddEdge(1, 3);
        graph.AddEdge(2, 3);
        graph.AddEdge(2, 4);
        graph.AddEdge(3, 5);
        graph.AddEdge(4, 5);

        Console.WriteLine("Social Network Friend Recommendations\n");

        // Task 1
        graph.FindFriends(2);

        // Task 2
        Console.WriteLine("User 0 and User 5 Connected: " +
            graph.AreConnected(0, 5));

        // Task 3
        graph.ShortestPath(0, 5);

        // Task 4
        graph.UsersAtDistanceTwo(1);

        // Task 5
        Console.WriteLine("Cycle Present: " + graph.HasCycle());

        // Task 6
        graph.ConnectedComponents();
    }
}