// ### Lab 6 — `GroupBy` and `into`

// 1. Group products by `Category`; for each group print the category name and the count of products in it.
// 2. Using query syntax with `into`, group by `Category`, keep only categories with 3 or more products, and order the remaining groups by total inventory value (`Sum(p => p.Price)`) descending.
// 3. For each category group, compute and print: count, total value, average price, and the single most expensive product's name (all via chained aggregation methods on the group).
// 4. Group by a composite key: `(Category, InStock)` — print each group's key and count.

// **Deliverable:** Console app printing all four grouped reports clearly labeled.

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
        // LAB 6 - GROUPBY AND INTO
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
        Console.WriteLine("LAB 6 - GROUPBY AND INTO");
        Console.WriteLine("==============================================");


        // =========================================================
        // 1. GROUP PRODUCTS BY CATEGORY
        // =========================================================

        // GroupBy() creates one group for every unique category.
        //
        // Each group contains:
        // - Key   -> category name
        // - Items -> products belonging to that category
        var groupedProducts = products
            .GroupBy(p => p.Category);

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("1. PRODUCT COUNT BY CATEGORY");
        Console.WriteLine("----------------------------------------------");

        foreach (var group in groupedProducts)
        {
            Console.WriteLine(
                $"Category: {group.Key} | Count: {group.Count()}"
            );
        }


        // =========================================================
        // 2. QUERY SYNTAX WITH INTO
        // =========================================================

        // First group products by Category.
        //
        // "into categoryGroup" stores the grouping result in
        // another range variable.
        //
        // We then:
        // 1. Keep categories containing at least 3 products.
        // 2. Calculate total inventory value.
        // 3. Sort categories by total value descending.
        var largeCategories =
            from p in products
            group p by p.Category into categoryGroup
            let totalValue = categoryGroup.Sum(p => p.Price)
            where categoryGroup.Count() >= 3
            orderby totalValue descending
            select new
            {
                Category = categoryGroup.Key,
                Count = categoryGroup.Count(),
                TotalValue = totalValue
            };

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("2. CATEGORIES WITH 3 OR MORE PRODUCTS");
        Console.WriteLine("ORDERED BY TOTAL VALUE DESCENDING");
        Console.WriteLine("----------------------------------------------");

        foreach (var category in largeCategories)
        {
            Console.WriteLine(
                $"Category: {category.Category,-12} | " +
                $"Count: {category.Count,-2} | " +
                $"Total Value: Rs.{category.TotalValue:F2}"
            );
        }


        // =========================================================
        // 3. CATEGORY-WISE AGGREGATION
        // =========================================================

        // For every group, calculate:
        // - Number of products
        // - Total product value
        // - Average price
        // - Most expensive product
        //
        // Select() converts every group into a summary object.
        var categoryReports = products
            .GroupBy(p => p.Category)
            .Select(group => new
            {
                Category = group.Key,

                // Number of products in the group
                Count = group.Count(),

                // Sum of all product prices
                TotalValue = group.Sum(p => p.Price),

                // Average product price
                AveragePrice = group.Average(p => p.Price),

                // First product after sorting by price descending
                // is the most expensive product.
                MostExpensiveProduct = group
                    .OrderByDescending(p => p.Price)
                    .First()
                    .Name
            });

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("3. CATEGORY-WISE DETAILED REPORT");
        Console.WriteLine("----------------------------------------------");

        foreach (var report in categoryReports)
        {
            Console.WriteLine(
                $"Category: {report.Category}"
            );

            Console.WriteLine(
                $"  Count: {report.Count}"
            );

            Console.WriteLine(
                $"  Total Value: Rs.{report.TotalValue:F2}"
            );

            Console.WriteLine(
                $"  Average Price: Rs.{report.AveragePrice:F2}"
            );

            Console.WriteLine(
                $"  Most Expensive: {report.MostExpensiveProduct}"
            );

            Console.WriteLine();
        }


        // =========================================================
        // 4. GROUP BY COMPOSITE KEY
        // =========================================================

        // We can group using more than one property.
        //
        // Here products are grouped by:
        // 1. Category
        // 2. InStock status
        //
        // The resulting key contains both values.
        var compositeGroups = products
            .GroupBy(p => new
            {
                p.Category,
                p.InStock
            });

        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("4. GROUP BY CATEGORY + STOCK STATUS");
        Console.WriteLine("----------------------------------------------");

        foreach (var group in compositeGroups)
        {
            Console.WriteLine(
                $"Category: {group.Key.Category,-12} | " +
                $"In Stock: {group.Key.InStock,-5} | " +
                $"Count: {group.Count()}"
            );
        }


        // ---------------------------------------------------------
        // END OF LAB
        // ---------------------------------------------------------

        Console.WriteLine();
        Console.WriteLine("==============================================");
        Console.WriteLine("LAB 6 COMPLETED");
        Console.WriteLine("==============================================");
    }
}