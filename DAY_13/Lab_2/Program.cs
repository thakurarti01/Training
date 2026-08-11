using System;

public class LibraryBook
{
    // Private field
    private string _isbn;

    // Public field
    public string Title;

    // Protected field
    protected string ShelfLocation = "Unassigned";

    // Internal field
    internal int CopiesAvailable;

    // Static field
    public static int TotalBooksCreated;

    // Constructor
    public LibraryBook(string title, string isbn)
    {
        Title = title;
        _isbn = isbn;

        // Default copies = 1
        CopiesAvailable = 1;

        // Increase total books
        TotalBooksCreated++;
    }

    // Protected internal method
    protected internal void Relocate(string newLocation)
    {
        ShelfLocation = newLocation;
    }

    // Private protected method
    private protected void AdjustCopies(int delta)
    {
        CopiesAvailable += delta;
    }
}

// Derived class
public class ReferenceBook : LibraryBook
{
    public ReferenceBook(string title, string isbn)
        : base(title, isbn)
    {
    }

    public void PrintLocation()
    {
        // Accessing protected field
        Console.WriteLine(
            $"ReferenceBook shelf location before Relocate: {ShelfLocation}"
        );

        // Calling protected internal method
        Relocate("Reference Section");

        Console.WriteLine(
            $"ReferenceBook shelf location after Relocate: {ShelfLocation}"
        );

        // Calling private protected method
        AdjustCopies(2);

        Console.WriteLine(
            $"Copies available after AdjustCopies(+2): {CopiesAvailable}"
        );
    }
}

class Program
{
    static void Main()
    {
        // Create three LibraryBook objects
        LibraryBook book1 = new LibraryBook("C# Basics", "ISBN001");
        Console.WriteLine(
            $"Book 1 created. Total books so far: {LibraryBook.TotalBooksCreated}"
        );

        LibraryBook book2 = new LibraryBook("C# Advanced", "ISBN002");
        Console.WriteLine(
            $"Book 2 created. Total books so far: {LibraryBook.TotalBooksCreated}"
        );

        LibraryBook book3 = new LibraryBook("C# Programming", "ISBN003");
        Console.WriteLine(
            $"Book 3 created. Total books so far: {LibraryBook.TotalBooksCreated}"
        );

        Console.WriteLine();

        // Create a ReferenceBook
        ReferenceBook referenceBook =
            new ReferenceBook("C# Reference", "ISBN004");

        referenceBook.PrintLocation();
    }
}