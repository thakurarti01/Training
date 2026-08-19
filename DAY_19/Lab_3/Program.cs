// ### Lab 3 — BFS and DFS

// Given this graph (as a `Dictionary<string, List<string>>`):
// ```
// A -> B, C
// B -> D
// C -> D
// D -> E
// ```

// 1. Implement `BreadthFirstSearch(graph, "A")` using `Queue<string>` and `HashSet<string>` for visited-tracking.
// 2. Implement `DepthFirstSearch(graph, "A")` using `Stack<string>`.
// 3. Print both traversal orders and explain in a comment why they differ.

// **Deliverable:** Console app printing both traversal results.

// ---

using System;
using System.Collections.Generic;

class Program
{
    // =========================================================
    // BFS - Breadth-First Search
    // =========================================================

    static List<string> BreadthFirstSearch(
        Dictionary<string, List<string>> graph,
        string start)
    {
        // Queue is used because BFS visits nodes level by level.
        Queue<string> queue = new Queue<string>();

        // HashSet keeps track of already visited nodes.
        // This prevents the same node from being processed twice.
        HashSet<string> visited = new HashSet<string>();

        // This list stores the final BFS traversal order.
        List<string> result = new List<string>();

        // Start the traversal from the given node.
        queue.Enqueue(start);
        visited.Add(start);

        // Continue until there are no more nodes to process.
        while (queue.Count > 0)
        {
            // Remove the first node from the queue.
            string current = queue.Dequeue();

            // Store the node in the traversal result.
            result.Add(current);

            // Visit all neighbours of the current node.
            foreach (string neighbour in graph[current])
            {
                // Only add a node if it has not been visited before.
                if (!visited.Contains(neighbour))
                {
                    visited.Add(neighbour);
                    queue.Enqueue(neighbour);
                }
            }
        }

        return result;
    }


    // =========================================================
    // DFS - Depth-First Search
    // =========================================================

    static List<string> DepthFirstSearch(
        Dictionary<string, List<string>> graph,
        string start)
    {
        // Stack is used because DFS explores one path deeply
        // before going back and exploring another path.
        Stack<string> stack = new Stack<string>();

        // HashSet prevents already visited nodes from being
        // processed again.
        HashSet<string> visited = new HashSet<string>();

        // Stores the final DFS traversal order.
        List<string> result = new List<string>();

        // Put the starting node into the stack.
        stack.Push(start);

        // Continue until the stack becomes empty.
        while (stack.Count > 0)
        {
            // Remove the top node from the stack.
            string current = stack.Pop();

            // Skip the node if it was already visited.
            if (visited.Contains(current))
            {
                continue;
            }

            // Mark the node as visited.
            visited.Add(current);

            // Store the node in the traversal result.
            result.Add(current);

            // Add neighbouring nodes to the stack.
            // Reverse order is used so that the first neighbour
            // is processed first when it is popped.
            List<string> neighbours = graph[current];

            for (int i = neighbours.Count - 1; i >= 0; i--)
            {
                string neighbour = neighbours[i];

                if (!visited.Contains(neighbour))
                {
                    stack.Push(neighbour);
                }
            }
        }

        return result;
    }


    // =========================================================
    // MAIN METHOD
    // =========================================================

    static void Main()
    {
        Console.WriteLine("==============================");
        Console.WriteLine("LAB 3 - BFS AND DFS");
        Console.WriteLine("==============================");


        // ---------------------------------------------------------
        // Create the graph using a Dictionary.
        // ---------------------------------------------------------

        // Each key represents a node.
        // The List<string> contains its neighbouring nodes.
        Dictionary<string, List<string>> graph =
            new Dictionary<string, List<string>>
            {
                { "A", new List<string> { "B", "C" } },
                { "B", new List<string> { "D" } },
                { "C", new List<string> { "D" } },
                { "D", new List<string> { "E" } },
                { "E", new List<string>() }
            };


        // ---------------------------------------------------------
        // Perform BFS starting from A.
        // ---------------------------------------------------------

        List<string> bfsResult =
            BreadthFirstSearch(graph, "A");

        Console.WriteLine("\nBFS Traversal:");
        Console.WriteLine(string.Join(" -> ", bfsResult));


        // ---------------------------------------------------------
        // Perform DFS starting from A.
        // ---------------------------------------------------------

        List<string> dfsResult =
            DepthFirstSearch(graph, "A");

        Console.WriteLine("\nDFS Traversal:");
        Console.WriteLine(string.Join(" -> ", dfsResult));


        // ---------------------------------------------------------
        // Explanation
        // ---------------------------------------------------------

        // BFS uses a Queue, so it explores all nodes at the
        // current level before moving to the next level.
        //
        // DFS uses a Stack, so it follows one path as deeply
        // as possible before going back.
        Console.WriteLine("\nWhy are they different?");
        Console.WriteLine(
            "BFS explores level by level using a Queue, " +
            "while DFS explores deeply using a Stack."
        );
    }
}