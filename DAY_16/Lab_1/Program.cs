using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        // TODO 1: ZIP code - 5 digits or 5+4 digits
        string zipPattern = @"^\d{5}(-\d{4})?$";

        Console.WriteLine("ZIP \"12345\": " +
            Regex.IsMatch("12345", zipPattern));

        Console.WriteLine("ZIP \"12345-6789\": " +
            Regex.IsMatch("12345-6789", zipPattern));

        Console.WriteLine("ZIP \"1234\": " +
            Regex.IsMatch("1234", zipPattern));


        // TODO 2: Username - 3 to 16 chars, no spaces, cannot start with digit
        string usernamePattern = @"^[A-Za-z_][A-Za-z0-9_]{2,15}$";

        Console.WriteLine("\nUsername \"user 1\": " +
            Regex.IsMatch("user 1", usernamePattern));

        Console.WriteLine("Username \"luser\": " +
            Regex.IsMatch("luser", usernamePattern));

        Console.WriteLine("Username \"ab\": " +
            Regex.IsMatch("ab", usernamePattern));


        // TODO 3: Hex color - # followed by exactly 6 hex digits
        string hexPattern = @"^#[0-9A-Fa-f]{6}$";

        Console.WriteLine("\nHex \"#1A2B3C\": " +
            Regex.IsMatch("#1A2B3C", hexPattern));

        Console.WriteLine("Hex \"#GGGGGG\": " +
            Regex.IsMatch("#GGGGGG", hexPattern));

        Console.WriteLine("Hex \"1A2B3C\": " +
            Regex.IsMatch("1A2B3C", hexPattern));


        // TODO 4: Password - at least 8 chars, one uppercase and one digit
        // Multiple checks are easier to understand than one large Regex.
        string password = "Password1";

        bool passwordValid =
            password.Length >= 8 &&
            Regex.IsMatch(password, @"[A-Z]") &&
            Regex.IsMatch(password, @"\d");

        Console.WriteLine("\nPassword \"Password1\": " + passwordValid);

        password = "password";

        passwordValid =
            password.Length >= 8 &&
            Regex.IsMatch(password, @"[A-Z]") &&
            Regex.IsMatch(password, @"\d");

        Console.WriteLine("Password \"password\": " + passwordValid);

        password = "pass1";

        passwordValid =
            password.Length >= 8 &&
            Regex.IsMatch(password, @"[A-Z]") &&
            Regex.IsMatch(password, @"\d");

        Console.WriteLine("Password \"pass1\": " + passwordValid);


        // TODO 5: Sentence - letters/spaces followed by one . ! or ?
        string sentencePattern = @"^[A-Za-z ]+[.!?]$";

        Console.WriteLine("\nSentence \"Hello there.\": " +
            Regex.IsMatch("Hello there.", sentencePattern));

        Console.WriteLine("Sentence \"Wait...\": " +
            Regex.IsMatch("Wait...", sentencePattern));

        Console.WriteLine("Sentence \"Really?\": " +
            Regex.IsMatch("Really?", sentencePattern));
    }
}