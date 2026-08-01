using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.Write("Enter n: ");
        int n = int.Parse(Console.ReadLine());

        Console.Write("Enter number of queries: ");
        int q = int.Parse(Console.ReadLine());

        List<int>[] seq = new List<int>[n];

        for (int i = 0; i < n; i++)
        {
            seq[i] = new List<int>();
        }

        int lastAnswer = 0;

        Console.WriteLine("Enter queries:");

        for (int i = 0; i < q; i++)
        {
            string[] input = Console.ReadLine().Split();

            int type = int.Parse(input[0]);
            int x = int.Parse(input[1]);
            int y = int.Parse(input[2]);

            int index = (x ^ lastAnswer) % n;

            if (type == 1)
            {
                seq[index].Add(y);
            }
            else if (type == 2)
            {
                lastAnswer = seq[index][y % seq[index].Count];
                Console.WriteLine(lastAnswer);
            }
        }
    }
}