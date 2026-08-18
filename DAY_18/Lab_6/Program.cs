// ### Lab 6 — Generics: Class, Method, and Constraints

// 1. Write a generic method:
//    ```csharp
//    public static void Swap<T>(ref T a, ref T b)
//    ```
// 2. Write a generic class `Pair<TFirst, TSecond>` with `First`, `Second` properties, a constructor, and an overridden `ToString()`.
// 3. Write a generic class `MinMaxTracker<T> where T : IComparable<T>` that:
//    - Has an `Add(T value)` method
//    - Tracks and exposes `Min` and `Max` properties in O(1) per add (don't rescan the whole collection each time)
// 4. Write a generic method `bool AllMatch<T>(IEnumerable<T> items, Func<T, bool> predicate)` that returns true only if every item satisfies the predicate.
// 5. Test all four with at least two different type arguments each (e.g., `int` and a custom `Product` class implementing `IComparable<Product>` by price).

// **Deliverable:** Console app exercising each generic construct with printed proof of correctness.

// ---


using System;
using System.Collections.Generic;


// =============================================================
// 1. GENERIC METHODS
// =============================================================

class GenericMethods
{
    // T allows the same Swap method to work with different types.
    public static void Swap<T>(ref T a, ref T b)
    {
        // Store the first value temporarily before swapping.
        T temp = a;

        a = b;
        b = temp;
    }


    // Checks whether every item satisfies the given condition.
    public static bool AllMatch<T>(
        IEnumerable<T> items,
        Func<T, bool> predicate)
    {
        // Check each item one by one.
        foreach (T item in items)
        {
            // Return false immediately if one item fails.
            if (!predicate(item))
            {
                return false;
            }
        }

        // If no item failed, all items matched.
        return true;
    }
}


// =============================================================
// 2. GENERIC PAIR CLASS
// =============================================================

// TFirst and TSecond allow the pair to store two different types.
class Pair<TFirst, TSecond>
{
    public TFirst First { get; set; }

    public TSecond Second { get; set; }


    // Constructor initializes both values.
    public Pair(TFirst first, TSecond second)
    {
        First = first;
        Second = second;
    }


    // Provides a readable representation of the pair.
    public override string ToString()
    {
        return $"First: {First}, Second: {Second}";
    }
}


// =============================================================
// 3. GENERIC MIN-MAX TRACKER
// =============================================================

// IComparable<T> is required because we need to compare
// values to determine the minimum and maximum.
class MinMaxTracker<T> where T : IComparable<T>
{
    public T Min { get; private set; }

    public T Max { get; private set; }


    // Keeps track of whether the first value has been added.
    private bool hasValue = false;


    public void Add(T value)
    {
        // For the first value, both Min and Max are the same.
        if (!hasValue)
        {
            Min = value;
            Max = value;

            hasValue = true;

            return;
        }


        // Compare the new value with the current minimum.
        if (value.CompareTo(Min) < 0)
        {
            Min = value;
        }


        // Compare the new value with the current maximum.
        if (value.CompareTo(Max) > 0)
        {
            Max = value;
        }
    }
}


// =============================================================
// 4. PRODUCT CLASS
// =============================================================

// Product implements IComparable so products can be compared.
class Product : IComparable<Product>
{
    public string Name { get; set; }

    public double Price { get; set; }


    public Product(string name, double price)
    {
        Name = name;
        Price = price;
    }


    // Define product comparison based on price.
    public int CompareTo(Product other)
    {
        return Price.CompareTo(other.Price);
    }


    public override string ToString()
    {
        return $"{Name} - ₹{Price}";
    }
}


// =============================================================
// MAIN PROGRAM
// =============================================================

class Program
{
    static void Main()
    {
        // =====================================================
        // 1. TEST GENERIC SWAP
        // =====================================================

        Console.WriteLine("===== 1. GENERIC SWAP =====");


        // Swap integers using the same generic method.
        int number1 = 10;
        int number2 = 20;

        Console.WriteLine(
            $"Before swap: {number1}, {number2}");

        GenericMethods.Swap(
            ref number1,
            ref number2);

        Console.WriteLine(
            $"After swap: {number1}, {number2}");


        // The same method also works with strings.
        string word1 = "Hello";
        string word2 = "World";

        Console.WriteLine(
            $"\nBefore swap: {word1}, {word2}");

        GenericMethods.Swap(
            ref word1,
            ref word2);

        Console.WriteLine(
            $"After swap: {word1}, {word2}");


        // =====================================================
        // 2. TEST GENERIC PAIR
        // =====================================================

        Console.WriteLine("\n===== 2. GENERIC PAIR =====");


        // First Pair: int + string
        Pair<int, string> pair1 =
            new Pair<int, string>(1, "Arti");

        Console.WriteLine(pair1);


        // Second Pair: string + double
        // This proves that the same class works with
        // different type combinations.
        Pair<string, double> pair2 =
            new Pair<string, double>("Price", 99.50);

        Console.WriteLine(pair2);


        // =====================================================
        // 3. TEST MIN-MAX TRACKER WITH INT
        // =====================================================

        Console.WriteLine("\n===== 3. MIN-MAX WITH INT =====");


        MinMaxTracker<int> numberTracker =
            new MinMaxTracker<int>();


        // Add numbers one at a time.
        numberTracker.Add(10);
        numberTracker.Add(5);
        numberTracker.Add(25);
        numberTracker.Add(2);
        numberTracker.Add(15);


        Console.WriteLine(
            "Minimum: " + numberTracker.Min);

        Console.WriteLine(
            "Maximum: " + numberTracker.Max);


        // =====================================================
        // 4. TEST MIN-MAX TRACKER WITH PRODUCT
        // =====================================================

        Console.WriteLine("\n===== 4. MIN-MAX WITH PRODUCT =====");


        // Product implements IComparable<Product>,
        // so it can be used with MinMaxTracker.
        MinMaxTracker<Product> productTracker =
            new MinMaxTracker<Product>();


        productTracker.Add(
            new Product("Laptop", 50000));

        productTracker.Add(
            new Product("Phone", 30000));

        productTracker.Add(
            new Product("Tablet", 20000));


        Console.WriteLine(
            "Cheapest: " + productTracker.Min);

        Console.WriteLine(
            "Most Expensive: " + productTracker.Max);


        // =====================================================
        // 5. TEST ALLMATCH WITH INT
        // =====================================================

        Console.WriteLine("\n===== 5. ALLMATCH WITH INT =====");


        List<int> numbers =
            new List<int>
            {
                2, 4, 6, 8
            };


        // Lambda checks whether every number is even.
        bool allEven =
            GenericMethods.AllMatch(
                numbers,
                number => number % 2 == 0);


        Console.WriteLine(
            "Are all numbers even? " + allEven);


        // =====================================================
        // 6. TEST ALLMATCH WITH PRODUCT
        // =====================================================

        Console.WriteLine(
            "\n===== 6. ALLMATCH WITH PRODUCT =====");


        List<Product> products =
            new List<Product>
            {
                new Product("Pen", 20),
                new Product("Book", 50),
                new Product("Bag", 80)
            };


        // Check whether every product costs less than ₹100.
        bool allAffordable =
            GenericMethods.AllMatch(
                products,
                product => product.Price < 100);


        Console.WriteLine(
            "Are all products below ₹100? " +
            allAffordable);
    }
}