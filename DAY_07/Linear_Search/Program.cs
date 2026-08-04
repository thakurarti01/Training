using System;

class LinearSearch
{
    static int Search(int[] arr, int key)
    {
        // Check each element one by one
        for (int i = 0; i < arr.Length; i++)
        {
            // If the key is found, return its index
            if (arr[i] == key)
                return i;
        }

        // Key not found
        return -1;
    }

    static void Main()
    {
        Console.Write("Enter the number of elements: ");
        int n = Convert.ToInt32(Console.ReadLine());

        int[] arr = new int[n];

        Console.WriteLine("Enter the array elements:");

        // Take array input from the user
        for (int i = 0; i < n; i++)
        {
            Console.Write("Element " + (i + 1) + ": ");
            arr[i] = Convert.ToInt32(Console.ReadLine());
        }

        Console.Write("Enter the element to search: ");
        int key = Convert.ToInt32(Console.ReadLine());

        int result = Search(arr, key);

        if (result != -1)
            Console.WriteLine("Element found at index: " + result);
        else
            Console.WriteLine("Element not found.");
    }
}