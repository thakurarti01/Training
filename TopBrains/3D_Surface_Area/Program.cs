using System;

class Program
{
    static void Main()
    {
        string[] firstLine = Console.ReadLine().Split(' ');

        int H = int.Parse(firstLine[0]);
        int W = int.Parse(firstLine[1]);

        int[,] A = new int[H, W];

        for (int i = 0; i < H; i++)
        {
            string[] row = Console.ReadLine().Split(' ');

            for (int j = 0; j < W; j++)
            {
                A[i, j] = int.Parse(row[j]);
            }
        }

        int surfaceArea = 0;

        for (int i = 0; i < H; i++)
        {
            for (int j = 0; j < W; j++)
            {
                int height = A[i, j];

                if (height == 0)
                    continue;

                // Top and Bottom
                surfaceArea += 2;

                // Front
                if (i == 0)
                    surfaceArea += height;
                else
                    surfaceArea += Math.Max(0, height - A[i - 1, j]);

                // Back
                if (i == H - 1)
                    surfaceArea += height;
                else
                    surfaceArea += Math.Max(0, height - A[i + 1, j]);

                // Left
                if (j == 0)
                    surfaceArea += height;
                else
                    surfaceArea += Math.Max(0, height - A[i, j - 1]);

                // Right
                if (j == W - 1)
                    surfaceArea += height;
                else
                    surfaceArea += Math.Max(0, height - A[i, j + 1]);
            }
        }

        Console.WriteLine(surfaceArea);
    }
}
