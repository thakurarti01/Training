// ### Lab 7 — Deferred vs. Immediate Execution

// 1. Build a `Where` query (deferred) over a `List<Product>`, print a "query built" message, then add a new product to the underlying list that matches the filter, THEN enumerate the query and show the new product appears.
// 2. Repeat the same experiment but call `.ToList()` immediately after building the query — add a new matching product afterward — and show the snapshot does NOT include the new product.
// 3. Build a query that has an expensive-looking (simulated with a `Console.WriteLine` inside the predicate) `Where` clause, and enumerate it TWICE with two separate `foreach` loops — show (via the printed side-effects) that the predicate runs again on the second enumeration, then fix it by materializing once with `.ToList()` and reusing that list for both loops.

// **Deliverable:** Console app with clear before/after output demonstrating deferred execution, snapshotting, and the double-enumeration cost + fix.

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
        // LAB 7 - DEFERRED VS IMMEDIATE EXECUTION
        // ---------------------------------------------------------

        Console.WriteLine("==============================================");
        Console.WriteLine("LAB 7 - DEFERRED VS IMMEDIATE EXECUTION");
        Console.WriteLine("==============================================");


        // =========================================================
        // 1. DEFERRED EXECUTION
        // =========================================================

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
            }
        };


        // Where() creates a deferred query.
        //
        // At this point the filtering operation has NOT
        // actually been executed.
        var deferredQuery = products
            .Where(p => p.Price < 1000);

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("1. DEFERRED EXECUTION");
        Console.WriteLine("----------------------------------------------");

        Console.WriteLine("Query built, but not yet enumerated.");


        // Add a new product AFTER creating the query.
        //
        // This product matches the Where() condition.
        products.Add(new Product
        {
            Id = 4,
            Name = "Headphones",
            Category = "Electronics",
            Price = 999,
            InStock = true
        });

        Console.WriteLine(
            "New product 'Headphones' added after query was built."
        );


        Console.WriteLine();
        Console.WriteLine("Enumerating deferred query:");

        // The query executes NOW.
        //
        // Since Headphones was added before enumeration,
        // it appears in the result.
        foreach (Product product in deferredQuery)
        {
            Console.WriteLine(
                $"{product.Name} - Rs.{product.Price}"
            );
        }

        Console.WriteLine();
        Console.WriteLine(
            "Result: The newly added product appears because " +
            "the query uses deferred execution."
        );


        // =========================================================
        // 2. IMMEDIATE EXECUTION USING TOLIST()
        // =========================================================

        List<Product> productsSnapshot = new List<Product>
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
            }
        };


        // ToList() forces the query to execute immediately.
        //
        // The matching products are now copied into a new List.
        List<Product> immediateQuery = productsSnapshot
            .Where(p => p.Price < 1000)
            .ToList();

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("2. IMMEDIATE EXECUTION USING TOLIST()");
        Console.WriteLine("----------------------------------------------");

        Console.WriteLine(
            "Query executed immediately using ToList()."
        );


        // Add a matching product AFTER ToList().
        productsSnapshot.Add(new Product
        {
            Id = 4,
            Name = "Headphones",
            Category = "Electronics",
            Price = 999,
            InStock = true
        });

        Console.WriteLine(
            "New product 'Headphones' added after ToList()."
        );


        Console.WriteLine();
        Console.WriteLine("Enumerating the ToList() result:");

        // Headphones does NOT appear because immediateQuery
        // is already a snapshot created before the new product
        // was added.
        foreach (Product product in immediateQuery)
        {
            Console.WriteLine(
                $"{product.Name} - Rs.{product.Price}"
            );
        }

        Console.WriteLine();
        Console.WriteLine(
            "Result: The newly added product does NOT appear " +
            "because ToList() created a snapshot."
        );


        // =========================================================
        // 3. DOUBLE ENUMERATION OF A DEFERRED QUERY
        // =========================================================

        List<Product> expensiveProducts = new List<Product>
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
                Name = "Charger",
                Category = "Electronics",
                Price = 850,
                InStock = true
            }
        };


        // The Console.WriteLine() inside Where() simulates
        // an expensive operation.
        //
        // Because the query is deferred, this code runs every
        // time the query is enumerated.
        var expensiveQuery = expensiveProducts
            .Where(p =>
            {
                Console.WriteLine(
                    $"Checking price for {p.Name}..."
                );

                return p.Price > 500;
            });


        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("3. DEFERRED QUERY - FIRST ENUMERATION");
        Console.WriteLine("----------------------------------------------");

        foreach (Product product in expensiveQuery)
        {
            Console.WriteLine(
                $"Selected: {product.Name}"
            );
        }


        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("3. DEFERRED QUERY - SECOND ENUMERATION");
        Console.WriteLine("----------------------------------------------");

        // The Where() predicate runs again.
        //
        // Therefore, the "Checking price..." messages appear
        // again for every product.
        foreach (Product product in expensiveQuery)
        {
            Console.WriteLine(
                $"Selected: {product.Name}"
            );
        }


        // =========================================================
        // FIX - MATERIALIZE THE QUERY ONCE
        // =========================================================

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("FIX - MATERIALIZE USING TOLIST()");
        Console.WriteLine("----------------------------------------------");


        // ToList() executes the filtering operation only once
        // and stores the result in memory.
        var materializedProducts = expensiveProducts
            .Where(p =>
            {
                Console.WriteLine(
                    $"Checking price for {p.Name}..."
                );

                return p.Price > 500;
            })
            .ToList();


        Console.WriteLine();
        Console.WriteLine("First enumeration of materialized list:");

        // No Where() predicate runs here.
        // We are simply reading the already-created List.
        foreach (Product product in materializedProducts)
        {
            Console.WriteLine(
                $"Selected: {product.Name}"
            );
        }


        Console.WriteLine();
        Console.WriteLine("Second enumeration of materialized list:");

        // Again, no filtering predicate runs.
        // The already calculated results are reused.
        foreach (Product product in materializedProducts)
        {
            Console.WriteLine(
                $"Selected: {product.Name}"
            );
        }


        // ---------------------------------------------------------
        // END OF LAB
        // ---------------------------------------------------------

        Console.WriteLine();
        Console.WriteLine("==============================================");
        Console.WriteLine("LAB 7 COMPLETED");
        Console.WriteLine("==============================================");
    }
}