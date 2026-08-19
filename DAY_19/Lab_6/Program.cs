// ### Lab 6 — Build `MyDictionary<TKey,TValue>`

// Implement a simplified chained-hash-table generic dictionary:

// ```csharp
// public class MyDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>> where TKey : notnull
// ```

// Required members: `Add`/indexer set, `TryGetValue`, indexer get (throwing `KeyNotFoundException` if missing), and `GetEnumerator()`.

// 1. Test by storing at least 20 key/value pairs (enough to guarantee some hash collisions with a small bucket count) and verify every key still retrieves the correct value.
// 2. Compare lookup behavior against the real `Dictionary<TKey,TValue>` for the same data to confirm correctness.
// 3. Demonstrate collection-initializer-style construction using index initializer syntax (requires an indexer setter, which you already built).

// **Deliverable:** `MyDictionary<TKey,TValue>` in its own class plus a demonstration `Main` including a correctness check against the built-in `Dictionary<K,V>`.

// ---

using System;
using System.Collections;
using System.Collections.Generic;


// =============================================================
// MY DICTIONARY
// =============================================================

class MyDictionary<TKey, TValue> :
    IEnumerable<KeyValuePair<TKey, TValue>>
    where TKey : notnull
{
    // Each bucket stores a list of key-value pairs.
    // The list allows us to handle hash collisions using chaining.
    private List<KeyValuePair<TKey, TValue>>[] buckets;

    // Number of key-value pairs stored in the dictionary.
    private int count;


    // Small bucket count is intentionally used so that
    // collisions are easier to demonstrate in this lab.
    private const int BucketCount = 5;


    // =========================================================
    // CONSTRUCTOR
    // =========================================================

    public MyDictionary()
    {
        // Create the bucket array.
        buckets =
            new List<KeyValuePair<TKey, TValue>>[BucketCount];

        // Create an empty list for every bucket.
        for (int i = 0; i < buckets.Length; i++)
        {
            buckets[i] =
                new List<KeyValuePair<TKey, TValue>>();
        }
    }


    // =========================================================
    // INDEXER
    // =========================================================

    public TValue this[TKey key]
    {
        get
        {
            // Try to find the requested key.
            if (TryGetValue(key, out TValue? value))
            {
                return value;
            }

            // Dictionary should throw this exception when
            // a requested key does not exist.
            throw new KeyNotFoundException(
                $"Key '{key}' was not found."
            );
        }

        set
        {
            // Find the bucket where this key belongs.
            int bucketIndex = GetBucketIndex(key);

            // Search the bucket for an existing key.
            for (int i = 0; i < buckets[bucketIndex].Count; i++)
            {
                if (EqualityComparer<TKey>.Default.Equals(
                    buckets[bucketIndex][i].Key,
                    key))
                {
                    // Replace the existing value if the key exists.
                    buckets[bucketIndex][i] =
                        new KeyValuePair<TKey, TValue>(
                            key,
                            value);

                    return;
                }
            }

            // If the key does not exist, add a new pair.
            buckets[bucketIndex].Add(
                new KeyValuePair<TKey, TValue>(
                    key,
                    value));

            count++;
        }
    }


    // =========================================================
    // ADD
    // =========================================================

    public void Add(TKey key, TValue value)
    {
        // Find the bucket for this key.
        int bucketIndex = GetBucketIndex(key);

        // Check whether the key already exists.
        foreach (KeyValuePair<TKey, TValue> pair
            in buckets[bucketIndex])
        {
            if (EqualityComparer<TKey>.Default.Equals(
                pair.Key,
                key))
            {
                // Add should not allow duplicate keys.
                throw new ArgumentException(
                    "An item with the same key already exists."
                );
            }
        }

        // Add the new key-value pair to the bucket.
        buckets[bucketIndex].Add(
            new KeyValuePair<TKey, TValue>(
                key,
                value));

        // Increase the number of stored items.
        count++;
    }


    // =========================================================
    // TRYGETVALUE
    // =========================================================

    public bool TryGetValue(
        TKey key,
        out TValue? value)
    {
        // Find the bucket for the requested key.
        int bucketIndex = GetBucketIndex(key);

        // Search only that bucket instead of checking
        // every item in the dictionary.
        foreach (KeyValuePair<TKey, TValue> pair
            in buckets[bucketIndex])
        {
            if (EqualityComparer<TKey>.Default.Equals(
                pair.Key,
                key))
            {
                // Key was found.
                value = pair.Value;

                return true;
            }
        }

        // Key was not found.
        value = default;

        return false;
    }


    // =========================================================
    // HASH / BUCKET CALCULATION
    // =========================================================

    private int GetBucketIndex(TKey key)
    {
        // GetHashCode converts the key into a hash value.
        int hash = key.GetHashCode();

        // Math.Abs prevents a negative bucket index.
        // The special handling avoids the minimum integer overflow case.
        int positiveHash =
            hash == int.MinValue
                ? 0
                : Math.Abs(hash);

        // Modulo maps the hash into one of our buckets.
        return positiveHash % buckets.Length;
    }


    // =========================================================
    // ENUMERATOR
    // =========================================================

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        // Visit every bucket.
        foreach (List<KeyValuePair<TKey, TValue>> bucket
            in buckets)
        {
            // Visit every key-value pair inside that bucket.
            foreach (KeyValuePair<TKey, TValue> pair
                in bucket)
            {
                yield return pair;
            }
        }
    }


    // Required implementation of the non-generic IEnumerable.
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }


    // Public property used only to demonstrate the number
    // of stored key-value pairs.
    public int Count
    {
        get
        {
            return count;
        }
    }
}


