// ### Lab 1 — Query Syntax vs Method Syntax Equivalence

// 1. Write the same query FOUR times, proving they all produce identical results: "products under Rs.1000, ordered by name" —
//    - (a) fully in method syntax
//    - (b) fully in query syntax
//    - (c) query syntax for the `where`, piped into a method-syntax `.OrderBy(...)`
//    - (d) method-syntax `.Where(...)`, piped into a `select ... from` wrapped query-syntax `orderby` (hint: you'll need parentheses around the query-syntax portion)
// 2. Print all four results and confirm (via a comment or a `SequenceEqual` check) they match.

// **Deliverable:** Console app demonstrating equivalence with printed proof.

// ---

using System;
using System.Collections.Generic;
using System.Linq;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public bool InStock { get; set; }
}

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------
        // LAB 1 - Query Syntax vs Method Syntax Equivalence
        // ---------------------------------------------------------

        // Creating the shared product dataset
        List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Keyboard", Category = "Electronics", Price = 799, InStock = true },
            new Product { Id = 2, Name = "Mouse", Category = "Electronics", Price = 499, InStock = true },
            new Product { Id = 3, Name = "Monitor", Category = "Electronics", Price = 8999, InStock = true },
            new Product { Id = 4, Name = "Notebook", Category = "Stationery", Price = 150, InStock = true },
            new Product { Id = 5, Name = "Pen", Category = "Stationery", Price = 50, InStock = true },
            new Product { Id = 6, Name = "Backpack", Category = "Accessories", Price = 999, InStock = false },
            new Product { Id = 7, Name = "Bottle", Category = "Accessories", Price = 450, InStock = true },
            new Product { Id = 8, Name = "Headphones", Category = "Electronics", Price = 999, InStock = false },
            new Product { Id = 9, Name = "Calculator", Category = "Stationery", Price = 700, InStock = true },
            new Product { Id = 10, Name = "Charger", Category = "Electronics", Price = 850, InStock = true },
            new Product { Id = 11, Name = "Diary", Category = "Stationery", Price = 300, InStock = false },
            new Product { Id = 12, Name = "Wallet", Category = "Accessories", Price = 600, InStock = true }
        };

        Console.WriteLine("==============================================");
        Console.WriteLine("LAB 1 - LINQ QUERY SYNTAX VS METHOD SYNTAX");
        Console.WriteLine("==============================================");

        // Requirement:
        // Find products with Price less than Rs.1000
        // and order them alphabetically by Name.

        // ---------------------------------------------------------
        // (A) FULLY IN METHOD SYNTAX
        // ---------------------------------------------------------

        // Where() filters the products.
        // OrderBy() sorts the filtered products by Name.
        var methodSyntax = products
            .Where(p => p.Price < 1000)
            .OrderBy(p => p.Name);


        // ---------------------------------------------------------
        // (B) FULLY IN QUERY SYNTAX
        // ---------------------------------------------------------

        // Query syntax uses SQL-like keywords such as
        // from, where and orderby.
        var querySyntax =
            from p in products
            where p.Price < 1000
            orderby p.Name
            select p;


        // ---------------------------------------------------------
        // (C) QUERY SYNTAX WHERE + METHOD SYNTAX ORDERBY
        // ---------------------------------------------------------

        // First perform filtering using query syntax.
        var filteredUsingQuery =
            from p in products
            where p.Price < 1000
            select p;

        // Then order the filtered result using method syntax.
        var queryThenMethod = filteredUsingQuery
            .OrderBy(p => p.Name);


        // ---------------------------------------------------------
        // (D) METHOD SYNTAX WHERE + QUERY SYNTAX ORDERBY
        // ---------------------------------------------------------

        // First filter using method syntax.
        var filteredUsingMethod = products
            .Where(p => p.Price < 1000);

        // Then use query syntax for ordering.
        //
        // Parentheses are used because the query operates on
        // the result of the Where() method.
        var methodThenQuery =
            from p in filteredUsingMethod
            orderby p.Name
            select p;


        // ---------------------------------------------------------
        // PRINT ALL FOUR RESULTS
        // ---------------------------------------------------------

        PrintProducts("A) Fully Method Syntax", methodSyntax);

        PrintProducts("B) Fully Query Syntax", querySyntax);

        PrintProducts("C) Query Where + Method OrderBy", queryThenMethod);

        PrintProducts("D) Method Where + Query OrderBy", methodThenQuery);


        // ---------------------------------------------------------
        // CHECK WHETHER ALL FOUR RESULTS ARE IDENTICAL
        // ---------------------------------------------------------

        // SequenceEqual() compares both sequences element-by-element.
        // Since all four queries use the same filtering and ordering,
        // they should produce exactly the same sequence.
        bool allMatch =
            methodSyntax.SequenceEqual(querySyntax)
            && methodSyntax.SequenceEqual(queryThenMethod)
            && methodSyntax.SequenceEqual(methodThenQuery);

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("EQUIVALENCE CHECK");
        Console.WriteLine("----------------------------------------------");

        Console.WriteLine(
            allMatch
                ? "All four queries produce identical results."
                : "The queries produce different results."
        );
    }


    // Helper method to print products in a formatted way
    static void PrintProducts(
        string title,
        IEnumerable<Product> products)
    {
        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine(title);
        Console.WriteLine("----------------------------------------------");

        foreach (Product product in products)
        {
            Console.WriteLine(
                $"Id: {product.Id,-2} | " +
                $"Name: {product.Name,-12} | " +
                $"Category: {product.Category,-12} | " +
                $"Price: Rs.{product.Price:F2}"
            );
        }
    }
}