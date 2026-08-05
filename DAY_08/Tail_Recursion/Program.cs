using System;

class Program
{
    static void Print(int n)
    {
        if (n == 0)
            return;

        Console.WriteLine(n);

        // Last statement
        Print(n - 1);
    }

    static void Main()
    {
        Print(5);
    }
}