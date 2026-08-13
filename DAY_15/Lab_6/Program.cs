using System;
using System.Collections.Generic;


// Stores optional metadata for a cache entry
public class CacheEntryOptions
{
    public string Label { get; set; } = string.Empty;
    public bool Pinned { get; set; }
}


// Generic cache that can store any key-value types
public class TypedCache<TKey, TValue> where TKey : notnull
{
    // Dictionary stores key-value pairs
    private readonly Dictionary<TKey, TValue> _store = new();

    // Counts all instances of this closed generic type
    private static int _totalInstances;


    // Constructor increases the instance count
    public TypedCache()
    {
        _totalInstances++;
    }


    // Indexer allows reading and writing using a key
    public TValue this[TKey key]
    {
        get
        {
            // Throw exception if key doesn't exist
            if (!_store.TryGetValue(key, out TValue? value))
            {
                throw new KeyNotFoundException(
                    $"The given key '{key}' was not present in the cache."
                );
            }

            return value;
        }

        set
        {
            // Adds a new key or overwrites existing value
            _store[key] = value;
        }
    }


    // Read-only property that returns number of entries
    public int Count => _store.Count;


    // Static property returns number of cache objects created
    public static int TotalCacheInstances => _totalInstances;


    // Prints the number of instances of this generic type
    public static void PrintGlobalStats()
    {
        Console.WriteLine(
            $"Global TypedCache<{typeof(TKey).Name}, " +
            $"{typeof(TValue).Name}> instances created: " +
            $"{_totalInstances}"
        );
    }


    // Adds or updates a cache entry
    // Options are optional metadata
    public void Add(
        TKey key,
        TValue value,
        CacheEntryOptions? options = null)
    {
        _store[key] = value;

        // Metadata can be processed here if needed
        if (options != null)
        {
            Console.WriteLine(
                $"Added '{key}' - Label: {options.Label}, " +
                $"Pinned: {options.Pinned}"
            );
        }
    }
}


class Program
{
    static void Main()
    {
        // Create first cache
        TypedCache<string, int> cache1 =
            new TypedCache<string, int>();

        // Add entries using Add()
        cache1.Add("a", 1);

        cache1.Add(
            "b",
            2,
            new CacheEntryOptions
            {
                Label = "Important",
                Pinned = true
            }
        );


        // Read value using the indexer
        Console.WriteLine($"cache1[a] = {cache1["a"]}");

        // Display number of entries
        Console.WriteLine($"cache1 count: {cache1.Count}");


        // Create second cache of the same closed generic type
        TypedCache<string, int> cache2 =
            new TypedCache<string, int>();

        cache2.Add("c", 3);


        // Try to access a missing key
        try
        {
            Console.WriteLine(cache1["z"]);
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine(
                $"Missing key caught: {ex.Message}"
            );
        }


        // Display total instances of TypedCache<string, int>
        TypedCache<string, int>.PrintGlobalStats();
    }
}