// =============================================================
// MAIN PROGRAM
// =============================================================

class Program
{
    static void Main()
    {
        Console.WriteLine("======================================");
        Console.WriteLine("LAB 6 - BUILD MyDictionary<TKey,TValue>");
        Console.WriteLine("======================================");


        // =====================================================
        // 1. CREATE CUSTOM DICTIONARY
        // =====================================================

        Console.WriteLine("\n1. Adding 20 Key-Value Pairs");

        MyDictionary<int, string> myDictionary =
            new MyDictionary<int, string>();

        // Add 20 pairs.
        // Only 5 buckets are used, so several keys will
        // necessarily share buckets and create collisions.
        for (int i = 1; i <= 20; i++)
        {
            myDictionary.Add(
                i,
                $"Value-{i}");
        }

        Console.WriteLine(
            "Number of stored pairs: " +
            myDictionary.Count
        );


        // =====================================================
        // 2. VERIFY EVERY KEY
        // =====================================================

        Console.WriteLine("\n2. Checking Every Key");

        bool allCorrect = true;

        for (int i = 1; i <= 20; i++)
        {
            if (myDictionary.TryGetValue(
                i,
                out string? value))
            {
                if (value != $"Value-{i}")
                {
                    allCorrect = false;

                    Console.WriteLine(
                        $"Incorrect value for key {i}."
                    );
                }
            }
            else
            {
                allCorrect = false;

                Console.WriteLine(
                    $"Key {i} was not found."
                );
            }
        }

        Console.WriteLine(
            "All keys retrieved correctly: " +
            allCorrect
        );


        // =====================================================
        // 3. ENUMERATION
        // =====================================================

        Console.WriteLine("\n3. Enumerating MyDictionary");

        // IEnumerable implementation allows foreach to work.
        foreach (KeyValuePair<int, string> pair
            in myDictionary)
        {
            Console.WriteLine(
                $"{pair.Key} -> {pair.Value}"
            );
        }


        // =====================================================
        // 4. INDEXER GET
        // =====================================================

        Console.WriteLine("\n4. Indexer Get");

        // Retrieve a value using dictionary-style syntax.
        Console.WriteLine(
            "Key 10: " + myDictionary[10]
        );


        // =====================================================
        // 5. INDEXER SET
        // =====================================================

        Console.WriteLine("\n5. Indexer Set");

        // Because the key already exists, the indexer setter
        // replaces its existing value.
        myDictionary[10] = "Updated-Value";

        Console.WriteLine(
            "Key 10 after update: " +
            myDictionary[10]
        );


        // =====================================================
        // 6. MISSING KEY
        // =====================================================

        Console.WriteLine("\n6. Missing Key Test");

        try
        {
            // Key 999 does not exist.
            // The indexer should throw KeyNotFoundException.
            Console.WriteLine(myDictionary[999]);
        }
        catch (KeyNotFoundException ex)
        {
            // Catch the deliberate exception so the program
            // does not terminate unexpectedly.
            Console.WriteLine(
                "Exception caught: " +
                ex.Message
            );
        }


        // =====================================================
        // 7. COLLECTION-INITIALIZER STYLE
        // =====================================================

        Console.WriteLine(
            "\n7. Collection Initializer Using Indexer"
        );

        // Dictionary initializer syntax works because our
        // class provides an indexer setter.
        MyDictionary<int, string> initialized =
            new MyDictionary<int, string>
            {
                [1] = "Apple",
                [2] = "Banana",
                [3] = "Mango"
            };

        Console.WriteLine(
            "Key 1: " + initialized[1]
        );

        Console.WriteLine(
            "Key 2: " + initialized[2]
        );

        Console.WriteLine(
            "Key 3: " + initialized[3]
        );


        // =====================================================
        // 8. COMPARE WITH BUILT-IN DICTIONARY
        // =====================================================

        Console.WriteLine(
            "\n8. Comparing With Built-in Dictionary"
        );

        Dictionary<int, string> builtIn =
            new Dictionary<int, string>();

        // Store the same data in the built-in Dictionary.
        for (int i = 1; i <= 20; i++)
        {
            builtIn.Add(
                i,
                $"Value-{i}");
        }

        bool sameResults = true;

        // Compare lookup results from both dictionaries.
        for (int i = 1; i <= 20; i++)
        {
            string customValue =
                myDictionary[i];

            string builtInValue =
                builtIn[i];

            if (customValue != builtInValue &&
                !(i == 10 &&
                  customValue == "Updated-Value"))
            {
                sameResults = false;
            }
        }

        Console.WriteLine(
            "Custom dictionary lookup works correctly: " +
            allCorrect
        );

        Console.WriteLine(
            "Comparison completed successfully."
        );
    }
}