using System;

class Program
{
    static void Main()
    {
        // Ask user for number of elements
        Console.Write("Enter number of elements: ");
        int n = int.Parse(Console.ReadLine());

        int[] arr = new int[n];

        // Input sorted array
        Console.WriteLine("Enter elements in sorted order:");
        for (int i = 0; i < n; i++)
        {
            arr[i] = int.Parse(Console.ReadLine());
        }

        // Input key to search
        Console.Write("Enter element to search: ");
        int key = int.Parse(Console.ReadLine());

        int result = FibonacciSearch(arr, key);

        if (result != -1)
            Console.WriteLine("Element found at index: " + result);
        else
            Console.WriteLine("Element not found.");
    }

    static int FibonacciSearch(int[] arr, int key)
    {
        int n = arr.Length;

        // First two Fibonacci numbers
        int fibMMm2 = 0;   // (m-2)'th Fibonacci number
        int fibMMm1 = 1;   // (m-1)'th Fibonacci number

        // Third Fibonacci number
        int fibM = fibMMm2 + fibMMm1;

        // Keep generating Fibonacci numbers until
        // fibM becomes greater than or equal to array size
        while (fibM < n)
        {
            fibMMm2 = fibMMm1;
            fibMMm1 = fibM;
            fibM = fibMMm2 + fibMMm1;
        }

        // Marks eliminated range from front of array
        // Initially nothing is eliminated
        int offset = -1;

        // Continue until there are elements to inspect
        while (fibM > 1)
        {
            // Calculate index to compare
            // Math.Min prevents index from going outside array
            int i = Math.Min(offset + fibMMm2, n - 1);

            // If key is greater, move to right subarray
            if (arr[i] < key)
            {
                // Eliminate left part including current element
                offset = i;

                // Move Fibonacci numbers down by one position
                fibM = fibMMm1;
                fibMMm1 = fibMMm2;
                fibMMm2 = fibM - fibMMm1;
            }

            // If key is smaller, move to left subarray
            else if (arr[i] > key)
            {
                // Move Fibonacci numbers down by two positions
                fibM = fibMMm2;
                fibMMm1 = fibMMm1 - fibMMm2;
                fibMMm2 = fibM - fibMMm1;
            }

            // Element found
            else
            {
                return i;
            }
        }

        // Check if last remaining element is the key
        if (fibMMm1 == 1 &&
            offset + 1 < n &&
            arr[offset + 1] == key)
        {
            return offset + 1;
        }

        // Element not found
        return -1;
    }
}