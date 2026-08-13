using System;
using System.Collections.Generic;


// 1. PLAYLIST CLASS

// Playlist is a custom collection that stores song names.
public class Playlist
{
    // Private List used to store all song titles.
    // readonly means we cannot assign a new List,
    // but we can still add or modify items inside the List.
    private readonly List<string> _songs = new();


    // Property that returns the total number of songs.
    // We use Count from the List.
    public int Count
    {
        get { return _songs.Count; }
    }


    // Method to add a new song to the playlist.
    public void Add(string title)
    {
        _songs.Add(title);
    }


    // INDEXER

    // This indexer allows us to access songs using an integer
    // index, just like we access elements of an array.
    //
    // Example:
    // playlist[0]
    // playlist[1]
    //
    // get -> returns the song at the given index.
    // set -> replaces the song at the given index.
    public string this[int index]
    {
        get
        {
            return _songs[index];
        }

        set
        {
            _songs[index] = value;
        }
    }
}



// 2. TEAM ROSTER CLASS

// TeamRoster stores player names along with jersey numbers.
public class TeamRoster
{
    // Dictionary stores:
    // Key   -> Player name
    // Value -> Jersey number
    private readonly Dictionary<string, int> _numbers = new();


    // STRING INDEXER

    // This indexer allows us to access a player's jersey
    // number using the player's name.
    //
    // Example:
    // roster["Alice"]
    //
    // If the player exists, we return their jersey number.
    // If the player does not exist, we return -1.
    public int this[string playerName]
    {
        get
        {
            // TryGetValue checks whether the player exists.
            if (_numbers.TryGetValue(playerName, out int number))
            {
                return number;
            }

            // If player is not found, return -1.
            return -1;
        }

        set
        {
            // Add the player and jersey number.
            // If the player already exists, the value is updated.
            _numbers[playerName] = value;
        }
    }
}



// 3. MATRIX CLASS

// Matrix is a custom collection that represents
// a two-dimensional grid.
public class Matrix
{
    // Two-dimensional integer array.
    // It will store values using rows and columns.
    private readonly int[,] _cells;


    // Constructor receives the number of rows and columns.
    public Matrix(int rows, int cols)
    {
        // Create a 2D array of the specified size.
        _cells = new int[rows, cols];
    }


    // TWO-PARAMETER INDEXER

    // This indexer allows us to access the Matrix
    // using both row and column.
    //
    // Example:
    // matrix[0, 1]
    //
    // First parameter = row
    // Second parameter = column
    public int this[int row, int col]
    {
        get
        {
            // Return the value at the specified row and column.
            return _cells[row, col];
        }

        set
        {
            // Store the new value at the specified position.
            _cells[row, col] = value;
        }
    }
}



class Program
{
    static void Main()
    {
        // PLAYLIST TEST

        // Create a Playlist object.
        Playlist playlist = new Playlist();

        // Add three songs.
        playlist.Add("Song A");
        playlist.Add("Song B");
        playlist.Add("Song C");


        // Use the indexer to replace the second song.
        //
        // Index starts from 0:
        // playlist[0] -> Song A
        // playlist[1] -> Song B
        // playlist[2] -> Song C
        //
        // So playlist[1] replaces Song B.
        playlist[1] = "Song B (Replaced)";


        // Print all songs using the indexer getter.
        Console.Write("Playlist: ");

        for (int i = 0; i < playlist.Count; i++)
        {
            Console.Write(playlist[i]);

            // Print comma between songs.
            if (i < playlist.Count - 1)
            {
                Console.Write(", ");
            }
        }

        Console.WriteLine();


        // TEAM ROSTER TEST

        // Create TeamRoster object.
        TeamRoster roster = new TeamRoster();


        // Add players using the string indexer.
        //
        // Player name is used as the index
        // and jersey number is assigned as the value.
        roster["Alice"] = 7;
        roster["Bob"] = 10;
        roster["Charlie"] = 23;


        // Access Alice's jersey number using her name.
        Console.WriteLine(
            $"TeamRoster - Alice: {roster["Alice"]}"
        );


        // Zoe does not exist in the dictionary.
        // Therefore, the indexer returns -1.
        Console.WriteLine(
            $"TeamRoster - Zoe (not on roster): {roster["Zoe"]}"
        );


        // MATRIX TEST

        // Create a 3 x 3 Matrix.
        //
        // 3 rows and 3 columns.
        Matrix matrix = new Matrix(3, 3);


        // Set some values using the two-parameter indexer.
        matrix[0, 0] = 3;
        matrix[0, 1] = 4;
        matrix[0, 2] = 5;

        matrix[1, 0] = 6;
        matrix[1, 1] = 7;
        matrix[1, 2] = 8;

        matrix[2, 0] = 9;
        matrix[2, 1] = 10;
        matrix[2, 2] = 11;


        // Print the complete Matrix.
        // We use two loops:
        // Outer loop -> moves through rows.
        // Inner loop -> moves through columns.
        Console.WriteLine("Matrix:");

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                // Read the value using the Matrix indexer.
                Console.Write(matrix[row, col] + " ");
            }

            // Move to the next row.
            Console.WriteLine();
        }
    }
}