// ### Lab 8 — Comprehensive Mini Report

// Using the product dataset, produce a single console report combining everything from this module:

// 1. Filter to in-stock products only (`Where`).
// 2. Group by `Category` (`GroupBy`).
// 3. Within each group, order products by price descending (`OrderByDescending` inside the group projection).
// 4. Order the categories themselves by total category value descending (`into` + `orderby`, or method-syntax equivalent).
// 5. Project each category group into a summary object with `Category`, `ItemCount`, `TotalValue`, and `TopProduct` (name of the most expensive item).
// 6. Print the final report, one section per category, in descending total-value order, using both a query-syntax version and a method-syntax version — confirm they match.

// **Deliverable:** Console app producing a formatted, readable multi-category report, built two ways (query syntax and method syntax) with matching output.

// ---

using System;
using System.Collections.Generic;
using System.Linq;

// ---------------------------------------------------------
// PRODUCT CLASS
// ---------------------------------------------------------

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public bool InStock { get; set; }
}


// ---------------------------------------------------------
// CATEGORY SUMMARY DTO
// ---------------------------------------------------------

// Stores the final report information for each category.
public class CategorySummary
{
    public string Category { get; set; }
    public int ItemCount { get; set; }
    public decimal TotalValue { get; set; }
    public string TopProduct { get; set; }

    // Used to compare two summary objects.
    public override bool Equals(object obj)
    {
        if (obj is not CategorySummary other)
            return false;

        return Category == other.Category
            && ItemCount == other.ItemCount
            && TotalValue == other.TotalValue
            && TopProduct == other.TopProduct;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(
            Category,
            ItemCount,
            TotalValue,
            TopProduct
        );
    }
}


class Program
{
    static void Main()
    {
        // ---------------------------------------------------------
        // LAB 8 - COMPREHENSIVE MINI REPORT
        // ---------------------------------------------------------

        List<Product> products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Keyboard",
                Category = "Electronics",
                Price = 799,
                InStock = true
            },

            new Product
            {
                Id = 2,
                Name = "Mouse",
                Category = "Electronics",
                Price = 499,
                InStock = true
            },

            new Product
            {
                Id = 3,
                Name = "Monitor",
                Category = "Electronics",
                Price = 8999,
                InStock = true
            },

            new Product
            {
                Id = 4,
                Name = "Notebook",
                Category = "Stationery",
                Price = 150,
                InStock = true
            },

            new Product
            {
                Id = 5,
                Name = "Pen",
                Category = "Stationery",
                Price = 50,
                InStock = true
            },

            new Product
            {
                Id = 6,
                Name = "Backpack",
                Category = "Accessories",
                Price = 999,
                InStock = false
            },

            new Product
            {
                Id = 7,
                Name = "Bottle",
                Category = "Accessories",
                Price = 450,
                InStock = true
            },

            new Product
            {
                Id = 8,
                Name = "Headphones",
                Category = "Electronics",
                Price = 999,
                InStock = false
            },

            new Product
            {
                Id = 9,
                Name = "Calculator",
                Category = "Stationery",
                Price = 700,
                InStock = true
            },

            new Product
            {
                Id = 10,
                Name = "Charger",
                Category = "Electronics",
                Price = 850,
                InStock = true
            },

            new Product
            {
                Id = 11,
                Name = "Diary",
                Category = "Stationery",
                Price = 300,
                InStock = false
            },

