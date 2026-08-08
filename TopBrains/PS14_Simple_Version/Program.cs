using System;
using System.Collections.Generic;
using System.Linq;

class Solution
{
    /*
        
        SUMMARY:
        
        We have an n x m grid.

        Some cells contain train tracks.

        We need to find how many cells do NOT contain tracks.

        Formula:

            Free Cells = Total Cells - Occupied Cells

        Total Cells = n * m

        The main challenge is that tracks can overlap.
        Therefore, we group tracks by row, sort them, and merge
        overlapping tracks before counting them.
    */

    public static long gridlandMetro(
        int n,
        int m,
        int k,
        List<List<int>> track)
    {
        /*
            --------------------------------------------------------
            STEP 1: Group tracks by row
            --------------------------------------------------------

            Dictionary key   = row number
            Dictionary value = list of (start column, end column)

            Example:

            Input:
                2 3 6
                2 5 8
                4 1 3

            Dictionary:

                Row 2 -> (3,6), (5,8)
                Row 4 -> (1,3)
        */

        Dictionary<int, List<(int start, int end)>> rows =
            new Dictionary<int, List<(int start, int end)>>();


        // Go through every track
        foreach (var t in track)
        {
            int row = t[0];
            int start = t[1];
            int end = t[2];

            // If this row doesn't exist in dictionary,
            // create an empty list for it.
            if (!rows.ContainsKey(row))
            {
                rows[row] = new List<(int, int)>();
            }

            // Add the track to that row.
            rows[row].Add((start, end));
        }


        /*
            This variable stores the total number of cells
            occupied by train tracks.
        */

        long occupied = 0;


        
            
            // STEP 2: Process every row that contains tracks
            
        

        foreach (var row in rows)
        {
            /*
                Sort tracks according to their starting column.

                Example:

                    (5,8)
                    (2,4)
                    (3,7)

                becomes:

                    (2,4)
                    (3,7)
                    (5,8)
            */

            row.Value.Sort(
                (a, b) => a.start.CompareTo(b.start)
            );


            /*
                ----------------------------------------------------
                STEP 3: Start with the first track
                ----------------------------------------------------

                Suppose sorted tracks are:

                    (2,5)
                    (4,8)
                    (10,12)

                Initially:

                    currentStart = 2
                    currentEnd   = 5
            */

            int currentStart = row.Value[0].start;
            int currentEnd = row.Value[0].end;


            // STEP 4: Check the remaining tracks
                
            

            for (int i = 1; i < row.Value.Count; i++)
            {
                int start = row.Value[i].start;
                int end = row.Value[i].end;


                /*
                    Check whether the new track overlaps
                    or touches the current track.

                    Example:

                    Current = (2,5)
                    New     = (4,8)

                    Since:

                        4 <= 5 + 1

                    they overlap/touch.

                    Therefore merge them.
                */

                if (start <= currentEnd + 1)
                {
                    /*
                        Extend the current track.

                        Example:

                        Current = (2,5)
                        New     = (4,8)

                        Merged = (2,8)
                    */

                    currentEnd = Math.Max(currentEnd, end);
                }
                else
                {
                    /*
                        The new track does NOT overlap.

                        Therefore the current track is finished.

                        Count its cells:

                            currentEnd - currentStart + 1
                    */

                    occupied += currentEnd - currentStart + 1;


                    
                        // Start processing the new track.
                    

                    currentStart = start;
                    currentEnd = end;
                }
            }


            /*
                ----------------------------------------------------
                STEP 5: Count the last merged track
                ----------------------------------------------------

                The last track hasn't been added inside the loop,
                so we add it here.
            */

            occupied += currentEnd - currentStart + 1;
        }


        /*
            --------------------------------------------------------
            STEP 6: Calculate total number of cells
            --------------------------------------------------------

            Important:

            n and m can be very large.

            Therefore we use long.

            Example:

                n = 1,000,000,000
                m = 1,000,000,000

                n * m = 1,000,000,000,000,000,000

            This is too large for int.
        */

        long totalCells = (long)n * m;


        
            
            // STEP 7: Calculate free cells
            
        

        long freeCells = totalCells - occupied;


        return freeCells;
    }


    
        // MAIN METHOD
    

    public static void Main(string[] args)
    {
        /*
            First line contains:

                n m k

            n = number of rows
            m = number of columns
            k = number of tracks
        */

        string[] firstLine = Console.ReadLine().Split();

        int n = int.Parse(firstLine[0]);
        int m = int.Parse(firstLine[1]);
        int k = int.Parse(firstLine[2]);


        /*
            Create a list to store all tracks.

            Each track contains:

                row
                starting column
                ending column
        */

        List<List<int>> track = new List<List<int>>();


        
           //  Read all k tracks.
        

        for (int i = 0; i < k; i++)
        {
            track.Add(
                Console.ReadLine()
                       .Split()
                       .Select(int.Parse)
                       .ToList()
            );
        }


        
            // Call the main function.
        

        long result = gridlandMetro(n, m, k, track);


        
            // Print the number of cells without tracks.
        

        Console.WriteLine(result);
    }
}