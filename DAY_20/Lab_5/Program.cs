// ### Lab 5 — Anonymous Methods + Closures

// 1. Using the `delegate` keyword (not a lambda), write an anonymous method assigned to an `Action<int>` that squares and prints its argument.
// 2. Write an anonymous method that captures and increments an outer `int total` variable each time it's called; call it 5 times and print `total` afterward to prove the closure mutated the outer variable.
// 3. Rewrite both anonymous methods as lambdas and confirm identical behavior — add a comment noting the syntactic difference.

// **Deliverable:** Console app with both anonymous-method and lambda versions side by side, output proving closures work identically in both forms.

// ---

using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("===== LAB 5: Anonymous Methods + Closures =====\n");

        // Anonymous method using the 'delegate' keyword.
        // It accepts an integer and prints its square.
        Action<int> squareAnonymous = delegate (int number)
        {
            Console.WriteLine($"Square of {number}: {number * number}");
        };

        // Call the anonymous method.
        squareAnonymous(5);

        Console.WriteLine("\n===== Anonymous Method Closure =====");

        // This variable belongs to the outer method.
        int total = 0;

        // The anonymous method captures the outer 'total' variable.
        Action addAnonymous = delegate
        {
            total++;
        };

        // Call the delegate five times.
        for (int i = 0; i < 5; i++)
        {
            addAnonymous();
        }

        // The closure has modified the original outer variable.
        Console.WriteLine($"Total after anonymous method: {total}");

        Console.WriteLine("\n===== Lambda Version =====");

        // The anonymous method above can be written more shortly
        // using a lambda expression.
        Action<int> squareLambda = number =>
        {
            Console.WriteLine($"Square of {number}: {number * number}");
        };

        squareLambda(5);

        Console.WriteLine("\n===== Lambda Closure =====");

        int lambdaTotal = 0;

        // Lambda captures lambdaTotal from the surrounding scope.
        Action addLambda = () =>
        {
            lambdaTotal++;
        };

        // Call the lambda five times.
        for (int i = 0; i < 5; i++)
        {
            addLambda();
        }

        Console.WriteLine($"Total after lambda: {lambdaTotal}");

        /*
         * Anonymous methods and lambda expressions can both create closures.
         *
         * The main difference here is syntax:
         *
         * Anonymous method:
         * delegate { ... }
         *
         * Lambda:
         * () => { ... }
         *
         * Lambda expressions are generally shorter and more commonly used
         * in modern C# code.
         */
    }
}