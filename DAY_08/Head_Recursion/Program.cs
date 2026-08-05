using System;

class Program
{
    static void Print(int n)
    {
        if (n == 0)
            return;

        Print(n - 1);

        Console.WriteLine(n);
    }

    static void Main()
    {
        Print(5);
    }
}