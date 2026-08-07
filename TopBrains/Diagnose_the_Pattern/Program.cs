using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("===== Recursion Patterns =====");
        Console.WriteLine("1. Head Recursion");
        Console.WriteLine("2. Tail Recursion");
        Console.WriteLine("3. Tree Recursion");
        Console.WriteLine("4. Indirect Recursion");

        Console.Write("\nEnter your choice: ");
        int choice = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter a number: ");
        int n = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine();

        switch (choice)
        {
            case 1:
                Console.WriteLine("Head Recursion:");
                HeadRecursion.Display(n);
                break;

            case 2:
                Console.WriteLine("Tail Recursion:");
                TailRecursion.Display(n);
                break;

            case 3:
                Console.WriteLine("Tree Recursion:");
                TreeRecursion.Display(n);
                break;

            case 4:
                Console.WriteLine("Indirect Recursion:");
                IndirectRecursion.MethodA(n);
                break;

            default:
                Console.WriteLine("Invalid Choice");
                break;
        }
    }
}