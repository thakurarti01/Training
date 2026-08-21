// ### Lab 5 — `OrderBy` / `ThenBy`

// 1. Sort products by `Category` ascending, then by `Price` descending within each category (`OrderBy` + `ThenByDescending`).
// 2. Deliberately write the "bug" version using `.OrderBy(p => p.Category).OrderBy(p => p.Price)` and print the result — in a comment, explain why the category ordering is lost.
// 3. Fix it with `ThenBy` and print the corrected result for comparison.
// 4. Sort with 3 keys: `InStock` (in-stock first), then `Category` ascending, then `Name` ascending.

// **Deliverable:** Console app showing the buggy vs. fixed multi-key sort side by side, plus the 3-key sort.

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
        // LAB 5 - ORDERBY / THENBY
        // ---------------------------------------------------------

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
        Console.WriteLine("LAB 5 - ORDERBY / THENBY");
        Console.WriteLine("==============================================");


        // =========================================================
        // 1. CATEGORY ASCENDING + PRICE DESCENDING
        // =========================================================

        // OrderBy() creates the primary sorting key.
        // ThenByDescending() creates the secondary sorting key.
        //
        // Result:
        // 1. Categories are sorted alphabetically.
        // 2. Products inside each category are sorted by price
        //    from highest to lowest.
        var correctSort = products
            .OrderBy(p => p.Category)
            .ThenByDescending(p => p.Price);

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("1. CATEGORY ASCENDING + PRICE DESCENDING");
        Console.WriteLine("----------------------------------------------");

        PrintProducts(correctSort);


        // =========================================================
        // 2. BUG VERSION - TWO ORDERBY CALLS
        // =========================================================

        // This is the incorrect approach.
        //
        // The second OrderBy() starts a NEW primary ordering.
        // Therefore, the previous Category ordering is lost.
        var bugSort = products
            .OrderBy(p => p.Category)
            .OrderBy(p => p.Price);

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("2. BUG VERSION - TWO ORDERBY CALLS");
        Console.WriteLine("----------------------------------------------");

        PrintProducts(bugSort);

        // IMPORTANT:
        //
        // OrderBy(Category) followed by OrderBy(Price)
        // does NOT mean:
        //
        // Category -> Price
        //
        // The second OrderBy(Price) becomes the new primary sort.
        //
        // Use ThenBy() when you want a secondary sorting condition.


        // =========================================================
        // 3. FIXED VERSION USING THENBY
        // =========================================================

        // ThenByDescending() preserves the first ordering
        // and applies price sorting only when categories are equal.
        var fixedSort = products
            .OrderBy(p => p.Category)
            .ThenByDescending(p => p.Price);

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("3. FIXED VERSION - ORDERBY + THENBY");
        Console.WriteLine("----------------------------------------------");

        PrintProducts(fixedSort);


        // =========================================================
        // COMPARE BUGGY AND CORRECT VERSION
        // =========================================================

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("BUG VS FIX EXPLANATION");
        Console.WriteLine("----------------------------------------------");

        Console.WriteLine(
            "Bug: OrderBy(Category).OrderBy(Price) " +
            "makes Price the new primary sort."
        );

        Console.WriteLine(
            "Fix: OrderBy(Category).ThenByDescending(Price) " +
            "keeps Category as the primary sort."
        );


        // =========================================================
        // 4. THREE-KEY SORT
        // =========================================================

        // We need:
        // 1. InStock products first
        // 2. Category alphabetically
        // 3. Name alphabetically
        //
        // OrderByDescending(p => p.InStock)
        // puts true before false.
        //
        // ThenBy() adds the second and third sorting keys.
        var threeKeySort = products
            .OrderByDescending(p => p.InStock)
            .ThenBy(p => p.Category)
            .ThenBy(p => p.Name);

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("4. THREE-KEY SORT");
        Console.WriteLine("In Stock -> Category -> Name");
        Console.WriteLine("----------------------------------------------");

        PrintProducts(threeKeySort);


        // =========================================================
        // END OF LAB
        // =========================================================

        Console.WriteLine();
        Console.WriteLine("==============================================");
        Console.WriteLine("LAB 5 COMPLETED");
        Console.WriteLine("==============================================");
    }


    // Helper method to print product information
    static void PrintProducts(IEnumerable<Product> products)
    {
        foreach (Product product in products)
        {
            Console.WriteLine(
                $"Id: {product.Id,-2} | " +
                $"Name: {product.Name,-12} | " +
                $"Category: {product.Category,-12} | " +
                $"Price: Rs.{product.Price,7:F2} | " +
                $"In Stock: {product.InStock}"
            );
        }
    }
}