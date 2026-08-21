// ### Lab 3 — `Where` Filtering

// 1. Filter products under Rs.500.
// 2. Filter products that are BOTH in a specific category AND in stock.
// 3. Filter using the index-aware `Where` overload to get only products at even positions in the original list.
// 4. Chain two separate `.Where()` calls vs. one `.Where()` with `&&` — confirm they produce identical results (they should — LINQ composes predicates this way commonly for optional/conditional filters).

// **Deliverable:** Console app printing all four filtered results with counts.

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
        // LAB 3 - WHERE FILTERING
        // ---------------------------------------------------------

        // Shared product dataset
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
        Console.WriteLine("LAB 3 - WHERE FILTERING");
        Console.WriteLine("==============================================");


        // ---------------------------------------------------------
        // 1. FILTER PRODUCTS UNDER RS.500
        // ---------------------------------------------------------

        // Where() keeps only products whose price is less than 500.
        var under500 = products
            .Where(p => p.Price < 500);

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("1. PRODUCTS UNDER RS.500");
        Console.WriteLine("----------------------------------------------");

        PrintProducts(under500);

        Console.WriteLine($"Count: {under500.Count()}");


        // ---------------------------------------------------------
        // 2. FILTER BY CATEGORY AND STOCK STATUS
        // ---------------------------------------------------------

        // This example finds Electronics products
        // that are currently in stock.
        var electronicsInStock = products
            .Where(p => p.Category == "Electronics" && p.InStock);

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("2. ELECTRONICS PRODUCTS THAT ARE IN STOCK");
        Console.WriteLine("----------------------------------------------");

        PrintProducts(electronicsInStock);

        Console.WriteLine($"Count: {electronicsInStock.Count()}");


        // ---------------------------------------------------------
        // 3. INDEX-AWARE WHERE
        // ---------------------------------------------------------

        // Where() also has an overload that provides the index.
        //
        // index % 2 == 0 means positions 0, 2, 4, 6...
        // In other words, even indexes from the original list.
        var evenIndexProducts = products
            .Where((p, index) => index % 2 == 0);

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("3. PRODUCTS AT EVEN INDEX POSITIONS");
        Console.WriteLine("----------------------------------------------");

        PrintProducts(evenIndexProducts);

        Console.WriteLine($"Count: {evenIndexProducts.Count()}");


        // ---------------------------------------------------------
        // 4. TWO WHERE CALLS VS ONE WHERE WITH &&
        // ---------------------------------------------------------

        // Version 1:
        // Apply the two filtering conditions separately.
        var twoWhereCalls = products
            .Where(p => p.Category == "Stationery")
            .Where(p => p.InStock);


        // Version 2:
        // Combine both conditions inside one Where().
        var singleWhereCall = products
            .Where(p => p.Category == "Stationery" && p.InStock);


        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("4. TWO WHERE CALLS");
        Console.WriteLine("----------------------------------------------");

        PrintProducts(twoWhereCalls);

        Console.WriteLine($"Count: {twoWhereCalls.Count()}");


        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("4. SINGLE WHERE WITH &&");
        Console.WriteLine("----------------------------------------------");

        PrintProducts(singleWhereCall);

        Console.WriteLine($"Count: {singleWhereCall.Count()}");


        // SequenceEqual() checks whether both filtered sequences
        // contain the same products in the same order.
        bool sameResult = twoWhereCalls.SequenceEqual(singleWhereCall);

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("EQUIVALENCE CHECK");
        Console.WriteLine("----------------------------------------------");

        Console.WriteLine(
            sameResult
                ? "Both filtering approaches produce identical results."
                : "The filtering approaches produce different results."
        );


        Console.WriteLine();
        Console.WriteLine("==============================================");
        Console.WriteLine("LAB 3 COMPLETED");
        Console.WriteLine("==============================================");
    }


    // Helper method to print product details
    static void PrintProducts(IEnumerable<Product> products)
    {
        foreach (Product product in products)
        {
            Console.WriteLine(
                $"Id: {product.Id,-2} | " +
                $"Name: {product.Name,-12} | " +
                $"Category: {product.Category,-12} | " +
                $"Price: Rs.{product.Price:F2} | " +
                $"In Stock: {product.InStock}"
            );
        }
    }
}