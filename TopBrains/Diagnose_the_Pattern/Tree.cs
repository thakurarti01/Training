using System;

class TreeRecursion
{
    public static void Display(int n)
    {
        if (n <= 0)
            return;

        Console.WriteLine(n);

        // First recursive call
        Display(n - 1);

        // Second recursive call
        Display(n - 2);
    }
}