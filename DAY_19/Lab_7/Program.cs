// ### Lab 7 — Generic Interface + Custom Add Overloads for Collection Initializers

// 1. Define `public interface IRepository<T> where T : class { void Add(T item); T? GetById(int id); IEnumerable<T> GetAll(); }`.
// 2. Implement `InMemoryRepository<T> : IRepository<T> where T : class, IEntity` backed by your `MyDictionary<int, T>` from Lab 6 (or `Dictionary<int,T>` if you prefer to isolate the concern).
// 3. Build a `TagList` class (as in the guide) implementing `IEnumerable<string>` with **two overloaded `Add` methods** — one taking a single string, one taking `(string tag, bool highlighted)`.
// 4. Demonstrate constructing a `TagList` using mixed collection-initializer syntax exercising both `Add` overloads.

// **Deliverable:** Console app showing the repository storing/retrieving a custom entity, and the `TagList` built via initializer syntax.

// ---

using System;
using System.Collections;
using System.Collections.Generic;


// =============================================================
// 1. ENTITY INTERFACE
// =============================================================

// Every entity stored in the repository must provide an Id.
public interface IEntity
{
    int Id { get; }
}


// =============================================================
// 2. GENERIC REPOSITORY INTERFACE
// =============================================================

// T must be a reference type because the question specifies
// the constraint "where T : class".
public interface IRepository<T> where T : class
{
    // Adds an entity to the repository.
    void Add(T item);

    // Finds an entity using its integer ID.
    T? GetById(int id);

    // Returns all stored entities.
    IEnumerable<T> GetAll();
}


// =============================================================
// 3. IN-MEMORY REPOSITORY
// =============================================================

// The repository only accepts reference types that also
// implement IEntity, because it needs to access item.Id.
public class InMemoryRepository<T> : IRepository<T>
    where T : class, IEntity
{
    // Dictionary stores entities using their Id as the key.
    private Dictionary<int, T> items =
        new Dictionary<int, T>();


    // =========================================================
    // ADD
    // =========================================================

    public void Add(T item)
    {
        // Use the entity's Id as the dictionary key.
        items[item.Id] = item;
    }


    // =========================================================
    // GET BY ID
    // =========================================================

    public T? GetById(int id)
    {
        // TryGetValue safely checks whether the entity exists.
        if (items.TryGetValue(id, out T? item))
        {
            return item;
        }

        // Return null when the requested entity does not exist.
        return null;
    }


    // =========================================================
    // GET ALL
    // =========================================================

    public IEnumerable<T> GetAll()
    {
        // Return all entities stored in the dictionary.
        return items.Values;
    }
}


// =============================================================
// 4. CUSTOM ENTITY - PRODUCT
// =============================================================

public class Product : IEntity
{
    // ID is required by IEntity.
    public int Id { get; set; }

    // Product name is stored here.
    public string Name { get; set; }

    // Product price is stored here.
    public double Price { get; set; }


    public Product(int id, string name, double price)
    {
        Id = id;
        Name = name;
        Price = price;
    }


    // Override ToString so that Product objects can be
    // displayed in a readable format.
    public override string ToString()
    {
        return $"{Id} - {Name} - ₹{Price}";
    }
}


// =============================================================
// 5. TAG CLASS
// =============================================================

public class Tag
{
    // Stores the tag text.
    public string Name { get; set; }

    // Indicates whether the tag is highlighted.
    public bool Highlighted { get; set; }


    public Tag(string name, bool highlighted)
    {
        Name = name;
        Highlighted = highlighted;
    }


    public override string ToString()
    {
        return Highlighted
            ? $"{Name} [Highlighted]"
            : Name;
    }
}


// =============================================================
// 6. TAGLIST
// =============================================================

public class TagList : IEnumerable<string>
{
    // Internally store the tags as Tag objects.
    private List<Tag> tags = new List<Tag>();


    // =========================================================
    // ADD OVERLOAD 1
    // =========================================================

    // This Add method accepts only a tag name.
    public void Add(string tag)
    {
        // A normal tag is not highlighted by default.
        tags.Add(new Tag(tag, false));
    }


    // =========================================================
    // ADD OVERLOAD 2
    // =========================================================

    // This overloaded Add method accepts both tag name
    // and highlighted status.
    public void Add(string tag, bool highlighted)
    {
        // Store the tag using the supplied highlighted value.
        tags.Add(new Tag(tag, highlighted));
    }


    // =========================================================
    // ENUMERATOR
    // =========================================================

    public IEnumerator<string> GetEnumerator()
    {
        // Return only the tag names during foreach.
        foreach (Tag tag in tags)
        {
            yield return tag.ToString();
        }
    }


    // Required non-generic IEnumerable implementation.
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}


// =============================================================
// MAIN PROGRAM
// =============================================================

class Program
{
    static void Main()
    {
        Console.WriteLine("==============================================");
        Console.WriteLine("LAB 7 - GENERIC INTERFACE + ADD OVERLOADS");
        Console.WriteLine("==============================================");


        // =====================================================
        // 1. REPOSITORY DEMONSTRATION
        // =====================================================

        Console.WriteLine("\n1. InMemoryRepository<Product>");

        // Create a repository specifically for Product objects.
        InMemoryRepository<Product> repository =
            new InMemoryRepository<Product>();


        // Create some Product objects.
        Product product1 =
            new Product(101, "Laptop", 65000);

        Product product2 =
            new Product(102, "Mouse", 1200);

        Product product3 =
            new Product(103, "Keyboard", 2500);


        // Add the products to the repository.
        repository.Add(product1);
        repository.Add(product2);
        repository.Add(product3);


        Console.WriteLine("All products:");

        // GetAll returns every stored product.
        foreach (Product product in repository.GetAll())
        {
            Console.WriteLine(product);
        }


        // =====================================================
        // 2. GET BY ID
        // =====================================================

        Console.WriteLine("\n2. Get Product By ID");

        // Search for product with ID 102.
        Product? foundProduct =
            repository.GetById(102);

        if (foundProduct != null)
        {
            Console.WriteLine(
                "Found: " + foundProduct
            );
        }
        else
        {
            Console.WriteLine("Product not found.");
        }


        // =====================================================
        // 3. TEST MISSING ID
        // =====================================================

        Console.WriteLine("\n3. Missing Product Test");

        // Search for an ID that does not exist.
        Product? missingProduct =
            repository.GetById(999);

        if (missingProduct == null)
        {
            Console.WriteLine(
                "Product with ID 999 was not found."
            );
        }


        // =====================================================
        // 4. TAGLIST
        // =====================================================

        Console.WriteLine("\n4. TagList With Overloaded Add Methods");

        // Collection initializer can automatically call
        // the appropriate Add() overload.
        TagList tags = new TagList
        {
            "CSharp",
            { "Generics", true },
            "Collections",
            { "Advanced", true }
        };


        // foreach works because TagList implements IEnumerable<string>.
        Console.WriteLine("Tags:");

        foreach (string tag in tags)
        {
            Console.WriteLine(tag);
        }


        Console.WriteLine("\nProgram completed successfully.");
    }
}