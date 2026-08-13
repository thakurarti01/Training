using System;
using System.Collections.Generic;

// Generic class.
// T means the class can work with any data type.
public class Box<T>
{
    // Private variable of generic type T.
    // Its actual type depends on what we pass while creating the Box.
    private T _value;

    // Constructor that accepts a value of type T.
    public Box(T value)
    {
        _value = value;
    }

    // Returns the value stored inside the Box.
    public T GetValue()
    {
        return _value;
    }

    // Replaces the old value with a new value.
    public void Replace(T newValue)
    {
        _value = newValue;
    }

    // Generic method that creates and returns a default object of type T.
    // The new() constraint means T must have a public parameterless constructor.
    public static T CreateDefault<T>() where T : new()
    {
        return new T();
    }
}


// Generic Pair class.
// It accepts two different types:
// TFirst  -> type of First
// TSecond -> type of Second
public class Pair<TFirst, TSecond>
{
    // Property to store the first value.
    public TFirst First { get; set; }

    // Property to store the second value.
    public TSecond Second { get; set; }

    // Constructor to initialize both values.
    public Pair(TFirst first, TSecond second)
    {
        First = first;
        Second = second;
    }

    // Override ToString() so that the Pair
    // can be printed in the required format.
    public override string ToString()
    {
        return $"({First}, {Second})";
    }
}


// Generic SortedBox class.
// IComparable<T> allows objects of type T to be compared and sorted.
public class SortedBox<T> where T : IComparable<T>
{
    // Internal list used to store all the items.
    private List<T> _items = new List<T>();

    // Adds a new item to the list.
    // After adding, the list is sorted automatically.
    public void Add(T item)
    {
        _items.Add(item);
        _items.Sort();
    }

    // Property that allows us to access the sorted items.
    public List<T> Items
    {
        get { return _items; }
    }
}


class Program
{
    static void Main()
    {
        // 1. Creating a Box<int>

        // Here T becomes int.
        // So Box can store an integer value.
        Box<int> intBox = new Box<int>(42);

        Console.WriteLine($"Box<int>: {intBox.GetValue()}");


        // 2. Creating a Box<string>

        // Here T becomes string.
        // The same generic Box class can now store a string.
        Box<string> stringBox = new Box<string>("Hello");

        Console.WriteLine($"Box<string>: {stringBox.GetValue()}");


        // 3. Creating a Box<DateTime>

        // Here T becomes DateTime.
        // This proves that our generic class works
        // with another completely different data type.
        Box<DateTime> dateBox =
            new Box<DateTime>(new DateTime(2026, 8, 12));

        Console.WriteLine(
            $"Box<DateTime>: {dateBox.GetValue():yyyy-MM-dd}"
        );


        // 4. Calling the generic CreateDefault<T>() method

        // The method creates a default DateTime object.
        // DateTime has a parameterless constructor,
        // so it satisfies the new() constraint.
        DateTime defaultDate =
            Box<int>.CreateDefault<DateTime>();

        Console.WriteLine($"Default DateTime: {defaultDate}");


        // 5. Creating a Pair<string, int>

        // TFirst becomes string.
        // TSecond becomes int.
        //
        // So this Pair stores:
        // First  = "Age"
        // Second = 30
        Pair<string, int> pair =
            new Pair<string, int>("Age", 30);

        // ToString() is automatically called when
        // we print the pair object.
        Console.WriteLine($"Pair: {pair}");


        // 6. Creating a SortedBox<int>

        // T becomes int.
        // int implements IComparable<int>,
        // so integers can be compared and sorted.
        SortedBox<int> sortedBox =
            new SortedBox<int>();


        // Add numbers in an unsorted order.
        sortedBox.Add(5);
        sortedBox.Add(1);
        sortedBox.Add(3);


        // Display the sorted values.
        Console.Write(
            "SortedBox after adding 5, 1, 3: "
        );

        foreach (int item in sortedBox.Items)
        {
            Console.Write(item + " ");
        }
    }
}