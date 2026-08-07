using System;

class IndirectRecursion
{
    public static void MethodA(int n)
    {
        if (n <= 0)
            return;

        Console.WriteLine("A : " + n);

        MethodB(n - 1);
    }

    public static void MethodB(int n)
    {
        if (n <= 0)
            return;

        Console.WriteLine("B : " + n);

        MethodA(n - 1);
    }
}