using System;

class Array_Merge
{
    public static void Merge()
    {
        Console.Write("Enter size of 1st array: ");
        int size1 = Convert.ToInt32(Console.ReadLine());

        int[] first_array = new int[size1];

        Console.WriteLine("Enter elements of 1st array:");
        for(int i = 0; i < size1; i++)
        {
            first_array[i] = Convert.ToInt32(Console.ReadLine());

        }

        Console.Write("Enter size of 2nd array: ");
        int size2 = Convert.ToInt32(Console.ReadLine());

        int[] second_array = new int[size2];

        Console.WriteLine("Enter elements of 2nd array:");
        for(int i = 0; i < size2; i++)
        {
            second_array[i] = Convert.ToInt32(Console.ReadLine());
        }

        int[] merged_array = new int[size1+size2];

        for(int i = 0; i < size1; i++)
        {
            merged_array[i] = first_array[i];

        }
        for(int i = 0; i<size2; i++)
        {
            merged_array[size1+i] = second_array[i];
        }

        Console.WriteLine("Merged array: ");
        for(int i = 0; i<merged_array.Length; i++)
        {
            Console.Write(merged_array[i] + " ");

        }

    }
}