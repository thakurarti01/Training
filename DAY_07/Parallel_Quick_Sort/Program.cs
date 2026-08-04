using System;
using System.Threading.Tasks;

class ParallelQuickSort
{
    // Partition function
    static int Partition(int[] arr, int low, int high)
    {
        // Choose the last element as pivot
        int pivot = arr[high];

        // Index of smaller element
        int i = low - 1;

        // Compare every element with pivot
        for (int j = low; j < high; j++)
        {
            // If current element is smaller than pivot
            if (arr[j] < pivot)
            {
                i++;

                // Swap arr[i] and arr[j]
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

        // Place pivot at its correct position
        int t = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = t;

        return i + 1;
    }

    static void ParallelQuickSortMethod(int[] arr, int low, int high)
    {
        if (low < high)
        {
            // Find pivot position
            int pi = Partition(arr, low, high);

            // Sort left and right partitions simultaneously
            Parallel.Invoke(
                () => ParallelQuickSortMethod(arr, low, pi - 1),
                () => ParallelQuickSortMethod(arr, pi + 1, high)
            );
        }
    }

    static void Main()
    {
        int[] arr = { 10, 7, 8, 9, 1, 5, 12, 4, 15, 2 };

        Console.WriteLine("Original Array:");
        foreach (int num in arr)
            Console.Write(num + " ");

        ParallelQuickSortMethod(arr, 0, arr.Length - 1);

        Console.WriteLine("\n\nSorted Array:");
        foreach (int num in arr)
            Console.Write(num + " ");
    }
}