using System;
using System.Collections.Generic;
using System.Linq;

// Abstract class provides a common structure for all notification channels
public abstract class NotificationChannel
{
    // Concrete method that safely tries to send a message
    public bool TrySend(string message)
    {
        try
        {
            // Calls the abstract Send() method
            return Send(message);
        }
        catch
        {
            // If Send() throws an exception, return false
            return false;
        }
    }

    // Child classes must provide their own implementation of Send()
    protected abstract bool Send(string message);
}

// Email channel
public class EmailChannel : NotificationChannel
{
    // Email sending always succeeds
    protected override bool Send(string message)
    {
        return true;
    }
}

// SMS channel
public class SmsChannel : NotificationChannel
{
    protected override bool Send(string message)
    {
        // SMS cannot contain more than 160 characters
        if (message.Length > 160)
        {
            // Throw exception so TrySend() can catch it
            throw new Exception("SMS message is too long");
        }

        // If message is 160 characters or less, sending succeeds
        return true;
    }
}

class Program
{
    static void Main()
    {
        // Create a list containing different notification channels
        List<NotificationChannel> channels = new List<NotificationChannel>
        {
            new EmailChannel(),
            new SmsChannel()
        };

        // Short message - both Email and SMS should succeed
        string shortMessage = "Hello, this is a short notification.";

        // Long message - SMS should fail because it exceeds 160 characters
        string longMessage = new string('A', 161);

        // Store the results of sending messages
        var results = new List<(NotificationChannel Channel, bool Success)>
        {
            (channels[0], channels[0].TrySend(shortMessage)),
            (channels[1], channels[1].TrySend(shortMessage)),
            (channels[0], channels[0].TrySend(longMessage)),
            (channels[1], channels[1].TrySend(longMessage))
        };

        // LINQ + anonymous type: create a lightweight report
        var report = results.Select(r => new
        {
            ChannelType = r.Channel.GetType().Name,
            Success = r.Success
        });

        // Print each report entry
        foreach (var item in report)
        {
            Console.WriteLine(
                $"{item.ChannelType}: {(item.Success ? "Success" : "Failed")}"
            );
        }

        // Count successful and failed results
        int succeeded = report.Count(r => r.Success);
        int failed = report.Count(r => !r.Success);

        Console.WriteLine();
        Console.WriteLine($"Succeeded: {succeeded}");
        Console.WriteLine($"Failed: {failed}");
    }
}