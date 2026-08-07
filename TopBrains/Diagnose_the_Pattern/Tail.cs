using System;

class TailRecursion
{
    public static void Display(int n)
    {
        if (n == 0)
            return;

        // Work first
        Console.WriteLine(n);

        // Recursive call last
        Display(n - 1);
    }
}