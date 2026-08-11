using System;
public class Formatter
{
    // Overload 1: Format an integer
    public string Format(int value)
    {
        return value.ToString();
    }

    // Overload 2: Format a double with 2 decimal places
    public string Format(double value)
    {
        return value.ToString("F2");
    }

    // Overload 3: Treat two integers as a fraction
    public string Format(int numerator, int denominator)
    {
        return $"{numerator}/{denominator}";
    }
}



// 2. Notifier Base Class
// Demonstrates Virtual and Non-Virtual Methods


public class Notifier
{
    // Virtual method - can be overridden by a child class
    public virtual void Send()
    {
        Console.WriteLine("Notifier: generic send");
    }

    // Non-virtual method - cannot be overridden
    public void Log()
    {
        Console.WriteLine("Notifier: generic log");
    }
}


// 3. EmailNotifier Class
// Demonstrates Method Overriding and Method Hiding

public class EmailNotifier : Notifier
{
    // Override the virtual Send() method
    public override void Send()
    {
        Console.WriteLine("EmailNotifier: sending email");
    }

    // Hide the parent Log() method using new
    public new void Log()
    {
        Console.WriteLine("EmailNotifier: logging to email log");
    }
}


// 5. Vector2 Struct
// Demonstrates Operator Overloading

public struct Vector2
{
    public double X;
    public double Y;

    // Constructor
    public Vector2(double x, double y)
    {
        X = x;
        Y = y;
    }

    // Overload + operator
    // Adds two vectors
    public static Vector2 operator +(Vector2 a, Vector2 b)
    {
        return new Vector2(a.X + b.X, a.Y + b.Y);
    }

    // Overload * operator
    // Multiplies a vector by a scalar
    public static Vector2 operator *(Vector2 vector, double scalar)
    {
        return new Vector2(
            vector.X * scalar,
            vector.Y * scalar
        );
    }

    // Convert Vector2 into readable text
    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}


class Program
{
    static void Main()
    {
        // 1. Testing Formatter Method Overloading

        Formatter formatter = new Formatter();

        Console.WriteLine($"Format(7) -> \"{formatter.Format(7)}\"");
        Console.WriteLine($"Format(3.5) -> \"{formatter.Format(3.5)}\"");
        Console.WriteLine($"Format(3, 4) -> \"{formatter.Format(3, 4)}\"");


        // 2, 3 and 4. Testing Notifier and EmailNotifier

        Console.WriteLine();
        Console.WriteLine("Through EmailNotifier variable:");

        // EmailNotifier reference
        EmailNotifier emailNotifier = new EmailNotifier();

        // Calls EmailNotifier's overridden Send()
        emailNotifier.Send();

        // Calls EmailNotifier's hidden Log()
        emailNotifier.Log();


        Console.WriteLine();
        Console.WriteLine("Through Notifier variable:");

        // Base class reference pointing to EmailNotifier object
        Notifier notifier = emailNotifier;

        // Because Send() is virtual, the overridden
        // EmailNotifier version is called.
        notifier.Send();

        // Because Log() is NOT virtual, the Notifier
        // version is called.
        notifier.Log();


        // 5. Testing Vector2 Operator Overloading

        Console.WriteLine();
        Console.WriteLine("Vector2 operations:");

        Vector2 v1 = new Vector2(2, 3);
        Vector2 v2 = new Vector2(4, 5);

        // Uses overloaded + operator
        Vector2 sum = v1 + v2;

        // Uses overloaded * operator
        Vector2 scaled = v1 * 2;

        Console.WriteLine($"v1 + v2 = {sum}");
        Console.WriteLine($"v1 * 2 = {scaled}");
    }
}