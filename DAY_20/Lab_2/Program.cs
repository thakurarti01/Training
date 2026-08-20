// ### Lab 2 — Declaring and Using Delegates

// 1. Declare a custom delegate `public delegate double Discount(double price);`.
// 2. Write three matching methods: `NoDiscount`, `TenPercentOff`, `HalfOff`.
// 3. Write a method `ApplyDiscount(double price, Discount discount)` that invokes the passed delegate.
// 4. Call `ApplyDiscount` three times, once per discount method, printing the result each time.
// 5. Store all three methods in a `List<Discount>` and iterate the list, invoking each one against the same price, printing every result.

// **Deliverable:** Console app showing delegate declaration, instantiation with different methods, and invocation via both direct calls and a list of delegates.

// ---

using System;
using System.Collections.Generic;

// Custom delegate that can refer to any method
// accepting a double and returning a double.
public delegate double Discount(double price);

class Program
{
    // Method that applies no discount.
    static double NoDiscount(double price)
    {
        return price;
    }

    // Method that applies a 10% discount.
    static double TenPercentOff(double price)
    {
        return price * 0.90;
    }

    // Method that applies a 50% discount.
    static double HalfOff(double price)
    {
        return price * 0.50;
    }

    // This method receives a delegate as a parameter.
    // It allows us to decide which discount should be applied.
    static double ApplyDiscount(double price, Discount discount)
    {
        return discount(price);
    }

    static void Main()
    {
        Console.WriteLine("===== LAB 2: Delegates =====\n");

        double price = 1000;

        // Passing different methods as delegates.
        double result1 = ApplyDiscount(price, NoDiscount);
        double result2 = ApplyDiscount(price, TenPercentOff);
        double result3 = ApplyDiscount(price, HalfOff);

        Console.WriteLine($"Original Price: {price}");
        Console.WriteLine($"No Discount: {result1}");
        Console.WriteLine($"10% Discount: {result2}");
        Console.WriteLine($"50% Discount: {result3}");

        Console.WriteLine("\n===== List of Delegates =====");

        // A list can store multiple methods having the same signature.
        List<Discount> discounts = new List<Discount>
        {
            NoDiscount,
            TenPercentOff,
            HalfOff
        };

        // Invoke every delegate in the list using the same price.
        foreach (Discount discount in discounts)
        {
            double result = discount(price);
            Console.WriteLine($"Discounted Price: {result}");
        }
    }
}