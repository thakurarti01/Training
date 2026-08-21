// ### Lab 2 — `Select` Projections

// 1. Project the product list to just names (`IEnumerable<string>`).
// 2. Project to an anonymous type containing `Name` and a computed `PriceWithTax` (assume 18% tax).
// 3. Project to a named `ProductSummaryDto { string Name; string PriceLabel; }` class, where `PriceLabel` is a formatted string like `"Rs.999.00"`.
// 4. Use the index-aware `Select` overload to project each product into `"#1: Keyboard"`-style strings.

// **Deliverable:** Console app printing all four projection results.

// ---

using System;
using System.Collections.Generic;
using System.Linq;

// Represents a product
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public bool InStock { get; set; }
}

// Named DTO used to store a simplified product summary
public class ProductSummaryDto
{
    public string Name { get; set; }
    public string PriceLabel { get; set; }
}

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------
        // LAB 2 - SELECT PROJECTIONS
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
        Console.WriteLine("LAB 2 - SELECT PROJECTIONS");
        Console.WriteLine("==============================================");


        // ---------------------------------------------------------
        // 1. PROJECT PRODUCTS TO ONLY THEIR NAMES
        // ---------------------------------------------------------

        // Select() transforms each Product object into its Name.
        // The resulting collection contains only strings.
        IEnumerable<string> productNames = products
            .Select(p => p.Name);

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("1. PRODUCT NAMES");
        Console.WriteLine("----------------------------------------------");

        foreach (string name in productNames)
        {
            Console.WriteLine(name);
        }


        // ---------------------------------------------------------
        // 2. PROJECT TO AN ANONYMOUS TYPE
        // ---------------------------------------------------------

        // We create a new object containing:
        // - Product Name
        // - Price including 18% GST/tax
        //
        // Anonymous types are useful when we need temporary
        // objects without creating a separate class.
        var productsWithTax = products
            .Select(p => new
            {
                Name = p.Name,
                PriceWithTax = p.Price * 1.18m
            });

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("2. NAME AND PRICE WITH 18% TAX");
        Console.WriteLine("----------------------------------------------");

        foreach (var product in productsWithTax)
        {
            Console.WriteLine(
                $"Name: {product.Name,-12} | " +
                $"Price With Tax: Rs.{product.PriceWithTax:F2}"
            );
        }


        // ---------------------------------------------------------
        // 3. PROJECT TO A NAMED DTO
        // ---------------------------------------------------------

        // ProductSummaryDto is a named class.
        // It contains only the information required for the summary.
        IEnumerable<ProductSummaryDto> productSummaries = products
            .Select(p => new ProductSummaryDto
            {
                Name = p.Name,

                // C2 formats the decimal value as currency.
                // Here we explicitly use "Rs." as requested.
                PriceLabel = $"Rs.{p.Price:F2}"
            });

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("3. PRODUCT SUMMARY DTO");
        Console.WriteLine("----------------------------------------------");

        foreach (ProductSummaryDto summary in productSummaries)
        {
            Console.WriteLine(
                $"Name: {summary.Name,-12} | " +
                $"Price: {summary.PriceLabel}"
            );
        }


        // ---------------------------------------------------------
        // 4. INDEX-AWARE SELECT
        // ---------------------------------------------------------

        // Select() has an overload that provides the current index.
        //
        // The index starts from 0, so we add 1 to display
        // user-friendly numbering starting from 1.
        IEnumerable<string> indexedProducts = products
            .Select((p, index) => $"#{index + 1}: {p.Name}");

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("4. INDEX-AWARE SELECT");
        Console.WriteLine("----------------------------------------------");

        foreach (string product in indexedProducts)
        {
            Console.WriteLine(product);
        }


        // ---------------------------------------------------------
        // END OF LAB
        // ---------------------------------------------------------

        Console.WriteLine();
        Console.WriteLine("==============================================");
        Console.WriteLine("LAB 2 COMPLETED");
        Console.WriteLine("==============================================");
    }
}