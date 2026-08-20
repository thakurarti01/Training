// ### Lab 1 — `var` vs Explicit Types vs `dynamic`

// 1. Declare the same value three ways: `var count = 10;`, `int countExplicit = 10;`, `dynamic countDynamic = 10;`. Print each and print `.GetType()` for all three.
// 2. Attempt `countDynamic = "now text";` followed by using it in an arithmetic expression (e.g., `countDynamic + 5`). Catch and print the resulting runtime exception.
// 3. Create an anonymous type `var point = new { X = 3, Y = 7 };` and print its properties. Try (and comment out) an assignment to `point.X` — note the compiler error in a comment.
// 4. Write a short paragraph (as a code comment) explaining when you'd choose `dynamic` over `var` in a real project — cite a scenario from the guide.

// **Deliverable:** Console app demonstrating all three typing approaches with the runtime exception caught and reported, not crashing the app.

// ---

using System;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        Console.WriteLine("===== LAB 1: var vs Explicit Type vs dynamic =====\n");

        // 'var' lets the compiler automatically determine the data type.
        // Here, 10 is an integer, so count becomes int.
        var count = 10;

        // Explicit type declaration clearly tells the compiler
        // that countExplicit is an integer.
        int countExplicit = 10;

        // 'dynamic' allows the variable's type to be determined at runtime.
        // Unlike var, its type can change during execution.
        dynamic countDynamic = 10;

        // Displaying the values of all three variables.
        Console.WriteLine($"var value: {count}");
        Console.WriteLine($"Explicit value: {countExplicit}");
        Console.WriteLine($"dynamic value: {countDynamic}");

        Console.WriteLine();

        // GetType() shows the actual runtime type of each variable.
        Console.WriteLine($"var type: {count.GetType()}");
        Console.WriteLine($"Explicit type: {countExplicit.GetType()}");
        Console.WriteLine($"dynamic type: {countDynamic.GetType()}");

        Console.WriteLine("\n===== Dynamic Runtime Error =====");

        // dynamic can change its type during runtime.
        countDynamic = "now text";

        try
        {
            // The compiler allows this because countDynamic is dynamic.
            // But at runtime, string + int is not a valid operation.
            var result = countDynamic + 5;

            Console.WriteLine(result);
        }
        catch (RuntimeBinderException ex)
        {
            // This exception is caught so that the application
            // does not crash when the invalid operation is performed.
            Console.WriteLine($"Runtime error: {ex.Message}");
        }

        Console.WriteLine("\n===== Anonymous Type =====");

        // Anonymous types allow us to create a small object
        // without explicitly creating a class.
        var point = new
        {
            X = 3,
            Y = 7
        };

        // Accessing properties of the anonymous object.
        Console.WriteLine($"X = {point.X}");
        Console.WriteLine($"Y = {point.Y}");

        // point.X = 10;
        // Compiler error:
        // "Property or indexer '<anonymous type>.X' cannot be assigned to -- it is read only."

        /*
         * var vs dynamic:
         *
         * I would normally prefer 'var' because the compiler still knows
         * the type at compile time and provides type safety.
         *
         * I would choose 'dynamic' when working with data whose type
         * cannot be easily known at compile time, such as certain COM
         * objects, reflection scenarios, or dynamic data from external APIs.
         */

        Console.WriteLine("\nProgram completed successfully.");
    }
}