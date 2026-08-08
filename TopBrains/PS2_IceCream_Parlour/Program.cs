using System;
using System.Linq;

class Solution
{
    public static int[] icecreamParlor(int money, int[] cost)
    {
        // Check every pair of ice cream prices
        for (int i = 0; i < cost.Length; i++)
        {
            for (int j = i + 1; j < cost.Length; j++)
            {
                // If the two prices add up to the money
                if (cost[i] + cost[j] == money)
                {
                    // +1 because HackerRank uses 1-based indexing
                    return new int[] { i + 1, j + 1 };
                }
            }
        }

        return new int[] { };
    }

    public static void Main(string[] args)
    {
        int t = int.Parse(Console.ReadLine());

        for (int test = 0; test < t; test++)
        {
            int money = int.Parse(Console.ReadLine());
            int n = int.Parse(Console.ReadLine());

            int[] cost = Console.ReadLine()
                                .Split()
                                .Select(int.Parse)
                                .ToArray();

            int[] result = icecreamParlor(money, cost);

            Console.WriteLine($"{result[0]} {result[1]}");
        }
    }
}