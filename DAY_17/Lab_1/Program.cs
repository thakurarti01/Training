using System;

class Program
{
    // Parses and validates the age
    static int ParseAge(string input)
    {
        Console.WriteLine("Step 1");

        // Throws FormatException for non-numeric input
        int age = int.Parse(input);

        // Throws exception if age is outside valid range
        if (age < 0 || age > 150)
        {
            throw new ArgumentOutOfRangeException(
                "input",
                age,
                "Age must be between 0 and 150"
            );
        }

        // Runs only when the input is valid
        Console.WriteLine("Step 2 (only if valid)");

        return age;
    }

    static void Main()
    {
        // Test 1: Non-numeric input
        Console.WriteLine("ParseAge(\"abc\")");

        try
        {
            ParseAge("abc");
        }
        catch (FormatException ex)
        {
            Console.WriteLine(
                "Caught FormatException: " + ex.Message
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                "Caught general Exception: " + ex.Message
            );
        }

        Console.WriteLine();

        // Test 2: Number outside allowed range
        try
        {
            ParseAge("200");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine(
                "Caught ArgumentOutOfRangeException: "
                + ex.Message
            );
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(
                "Caught ArgumentException: " + ex.Message
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                "Caught general Exception: " + ex.Message
            );
        }

        Console.WriteLine();

        // Test 3: Valid input
        try
        {
            int result = ParseAge("30");

            Console.WriteLine("Result: " + result);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        /*
        WRONG ORDER:

        catch (Exception ex)
        {
        }
        catch (ArgumentException ex)
        {
        }

        This does not compile because Exception is the
        parent of ArgumentException, so the second catch
        becomes unreachable.
        */
    }
}