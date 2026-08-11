using System;


// 1. ENUM
// Represents the different types of shapes

public enum ShapeKind
{
    Circle,
    Rectangle,
    Triangle
}


// 2. ABSTRACT BASE CLASS

public abstract class Shape
{
    // Property to store the type of shape
    public ShapeKind Kind { get; protected set; }

    // Abstract method for calculating area
    public abstract double Area();

    // Abstract method for calculating perimeter
    public abstract double Perimeter();

    // Concrete override of ToString()
    // This method works for every derived Shape
    public override string ToString()
    {
        return $"{Kind}: Area={Area():F2}, Perimeter={Perimeter():F2}";
    }
}


// 3. CIRCLE

public class Circle : Shape
{
    public double Radius { get; }

    // Constructor
    public Circle(double radius)
    {
        Radius = radius;
        Kind = ShapeKind.Circle;
    }

    // Calculate area of circle
    // Formula: πr²
    public override double Area()
    {
        return Math.PI * Radius * Radius;
    }

    // Calculate perimeter/circumference
    // Formula: 2πr
    public override double Perimeter()
    {
        return 2 * Math.PI * Radius;
    }
}


// 3. RECTANGLE

public class Rectangle : Shape
{
    public double Width { get; }
    public double Height { get; }

    // Constructor
    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
        Kind = ShapeKind.Rectangle;
    }

    // Calculate area
    // Formula: width × height
    public override double Area()
    {
        return Width * Height;
    }

    // Calculate perimeter
    // Formula: 2(width + height)
    public override double Perimeter()
    {
        return 2 * (Width + Height);
    }
}


// 3. TRIANGLE

public class Triangle : Shape
{
    public double SideA { get; }
    public double SideB { get; }
    public double SideC { get; }

    // Constructor
    public Triangle(double sideA, double sideB, double sideC)
    {
        SideA = sideA;
        SideB = sideB;
        SideC = sideC;
        Kind = ShapeKind.Triangle;
    }

    // Calculate perimeter
    public override double Perimeter()
    {
        return SideA + SideB + SideC;
    }

    // Calculate area using Heron's formula
    //
    // s = (a + b + c) / 2
    // Area = √(s(s-a)(s-b)(s-c))
    public override double Area()
    {
        double s = Perimeter() / 2;

        return Math.Sqrt(
            s * (s - SideA) *
            (s - SideB) *
            (s - SideC)
        );
    }
}


// 4. BOUNDING BOX STRUCT
// Demonstrates struct and operator overloading

public struct BoundingBox
{
    public double Width;
    public double Height;

    // Constructor
    public BoundingBox(double width, double height)
    {
        Width = width;
        Height = height;
    }

    // Overload * operator
    // Scales both Width and Height by the given factor
    public static BoundingBox operator *(BoundingBox box, double factor)
    {
        return new BoundingBox(
            box.Width * factor,
            box.Height * factor
        );
    }

    // Makes the BoundingBox easier to print
    public override string ToString()
    {
        return $"Width={Width:F2}, Height={Height:F2}";
    }
}


// 5. SHAPE MATH
// Demonstrates METHOD OVERLOADING

public static class ShapeMath
{
    // Overload 1:
    // Calculate total area of ALL shapes
    public static double TotalArea(Shape[] shapes)
    {
        double total = 0;

        foreach (Shape shape in shapes)
        {
            total += shape.Area();
        }

        return total;
    }

    // Overload 2:
    // Calculate total area of only a specific ShapeKind
    public static double TotalArea(
        Shape[] shapes,
        ShapeKind onlyKind)
    {
        double total = 0;

        foreach (Shape shape in shapes)
        {
            if (shape.Kind == onlyKind)
            {
                total += shape.Area();
            }
        }

        return total;
    }
}



class Program
{
    static void Main()
    {
        // Create a collection containing different shapes

        Shape[] shapes =
        {
            new Circle(3),
            new Rectangle(4, 6),
            new Triangle(3, 4, 5)
        };


        // Polymorphism
        // Each object is stored using a Shape reference.
        // The correct Area() and Perimeter() methods
        // are automatically called.

        Console.WriteLine("Shapes:");

        foreach (Shape shape in shapes)
        {
            Console.WriteLine(shape);
        }


        // Calculate total area of ALL shapes
        // Uses the first TotalArea() overload

        double totalArea = ShapeMath.TotalArea(shapes);

        Console.WriteLine();
        Console.WriteLine($"Total Area: {totalArea:F2}");


        // Calculate total area of CIRCLES only
        // Uses the second TotalArea() overload

        double circleArea =
            ShapeMath.TotalArea(shapes, ShapeKind.Circle);

        Console.WriteLine(
            $"Total Circle Area: {circleArea:F2}"
        );


        // BoundingBox operator overloading
        

        BoundingBox box = new BoundingBox(10, 5);

        // Uses overloaded * operator
        BoundingBox scaledBox = box * 2;

        Console.WriteLine();
        Console.WriteLine($"Original Box: {box}");
        Console.WriteLine($"Scaled Box: {scaledBox}");
    }
}