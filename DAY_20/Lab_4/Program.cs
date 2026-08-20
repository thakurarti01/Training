// ### Lab 4 — `Func<>`, `Action<>`, `Predicate<T>`

// 1. Write a `Func<int, int, int>` for addition and one for multiplication (as lambdas, no custom delegate type).
// 2. Write an `Action<string>` that logs a message with a timestamp prefix.
// 3. Write a `Predicate<int>` (or `Func<int,bool>`) that checks if a number is prime; use it to filter a `List<int>` of 1–50 down to just the primes.
// 4. Write a generic method `void Repeat(int times, Action action)` that invokes `action` the given number of times; call it with a lambda that prints "Tick".

// **Deliverable:** Console app exercising all four generic delegate types with printed proof of correctness.

// ---

using System;
using System.Collections.Generic;

class Program
{
    // Repeat executes the supplied Action a specified number of times.
    static void Repeat(int times, Action action)
    {
        for (int i = 0; i < times; i++)
        {
            // Invoke the Action delegate.
            action();
        }
    }

    // Method used by Predicate<int> to check whether a number is prime.
    static bool IsPrime(int number)
    {
        // Numbers less than 2 are not prime.
        if (number < 2)
            return false;

        // Check divisibility from 2 up to the square root of the number.
        for (int i = 2; i * i <= number; i++)
        {
            if (number % i == 0)
                return false;
        }

        return true;
    }

    static void Main()
    {
        Console.WriteLine("===== LAB 4: Generic Delegates =====\n");

        // Func<int, int, int> accepts two integers
        // and returns an integer.
        Func<int, int, int> addition = (a, b) => a + b;

        // Lambda for multiplication.
        Func<int, int, int> multiplication = (a, b) => a * b;

        Console.WriteLine($"Addition: {addition(10, 5)}");
        Console.WriteLine($"Multiplication: {multiplication(10, 5)}");

        Console.WriteLine("\n===== Action =====");

        // Action<string> accepts a string but does not return a value.
        Action<string> logger = message =>
        {
            Console.WriteLine(
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}"
            );
        };

        logger("Application started.");

        Console.WriteLine("\n===== Predicate =====");

        // Predicate<int> accepts an integer and returns true/false.
        Predicate<int> primeChecker = IsPrime;

        List<int> numbers = new List<int>();

        // Create numbers from 1 to 50.
        for (int i = 1; i <= 50; i++)
        {
            numbers.Add(i);
        }

        // Find only the numbers for which the predicate returns true.
        List<int> primes = numbers.FindAll(primeChecker);

        Console.WriteLine("Prime numbers from 1 to 50:");
        Console.WriteLine(string.Join(", ", primes));

        Console.WriteLine("\n===== Repeat Method =====");

        // Pass a lambda as an Action to Repeat().
        Repeat(5, () =>
        {
            Console.WriteLine("Tick");
        });
    }
}