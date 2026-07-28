using System;

class Array_SecLargest
{
    public static void SecLargest()
    {
        Console.WriteLine("Enter the size of the array:");
        int size = Convert.ToInt32(Console.ReadLine());

        int[] array = new int[size];

        Console.WriteLine("Enter elements of the array:");
        for (int i = 0; i < size; i++)
        {
            array[i] = Convert.ToInt32(Console.ReadLine());
        }

        int largest = array[0];
        int secondLargest = int.MinValue;   

        for (int i = 1; i < size; i++)
        {
            if (array[i] > largest)
            {
                secondLargest = largest;
                largest = array[i];
            }
            else if (array[i] > secondLargest && array[i] != largest)
            {
                secondLargest = array[i];
            }
        }

        if (secondLargest == int.MinValue)
        {
            Console.WriteLine("Second largest element does not exist.");
        }
        else
        {
            Console.WriteLine("Second largest element is: " + secondLargest);
        }
    }
}