using System;

class Program
{
    static void Main()
    {
        int[,] arr = new int[6, 6];

        Console.WriteLine("Enter 36 elements:");

        // Input
        for (int i = 0; i < 6; i++)
        {
            string[] input = Console.ReadLine().Split();

            for (int j = 0; j < 6; j++)
            {
                arr[i, j] = int.Parse(input[j]);
            }
        }

        int maxSum = int.MinValue;

        // Find all hourglasses
        for (int i = 0; i <= 3; i++)
        {
            for (int j = 0; j <= 3; j++)
            {
                int sum =
                    arr[i, j] + arr[i, j + 1] + arr[i, j + 2] +
                    arr[i + 1, j + 1] +
                    arr[i + 2, j] + arr[i + 2, j + 1] + arr[i + 2, j + 2];

                if (sum > maxSum)
                {
                    maxSum = sum;
                }
            }
        }

        Console.WriteLine("Maximum Hourglass Sum = " + maxSum);
    }
}