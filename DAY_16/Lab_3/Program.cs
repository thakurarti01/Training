using System;
using System.Text.RegularExpressions;
using System.Globalization;

class Program
{
    static void Main()
    {
        // TODO 1: Parse log using named groups
        string logLine =
            "2026-08-14 09:15:32 ERROR Connection timed out";

        string logPattern =
            @"^(?<date>\d{4}-\d{2}-\d{2})\s+" +
            @"(?<time>\d{2}:\d{2}:\d{2})\s+" +
            @"(?<level>[A-Z]+)\s+" +
            @"(?<message>.+)$";

        Match logMatch = Regex.Match(logLine, logPattern);

        if (logMatch.Success)
        {
            // Access values using the group names
            Console.WriteLine(
                "date=" + logMatch.Groups["date"].Value);

            Console.WriteLine(
                "time=" + logMatch.Groups["time"].Value);

            Console.WriteLine(
                "level=" + logMatch.Groups["level"].Value);

            Console.WriteLine(
                "message=" + logMatch.Groups["message"].Value);
        }


        // TODO 2: Find key=value pairs using named groups
        string kvText = "name=Alice;age=30;city=NYC";

        string kvPattern =
            @"(?<key>[A-Za-z]+)=(?<value>[^;]+)";

        MatchCollection pairs = Regex.Matches(
            kvText,
            kvPattern
        );

        foreach (Match pair in pairs)
        {
            Console.WriteLine(
                pair.Groups["key"].Value +
                "=" +
                pair.Groups["value"].Value
            );
        }


        // TODO 3: Format numbers using MatchEvaluator
        string numbers =
            "Revenue: 1234567, Costs: 89000";

        string formattedNumbers = Regex.Replace(
            numbers,
            @"\b\d+\b",

            // Runs for every number found
            match =>
            {
                long number = long.Parse(match.Value);

                // N0 adds thousands separators
                return number.ToString(
                    "N0",
                    CultureInfo.InvariantCulture
                );
            }
        );

        Console.WriteLine(formattedNumbers);


        // TODO 4: Convert ALL CAPS words to Title Case
        string shouting =
            "THIS IS URGENT please respond";

        string converted = Regex.Replace(
            shouting,
            @"\b[A-Z]{2,}\b",

            // Runs for every ALL CAPS word
            match =>
            {
                string word = match.Value.ToLower();

                // Capitalize only the first letter
                return char.ToUpper(word[0]) +
                       word.Substring(1);
            }
        );

        Console.WriteLine(converted);
    }
}