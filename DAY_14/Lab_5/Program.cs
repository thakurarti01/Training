using System;
using System.Collections.Generic;
using System.Linq;

// 1. Interface for anything that has an ID
public interface IIdentifiable
{
    string Id { get; }
}

// PaymentMethod extends IIdentifiable
// It adds DisplayName and Charge()
public interface IPaymentMethod : IIdentifiable
{
    string DisplayName { get; }

    PaymentResult Charge(decimal amount);
}

// 2. Small encapsulated result class
public class PaymentResult
{
    // Indicates whether payment was successful
    public bool Success { get; }

    // Message describing the result
    public string Message { get; }

    // Constructor validates the message
    public PaymentResult(bool success, string message)
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message));

        Success = success;
        Message = message;
    }
}

// 3. Abstract base class
// Implements IPaymentMethod
public abstract class PaymentMethodBase : IPaymentMethod
{
    // ID of the payment method
    public string Id { get; }

    // Display name of the payment method
    public string DisplayName { get; }

    // Protected constructor
    // Only this class and its child classes can call it
    protected PaymentMethodBase(string id, string displayName)
    {
        Id = id;
        DisplayName = displayName;
    }

    // Abstract method
    // Child classes must provide their own Charge() implementation
    public abstract PaymentResult Charge(decimal amount);
}

// 4. Credit card payment method
public class CreditCardPayment : PaymentMethodBase
{
    // Constructor passes ID and display name to base class
    public CreditCardPayment(string id, string displayName)
        : base(id, displayName)
    {
    }

    // Credit card fails when amount is greater than 5000
    public override PaymentResult Charge(decimal amount)
    {
        if (amount > 5000)
        {
            return new PaymentResult(
                false,
                "Credit card limit exceeded"
            );
        }

        return new PaymentResult(
            true,
            "Credit card payment successful"
        );
    }
}

// CashPayment is sealed
// No other class can inherit from it
public sealed class CashPayment : PaymentMethodBase
{
    public CashPayment(string id, string displayName)
        : base(id, displayName)
    {
    }

    // Cash payment always succeeds
    public override PaymentResult Charge(decimal amount)
    {
        return new PaymentResult(
            true,
            "Cash payment successful"
        );
    }
}

/*
    This would NOT compile because CashPayment is sealed:

    public class InvalidCashPayment : CashPayment
    {
    }
*/

class Program
{
    static void Main()
    {
        // 5. Create a list containing different payment methods
        List<IPaymentMethod> payments = new List<IPaymentMethod>
        {
            new CreditCardPayment("CC-1", "Visa ...1234"),
            new CashPayment("CASH-1", "Cash Drawer")
        };

        // Amounts to test
        decimal[] amounts =
        {
            1500m,
            6000m
        };

        // Charge each payment method with each amount
        var results = new List<(IPaymentMethod Payment, decimal Amount,
                                PaymentResult Result)>();

        foreach (IPaymentMethod payment in payments)
        {
            foreach (decimal amount in amounts)
            {
                PaymentResult result = payment.Charge(amount);

                results.Add((payment, amount, result));
            }
        }

        // Anonymous type + LINQ
        // Creates the settlement report without creating a Report class
        var report = results.Select(r => new
        {
            Id = r.Payment.Id,
            DisplayName = r.Payment.DisplayName,
            AmountAttempted = r.Amount,
            Success = r.Result.Success
        });

        // Print the settlement report
        Console.WriteLine("Settlement Report");
        Console.WriteLine("-----------------");

        foreach (var item in report)
        {
            Console.WriteLine(
                $"{item.Id} | {item.DisplayName} | " +
                $"AmountAttempted={item.AmountAttempted:F2} | " +
                $"Success={item.Success}"
            );
        }

        // Calculate total amount successfully settled
        decimal totalSuccessful = report
            .Where(r => r.Success)
            .Sum(r => r.AmountAttempted);

        Console.WriteLine();
        Console.WriteLine(
            $"Total successfully settled: {totalSuccessful:F2}"
        );
    }
}