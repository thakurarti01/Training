using System;

class IntroSort
{
    static void Sort(int[] arr)
    {
        /*
         * C# provides a built-in sorting method: Array.Sort().
         *
         * Internally, Array.Sort() uses IntroSort (Introspective Sort)
         * for most primitive data types like int, double, etc.
         *
         * Why are we using Array.Sort() instead of writing IntroSort manually?
         * ---------------------------------------------------------------
         * The actual IntroSort implementation is very lengthy
         * (around 150–300+ lines of code).
         *
         * It combines three different sorting algorithms:
         *
         * 1. Quick Sort
         *    - Used initially because it is very fast on average.
         *
         * 2. Heap Sort
         *    - If Quick Sort recursion becomes too deep
         *      (which may lead to O(n²) performance),
         *      IntroSort switches to Heap Sort to guarantee
         *      O(n log n) worst-case time complexity.
         *
         * 3. Insertion Sort
         *    - When the remaining partition becomes very small
         *      (usually around 16 elements or fewer),
         *      it switches to Insertion Sort because it is
         *      faster than Quick Sort on small arrays.
         *
         * Overall Process:
         *
         * Start
         *   ↓
         * Quick Sort
         *   ↓
         * Is recursion depth too large?
         *      Yes → Switch to Heap Sort
         *      No  → Continue Quick Sort
         *   ↓
         * Is partition very small?
         *      Yes → Use Insertion Sort
         *   ↓
         * Array Sorted
         *
         * Therefore, calling Array.Sort() is the standard
         * and recommended way to use IntroSort in C#.
         */

        Array.Sort(arr);
    }

    static void Main()
    {
        int[] arr = { 20, 10, 50, 30, 70, 60 };

        Sort(arr);

        Console.WriteLine("Sorted Array:");
        foreach (int x in arr)
            Console.Write(x + " ");
    }
}