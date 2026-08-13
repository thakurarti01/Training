using System;

// Static utility class
// Cannot be instantiated and contains only static members
public static class StringUtils
{
    // Checks whether a string reads the same forwards and backwards
    public static bool IsPalindrome(string s)
    {
        string reversed = Reverse(s);
        return s == reversed;
    }

    // Reverses the given string
    public static string Reverse(string s)
    {
        char[] chars = s.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }

    // Counts the number of words in a string
    public static int WordCount(string s)
    {
        if (string.IsNullOrWhiteSpace(s))
            return 0;

        return s.Split(
            new[] { ' ', '\t' },
            StringSplitOptions.RemoveEmptyEntries
        ).Length;
    }
}


// Instance-based class that also uses static members
public class TrackedWidget
{
    // Unique ID for each object
    public Guid InstanceId { get; }

    // Shared count of all active objects
    public static int LiveCount { get; private set; }

    // Constructor creates an ID and increases count
    public TrackedWidget()
    {
        InstanceId = Guid.NewGuid();
        LiveCount++;
    }

    // Decreases count when object is disposed
    public void Dispose()
    {
        LiveCount--;
    }

    // Prints object information
    public void PrintInfo()
    {
        Console.WriteLine(
            $"InstanceId: {InstanceId}, LiveCount: {LiveCount}"
        );
    }
}


class Program
{
    static void Main()
    {
        // Testing StringUtils static methods

        Console.WriteLine(
            $"IsPalindrome(\"level\"): {StringUtils.IsPalindrome("level")}"
        );

        Console.WriteLine(
            $"Reverse(\"Hello\"): {StringUtils.Reverse("Hello")}"
        );

        Console.WriteLine(
            $"WordCount(\"quick brown fox jumps\"): " +
            $"{StringUtils.WordCount("quick brown fox jumps")}"
        );


        // Static class cannot be instantiated
        // Uncommenting this will give a compilation error:
        //
        // StringUtils utils = new StringUtils();


        // Testing TrackedWidget

        // Create three objects
        TrackedWidget widget1 = new TrackedWidget();
        TrackedWidget widget2 = new TrackedWidget();
        TrackedWidget widget3 = new TrackedWidget();

        Console.WriteLine(
            $"LiveCount after creating 3 widgets: " +
            $"{TrackedWidget.LiveCount}"
        );

        // Print information for each object
        widget1.PrintInfo();
        widget2.PrintInfo();
        widget3.PrintInfo();

        // Dispose two objects
        widget1.Dispose();
        widget2.Dispose();

        Console.WriteLine(
            $"LiveCount after disposing 2: " +
            $"{TrackedWidget.LiveCount}"
        );
    }
}