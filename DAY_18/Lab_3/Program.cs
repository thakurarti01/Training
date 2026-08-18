// ### Lab 3 — `Dictionary<K,V>` Inventory Lookup

// Build an **Inventory Lookup System**.

// 1. Use `Dictionary<string, int>` where key = SKU code, value = quantity on hand.
// 2. Load at least 8 sample SKUs.
// 3. Implement:
//    - `RestockItem(sku, quantity)` — adds to existing quantity, or inserts if new (use `TryGetValue`/indexer — no unnecessary `ContainsKey` + indexer double-lookup).
//    - `SellItem(sku, quantity)` — throws a custom exception `InsufficientStockException` if not enough stock.
//    - `LowStockReport(int threshold)` — returns all SKUs with quantity below threshold, using an iteration technique appropriate for `Dictionary<K,V>`.
// 4. Handle the "key not found" case gracefully everywhere (no unhandled `KeyNotFoundException`).

// **Deliverable:** Console app; demonstrate a successful restock, a successful sale, an attempted oversell (exception caught and reported), and a low-stock report.

// ---

using System;
using System.Collections.Generic;


// Custom exception used when someone tries to sell
// more items than are currently available.
class InsufficientStockException : Exception
{
    public InsufficientStockException(string message)
        : base(message)
    {
    }
}


// Inventory class manages all SKU and quantity information.
class Inventory
{
    // Dictionary is suitable here because each SKU is unique
    // and we need to quickly find its available quantity.
    private Dictionary<string, int> inventory =
        new Dictionary<string, int>();


    // ---------------------------------------------------------
    // RestockItem
    // ---------------------------------------------------------

    public void RestockItem(string sku, int quantity)
    {
        // TryGetValue checks whether the SKU exists and
        // gives us its current quantity in one lookup.
        if (inventory.TryGetValue(sku, out int currentQuantity))
        {
            // If SKU already exists, add the new quantity
            // to the existing stock.
            inventory[sku] = currentQuantity + quantity;
        }
        else
        {
            // If SKU does not exist, create a new entry.
            inventory[sku] = quantity;
        }

        Console.WriteLine(
            $"Restocked {sku} by {quantity} units.");
    }


    // ---------------------------------------------------------
    // SellItem
    // ---------------------------------------------------------

    public void SellItem(string sku, int quantity)
    {
        // TryGetValue prevents KeyNotFoundException
        // when the requested SKU does not exist.
        if (!inventory.TryGetValue(sku, out int currentQuantity))
        {
            Console.WriteLine(
                $"SKU {sku} was not found.");

            return;
        }


        // Selling more than available stock is not allowed,
        // so we throw our custom exception.
        if (quantity > currentQuantity)
        {
            throw new InsufficientStockException(
                $"Insufficient stock for {sku}. " +
                $"Available: {currentQuantity}, " +
                $"Requested: {quantity}");
        }


        // Reduce the stock after a successful sale.
        inventory[sku] = currentQuantity - quantity;

        Console.WriteLine(
            $"Sold {quantity} units of {sku}.");
    }


    // ---------------------------------------------------------
    // LowStockReport
    // ---------------------------------------------------------

    public void LowStockReport(int threshold)
    {
        Console.WriteLine(
            $"\nItems with stock below {threshold}:");


        // Dictionary can be directly iterated using
        // KeyValuePair to access both SKU and quantity.
        foreach (KeyValuePair<string, int> item in inventory)
        {
            // item.Key = SKU
            // item.Value = quantity
            if (item.Value < threshold)
            {
                Console.WriteLine(
                    $"{item.Key} -> {item.Value} units");
            }
        }
    }


    // Displays the complete inventory.
    public void DisplayInventory()
    {
        Console.WriteLine("\nCurrent Inventory:");

        foreach (KeyValuePair<string, int> item in inventory)
        {
            Console.WriteLine(
                $"{item.Key} -> {item.Value} units");
        }
    }
}


class Program
{
    static void Main()
    {
        Inventory inventory = new Inventory();


        // =====================================================
        // 1. Load at least 8 sample SKUs
        // =====================================================

        Console.WriteLine("===== LOADING INVENTORY =====");

        inventory.RestockItem("SKU001", 20);
        inventory.RestockItem("SKU002", 15);
        inventory.RestockItem("SKU003", 8);
        inventory.RestockItem("SKU004", 50);
        inventory.RestockItem("SKU005", 5);
        inventory.RestockItem("SKU006", 30);
        inventory.RestockItem("SKU007", 12);
        inventory.RestockItem("SKU008", 25);

        inventory.DisplayInventory();


        // =====================================================
        // 2. Successful Restock
        // =====================================================

        Console.WriteLine("\n===== SUCCESSFUL RESTOCK =====");

        // SKU003 already exists, so its quantity will increase.
        inventory.RestockItem("SKU003", 10);

        inventory.DisplayInventory();


        // =====================================================
        // 3. Successful Sale
        // =====================================================

        Console.WriteLine("\n===== SUCCESSFUL SALE =====");

        // SKU001 has 20 units, so selling 5 is allowed.
        inventory.SellItem("SKU001", 5);

        inventory.DisplayInventory();


        // =====================================================
        // 4. Attempted Oversell
        // =====================================================

        Console.WriteLine("\n===== ATTEMPTED OVERSELL =====");

        try
        {
            // SKU005 has only 5 units,
            // so trying to sell 20 throws our custom exception.
            inventory.SellItem("SKU005", 20);
        }
        catch (InsufficientStockException ex)
        {
            // Catch the custom exception so the program
            // reports the problem instead of crashing.
            Console.WriteLine(
                "Exception caught: " + ex.Message);
        }


        // =====================================================
        // 5. Key Not Found Case
        // =====================================================

        Console.WriteLine("\n===== KEY NOT FOUND TEST =====");

        // SKU999 does not exist.
        // The method handles this gracefully using TryGetValue.
        inventory.SellItem("SKU999", 2);


        // =====================================================
        // 6. Low Stock Report
        // =====================================================

        Console.WriteLine("\n===== LOW STOCK REPORT =====");

        // Display items whose quantity is below 10.
        inventory.LowStockReport(10);
    }
}