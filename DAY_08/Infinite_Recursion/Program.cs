using System;

class Program
{
    static void Print()
    {
        Console.WriteLine("Hello");

        Print();
    }

    static void Main()
    {
        Print();
    }
}