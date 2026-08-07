using System;

class Program
{
    // Tail Recursive Factorial
    static int Factorial(int n, int accumulator = 1)
    {
        // Base case
        if (n == 0)
            return accumulator;

        // Recursive call is the last operation
        return Factorial(n - 1, accumulator * n);
    }

    static void Main()
    {
        Console.Write("Enter a number: ");
        int number = Convert.ToInt32(Console.ReadLine());

        int result = Factorial(number);

        Console.WriteLine("Factorial = " + result);
    }
}