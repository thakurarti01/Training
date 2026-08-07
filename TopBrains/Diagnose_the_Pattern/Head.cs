using System;

class HeadRecursion
{
    public static void Display(int n)
    {
        if (n == 0)
            return;

        // Recursive call first
        Display(n - 1);

        // Work after recursion
        Console.WriteLine(n);
    }
}