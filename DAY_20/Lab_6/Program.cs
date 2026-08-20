// ### Lab 6 — Lambda Expressions: Expression vs Statement Form

// 1. Write an expression-bodied lambda `Func<double, double, double> rectangleArea = (w, h) => w * h;`.
// 2. Write a statement-bodied lambda `Action<Order> printReceipt` that prints a multi-line formatted receipt (uses `{ }` with multiple statements).
// 3. Sort a `List<Product>` three different ways using lambda-based `Comparison<T>`/`Sort` overloads: by price ascending, by name descending, by a computed "discounted price" value.
// 4. Use `List<T>.RemoveAll(Predicate<T>)` with a lambda to remove all out-of-stock products from a list.

// **Deliverable:** Console app demonstrating all four, printing before/after state for the sort and removal steps.

// ---

using System;
using System.Collections.Generic;

class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
    public double DiscountPercent { get; set; }
    public bool InStock { get; set; }

    // Constructor used to create Product objects easily.
    public Product(string name, double price, double discountPercent, bool inStock)
    {
        Name = name;
        Price = price;
        DiscountPercent = discountPercent;
        InStock = inStock;
    }

    // Calculates the price after applying the discount.
    public double DiscountedPrice()
    {
        return Price - (Price * DiscountPercent / 100);
    }

    public override string ToString()
    {
        return $"{Name} - ₹{Price} - Stock: {InStock}";
    }
}

class Order
{
    public string OrderId { get; set; }
    public string CustomerName { get; set; }
    public double Amount { get; set; }

    public Order(string orderId, string customerName, double amount)
    {
        OrderId = orderId;
        CustomerName = customerName;
        Amount = amount;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("===== LAB 6: Lambda Expressions =====\n");

        // Expression-bodied lambda.
        // Since there is only one expression, braces and return are unnecessary.
        Func<double, double, double> rectangleArea = (w, h) => w * h;

        Console.WriteLine($"Rectangle Area: {rectangleArea(10, 5)}");

        Console.WriteLine("\n===== Statement-Bodied Lambda =====");

        // Statement-bodied lambda uses braces because it contains
        // multiple statements.
        Action<Order> printReceipt = order =>
        {
            Console.WriteLine("----- RECEIPT -----");
            Console.WriteLine($"Order ID: {order.OrderId}");
            Console.WriteLine($"Customer: {order.CustomerName}");
            Console.WriteLine($"Amount: ₹{order.Amount}");
            Console.WriteLine("-------------------");
        };

        Order order = new Order("ORD101", "Arti", 2500);
        printReceipt(order);

        Console.WriteLine("\n===== Product Sorting =====");

        List<Product> products = new List<Product>
        {
            new Product("Laptop", 60000, 10, true),
            new Product("Mouse", 1000, 5, true),
            new Product("Keyboard", 2500, 20, false),
            new Product("Monitor", 15000, 15, true)
        };

        Console.WriteLine("Original list:");
        PrintProducts(products);

        // Sort products by price in ascending order.
        products.Sort((p1, p2) => p1.Price.CompareTo(p2.Price));

        Console.WriteLine("\nSorted by Price Ascending:");
        PrintProducts(products);

        // Sort products by name in descending order.
        products.Sort((p1, p2) =>
            string.Compare(p2.Name, p1.Name, StringComparison.Ordinal));

        Console.WriteLine("\nSorted by Name Descending:");
        PrintProducts(products);

        // Sort products according to their calculated discounted price.
        products.Sort((p1, p2) =>
            p1.DiscountedPrice().CompareTo(p2.DiscountedPrice()));

        Console.WriteLine("\nSorted by Discounted Price:");
        PrintProducts(products);

        Console.WriteLine("\n===== Remove Out-of-Stock Products =====");

        // RemoveAll removes every product for which the predicate is true.
        int removedCount = products.RemoveAll(p => !p.InStock);

        Console.WriteLine($"Products removed: {removedCount}");

        Console.WriteLine("\nRemaining products:");
        PrintProducts(products);
    }

    // Helper method to display products.
    static void PrintProducts(List<Product> products)
    {
        foreach (Product product in products)
        {
            Console.WriteLine(product);
        }
    }
}