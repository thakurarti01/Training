using System;

class BinarySearch
{
    static int Search(int[] arr, int key)
    {
        int left = 0;
        int right = arr.Length - 1;

        // Continue until the search space becomes empty
        while (left <= right)
        {
            // Find the middle index
            int mid = (left + right) / 2;

            // If middle element is the key
            if (arr[mid] == key)
                return mid;

            // If key is greater, search the right half
            else if (arr[mid] < key)
                left = mid + 1;

            // If key is smaller, search the left half
            else
                right = mid - 1;
        }

        // Key not found
        return -1;
    }

    static void Main()
    {
        Console.Write("Enter the number of elements: ");
        int n = Convert.ToInt32(Console.ReadLine());

        int[] arr = new int[n];

        Console.WriteLine("Enter the array elements in sorted order:");

        // Take sorted array input from the user
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