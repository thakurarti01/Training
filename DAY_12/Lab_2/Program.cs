using System;
using System.Diagnostics;
using System.Text;

class Lab2
{
    // Build string using normal string concatenation
    static string BuildWithString(int count)
    {
        string result = "";

        for (int i = 0; i < count; i++)
        {
            result += "Append";
        }

        return result;
    }

    // Build string using StringBuilder
    static string BuildWithStringBuilder(int count)
    {
        StringBuilder result = new StringBuilder(count * 6);

        for (int i = 0; i < count; i++)
        {
            result.Append("Append");
        }

        return result.ToString();
    }

    // Measure execution time
    static void Measure(int count)
    {
        Stopwatch stopwatch = new Stopwatch();

        // Measure string concatenation
        stopwatch.Start();
        BuildWithString(count);
        stopwatch.Stop();

        long stringTime = stopwatch.ElapsedMilliseconds;

        // Measure StringBuilder
        stopwatch.Restart();
        BuildWithStringBuilder(count);
        stopwatch.Stop();

        long stringBuilderTime = stopwatch.ElapsedMilliseconds;

        Console.WriteLine($"String concatenation ({count:N0} items): {stringTime} ms");
        Console.WriteLine($"StringBuilder ({count:N0} items): {stringBuilderTime} ms");

        if (stringBuilderTime > 0)
        {
            double ratio = (double)stringTime / stringBuilderTime;
            Console.WriteLine(
                $"StringBuilder is roughly {ratio:F1}x faster on this run"
            );
        }
        else
        {
            Console.WriteLine(
                "StringBuilder completed too quickly to calculate an accurate ratio."
            );
        }

        Console.WriteLine();
    }

    static void Main()
    {
        // Test with 50,000 items
        Measure(50000);

        // Test with 200,000 items
        Measure(200000);
    }
}