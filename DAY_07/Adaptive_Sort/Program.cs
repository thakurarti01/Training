using System;

class AdaptiveSort
{
    static void InsertionSort(int[] arr)
    {
        // Start from the second element because
        // the first element is already considered sorted.
        for (int i = 1; i < arr.Length; i++)
        {
            // Store the current element.
            // It will be inserted into its correct position
            // in the sorted part of the array.
            int key = arr[i];

            // Start comparing from the previous element.
            int j = i - 1;

            // Compare the key with elements on its left.
            // If an element is greater than the key,
            // shift it one position to the right
            // to make space for the key.
            while (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j--;
            }

            // The correct position for the key has been found.
            // Place the key there.
            arr[j + 1] = key;
        }
    }

    static void Main()
    {
        int[] arr = { 5, 6, 7, 2, 8, 9 };

        InsertionSort(arr);

        Console.WriteLine("Sorted Array:");

        foreach (int num in arr)
            Console.Write(num + " ");
    }
}