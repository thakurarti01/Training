using System;

class Program
{
    static void Print(int n)
    {
        if (n == 0)
            return;

        Console.WriteLine(n);

        Print(n - 1);
    }

    static void Main()
    {
        Print(5);
    }
}