            new Product
            {
                Id = 12,
                Name = "Wallet",
                Category = "Accessories",
                Price = 600,
                InStock = true
            }
        };


        Console.WriteLine("==================================================");
        Console.WriteLine("LAB 8 - COMPREHENSIVE LINQ MINI REPORT");
        Console.WriteLine("==================================================");


        // =========================================================
        // QUERY SYNTAX VERSION
        // =========================================================

        // Step 1:
        // Filter only products that are currently in stock.
        //
        // Step 2:
        // Group the filtered products by Category.
        //
        // Step 3:
        // Use "into categoryGroup" so that we can continue
        // querying the grouped result.
        //
        // Step 4:
        // Calculate total category value.
        //
        // Step 5:
        // Order categories by total value descending.
        //
        // Step 6:
        // Project each group into CategorySummary.
        var querySyntaxReport =
            from p in products
            where p.InStock
            group p by p.Category into categoryGroup

            let totalValue = categoryGroup.Sum(p => p.Price)

            orderby totalValue descending

            select new CategorySummary
            {
                Category = categoryGroup.Key,

                // Number of in-stock products in this category
                ItemCount = categoryGroup.Count(),

                // Total price of all in-stock products
                TotalValue = totalValue,

                // Sort products from highest price to lowest
                // and select the first product's name.
                TopProduct = categoryGroup
                    .OrderByDescending(p => p.Price)
                    .First()
                    .Name
            };


        // =========================================================
        // METHOD SYNTAX VERSION
        // =========================================================

        // The same operation is performed using LINQ methods.
        //
        // Where()       -> filters products
        // GroupBy()     -> groups products
        // Select()      -> creates summary objects
        // OrderByDescending() -> sorts categories by total value
        var methodSyntaxReport = products
            .Where(p => p.InStock)

            // Group only the in-stock products
            .GroupBy(p => p.Category)

            // Create a summary for every category
            .Select(categoryGroup => new CategorySummary
            {
                Category = categoryGroup.Key,

                ItemCount = categoryGroup.Count(),

                TotalValue = categoryGroup.Sum(p => p.Price),

                TopProduct = categoryGroup
                    .OrderByDescending(p => p.Price)
                    .First()
                    .Name
            })

            // Sort categories by total value
            // from highest to lowest.
            .OrderByDescending(summary => summary.TotalValue);


        // =========================================================
        // PRINT QUERY SYNTAX REPORT
        // =========================================================

        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine("1. REPORT USING QUERY SYNTAX");
        Console.WriteLine("==================================================");

        PrintReport(querySyntaxReport);


        // =========================================================
        // PRINT METHOD SYNTAX REPORT
        // =========================================================

        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine("2. REPORT USING METHOD SYNTAX");
        Console.WriteLine("==================================================");

        PrintReport(methodSyntaxReport);


        // =========================================================
        // CONFIRM BOTH REPORTS MATCH
        // =========================================================

        // Convert both results to List so that they can be
        // compared element-by-element.
        List<CategorySummary> queryList =
            querySyntaxReport.ToList();

        List<CategorySummary> methodList =
            methodSyntaxReport.ToList();


        // SequenceEqual() checks:
        // - Same number of elements
        // - Same order
        // - Same values in each CategorySummary
        bool reportsMatch =
            queryList.SequenceEqual(methodList);


        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine("3. EQUIVALENCE CHECK");
        Console.WriteLine("==================================================");

        Console.WriteLine(
            reportsMatch
                ? "Query syntax and method syntax produce identical reports."
                : "The two reports are different."
        );


        // ---------------------------------------------------------
        // END OF LAB
        // ---------------------------------------------------------

        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine("LAB 8 COMPLETED");
        Console.WriteLine("==================================================");
    }


    // ---------------------------------------------------------
    // HELPER METHOD TO PRINT FINAL REPORT
    // ---------------------------------------------------------

    static void PrintReport(IEnumerable<CategorySummary> report)
    {
        foreach (CategorySummary summary in report)
        {
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"Category: {summary.Category}");
            Console.WriteLine("----------------------------------------------");

            Console.WriteLine(
                $"Number of Items : {summary.ItemCount}"
            );

            Console.WriteLine(
                $"Total Value     : Rs.{summary.TotalValue:F2}"
            );

            Console.WriteLine(
                $"Top Product     : {summary.TopProduct}"
            );
        }
    }
}