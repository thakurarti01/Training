// ### Lab 1 — Tuples

// 1. Write `(double Average, double Min, double Max) GetStats(IEnumerable<double> values)` returning aggregate stats as a named `ValueTuple`.
// 2. Call it and use deconstruction to extract `avg`, `min`, `max` into separate variables.
// 3. Write `(bool Success, string? ErrorMessage) TryParseAge(string input)` that returns `(true, null)` on success or `(false, "reason")` on failure — a common pattern replacing exceptions for expected failure cases.
// 4. Build a `Dictionary<(int Row, int Col), string> board` representing a tic-tac-toe board. Populate a few cells and print the board by iterating `(row, col)` from `(0,0)` to `(2,2)`, looking up each cell (default to `"-"` if empty).

// **Deliverable:** Console app demonstrating all four, with printed output proving correctness.

// ---

using System;
using System.Collections.Generic;

class Program
{
    // Returns Average, Minimum and Maximum as a named ValueTuple.
    // Tuple lets us return multiple related values from one method.
    static (double Average, double Min, double Max) GetStats(
        IEnumerable<double> values)
    {
        // Convert the collection to a list so we can easily calculate
        // count, minimum and maximum values.
        List<double> numbers = new List<double>(values);

        // We need at least one value to calculate statistics.
        if (numbers.Count == 0)
        {
            throw new ArgumentException("Collection cannot be empty.");
        }

        // Calculate the three required statistics.
        double average = 0;

        foreach (double number in numbers)
        {
            average += number;
        }

        average /= numbers.Count;

        double min = double.MaxValue;
        double max = double.MinValue;

        foreach (double number in numbers)
        {
            if (number < min)
            {
                min = number;
            }

            if (number > max)
            {
                max = number;
            }
        }

        // Return all three values using the named tuple members.
        return (average, min, max);
    }


    // Tries to convert the input into a valid age.
    // Instead of throwing an exception for expected invalid input,
    // we return Success and an optional ErrorMessage.
    static (bool Success, string? ErrorMessage) TryParseAge(string input)
    {
        // TryParse safely attempts the conversion without throwing
        // an exception when the input is not a number.
        if (!int.TryParse(input, out int age))
        {
            return (false, "Age must be a valid number.");
        }

        // Age should be within a reasonable valid range.
        if (age < 0 || age > 120)
        {
            return (false, "Age must be between 0 and 120.");
        }

        // null means there is no error.
        return (true, null);
    }


    static void Main()
    {
        Console.WriteLine("===== LAB 1 - TUPLES =====");


        // ---------------------------------------------------------
        // 1 & 2. Get statistics and use tuple deconstruction
        // ---------------------------------------------------------

        Console.WriteLine("\n1. Tuple Statistics");

        double[] values = { 10, 20, 30, 40, 50 };

        // The method returns three values inside one named tuple.
        var stats = GetStats(values);

        // Deconstruction extracts tuple values into separate variables.
        double avg = stats.Average;
        double min = stats.Min;
        double max = stats.Max;

        Console.WriteLine($"Average = {avg}");
        Console.WriteLine($"Minimum = {min}");
        Console.WriteLine($"Maximum = {max}");


        // ---------------------------------------------------------
        // 3. TryParseAge
        // ---------------------------------------------------------

        Console.WriteLine("\n2. TryParseAge");

        // Test with a valid age.
        var result1 = TryParseAge("21");

        if (result1.Success)
        {
            Console.WriteLine("Age 21 is valid.");
        }
        else
        {
            Console.WriteLine($"Error: {result1.ErrorMessage}");
        }

        // Test with an invalid age.
        var result2 = TryParseAge("abc");

        if (result2.Success)
        {
            Console.WriteLine("Age is valid.");
        }
        else
        {
            Console.WriteLine($"Error: {result2.ErrorMessage}");
        }


        // ---------------------------------------------------------
        // 4. Tic-Tac-Toe board using a Dictionary with tuple keys
        // ---------------------------------------------------------

        Console.WriteLine("\n3. Tic-Tac-Toe Board");

        // Row and Col together identify a cell on the board.
        // Tuple is used as the dictionary key.
        Dictionary<(int Row, int Col), string> board =
            new Dictionary<(int Row, int Col), string>();

        // Populate a few cells.
        board[(0, 0)] = "X";
        board[(1, 1)] = "O";
        board[(2, 2)] = "X";
        board[(0, 2)] = "O";


        // Print all 9 cells by visiting rows and columns from 0 to 2.
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                // TryGetValue checks whether the cell contains a value.
                // If it is empty, we display "-".
                if (board.TryGetValue((row, col), out string? value))
                {
                    Console.Write(value + " ");
                }
                else
                {
                    Console.Write("- ");
                }
            }

            Console.WriteLine();
        }
    }
}