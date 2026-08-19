// ### Lab 4 — The Collection API

// 1. Write a generic method `T[] Snapshot<T>(ICollection<T> source)` that uses `CopyTo` (not a `foreach` loop) to copy the collection into a correctly-sized array.
// 2. Write `bool TryAddAll<T>(ICollection<T> target, IEnumerable<T> items)` that checks `target.IsReadOnly` first, returns `false` without modifying anything if read-only, otherwise adds all items and returns `true`.
// 3. Demonstrate both methods working identically against a `List<T>`, a `HashSet<T>`, and a `LinkedList<T>` — proving the methods don't care about the concrete implementation.
// 4. Attempt `TryAddAll` against `array.AsReadOnly()` or a similar read-only wrapper and show it correctly refuses.

// **Deliverable:** Console app proving the "program to the interface" principle with at least 3 different concrete collection types.

// ---

using System;
using System.Collections.Generic;

class Program
{
    // =========================================================
    // 1. SNAPSHOT<T>
    // =========================================================

    static T[] Snapshot<T>(ICollection<T> source)
    {
        // Create an array with exactly the same size as the collection.
        // This ensures there is enough space for every element.
        T[] result = new T[source.Count];

        // CopyTo copies the collection directly into the array.
        // The question specifically requires CopyTo instead of foreach.
        source.CopyTo(result, 0);

        // Return the copied array.
        return result;
    }


    // =========================================================
    // 2. TRYADDALL<T>
    // =========================================================

    static bool TryAddAll<T>(
        ICollection<T> target,
        IEnumerable<T> items)
    {
        // Check IsReadOnly before modifying the collection.
        // If it is read-only, we must return false without
        // adding anything.
        if (target.IsReadOnly)
        {
            return false;
        }

        // Add every item to the target collection.
        foreach (T item in items)
        {
            target.Add(item);
        }

        // All items were added successfully.
        return true;
    }


    // =========================================================
    // Helper method to display a collection
    // =========================================================

    static void DisplayCollection<T>(
        string name,
        ICollection<T> collection)
    {
        Console.WriteLine(
            $"{name}: {string.Join(", ", collection)}"
        );
    }


    // =========================================================
    // MAIN METHOD
    // =========================================================

    static void Main()
    {
        Console.WriteLine("==============================");
        Console.WriteLine("LAB 4 - THE COLLECTION API");
        Console.WriteLine("==============================");


        // =====================================================
        // 3. TEST WITH LIST<T>
        // =====================================================

        Console.WriteLine("\n1. Testing with List<int>");

        List<int> list = new List<int> { 1, 2, 3 };

        // Snapshot works because List<int> implements ICollection<int>.
        int[] listSnapshot = Snapshot(list);

        Console.WriteLine(
            "Snapshot: " + string.Join(", ", listSnapshot)
        );

        // Add new values using the interface-based method.
        TryAddAll(list, new[] { 4, 5 });

        DisplayCollection("List after TryAddAll", list);


        // =====================================================
        // 4. TEST WITH HASHSET<T>
        // =====================================================

        Console.WriteLine("\n2. Testing with HashSet<int>");

        HashSet<int> hashSet = new HashSet<int> { 10, 20, 30 };

        // The same Snapshot method works with HashSet.
        int[] hashSetSnapshot = Snapshot(hashSet);

        Console.WriteLine(
            "Snapshot: " + string.Join(", ", hashSetSnapshot)
        );

        // The same TryAddAll method works with HashSet.
        TryAddAll(hashSet, new[] { 40, 50 });

        DisplayCollection("HashSet after TryAddAll", hashSet);


        // =====================================================
        // 5. TEST WITH LINKEDLIST<T>
        // =====================================================

        Console.WriteLine("\n3. Testing with LinkedList<int>");

        LinkedList<int> linkedList =
            new LinkedList<int>(new[] { 100, 200, 300 });

        // Snapshot also works with LinkedList because it implements
        // ICollection<int>.
        int[] linkedListSnapshot = Snapshot(linkedList);

        Console.WriteLine(
            "Snapshot: " + string.Join(", ", linkedListSnapshot)
        );

        // Add values using the same generic method.
        TryAddAll(linkedList, new[] { 400, 500 });

        DisplayCollection(
            "LinkedList after TryAddAll",
            linkedList
        );


        // =====================================================
        // 6. READ-ONLY COLLECTION
        // =====================================================

        Console.WriteLine("\n4. Testing Read-Only Collection");

        // Create a normal array first.
        int[] array = { 1, 2, 3 };

        // Array.AsReadOnly creates a read-only wrapper.
        IReadOnlyCollection<int> readOnlyView =
            Array.AsReadOnly(array);

        // Convert it to ICollection<int> so that it can be passed
        // to TryAddAll.
        ICollection<int> readOnlyCollection =
            (ICollection<int>)readOnlyView;

        // TryAddAll checks IsReadOnly and refuses to modify it.
        bool success = TryAddAll(
            readOnlyCollection,
            new[] { 4, 5 }
        );

        Console.WriteLine(
            "TryAddAll successful: " + success
        );

        Console.WriteLine(
            "Read-only collection: " +
            string.Join(", ", readOnlyCollection)
        );
    }
}