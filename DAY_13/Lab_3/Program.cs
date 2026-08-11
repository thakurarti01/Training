using System;

public class Appointment
{
    public string Title { get; }
    public DateTime Start { get; }
    public TimeSpan Duration { get; }
    public string Location { get; }

    public static int DefaultDurationMinutes;

    // Static constructor
    static Appointment()
    {
        Console.WriteLine("Appointment type initialized. Default duration set to 30 minutes.");
        DefaultDurationMinutes = 30;
    }

    // Full constructor
    public Appointment(string title, DateTime start,
                       TimeSpan duration, string location)
    {
        Title = title;
        Start = start;
        Duration = duration;
        Location = location;
    }

    // Two-argument constructor
    public Appointment(string title, DateTime start)
        : this(title, start, TimeSpan.FromMinutes(DefaultDurationMinutes), "TBD")
    {
    }

    // One-argument constructor
    public Appointment(string title)
        : this(title, DateTime.Now.AddDays(1))
    {
    }
}

class Program
{
    static void Main()
    {
        // Using full constructor
        Appointment full = new Appointment(
            "Standup",
            new DateTime(2026, 8, 12, 9, 0, 0),
            TimeSpan.FromMinutes(30),
            "Room 4"
        );

        // Using two-argument constructor
        Appointment twoArg = new Appointment(
            "Client Call",
            new DateTime(2026, 8, 12, 14, 0, 0)
        );

        // Using one-argument constructor
        Appointment oneArg = new Appointment("Follow Up");

        // Print details
        Console.WriteLine(
            $"Full: {full.Title} @ {full.Start:yyyy-MM-dd HH:mm}, " +
            $"{full.Duration.TotalMinutes:0} min, {full.Location}"
        );

        Console.WriteLine(
            $"Two-arg: {twoArg.Title} @ {twoArg.Start:yyyy-MM-dd HH:mm}, " +
            $"{twoArg.Duration.TotalMinutes:0} min, {twoArg.Location}"
        );

        Console.WriteLine(
            $"One-arg: {oneArg.Title} @ {oneArg.Start:yyyy-MM-dd HH:mm}, " +
            $"{oneArg.Duration.TotalMinutes:0} min, {oneArg.Location}"
        );

        Console.WriteLine(
            $"DefaultDurationMinutes: {Appointment.DefaultDurationMinutes}"
        );
    }
}