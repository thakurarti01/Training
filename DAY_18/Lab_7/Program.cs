// ### Lab 7 — Build Your Own Generic Collection

// 1. Implement a generic `FixedSizeStack<T>` class that:
//    - Has a fixed capacity set in the constructor
//    - Throws `InvalidOperationException` on `Push` when full, and on `Pop`/`Peek` when empty
//    - Implements `IEnumerable<T>` so it can be used in `foreach` (iterate top-to-bottom)
// 2. Implement `IReadOnlyCollection<T>` on the same class (expose `Count`).
// 3. Write a generic extension method:
//    ```csharp
//    public static FixedSizeStack<T> ToFixedSizeStack<T>(this IEnumerable<T> source, int capacity)
//    ```
// 4. Demonstrate: build a stack of `int`, iterate it with `foreach`, and convert a `List<string>` into a `FixedSizeStack<string>` using your extension method.

// **Deliverable:** Console app + the `FixedSizeStack<T>` class in its own file, demonstrating all requirements including the exception cases (caught and printed, not crashing the app).

// ---

using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // =====================================================
        // 1. Create a FixedSizeStack<int>
        // =====================================================

        Console.WriteLine("===== INTEGER STACK =====");

        // Create a stack with a fixed capacity of 3.
        FixedSizeStack<int> stack =
            new FixedSizeStack<int>(3);


        // Add three integers to the stack.
        stack.Push(10);
        stack.Push(20);
        stack.Push(30);


        // =====================================================
        // 2. Iterate using foreach
        // =====================================================

        Console.WriteLine("\nStack elements:");

        // foreach works because FixedSizeStack implements
        // IEnumerable<T>.
        // Elements are displayed from top to bottom.
        foreach (int number in stack)
        {
            Console.WriteLine(number);
        }


        // =====================================================
        // 3. Peek
        // =====================================================

        Console.WriteLine(
            "\nTop element: " + stack.Peek());


        // =====================================================
        // 4. Push exception when stack is full
        // =====================================================

        Console.WriteLine(
            "\n===== FULL STACK TEST =====");

        try
        {
            // Capacity is already 3, so this Push should fail.
            stack.Push(40);
        }
        catch (InvalidOperationException ex)
        {
            // Catch the exception so the program does not crash.
            Console.WriteLine(
                "Push Exception: " + ex.Message);
        }


        // =====================================================
        // 5. Pop an element
        // =====================================================

        Console.WriteLine(
            "\n===== POP TEST =====");

        // Pop removes and returns the top element.
        Console.WriteLine(
            "Popped: " + stack.Pop());


        // The next element is now at the top.
        Console.WriteLine(
            "New top: " + stack.Peek());


        // =====================================================
        // 6. Empty stack exception
        // =====================================================

        Console.WriteLine(
            "\n===== EMPTY STACK TEST =====");

        // Create an empty stack for testing exceptions.
        FixedSizeStack<int> emptyStack =
            new FixedSizeStack<int>(2);


        try
        {
            // Pop cannot be performed on an empty stack.
            emptyStack.Pop();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(
                "Pop Exception: " + ex.Message);
        }


        try
        {
            // Peek also cannot be performed on an empty stack.
            emptyStack.Peek();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(
                "Peek Exception: " + ex.Message);
        }


        // =====================================================
        // 7. Convert List<string> using extension method
        // =====================================================

        Console.WriteLine(
            "\n===== EXTENSION METHOD TEST =====");


        List<string> names =
            new List<string>
            {
                "Arti",
                "Rahul",
                "Neha"
            };


        // Extension method converts the List<string>
        // into a FixedSizeStack<string>.
        FixedSizeStack<string> nameStack =
            names.ToFixedSizeStack(3);


        Console.WriteLine(
            "Names in FixedSizeStack:");

        foreach (string name in nameStack)
        {
            Console.WriteLine(name);
        }


        // Count comes from IReadOnlyCollection<T>.
        Console.WriteLine(
            "Count: " + nameStack.Count);
    }
}