using System;

class Program
{
    static void Main()
    {
        try
        {
            int number = 512;
            int zero = 0;

            int result = number / zero;

            Console.WriteLine("Result: " + result);
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Cannot divide by zero.");
        }
    }
}