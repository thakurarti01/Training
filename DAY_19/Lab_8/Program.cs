// ### Lab 8 — Iterators

// 1. Write `IEnumerable<int> Fibonacci()` as an infinite iterator using `yield return`. Consume only the first 10 values with `.Take(10)`.
// 2. Write `IEnumerable<int> TakeWhilePositive(IEnumerable<int> source)` using `yield break` to stop at the first non-positive value.
// 3. Prove lazy evaluation: add a `Console.WriteLine` inside an iterator method, call the method, and show nothing prints until you actually `foreach` over the result.
// 4. Build a small `TreeNode<T> : IEnumerable<T>` class (as in the guide) with a recursive `yield return`-based `GetEnumerator()` performing depth-first traversal. Construct a tree with at least 2 levels and print the traversal via `foreach`.
// 5. Add a second named iterator method to any class from an earlier lab (e.g., `MyList<T>.InReverse()`) that yields elements in reverse order without allocating a second array.

// **Deliverable:** Console app demonstrating all five, including printed proof of lazy evaluation ordering.

// ---

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


// =============================================================
// TREE NODE CLASS
// =============================================================

class TreeNode<T> : IEnumerable<T>
{
    // Stores the value of the current tree node.
    public T Value { get; }

    // Stores all child nodes of the current node.
    public List<TreeNode<T>> Children { get; } =
        new List<TreeNode<T>>();


    // Constructor initializes the node with a value.
    public TreeNode(T value)
    {
        Value = value;
    }


    // Adds a child node to the current node.
    public void AddChild(TreeNode<T> child)
    {
        Children.Add(child);
    }


    // =========================================================
    // DEPTH-FIRST ITERATOR
    // =========================================================

    public IEnumerator<T> GetEnumerator()
    {
        // First return the current node.
        yield return Value;

        // Then recursively visit every child.
        // This creates a depth-first traversal.
        foreach (TreeNode<T> child in Children)
        {
            foreach (T value in child)
            {
                yield return value;
            }
        }
    }


    // Required non-generic IEnumerable implementation.
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}


// =============================================================
// PROGRAM CLASS
// =============================================================

class Program
{
    // =========================================================
    // 1. INFINITE FIBONACCI ITERATOR
    // =========================================================

    static IEnumerable<int> Fibonacci()
    {
        // First two Fibonacci numbers.
        int first = 0;
        int second = 1;

        // The loop is intentionally infinite.
        // The caller decides how many values to consume.
        while (true)
        {
            // Return the current Fibonacci number.
            yield return first;

            // Calculate the next Fibonacci number.
            int next = first + second;

            // Move the values forward.
            first = second;
            second = next;
        }
    }


    // =========================================================
    // 2. TAKE WHILE POSITIVE
    // =========================================================

    static IEnumerable<int> TakeWhilePositive(
        IEnumerable<int> source)
    {
        // Process each number from the source.
        foreach (int number in source)
        {
            // Stop completely when a non-positive value is found.
            if (number <= 0)
            {
                yield break;
            }

            // Return positive values one at a time.
            yield return number;
        }
    }


    // =========================================================
    // 3. LAZY EVALUATION DEMONSTRATION
    // =========================================================

    static IEnumerable<int> LazyNumbers()
    {
        Console.WriteLine(
            "Iterator started executing."
        );

        // This message is executed only when the iterator
        // is actually consumed.
        yield return 10;

        Console.WriteLine(
            "Producing second value."
        );

        yield return 20;

        Console.WriteLine(
            "Producing third value."
        );

        yield return 30;
    }


    // =========================================================
    // 4. REVERSE ITERATOR
    // =========================================================

    static IEnumerable<T> InReverse<T>(
        IEnumerable<T> source)
    {
        // Convert the source to a list so that we can access
        // elements using indexes from the end.
        List<T> items = source.ToList();

        // Start at the last element and move backwards.
        for (int i = items.Count - 1; i >= 0; i--)
        {
            // Return one element at a time in reverse order.
            yield return items[i];
        }
    }


    // =========================================================
    // MAIN METHOD
    // =========================================================

    static void Main()
    {
        Console.WriteLine("==============================");
        Console.WriteLine("LAB 8 - ITERATORS");
        Console.WriteLine("==============================");


        // =====================================================
        // 1. FIBONACCI
        // =====================================================

        Console.WriteLine("\n1. First 10 Fibonacci Numbers");

        // Fibonacci() is infinite, so Take(10) limits
        // consumption to only the first 10 values.
        IEnumerable<int> firstTen =
            Fibonacci().Take(10);

        foreach (int number in firstTen)
        {
            Console.Write(number + " ");
        }

        Console.WriteLine();


        // =====================================================
        // 2. TAKE WHILE POSITIVE
        // =====================================================

        Console.WriteLine("\n2. TakeWhilePositive");

        int[] numbers =
        {
            5,
            10,
            20,
            0,
            30,
            40
        };

        // The iterator should stop when it reaches 0.
        IEnumerable<int> positiveNumbers =
            TakeWhilePositive(numbers);

        foreach (int number in positiveNumbers)
        {
            Console.Write(number + " ");
        }

        Console.WriteLine();


        // =====================================================
        // 3. LAZY EVALUATION
        // =====================================================

        Console.WriteLine("\n3. Lazy Evaluation");

        Console.WriteLine(
            "Before calling iterator method."
        );

        // Calling the iterator method only creates the
        // enumerable; its body has not executed yet.
        IEnumerable<int> lazyResult =
            LazyNumbers();

        Console.WriteLine(
            "After calling iterator method."
        );

        Console.WriteLine(
            "Starting foreach..."
        );

        // The iterator starts executing only when foreach
        // requests its first value.
        foreach (int number in lazyResult)
        {
            Console.WriteLine(
                "Received: " + number
            );
        }


        // =====================================================
        // 4. TREE DEPTH-FIRST TRAVERSAL
        // =====================================================

        Console.WriteLine(
            "\n4. Tree Depth-First Traversal"
        );

        // Create the root node.
        TreeNode<string> root =
            new TreeNode<string>("A");

        // Create second-level nodes.
        TreeNode<string> b =
            new TreeNode<string>("B");

        TreeNode<string> c =
            new TreeNode<string>("C");

        // Add B and C as children of A.
        root.AddChild(b);
        root.AddChild(c);


        // Create third-level nodes.
        TreeNode<string> d =
            new TreeNode<string>("D");

        TreeNode<string> e =
            new TreeNode<string>("E");

        TreeNode<string> f =
            new TreeNode<string>("F");

        // Build the tree:
        //
        //          A
        //        /   \
        //       B     C
        //      / \     \
        //     D   E     F

        b.AddChild(d);
        b.AddChild(e);
        c.AddChild(f);


        // TreeNode implements IEnumerable<T>, so foreach
        // automatically uses its recursive iterator.
        foreach (string value in root)
        {
            Console.Write(value + " ");
        }

        Console.WriteLine();


        // =====================================================
        // 5. REVERSE ITERATOR
        // =====================================================

        Console.WriteLine(
            "\n5. Reverse Iterator"
        );

        int[] original =
        {
            1,
            2,
            3,
            4,
            5
        };

        // InReverse returns the values from last to first.
        // It uses yield return instead of creating another
        // reversed array.
        foreach (int number in InReverse(original))
        {
            Console.Write(number + " ");
        }

        Console.WriteLine();


        Console.WriteLine(
            "\nProgram completed successfully."
        );
    }
}