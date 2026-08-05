using System;

class Program
{
    static void Even(int n)
    {
        if (n == 0)
        {
            Console.WriteLine("Even");
            return;
        }

        Odd(n - 1);
    }

    static void Odd(int n)
    {
        if (n == 0)
        {
            Console.WriteLine("Odd");
            return;
        }

        Even(n - 1);
    }

    static void Main()
    {
        Console.Write("Enter number: ");
        int n = Convert.ToInt32(Console.ReadLine());

        Even(n);
    }
}