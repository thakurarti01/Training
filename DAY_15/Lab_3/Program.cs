using System;

// Subscription class demonstrating different property accessors
public class Subscription
{
    // Get-only property: can be read but not changed outside
    public string Id { get; }

    // Normal property: can be read and changed
    public string PlanName { get; set; } = string.Empty;

    // Init-only: can be set only during object creation
    public DateTime StartedAt { get; init; }

    // Public get, private set: outside can read, class can modify
    public bool IsActive { get; private set; } = true;

    // Computed property: calculates months from start date
    public int MonthsActive =>
        (DateTime.Now.Year - StartedAt.Year) * 12
        + DateTime.Now.Month - StartedAt.Month;

    // Constructor initializes the get-only Id
    public Subscription(string id)
    {
        Id = id;
    }

    // Changes IsActive using the private setter
    public void Cancel()
    {
        IsActive = false;
    }
}

class Program
{
    static void Main()
    {
        // Create object and set init property
        Subscription subscription = new Subscription("SUB-1")
        {
            PlanName = "Pro",
            StartedAt = new DateTime(2026, 1, 1)
        };

        // Display all subscription details
        Console.WriteLine(
            $"Id={subscription.Id}, " +
            $"Plan={subscription.PlanName}, " +
            $"Started={subscription.StartedAt:yyyy-MM-dd}, " +
            $"Active={subscription.IsActive}, " +
            $"MonthsActive={subscription.MonthsActive}"
        );

        // Cancel subscription
        subscription.Cancel();

        Console.WriteLine(
            $"After Cancel(): Active={subscription.IsActive}"
        );

        // Cannot modify because setter is private
        // subscription.IsActive = true;

        // Cannot modify because StartedAt uses init
        // subscription.StartedAt = DateTime.Now;
    }
}