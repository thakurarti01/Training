using System;

class Solution
{
    static void Main(string[] args)
    {
        int n = Convert.ToInt32(Console.ReadLine());

        int[] arr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

        for (int i = n - 1; i >= 0; i--)
        {
            Console.Write(arr[i]);

            if (i != 0)
                Console.Write(" ");
        }
    }
}