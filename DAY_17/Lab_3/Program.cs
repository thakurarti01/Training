using System;

class Program
{
    // Low-level method that performs division
    static int DivideInternal(int a, int b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException(
                "Cannot divide by zero in DivideInternal"
            );
        }

        return a / b;
    }

    // Correct rethrow
    static int CallSiteGood(int a, int b)
    {
        try
        {
            return DivideInternal(a, b);
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine(
                "[Good] Logging before rethrow..."
            );

            // Preserves the original stack trace
            throw;
        }
    }

    // Incorrect rethrow
    static int CallSiteBad(int a, int b)
    {
        try
        {
            return DivideInternal(a, b);
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine(
                "[Bad] Logging before rethrow..."
            );

            // Resets the stack trace
            throw ex;
        }
    }

    // Throws a new exception for invalid value
    static void Validate(int value)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(value),
                value,
                "Value must not be negative"
            );
        }
    }

    static void Main()
    {
        // Test correct rethrow
        try
        {
            CallSiteGood(10, 0);
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("Good stack trace:");
            Console.WriteLine(ex.StackTrace);
        }

        Console.WriteLine();

        // Test incorrect rethrow
        try
        {
            CallSiteBad(10, 0);
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("Bad stack trace:");
            Console.WriteLine(ex.StackTrace);
        }

        Console.WriteLine();

        // Test validation
        try
        {
            Validate(-5);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine(
                "Validate(-5): " + ex.Message
            );
        }
    }
}