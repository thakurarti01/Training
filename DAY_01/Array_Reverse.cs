using System;

class Array_Reverse
{
    public static void Reverse()
    {
        Console.Write("Enter the size of the array: ");
        int size = Convert.ToInt32(Console.ReadLine());

        int[] array = new int[size];

        Console.WriteLine("Enter the elements of the array:");
        for (int i = 0; i < size; i++)
        {
            array[i] = Convert.ToInt32(Console.ReadLine());
        }

        Console.WriteLine("Reversed array:");
        for (int i = size - 1; i >= 0; i--)
        {
            Console.Write(array[i] + " ");
        }
    }
}