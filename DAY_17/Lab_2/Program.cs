using System;

class Program
{
    // Demonstrates that finally always executes
    static void Process(int mode)
    {
        Console.WriteLine("Opening");

        try
        {
            // Mode 1 throws an exception
            if (mode == 1)
            {
                throw new InvalidOperationException(
                    "Simulated failure"
                );
            }

            Console.WriteLine("Working");

            // Mode 2 returns early
            if (mode == 2)
            {
                return;
            }

            Console.WriteLine("Finishing normally");
        }
        finally
        {
            // Always executes
            Console.WriteLine("Closing");
        }
    }

    // Simulated resource class
    class MyDisposable : IDisposable
    {
        public MyDisposable()
        {
            Console.WriteLine("Handle opened");
        }

        public void Dispose()
        {
            Console.WriteLine("Handle closed");
        }
    }

    static void Main()
    {
        // Normal execution
        Console.WriteLine("Process(0)");
        Process(0);

        Console.WriteLine();

        // Exception execution
        Console.WriteLine("Process(1)");

        try
        {
            Process(1);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine();

        // Return still executes finally
        Console.WriteLine("Process(2)");
        Process(2);

        Console.WriteLine();

        // using automatically calls Dispose()
        Console.WriteLine("Using MyDisposable");

        try
        {
            using (MyDisposable resource = new MyDisposable())
            {
                throw new Exception("Simulated exception");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}