// ---

// ### Lab 1 — Non-Generic vs Generic Collections

// **Goal:** Feel the pain of `System.Collections`, then fix it.

// 1. Create an `ArrayList` and add: `10`, `"twenty"`, `30.5`, `true`.
// 2. Write a loop that sums only the numeric-looking entries using `is` pattern matching and casting. Observe how easy it is to accidentally introduce a bug (e.g., someone adds a non-numeric object later).
// 3. Now redo the same task using `List<int>` — the compiler should refuse to let you add `"twenty"`.
// 4. Using `System.Diagnostics.Stopwatch`, benchmark inserting 2,000,000 integers into an `ArrayList` vs a `List<int>`. Print both timings.

// **Deliverable:** Console app printing the sum, a compile-error screenshot/comment explaining why step 3 rejects bad input, and the benchmark timings.

// ---

//----------------------------------------- CODE -----------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        // =========================================================
        // 1. Create an ArrayList and add different types of values
        // =========================================================

        // ArrayList is non-generic, so it can store different data types.
        ArrayList values = new ArrayList();

        values.Add(10);
        values.Add("twenty");
        values.Add(30.5);
        values.Add(true);


        // =========================================================
        // 2. Sum only the numeric values using pattern matching
        // =========================================================

        double sum = 0;

        foreach (object value in values)
        {
            // 'is int' checks whether the current value is an integer.
            // If true, we safely cast it to int and add it to the sum.
            if (value is int number)
            {
                sum += number;
            }

            // 'is double' checks whether the value is a decimal number.
            // We cast it to double before adding it to the sum.
            else if (value is double decimalNumber)
            {
                sum += decimalNumber;
            }

            // Strings and bool values are ignored because they are not numeric.
        }

        Console.WriteLine("ArrayList numeric sum: " + sum);


        // =========================================================
        // 3. Repeat the task using List<int>
        // =========================================================

        // List<int> is generic, so it can store only integers.
        List<int> numbers = new List<int>();

        numbers.Add(10);
        numbers.Add(20);
        numbers.Add(30);

        int intSum = 0;

        // Since List<int> contains only integers,
        // no type checking or casting is required.
        foreach (int number in numbers)
        {
            intSum += number;
        }

        Console.WriteLine("List<int> sum: " + intSum);


        // IMPORTANT:
        // The following line produces a compile-time error.
        // List<int> does not allow a string because it expects only int.
        //
        // numbers.Add("twenty");


        // =========================================================
        // 4. Benchmark ArrayList vs List<int>
        // =========================================================

        const int count = 2_000_000;

        // Stopwatch is used to measure how long each collection
        // takes to insert 2,000,000 integers.
        Stopwatch stopwatch = new Stopwatch();


        // -------------------- ArrayList Benchmark --------------------

        stopwatch.Start();

        ArrayList arrayList = new ArrayList();

        for (int i = 0; i < count; i++)
        {
            // ArrayList stores integers as objects,
            // which causes boxing of the int value.
            arrayList.Add(i);
        }

        stopwatch.Stop();

        Console.WriteLine(
            "ArrayList insertion time: " +
            stopwatch.ElapsedMilliseconds +
            " ms");


        // -------------------- List<int> Benchmark --------------------

        stopwatch.Restart();

        List<int> integerList = new List<int>();

        for (int i = 0; i < count; i++)
        {
            // List<int> directly stores integers,
            // so no boxing is required.
            integerList.Add(i);
        }

        stopwatch.Stop();

        Console.WriteLine(
            "List<int> insertion time: " +
            stopwatch.ElapsedMilliseconds +
            " ms");
    }
}