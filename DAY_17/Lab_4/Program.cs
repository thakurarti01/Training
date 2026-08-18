using System;

class Program
{
    // Low-level method
    static string ReadConfig(string key)
    {
        if (key == "timeout")
        {
            // Simulate invalid configuration
            throw new FormatException(
                "Value 'abc' is not a valid integer"
            );
        }

        return "Value is a valid integer";
    }

    // Converts low-level error into meaningful error
    static int GetTimeout()
    {
        try
        {
            string value = ReadConfig("timeout");

            return int.Parse(value);
        }
        catch (FormatException ex)
        {
            // Keep original exception as InnerException
            throw new InvalidOperationException(
                "Application configuration is invalid",
                ex
            );
        }
    }

    // Prints the complete exception chain
    static void PrintExceptionChain(Exception ex)
    {
        int depth = 0;

        while (ex != null)
        {
            Console.WriteLine(
                $"Depth {depth}: " +
                $"{ex.GetType().Name}: {ex.Message}"
            );

            // Move to the original exception
            ex = ex.InnerException;
            depth++;
        }
    }

    static void Main()
    {
        try
        {
            GetTimeout();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(
                "Top-level: " + ex.Message
            );

            Console.WriteLine(
                "Inner: " +
                ex.InnerException?.Message
            );

            Console.WriteLine(
                "Inner exception type: " +
                ex.InnerException?.GetType().Name
            );

            Console.WriteLine(
                "\nPrintExceptionChain:"
            );

            PrintExceptionChain(ex);
        }
    }
}