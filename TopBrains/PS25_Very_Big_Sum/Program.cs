using System;

class Program
{
    static void Main()
    {
        // Enter the number of elements
        int n = int.Parse(Console.ReadLine());

        // Enter n space-separated integers
        string[] input = Console.ReadLine().Split(' ');

        long sum = 0;

        // Calculate the sum
        for (int i = 0; i < n; i++)
        {
            sum += long.Parse(input[i]);
        }

        // Display the result
        Console.WriteLine(sum);
    }
}
