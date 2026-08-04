using System;

class TimSort
{
    const int RUN = 32;   // Size of each small block

    // Insertion Sort for small blocks
    static void InsertionSort(int[] arr, int left, int right)
    {
        for (int i = left + 1; i <= right; i++)
        {
            int key = arr[i];
            int j = i - 1;

            // Shift greater elements to the right
            while (j >= left && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j--;
            }

            // Insert key at correct position
            arr[j + 1] = key;
        }
    }

    // Merge two sorted halves
    static void Merge(int[] arr, int l, int m, int r)
    {
        int n1 = m - l + 1;
        int n2 = r - m;

        int[] left = new int[n1];
        int[] right = new int[n2];

        Array.Copy(arr, l, left, 0, n1);
        Array.Copy(arr, m + 1, right, 0, n2);

        int i = 0, j = 0, k = l;

        // Compare elements from both arrays
        while (i < n1 && j < n2)
        {
            if (left[i] <= right[j])
                arr[k++] = left[i++];
            else
                arr[k++] = right[j++];
        }

        // Copy remaining elements
        while (i < n1)
            arr[k++] = left[i++];

        while (j < n2)
            arr[k++] = right[j++];
    }

    static void Sort(int[] arr)
    {
        int n = arr.Length;

        // Step 1: Sort every RUN-sized block using Insertion Sort
        for (int i = 0; i < n; i += RUN)
            InsertionSort(arr, i, Math.Min(i + RUN - 1, n - 1));

        // Step 2: Merge sorted blocks
        for (int size = RUN; size < n; size *= 2)
        {
            for (int left = 0; left < n; left += 2 * size)
            {
                int mid = left + size - 1;
                int right = Math.Min(left + 2 * size - 1, n - 1);

                if (mid < right)
                    Merge(arr, left, mid, right);
            }
        }
    }

    static void Main()
    {
        int[] arr = { 5, 21, 7, 23, 19, 10, 3, 12 };

        Sort(arr);

        foreach (int x in arr)
            Console.Write(x + " ");
    }
}