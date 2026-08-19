// ### Lab 5 — Build `MyList<T>`

// Implement a simplified generic dynamic array class:

// ```csharp
// public class MyList<T> : IEnumerable<T>
// ```

// Required members: `Add`, `RemoveAt`, indexer `this[int]` (get and set), `Count`, capacity doubling on growth, and a working `GetEnumerator()` (via `yield return`).

// 1. Test with `int` and with a custom reference type.
// 2. Prove `foreach` works on your class.
// 3. Prove collection-initializer syntax works: `new MyList<int> { 1, 2, 3 }`.
// 4. Deliberately trigger and catch an out-of-range access.

// **Deliverable:** `MyList<T>` in its own class plus a demonstration `Main`.

// ---

using System;
using System.Collections;
using System.Collections.Generic;

class MyList<T> : IEnumerable<T>
{
    // Internal array stores the actual elements.
    private T[] items;

    // Count tells us how many elements are currently stored.
    private int count;

    // Initial capacity of the internal array.
    private const int InitialCapacity = 4;


    // =========================================================
    // CONSTRUCTOR
    // =========================================================

    public MyList()
    {
        // Create the internal array with a small initial capacity.
        items = new T[InitialCapacity];

        // Initially, the list contains zero elements.
        count = 0;
    }


    // =========================================================
    // COUNT PROPERTY
    // =========================================================

    public int Count
    {
        // Return the number of elements currently stored.
        get
        {
            return count;
        }
    }


    // =========================================================
    // INDEXER
    // =========================================================

    public T this[int index]
    {
        get
        {
            // Make sure the requested index is valid.
            CheckIndex(index);

            // Return the element at that index.
            return items[index];
        }

        set
        {
            // Make sure the requested index is valid.
            CheckIndex(index);

            // Replace the element at the specified index.
            items[index] = value;
        }
    }


    // =========================================================
    // ADD
    // =========================================================

    public void Add(T item)
    {
        // If the internal array is full, increase its capacity
        // before adding the new element.
        if (count == items.Length)
        {
            Grow();
        }

        // Store the new item at the next available position.
        items[count] = item;

        // Increase Count because one element was added.
        count++;
    }


    // =========================================================
    // REMOVE AT
    // =========================================================

    public void RemoveAt(int index)
    {
        // Validate the index before accessing the array.
        CheckIndex(index);

        // Shift every element after the removed element
        // one position to the left.
        for (int i = index; i < count - 1; i++)
        {
            items[i] = items[i + 1];
        }

        // Remove the duplicate reference/value at the end.
        items[count - 1] = default!;

        // Decrease Count because one element was removed.
        count--;
    }


    // =========================================================
    // CAPACITY DOUBLING
    // =========================================================

    private void Grow()
    {
        // Double the current capacity whenever the array is full.
        int newCapacity = items.Length * 2;

        // Create a new larger array.
        T[] newItems = new T[newCapacity];

        // Copy the existing elements into the new array.
        Array.Copy(items, newItems, count);

        // Replace the old array with the larger array.
        items = newItems;
    }


    // =========================================================
    // INDEX VALIDATION
    // =========================================================

    private void CheckIndex(int index)
    {
        // Valid indexes are from 0 to Count - 1.
        if (index < 0 || index >= count)
        {
            throw new ArgumentOutOfRangeException(
                nameof(index),
                "Index is outside the valid range."
            );
        }
    }


    // =========================================================
    // GET ENUMERATOR
    // =========================================================

    public IEnumerator<T> GetEnumerator()
    {
        // yield return makes this class work with foreach.
        // Only the actual elements are returned, not unused
        // positions in the internal array.
        for (int i = 0; i < count; i++)
        {
            yield return items[i];
        }
    }


    // Required because IEnumerable<T> also inherits
    // from the non-generic IEnumerable interface.
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}


// =============================================================
// CUSTOM REFERENCE TYPE FOR TESTING
// =============================================================

class Student
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Student(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public override string ToString()
    {
        return $"{Id} - {Name}";
    }
}


// =============================================================
// MAIN PROGRAM
// =============================================================

class Program
{
    static void Main()
    {
        Console.WriteLine("==============================");
        Console.WriteLine("LAB 5 - BUILD MyList<T>");
        Console.WriteLine("==============================");


        // =====================================================
        // 1. TEST MyList WITH INT
        // =====================================================

        Console.WriteLine("\n1. MyList<int>");

        MyList<int> numbers = new MyList<int>();

        // Add integers to our custom list.
        numbers.Add(10);
        numbers.Add(20);
        numbers.Add(30);
        numbers.Add(40);
        numbers.Add(50);

        Console.WriteLine("Count: " + numbers.Count);

        // foreach works because MyList<T> implements IEnumerable<T>.
        Console.WriteLine("Elements:");

        foreach (int number in numbers)
        {
            Console.WriteLine(number);
        }


        // =====================================================
        // 2. INDEXER GET AND SET
        // =====================================================

        Console.WriteLine("\n2. Indexer");

        // Get an element using the indexer.
        Console.WriteLine("Element at index 1: " + numbers[1]);

        // Change an element using the indexer setter.
        numbers[1] = 25;

        Console.WriteLine("After changing index 1:");

        foreach (int number in numbers)
        {
            Console.WriteLine(number);
        }


        // =====================================================
        // 3. REMOVE AT
        // =====================================================

        Console.WriteLine("\n3. RemoveAt");

        // Remove the element at index 2.
        numbers.RemoveAt(2);

        Console.WriteLine("After removing index 2:");

        foreach (int number in numbers)
        {
            Console.WriteLine(number);
        }


        // =====================================================
        // 4. COLLECTION INITIALIZER
        // =====================================================

        Console.WriteLine("\n4. Collection Initializer");

        // Collection initializer automatically calls Add()
        // for each value: Add(1), Add(2), Add(3).
        MyList<int> initializedList = new MyList<int>
        {
            1,
            2,
            3
        };

        Console.WriteLine("Collection-initialized list:");

        foreach (int number in initializedList)
        {
            Console.WriteLine(number);
        }


        // =====================================================
        // 5. TEST WITH CUSTOM REFERENCE TYPE
        // =====================================================

        Console.WriteLine("\n5. MyList<Student>");

        // Generics allow our MyList<T> to store custom objects too.
        MyList<Student> students = new MyList<Student>();

        students.Add(new Student(101, "Arti"));
        students.Add(new Student(102, "Rahul"));
        students.Add(new Student(103, "Priya"));

        // foreach works with custom reference types as well.
        foreach (Student student in students)
        {
            Console.WriteLine(student);
        }


        // =====================================================
        // 6. OUT-OF-RANGE ACCESS
        // =====================================================

        Console.WriteLine("\n6. Out-of-Range Test");

        try
        {
            // Index 10 does not exist, so our indexer will
            // deliberately throw an exception.
            Console.WriteLine(numbers[10]);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            // Catch the expected exception so the program
            // continues without an unhandled exception.
            Console.WriteLine("Exception caught: " + ex.Message);
        }


        Console.WriteLine("\nProgram completed successfully.");
    }
}