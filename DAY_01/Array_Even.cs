using System;

class Array_Even
{
    public static void Even()
    {
        Console.Write("Enter the size of the array: ");
        int size = Convert.ToInt32(Console.ReadLine());

        int[] array = new int[size];

        int evenCount = 0;
        int oddCount = 0;

        Console.WriteLine("Enter the elements of the array:");

        for (int i = 0; i < size; i++)
        {
            array[i] = Convert.ToInt32(Console.ReadLine());
        }

        Console.Write("Even elements are: ");
        for (int i = 0; i < size; i++)
        {
            if (array[i] % 2 == 0)
            {
                Console.Write(array[i] + " ");
                evenCount++;
            }
        }
        Console.WriteLine();
        Console.WriteLine("No. of even elements: " + evenCount);

        Console.WriteLine();

        Console.Write("Odd elements are: ");
        for (int i = 0; i < size; i++)
        {
            if (array[i] % 2 != 0)
            {
                Console.Write(array[i] + " ");
                oddCount++;
            }
        }
        Console.WriteLine();
        Console.WriteLine("No. of odd elements: " + oddCount);
    }
}