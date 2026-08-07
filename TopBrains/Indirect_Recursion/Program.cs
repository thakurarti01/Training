using System;

class Program
{
    // Handles positive numbers
    static bool IsPositiveChain(int n)
    {
        // Base case
        if (n == 0)
            return true;

        Console.WriteLine("Positive Chain : " + n);

        // Reduce by 1 and call the other function
        return IsNegativeChain(n - 1);
    }

    // Handles negative numbers
    static bool IsNegativeChain(int n)
    {
        // Base case
        if (n == 0)
            return true;

        Console.WriteLine("Negative Chain : " + n);

        // Increase by 1 and call the other function
        return IsPositiveChain(n + 1);
    }

    static void Main()
    {
        Console.Write("Enter a number: ");
        int number = Convert.ToInt32(Console.ReadLine());

        bool result;

        if (number >= 0)
            result = IsPositiveChain(number);
        else
            result = IsNegativeChain(number);

        Console.WriteLine("Reached Zero: " + result);
    }
}