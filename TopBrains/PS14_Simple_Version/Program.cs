using System;
using System.Collections.Generic;
using System.Linq;

class Solution
{
    public static long gridlandMetro(
        int n,
        int m,
        int k,
        List<List<int>> track)
    {
        // Store tracks grouped by row
        Dictionary<int, List<(int start, int end)>> rows =
            new Dictionary<int, List<(int start, int end)>>();

        foreach (var t in track)
        {
            int row = t[0];
            int start = t[1];
            int end = t[2];

            if (!rows.ContainsKey(row))
                rows[row] = new List<(int, int)>();

            rows[row].Add((start, end));
        }

        long occupied = 0;

        // Process every row containing tracks
        foreach (var row in rows)
        {
            // Sort tracks by starting column
            row.Value.Sort((a, b) => a.start.CompareTo(b.start));

            int currentStart = row.Value[0].start;
            int currentEnd = row.Value[0].end;

            for (int i = 1; i < row.Value.Count; i++)
            {
                int start = row.Value[i].start;
                int end = row.Value[i].end;

                // Overlapping or adjacent track
                if (start <= currentEnd + 1)
                {
                    currentEnd = Math.Max(currentEnd, end);
                }
                else
                {
                    // Add the previous merged track
                    occupied += currentEnd - currentStart + 1;

                    // Start a new track
                    currentStart = start;
                    currentEnd = end;
                }
            }

            // Add the last merged track
            occupied += currentEnd - currentStart + 1;
        }

        // Total cells - cells occupied by tracks
        long totalCells = (long)n * m;

        return totalCells - occupied;
    }

    public static void Main(string[] args)
    {
        string[] firstLine = Console.ReadLine().Split();

        int n = int.Parse(firstLine[0]);
        int m = int.Parse(firstLine[1]);
        int k = int.Parse(firstLine[2]);

        List<List<int>> track = new List<List<int>>();

        for (int i = 0; i < k; i++)
        {
            track.Add(
                Console.ReadLine()
                       .Split()
                       .Select(int.Parse)
                       .ToList()
            );
        }

        long result = gridlandMetro(n, m, k, track);

        Console.WriteLine(result);
    }
}