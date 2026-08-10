using System;

class Lab1
{
    static void Main(string[] args)
    {
        // Original string
        string original = " Hello, Training Team! ";

        // TODO 1: Trim the string into a new variable
        string trimmed = original.Trim();

        // TODO 2: Compare original and trimmed using ReferenceEquals
        Console.WriteLine(
            "ReferenceEquals(original, trimmed): " +
            object.ReferenceEquals(original, trimmed)
        );

        // TODO 3: Contains, StartsWith, IndexOf and Replace

        Console.WriteLine(
            "Contains \"Training\": " +
            trimmed.Contains("Training")
        );

        Console.WriteLine(
            "StartsWith trimmed \"Hello\": " +
            trimmed.StartsWith("Hello")
        );

        Console.WriteLine(
            "Index of first comma: " +
            trimmed.IndexOf(',')
        );

        string replaced = trimmed.Replace(
            "Training Team",
            "Engineering Team"
        );

        Console.WriteLine(
            "\"Training Team\" replaced -> " +
            replaced
        );

        // TODO 4: Split on spaces and commas
        string[] words = trimmed.Split(
            new char[] { ' ', ',' },
            StringSplitOptions.RemoveEmptyEntries
        );

        foreach (string word in words)
        {
            Console.WriteLine(word);
        }

        // TODO 5: IsNullOrWhiteSpace checks
        Console.WriteLine(
            "IsNullOrWhiteSpace(null): " +
            string.IsNullOrWhiteSpace(null)
        );

        Console.WriteLine(
            "IsNullOrWhiteSpace(\"\"): " +
            string.IsNullOrWhiteSpace("")
        );

        Console.WriteLine(
            "IsNullOrWhiteSpace(\"   \"): " +
            string.IsNullOrWhiteSpace("   ")
        );

        Console.WriteLine(
            "IsNullOrWhiteSpace(\"Ok\"): " +
            string.IsNullOrWhiteSpace("Ok")
        );
    }
}