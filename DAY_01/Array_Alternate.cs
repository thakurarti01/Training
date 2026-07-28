using System;

class Array_Alternate
{
    public static void Alternate()
    {
        Console.Write("Enter the size of the array: ");
        int size = Convert.ToInt32(Console.ReadLine());

        int[] array = new int[size];

        Console.WriteLine("Enter the elements of the array:");
        for (int i = 0; i < size; i++)
        {
            array[i] = Convert.ToInt32(Console.ReadLine());
        }

        Console.WriteLine("Alternate elements are:");
        for (int i = 0; i < size; i += 2)
        {
            Console.Write(array[i] + " ");
        }
    }
}