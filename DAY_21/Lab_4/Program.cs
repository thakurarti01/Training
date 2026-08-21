// ### Lab 4 — `OfType<T>`

// 1. Create a `List<object>` mixing `int`, `string`, `double`, and `Product` instances. Use `OfType<int>()`, then `OfType<string>()`, then `OfType<Product>()` to extract each subset.
// 2. Model a small shape hierarchy: `Shape` (base), `Circle : Shape { double Radius }`, `Rectangle : Shape { double Width, Height }`. Build a `List<Shape>` containing a mix of both. Use `OfType<Circle>()` to compute total circle area, and `OfType<Rectangle>()` to compute total rectangle area.
// 3. Demonstrate the difference between `OfType<Rectangle>()` and `Cast<Rectangle>()` on the same mixed list — show `Cast<Rectangle>()` throwing `InvalidCastException` when the list contains a `Circle`, caught and reported (not crashing the app).

// **Deliverable:** Console app demonstrating all three, including the caught `Cast<T>` exception.

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
// SHAPE HIERARCHY
// ---------------------------------------------------------

// Base class for all shapes
public class Shape
{
}

// Circle inherits from Shape
public class Circle : Shape
{
    public double Radius { get; set; }
}

// Rectangle inherits from Shape
public class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }
}


class Program
{
    static void Main()
    {
        // ---------------------------------------------------------
        // LAB 4 - OFTYPE<T>
        // ---------------------------------------------------------

        Console.WriteLine("==============================================");
        Console.WriteLine("LAB 4 - OFTYPE<T>");
        Console.WriteLine("==============================================");


        // =========================================================
        // 1. OFTYPE<T> WITH A MIXED LIST
        // =========================================================

        // List<object> can contain objects of different types.
        List<object> mixedList = new List<object>
        {
            10,
            25,
            "Hello",
            "LINQ",
            15.5,
            20.75,

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
            }
        };


        // ---------------------------------------------------------
        // Extract only integers
        // ---------------------------------------------------------

        // OfType<int>() ignores all objects that are not integers.
        IEnumerable<int> integers = mixedList.OfType<int>();

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("1A. INTEGER VALUES");
        Console.WriteLine("----------------------------------------------");

        foreach (int number in integers)
        {
            Console.WriteLine(number);
        }


        // ---------------------------------------------------------
        // Extract only strings
        // ---------------------------------------------------------

        // Only string objects are returned.
        IEnumerable<string> strings = mixedList.OfType<string>();

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("1B. STRING VALUES");
        Console.WriteLine("----------------------------------------------");

        foreach (string text in strings)
        {
            Console.WriteLine(text);
        }


        // ---------------------------------------------------------
        // Extract only Product objects
        // ---------------------------------------------------------

        // OfType<Product>() ignores int, string and double objects.
        IEnumerable<Product> productObjects = mixedList.OfType<Product>();

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("1C. PRODUCT OBJECTS");
        Console.WriteLine("----------------------------------------------");

        foreach (Product product in productObjects)
        {
            Console.WriteLine(
                $"Id: {product.Id} | " +
                $"Name: {product.Name} | " +
                $"Price: Rs.{product.Price}"
            );
        }


        // =========================================================
        // 2. OFTYPE WITH SHAPE HIERARCHY
        // =========================================================

        // Creating a list containing both Circle and Rectangle.
        List<Shape> shapes = new List<Shape>
        {
            new Circle { Radius = 5 },
            new Rectangle { Width = 10, Height = 4 },
            new Circle { Radius = 3 },
            new Rectangle { Width = 6, Height = 8 },
            new Circle { Radius = 2 }
        };


        // ---------------------------------------------------------
        // Extract circles
        // ---------------------------------------------------------

        // OfType<Circle>() returns only Circle objects.
        IEnumerable<Circle> circles = shapes.OfType<Circle>();

        // Calculate total area of all circles.
        // Formula: π × r²
        double totalCircleArea = circles
            .Sum(c => Math.PI * c.Radius * c.Radius);

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("2A. CIRCLE INFORMATION");
        Console.WriteLine("----------------------------------------------");

        foreach (Circle circle in circles)
        {
            double area = Math.PI * circle.Radius * circle.Radius;

            Console.WriteLine(
                $"Radius: {circle.Radius} | Area: {area:F2}"
            );
        }

        Console.WriteLine($"Total Circle Area: {totalCircleArea:F2}");


        // ---------------------------------------------------------
        // Extract rectangles
        // ---------------------------------------------------------

        // OfType<Rectangle>() returns only Rectangle objects.
        IEnumerable<Rectangle> rectangles = shapes.OfType<Rectangle>();

        // Calculate total area of all rectangles.
        // Formula: Width × Height
        double totalRectangleArea = rectangles
            .Sum(r => r.Width * r.Height);

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("2B. RECTANGLE INFORMATION");
        Console.WriteLine("----------------------------------------------");

        foreach (Rectangle rectangle in rectangles)
        {
            double area = rectangle.Width * rectangle.Height;

            Console.WriteLine(
                $"Width: {rectangle.Width} | " +
                $"Height: {rectangle.Height} | " +
                $"Area: {area:F2}"
            );
        }

        Console.WriteLine(
            $"Total Rectangle Area: {totalRectangleArea:F2}"
        );


        // =========================================================
        // 3. OFTYPE<T> VS CAST<T>
        // =========================================================

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("3. OFTYPE<T> VS CAST<T>");
        Console.WriteLine("----------------------------------------------");


        // ---------------------------------------------------------
        // OfType<Rectangle>()
        // ---------------------------------------------------------

        // OfType safely ignores objects that are not Rectangle.
        var onlyRectangles = shapes.OfType<Rectangle>();

        Console.WriteLine();
        Console.WriteLine("Using OfType<Rectangle>():");

        foreach (Rectangle rectangle in onlyRectangles)
        {
            Console.WriteLine(
                $"Rectangle: {rectangle.Width} x {rectangle.Height}"
            );
        }


        // ---------------------------------------------------------
        // Cast<Rectangle>()
        // ---------------------------------------------------------

        Console.WriteLine();
        Console.WriteLine("Using Cast<Rectangle>():");

        try
        {
            // Cast<Rectangle>() assumes that EVERY element
            // in the collection is a Rectangle.
            //
            // But our list contains Circle objects too.
            // Therefore, Cast<Rectangle>() will throw
            // InvalidCastException when it reaches a Circle.

            var castRectangles = shapes.Cast<Rectangle>();

            foreach (Rectangle rectangle in castRectangles)
            {
                Console.WriteLine(
                    $"Rectangle: {rectangle.Width} x {rectangle.Height}"
                );
            }
        }
        catch (InvalidCastException)
        {
            // Catching the exception prevents the application
            // from crashing.
            Console.WriteLine(
                "InvalidCastException caught: " +
                "The collection contains objects that are not Rectangle."
            );
        }


        // ---------------------------------------------------------
        // END OF LAB
        // ---------------------------------------------------------

        Console.WriteLine();
        Console.WriteLine("==============================================");
        Console.WriteLine("LAB 4 COMPLETED");
        Console.WriteLine("==============================================");
    }
}