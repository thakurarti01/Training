using System;

class Program
{
    static int Fun(int n)
    {
        if (n > 100)
            return n - 10;

        return Fun(Fun(n + 11));
    }

    static void Main()
    {
        Console.WriteLine(Fun(95));
    }
}