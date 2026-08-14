using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


// Represents one parsed log entry
public class LogEntry
{
    public string Date { get; init; } = string.Empty;
    public string Time { get; init; } = string.Empty;
    public string Level { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
}


public static class LogParser
{
    // Parse every line into a LogEntry
    public static List<LogEntry> ParseLog(string rawLog)
    {
        List<LogEntry> entries = new List<LogEntry>();

        // Named groups extract date, time, level and message
        string pattern =
            @"^(?<date>\d{4}-\d{2}-\d{2})\s+" +
            @"(?<time>\d{2}:\d{2}:\d{2})\s+" +
            @"(?<level>INFO|WARN|ERROR)\s+" +
            @"(?<message>.*)$";

        // Multiline makes ^ and $ work on every line
        MatchCollection matches = Regex.Matches(
            rawLog,
            pattern,
            RegexOptions.Multiline
        );

        foreach (Match match in matches)
        {
            // Create object using an object initializer
            LogEntry entry = new LogEntry
            {
                Date = match.Groups["date"].Value,
                Time = match.Groups["time"].Value,
                Level = match.Groups["level"].Value,
                Message = match.Groups["message"].Value
            };

            entries.Add(entry);
        }

        return entries;
    }


    // Mask error codes only on ERROR lines
    public static string RedactErrorCodes(string rawLog)
    {
        // Match only ERROR log lines
        string pattern =
            @"^(?<date>\d{4}-\d{2}-\d{2})\s+" +
            @"(?<time>\d{2}:\d{2}:\d{2})\s+" +
            @"ERROR\s+" +
            @"(?<message>.*)$";

        return Regex.Replace(
            rawLog,
            pattern,

            // Runs for every ERROR line
            match =>
            {
                string line = match.Value;

                // Replace code=number with code=***
                line = Regex.Replace(
                    line,
                    @"code=(\d+)",
                    m => "code=***"
                );

                return line;
            },

            RegexOptions.Multiline
        );
    }
}


class Program
{
    static void Main()
    {
        // Sample multi-line log
        string rawLog =
@"2026-08-14 09:15:32 INFO Service started
2026-08-14 09:16:12 WARN Disk usage high
2026-08-14 09:17:03 ERROR Request failed code=404
2026-08-14 09:18:27 INFO Request completed
2026-08-14 09:19:45 ERROR Upstream error code=500
2026-08-14 09:20:17 INFO Shutdown complete";


        // Parse the raw log
        List<LogEntry> entries =
            LogParser.ParseLog(rawLog);

        Console.WriteLine(
            "Parsed " + entries.Count + " entries."
        );


        // Count each log level
        int infoCount = 0;
        int warnCount = 0;
        int errorCount = 0;

        foreach (LogEntry entry in entries)
        {
            if (entry.Level == "INFO")
                infoCount++;

            else if (entry.Level == "WARN")
                warnCount++;

            else if (entry.Level == "ERROR")
                errorCount++;
        }

        Console.WriteLine(
            "Summary: INFO: " + infoCount +
            ", WARN: " + warnCount +
            ", ERROR: " + errorCount
        );


        // Redact error codes
        string redactedLog =
            LogParser.RedactErrorCodes(rawLog);

        Console.WriteLine("\nRedacted log");
        Console.WriteLine(redactedLog);
    }
}