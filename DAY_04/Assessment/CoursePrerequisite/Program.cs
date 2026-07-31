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

    // Add prerequisite -> course
    public void AddEdge(int from, int to)
    {
        adj[from].Add(to);
    }

    // Task 2: All prerequisites of a course
    public void PrintAllPrerequisites(int course)
    {
        bool[] visited = new bool[vertices];

        Console.Write("Prerequisites for Course " + course + ": ");

        for (int i = 0; i < vertices; i++)
        {
            FindPrerequisite(i, course, visited);
        }

        Console.WriteLine();
    }

    private bool FindPrerequisite(int start, int target, bool[] visited)
    {
        if (start == target)
            return false;

        visited[start] = true;

        foreach (int next in adj[start])
        {
            if (next == target)
            {
                Console.Write(start + " ");
                return true;
            }

            if (!visited[next])
            {
                if (FindPrerequisite(next, target, visited))
                {
                    Console.Write(start + " ");
                    return true;
                }
            }
        }

        return false;
    }

    // Task 3: Direct prerequisites
    public void DirectPrerequisites(int course)
    {
        Console.Write("Direct prerequisites of Course " + course + ": ");

        for (int i = 0; i < vertices; i++)
        {
            if (adj[i].Contains(course))
            {
                Console.Write(i + " ");
            }
        }

        Console.WriteLine();
    }

    // Task 4: Cycle Detection
    public bool HasCycle()
    {
        bool[] visited = new bool[vertices];
        bool[] recStack = new bool[vertices];

        for (int i = 0; i < vertices; i++)
        {
            if (HasCycleUtil(i, visited, recStack))
                return true;
        }

        return false;
    }

    private bool HasCycleUtil(int v, bool[] visited, bool[] recStack)
    {
        if (recStack[v])
            return true;

        if (visited[v])
            return false;

        visited[v] = true;
        recStack[v] = true;

        foreach (int n in adj[v])
        {
            if (HasCycleUtil(n, visited, recStack))
                return true;
        }

        recStack[v] = false;
        return false;
    }

    // Task 5: Topological Sort
    public void TopologicalSort()
    {
        int[] indegree = new int[vertices];

        for (int i = 0; i < vertices; i++)
        {
            foreach (int n in adj[i])
                indegree[n]++;
        }

        Queue<int> queue = new Queue<int>();

        for (int i = 0; i < vertices; i++)
        {
            if (indegree[i] == 0)
                queue.Enqueue(i);
        }

        Console.Write("Topological Order: ");

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            Console.Write(current + " ");

            foreach (int n in adj[current])
            {
                indegree[n]--;

                if (indegree[n] == 0)
                    queue.Enqueue(n);
            }
        }

        Console.WriteLine();
    }

    // Task 6: Courses with no prerequisites
    public void CoursesWithNoPrerequisites()
    {
        int[] indegree = new int[vertices];

        for (int i = 0; i < vertices; i++)
        {
            foreach (int n in adj[i])
                indegree[n]++;
        }

        Console.Write("Courses with no prerequisites: ");

        for (int i = 0; i < vertices; i++)
        {
            if (indegree[i] == 0)
                Console.Write(i + " ");
        }

        Console.WriteLine();
    }

    // Task 7: Count direct dependents
    public void CountDependents(int course)
    {
        Console.WriteLine("Courses directly depending on Course " + course + ": " + adj[course].Count);
    }
}

class Program
{
    static void Main()
    {
        Graph g = new Graph(6);

        // prerequisite -> course
        g.AddEdge(0, 1);
        g.AddEdge(0, 2);
        g.AddEdge(1, 3);
        g.AddEdge(2, 3);
        g.AddEdge(2, 4);
        g.AddEdge(3, 5);
        g.AddEdge(4, 5);

        g.PrintAllPrerequisites(5);

        g.DirectPrerequisites(3);

        if (g.HasCycle())
        {
            Console.WriteLine("Graph contains a cycle.");
        }
        else
        {
            Console.WriteLine("No cycle found.");
            g.TopologicalSort();
        }

        g.CoursesWithNoPrerequisites();

        g.CountDependents(2);
    }
}