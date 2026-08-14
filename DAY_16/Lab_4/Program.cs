using System;
using System.Text.RegularExpressions;

// Reusable Regex patterns
public static class PatternLibrary
{
    // Email validation pattern
    public static readonly Regex Email =
        new Regex(
            @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$",
            RegexOptions.Compiled
        );

    // US phone number pattern
    public static readonly Regex UsPhone =
        new Regex(
            @"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$",
            RegexOptions.Compiled
        );

    // Hex color pattern
    public static readonly Regex HexColor =
        new Regex(
            @"^#[0-9A-Fa-f]{6}$",
            RegexOptions.Compiled
        );


    // Wrapper method for email validation
    public static bool IsValidEmail(string value)
    {
        return Email.IsMatch(value);
    }

    // Wrapper method for phone validation
    public static bool IsValidPhone(string value)
    {
        return UsPhone.IsMatch(value);
    }

    // Wrapper method for hex color validation
    public static bool IsValidHexColor(string value)
    {
        return HexColor.IsMatch(value);
    }
}


class Program
{
    static void Main()
    {
        // TODO 3: IgnoreCase demonstration
        string text = "HELLO";

        // Without IgnoreCase, HELLO != hello
        bool withoutIgnoreCase =
            Regex.IsMatch(text, @"^hello$");

        // With IgnoreCase, uppercase/lowercase are ignored
        bool withIgnoreCase =
            Regex.IsMatch(
                text,
                @"^hello$",
                RegexOptions.IgnoreCase
            );

        Console.WriteLine(
            "IgnoreCase off: " +
            withoutIgnoreCase +
            ", IgnoreCase on: " +
            withIgnoreCase
        );


        // TODO 4: Multiline demonstration
        string multiLine =
            "apple\nbanana\ncherry";

        // ^ matches only the start of the whole string
        MatchCollection withoutMultiline =
            Regex.Matches(
                multiLine,
                @"^"
            );

        // ^ matches the start of every line
        MatchCollection withMultiline =
            Regex.Matches(
                multiLine,
                @"^",
                RegexOptions.Multiline
            );

        Console.WriteLine(
            "Line-start matches WITHOUT Multiline: " +
            withoutMultiline.Count
        );

        Console.WriteLine(
            "Line-start matches WITH Multiline: " +
            withMultiline.Count
        );


        // TODO 5: Test reusable PatternLibrary methods

        // Email: valid and invalid
        Console.WriteLine(
            "IsValidEmail(\"a@b.com\"): " +
            PatternLibrary.IsValidEmail("a@b.com")
        );

        Console.WriteLine(
            "IsValidEmail(\"not-an-email\"): " +
            PatternLibrary.IsValidEmail("not-an-email")
        );


        // Phone: valid and invalid
        Console.WriteLine(
            "IsValidPhone(\"123-456-7890\"): " +
            PatternLibrary.IsValidPhone("123-456-7890")
        );

        Console.WriteLine(
            "IsValidPhone(\"12345\"): " +
            PatternLibrary.IsValidPhone("12345")
        );


        // Hex color: valid and invalid
        Console.WriteLine(
            "IsValidHexColor(\"#1A2B3C\"): " +
            PatternLibrary.IsValidHexColor("#1A2B3C")
        );

        Console.WriteLine(
            "IsValidHexColor(\"GGGGGG\"): " +
            PatternLibrary.IsValidHexColor("GGGGGG")
        );
    }
}