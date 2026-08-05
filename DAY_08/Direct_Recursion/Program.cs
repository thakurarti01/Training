using System;

class Program
{
    static int Factorial(int n)
    {
        // Base case
        if (n == 1)
            return 1;

        // Recursive call
        return n * Factorial(n - 1);
    }

    static void Main()
    {
        Console.Write("Enter a number: ");
        int n = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Factorial = " + Factorial(n));
    }
}