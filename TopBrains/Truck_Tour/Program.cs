using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter number of petrol pumps: ");
        int n = int.Parse(Console.ReadLine());

        int[] petrol = new int[n];
        int[] distance = new int[n];

        Console.WriteLine("Enter petrol and distance:");

        for (int i = 0; i < n; i++)
        {
            string[] input = Console.ReadLine().Split();

            petrol[i] = int.Parse(input[0]);
            distance[i] = int.Parse(input[1]);
        }

        int start = 0;
        int balance = 0;
        int deficit = 0;

        for (int i = 0; i < n; i++)
        {
            balance += petrol[i] - distance[i];

            if (balance < 0)
            {
                start = i + 1;
                deficit += balance;
                balance = 0;
            }
        }

        if (balance + deficit >= 0)
            Console.WriteLine("Starting Pump Index: " + start);
        else
            Console.WriteLine("No Solution");
    }
}