using System;

// Base custom exception
class OrderValidationException : Exception
{
    public string FieldName { get; }

    // Standard constructors
    public OrderValidationException()
        : base()
    {
    }

    public OrderValidationException(string message)
        : base(message)
    {
    }

    public OrderValidationException(
        string message,
        Exception inner)
        : base(message, inner)
    {
    }

    // Constructor with field name
    public OrderValidationException(
        string message,
        string fieldName)
        : base(message)
    {
        FieldName = fieldName;
    }

    public OrderValidationException(
        string message,
        string fieldName,
        Exception inner)
        : base(message, inner)
    {
        FieldName = fieldName;
    }
}


// Specific exception for missing fields
class MissingFieldException : OrderValidationException
{
    public MissingFieldException(string fieldName)
        : base(
            $"Missing field: {fieldName}",
            fieldName)
    {
    }
}


// Specific exception for invalid quantity
class InvalidQuantityException : OrderValidationException
{
    public InvalidQuantityException(string fieldName)
        : base(
            $"Invalid quantity for field: {fieldName}",
            fieldName)
    {
    }
}


class Program
{
    // Validates order information
    static decimal ValidateOrder(
        string customerName,
        int quantity,
        decimal unitPrice)
    {
        if (string.IsNullOrWhiteSpace(customerName))
        {
            throw new MissingFieldException(
                "customerName"
            );
        }

        if (quantity <= 0)
        {
            throw new InvalidQuantityException(
                "quantity"
            );
        }

        if (unitPrice < 0)
        {
            throw new OrderValidationException(
                "Unit price cannot be negative",
                "unitPrice"
            );
        }

        // Return total when validation succeeds
        return quantity * unitPrice;
    }


    // Simulates database operation
    static void SaveOrder(
        string customerName,
        int quantity,
        decimal unitPrice)
    {
        throw new InvalidOperationException(
            "Database unavailable"
        );
    }


    // Processes and validates order
    static void ProcessOrder(
        string customerName,
        int quantity,
        decimal unitPrice)
    {
        try
        {
            // Validate before saving
            decimal total = ValidateOrder(
                customerName,
                quantity,
                unitPrice
            );

            try
            {
                SaveOrder(
                    customerName,
                    quantity,
                    unitPrice
                );
            }
            catch (InvalidOperationException ex)
            {
                // Wrap database error
                throw new OrderValidationException(
                    "Could not save order",
                    "database",
                    ex
                );
            }

            Console.WriteLine(
                $"Order total: ${total:F2}"
            );
        }
        catch (MissingFieldException ex)
        {
            Console.WriteLine(
                $"Missing field: {ex.FieldName}"
            );
        }
        catch (InvalidQuantityException ex)
        {
            Console.WriteLine(
                $"Invalid quantity for field: {ex.FieldName}"
            );
        }
        catch (OrderValidationException ex)
        {
            Console.WriteLine(
                $"Order validation failed: {ex.Message}"
            );

            if (ex.InnerException != null)
            {
                Console.WriteLine(
                    $"(caused by: " +
                    $"{ex.InnerException.Message})"
                );
            }
        }
        finally
        {
            // Always executes
            Console.WriteLine(
                "Order attempt complete."
            );
        }
    }


    static void Main()
    {
        // Missing customer
        Console.WriteLine("- Missing customer name");
        ProcessOrder("", 2, 99.98m);

        Console.WriteLine();

        // Invalid quantity
        Console.WriteLine("- Zero quantity");
        ProcessOrder("Arti", 0, 99.98m);

        Console.WriteLine();

        // Negative price
        Console.WriteLine("- Negative price");
        ProcessOrder("Arti", 2, -10m);

        Console.WriteLine();

        // Valid order but database fails
        Console.WriteLine(
            "- Valid order, SaveOrder fails"
        );
        ProcessOrder("Arti", 2, 99.98m);
    }
}