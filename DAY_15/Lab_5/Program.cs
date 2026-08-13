using System;
using System.Collections.Generic;


// Address class with auto-implemented properties
public class Address
{
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
}


// Order class
public class Order
{
    // Get-only property initialized through constructor
    public string OrderId { get; }

    // Address where the order will be shipped
    public Address? ShipTo { get; set; }

    // Collection initializer can add items directly
    public List<string> Items { get; set; } = new();

    // Total order amount
    public decimal Total { get; set; }

    // Constructor initializes OrderId
    public Order(string orderId)
    {
        OrderId = orderId;
    }
}


class Program
{
    static void Main()
    {
        // Object initializer with nested Address initializer
        // and collection initializer

        Order order1 = new Order("ORD-1")
        {
            // Nested object initializer
            ShipTo = new Address
            {
                Street = "Main Street",
                City = "Springfield",
                ZipCode = "62701"
            },

            // Collection initializer
            Items =
            {
                "Laptop",
                "Mouse"
            },

            Total = 59.98m
        };


        // Print order details
        Console.WriteLine(
            $"Order {order1.OrderId} ships to " +
            $"{order1.ShipTo?.City} with " +
            $"{order1.Items.Count} items, " +
            $"Total=${order1.Total}"
        );


        // Second order without a shipping address

        Order order2 = new Order("ORD-2")
        {
            ShipTo = null
        };


        // ?. safely checks if ShipTo is null
        if (order2.ShipTo == null)
        {
            Console.WriteLine(
                $"Order {order2.OrderId} has no shipping address " +
                $"set (ShipTo is null)"
            );
        }
    }
}