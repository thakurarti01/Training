// ### Lab 7 — The Loop-Variable Capture Pitfall

// 1. Write a `for` loop that creates 3 `Action` delegates, each intended to print its loop index, WITHOUT copying the index into a local variable first. Store them in a `List<Action>`, invoke all three after the loop, and observe (and explain in a comment) the actual output.
// 2. Fix it by copying the loop variable into a local variable inside the loop body before capturing it in the lambda. Show the corrected output.
// 3. Do the same experiment with a `foreach` loop instead of a `for` loop (no manual copy) and explain in a comment why the output differs from the uncorrected `for` loop version.

// **Deliverable:** Console app showing the buggy output, the fixed output, and the `foreach` comparison, each clearly labeled.

// ---

using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("===== LAB 7: Loop Variable Capture =====\n");

        // =========================================================
        // PART 1: for loop WITHOUT copying the loop variable
        // =========================================================

        List<Action> buggyActions = new List<Action>();

        for (int i = 0; i < 3; i++)
        {
            // The lambda captures the loop variable 'i'.
            // It does NOT immediately store its current value.
            buggyActions.Add(() =>
            {
                Console.WriteLine($"Buggy for loop: {i}");
            });
        }

        Console.WriteLine("Buggy for-loop output:");

        // All delegates are invoked after the loop has finished.
        foreach (Action action in buggyActions)
        {
            action();
        }

        /*
         * Actual output:
         * 3
         * 3
         * 3
         *
         * Why?
         * The lambda captures the variable itself, not a snapshot of
         * its value. After the loop finishes, i is 3.
         *
         * Therefore, every lambda reads the same final value: 3.
         */

        // =========================================================
        // PART 2: FIX using a local copy
        // =========================================================

        List<Action> fixedActions = new List<Action>();

        for (int i = 0; i < 3; i++)
        {
            // Create a new local variable for each iteration.
            // Each lambda now captures a different variable.
            int capturedIndex = i;

            fixedActions.Add(() =>
            {
                Console.WriteLine($"Fixed for loop: {capturedIndex}");
            });
        }

        Console.WriteLine("\nFixed for-loop output:");

        foreach (Action action in fixedActions)
        {
            action();
        }

        /*
         * Correct output:
         * 0
         * 1
         * 2
         *
         * Each lambda has its own capturedIndex variable.
         */

        // =========================================================
        // PART 3: foreach loop
        // =========================================================

        List<Action> foreachActions = new List<Action>();

        foreach (int number in new[] { 0, 1, 2 })
        {
            // In modern C#, the foreach iteration variable is
            // treated separately for each iteration.
            foreachActions.Add(() =>
            {
                Console.WriteLine($"Foreach loop: {number}");
            });
        }

        Console.WriteLine("\nForeach output:");

        foreach (Action action in foreachActions)
        {
            action();
        }

        /*
         * Output:
         * 0
         * 1
         * 2
         *
         * The foreach iteration variable is captured separately
         * for each iteration in modern C#.
         *
         * This is why foreach behaves differently from the
         * uncorrected for-loop example above.
         */
    }
}