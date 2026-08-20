// ### Lab 8 — Delegates as Callback Parameters (Mini Design Task)

// 1. Write `void ProcessBatch<T>(List<T> items, Action<T> onSuccess, Action<T, string> onFailure, Func<T, bool> validator)` that, for each item: validates it, then calls `onSuccess` if valid or `onFailure` with a reason string if not.
// 2. Call `ProcessBatch` against a `List<int>` where the validator rejects negative numbers, with lambda handlers that print success/failure messages differently.
// 3. Call it again against a `List<string>` where the validator rejects empty/whitespace strings, reusing the same generic method.

// **Deliverable:** Console app proving the same generic callback-driven method works correctly for two unrelated types and validation rules.

// ---

using System;
using System.Collections.Generic;

class Program
{
    // Generic method that processes any type of data.
    //
    // T can be int, string, or any other type.
    //
    // onSuccess -> called when validation succeeds.
    // onFailure -> called when validation fails.
    // validator -> decides whether the item is valid.
    static void ProcessBatch<T>(
        List<T> items,
        Action<T> onSuccess,
        Action<T, string> onFailure,
        Func<T, bool> validator)
    {
        // Process every item in the list.
        foreach (T item in items)
        {
            // Run the validation function for the current item.
            bool isValid = validator(item);

            if (isValid)
            {
                // Execute success callback when validation passes.
                onSuccess(item);
            }
            else
            {
                // Execute failure callback when validation fails.
                onFailure(item, "Validation failed.");
            }
        }
    }

    static void Main()
    {
        Console.WriteLine("===== LAB 8: Delegates as Callback Parameters =====\n");

        // =========================================================
        // PART 1: Integer list
        // =========================================================

        List<int> numbers = new List<int>
        {
            10,
            -5,
            20,
            -10,
            30
        };

        Console.WriteLine("===== Processing Integers =====");

        ProcessBatch(
            numbers,

            // Success callback.
            number =>
            {
                Console.WriteLine($"SUCCESS: {number} is valid.");
            },

            // Failure callback.
            (number, reason) =>
            {
                Console.WriteLine($"FAILURE: {number} -> {reason}");
            },

            // Validator: only non-negative numbers are valid.
            number => number >= 0
        );

        // =========================================================
        // PART 2: String list
        // =========================================================

        List<string> names = new List<string>
        {
            "Arti",
            "",
            "Rahul",
            "   ",
            "Ananya"
        };

        Console.WriteLine("\n===== Processing Strings =====");

        ProcessBatch(
            names,

            // Success callback for valid strings.
            name =>
            {
                Console.WriteLine($"SUCCESS: '{name}' is valid.");
            },

            // Failure callback for invalid strings.
            (name, reason) =>
            {
                Console.WriteLine($"FAILURE: Empty/invalid string -> {reason}");
            },

            // Validator rejects null, empty, and whitespace-only strings.
            name => !string.IsNullOrWhiteSpace(name)
        );

        /*
         * The important point of this lab is that ProcessBatch<T>
         * does not depend on a specific data type.
         *
         * The same method works with integers and strings because
         * validation and callbacks are supplied as delegates.
         *
         * This makes the method reusable and flexible.
         */
    }
}