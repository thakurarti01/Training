using System;

class Array_Sum
{
    public static void Sum()
    {
        Console.Write("Enter the size of the array: ");
        int size = Convert.ToInt32(Console.ReadLine());

        int[] array = new int[size];
        int sum = 0;

        Console.WriteLine("Enter the elements of the array:");

        for (int i = 0; i < size; i++)
        {
            array[i] = Convert.ToInt32(Console.ReadLine());
            sum += array[i];
        }

        Console.WriteLine("Sum of elements = " + sum);
    }
}