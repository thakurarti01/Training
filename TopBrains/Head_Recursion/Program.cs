using System;

class Program
{
    // Head Recursive Method
    static void SumDigitsReversed(int n)
    {
        // Base case
        if (n == 0)
            return;

        // Recursive call first
        SumDigitsReversed(n / 10);

        // Print digit after recursion returns
        Console.Write(n % 10);
    }

    static void Main()
    {
        Console.Write("Enter a number: ");
        int number = Convert.ToInt32(Console.ReadLine());

        Console.Write("Output: ");
        SumDigitsReversed(number);

        Console.WriteLine();
    }
}