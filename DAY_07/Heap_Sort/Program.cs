using System;

class HeapSort
{
    // Function to maintain Max Heap property
    static void Heapify(int[] arr, int n, int i)
    {
        int largest = i;          // Assume current node is largest
        int left = 2 * i + 1;     // Left child index
        int right = 2 * i + 2;    // Right child index

        // Check if left child exists and is greater
        if (left < n && arr[left] > arr[largest])
            largest = left;

        // Check if right child exists and is greater
        if (right < n && arr[right] > arr[largest])
            largest = right;

        // If largest is not the parent, swap them
        if (largest != i)
        {
            int temp = arr[i];
            arr[i] = arr[largest];
            arr[largest] = temp;

            // Heapify the affected subtree
            Heapify(arr, n, largest);
        }
    }

    static void Sort(int[] arr)
    {
        int n = arr.Length;

        // Step 1: Build Max Heap
        // Start from last non-leaf node and move upwards
        for (int i = n / 2 - 1; i >= 0; i--)
            Heapify(arr, n, i);

        // Step 2: One by one move largest element to the end
        for (int i = n - 1; i >= 0; i--)
        {
            // Swap root (largest) with last element
            int temp = arr[0];
            arr[0] = arr[i];
            arr[i] = temp;

            // Restore heap property for remaining elements
            Heapify(arr, i, 0);
        }
    }

    static void Main()
    {
        int[] arr = { 12, 11, 13, 5, 6, 7 };

        Sort(arr);

        Console.WriteLine("Sorted Array:");
        foreach (int x in arr)
            Console.Write(x + " ");
    }
}