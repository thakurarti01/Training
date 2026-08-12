using System;

public class InventoryItem
{
    // Private fields: outside code cannot directly modify these values
    private int _quantity;
    private decimal _unitPrice;

    // init means Name can be assigned only when the object is created
    public string Name { get; init; }

    // Quantity property with validation
    public int Quantity
    {
        get
        {
            return _quantity;       // Return the stored quantity
        }
        set
        {
            // Quantity cannot be negative
            if (value < 0)
                throw new ArgumentException("Quantity cannot be negative");

            _quantity = value;      // Store valid quantity
        }
    }

    // UnitPrice property with validation
    public decimal UnitPrice
    {
        get
        {
            return _unitPrice;      // Return the stored price
        }
        set
        {
            // Price must be greater than zero
            if (value <= 0)
                throw new ArgumentException(
                    "UnitPrice must be greater than zero");

            _unitPrice = value;     // Store valid price
        }
    }

    // Computed property: no separate field is needed
    // TotalValue is calculated whenever we access it
    public decimal TotalValue
    {
        get
        {
            return Quantity * UnitPrice;
        }
    }

    // Constructor: used to create a valid InventoryItem
    public InventoryItem(string name, int quantity, decimal unitPrice)
    {
        // Validate Name before assigning it
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Name cannot be null or whitespace");

        // Assign through properties so their validation runs
        Name = name;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}

class Program
{
    static void Main()
    {
        // Create a valid InventoryItem
        InventoryItem item = new InventoryItem(
            "Keyboard",
            3,
            45m
        );

        // Display the object's information
        Console.WriteLine(
            $"Created: {item.Name}, Qty={item.Quantity}, Price=${item.UnitPrice:F2}"
        );

        // TotalValue is calculated as Quantity × UnitPrice
        Console.WriteLine($"Total=${item.TotalValue:F2}");

        // Test Quantity validation
        try
        {
            // This should fail because quantity cannot be negative
            item.Quantity = -5;
        }
        catch (ArgumentException ex)
        {
            // Catch and display the expected error
            Console.WriteLine(
                $"Caught expected error setting Quantity=-5: {ex.Message}"
            );
        }

        // Test UnitPrice validation
        try
        {
            // This should fail because price cannot be zero
            item.UnitPrice = 0;
        }
        catch (ArgumentException ex)
        {
            // Catch and display the expected error
            Console.WriteLine(
                $"Caught expected error setting UnitPrice=0: {ex.Message}"
            );
        }
    }
}