using System;
using System.Text;

static class StringToolkit
{
    // 1. Reverse a string
    public static string Reverse(string input)
    {
        char[] chars = input.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }

    // 2. Count occurrences of a character
    public static int CountChar(string text, char searchChar)
    {
        int count = 0;

        foreach (char c in text)
        {
            if (c == searchChar)
            {
                count++;
            }
        }

        return count;
    }

    // 3. Remove duplicate characters
    public static string RemoveDuplicates(string input)
    {
        StringBuilder result = new StringBuilder();

        foreach (char c in input)
        {
            if (!result.ToString().Contains(c.ToString()))
            {
                result.Append(c);
            }
        }

        return result.ToString();
    }

    // 4. Check whether a string is a palindrome
    // Ignores spaces and case
    public static bool IsPalindrome(string input)
    {
        string cleaned = input.Replace(" ", "").ToLower();

        string reversed = Reverse(cleaned);

        return cleaned == reversed;
    }

    // 5. Convert string to Title Case
    public static string ToTitleCase(string input)
    {
        string[] words = input.ToLower().Split(' ');

        StringBuilder result = new StringBuilder();

        foreach (string word in words)
        {
            if (word.Length > 0)
            {
                result.Append(
                    char.ToUpper(word[0]) +
                    word.Substring(1)
                );

                result.Append(" ");
            }
        }

        return result.ToString().Trim();
    }

    // 6. Extract only digit characters
    public static string ExtractNumbers(string input)
    {
        StringBuilder result = new StringBuilder();

        foreach (char c in input)
        {
            if (char.IsDigit(c))
            {
                result.Append(c);
            }
        }

        return result.ToString();
    }

    // Main 
    public static void Main()
    {
        Console.WriteLine("Reverse(\"Hello\")");
        Console.WriteLine("-> " + Reverse("Hello"));

        Console.WriteLine();

        Console.WriteLine("CountChar(\"mississippi\", 's')");
        Console.WriteLine("-> " + CountChar("mississippi", 's'));

        Console.WriteLine();

        Console.WriteLine("RemoveDuplicates(\"mississippi\")");
        Console.WriteLine("-> " + RemoveDuplicates("mississippi"));

        Console.WriteLine();

        Console.WriteLine("IsPalindrome(\"race car\")");
        Console.WriteLine("-> " + IsPalindrome("race car"));

        Console.WriteLine();

        Console.WriteLine("ToTitleCase(\"hello training team\")");
        Console.WriteLine("-> " + ToTitleCase("hello training team"));

        Console.WriteLine();

        Console.WriteLine("ExtractNumbers(\"Order #4521, qty 3\")");
        Console.WriteLine("-> " + ExtractNumbers("Order #4521, qty 3"));
    }
}