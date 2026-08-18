using System;

class Program
{
    public static void Main()
    {
        int[] numbers = { 8, 16, 32, 64, 128, 256, 512 };

        try
        {
            int numerator = numbers[7];
            int denominator = 0;

            int result = numerator / denominator;

            Console.WriteLine("Result: " + result);
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Cannot divide by zero.");
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("Element not found.");
        }
        finally
        {
            Console.WriteLine("Finally block.");
        }
    }
}