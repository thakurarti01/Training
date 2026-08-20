// ### Lab 3 — Multicast Delegates

// 1. Declare `public delegate void OrderEvent(string orderId);`.
// 2. Create three separate handler methods: `LogToConsole`, `SendEmailSimulation`, `UpdateInventorySimulation` (each just prints a distinguishing message).
// 3. Combine all three into one multicast delegate using `+=` and invoke it once — confirm all three run, in the order added.
// 4. Remove one handler with `-=` and invoke again — confirm only the remaining two run.
// 5. Demonstrate the "-= doesn't work across different lambda instances" pitfall: subscribe two *lambdas* with identical bodies, then try to unsubscribe one using a freshly-written (not stored) lambda — show it fails to remove anything, then fix it by storing the original delegate reference and successfully unsubscribing.

// **Deliverable:** Console app clearly demonstrating multicast add/remove behavior and the reference-equality pitfall (both the failure and the fix).

// ---

using System;

public delegate void OrderEvent(string orderId);

class Program
{
    // First event handler.
    static void LogToConsole(string orderId)
    {
        Console.WriteLine($"Console Log: Order {orderId} received.");
    }

    // Second event handler.
    static void SendEmailSimulation(string orderId)
    {
        Console.WriteLine($"Email: Confirmation sent for Order {orderId}.");
    }

    // Third event handler.
    static void UpdateInventorySimulation(string orderId)
    {
        Console.WriteLine($"Inventory: Stock updated for Order {orderId}.");
    }

    static void Main()
    {
        Console.WriteLine("===== LAB 3: Multicast Delegates =====\n");

        // Create a delegate variable pointing to the first handler.
        OrderEvent orderHandler = LogToConsole;

        // += adds additional methods to the invocation list.
        orderHandler += SendEmailSimulation;
        orderHandler += UpdateInventorySimulation;

        Console.WriteLine("All three handlers:");
        orderHandler("ORD101");

        Console.WriteLine("\n===== Removing One Handler =====");

        // -= removes the specified method from the invocation list.
        orderHandler -= SendEmailSimulation;

        // Now only two handlers will execute.
        orderHandler("ORD102");

        Console.WriteLine("\n===== Lambda Reference Pitfall =====");

        // Store the first lambda in a variable.
        OrderEvent lambda1 = id =>
            Console.WriteLine($"Lambda Handler: {id}");

        // This is a separate lambda instance even though
        // it has exactly the same body.
        OrderEvent lambda2 = id =>
            Console.WriteLine($"Lambda Handler: {id}");

        OrderEvent lambdaHandler = lambda1;

        // Add both lambda handlers.
        lambdaHandler += lambda2;

        Console.WriteLine("\nBoth lambdas:");
        lambdaHandler("ORD103");

        // This creates a NEW lambda instance.
        // It does not refer to lambda1 or lambda2.
        lambdaHandler -= id =>
            Console.WriteLine($"Lambda Handler: {id}");

        Console.WriteLine("\nAfter trying to remove using a new lambda:");
        lambdaHandler("ORD104");

        /*
         * The above removal does not remove lambda1 or lambda2.
         *
         * Why?
         * Because the newly written lambda is a different delegate instance.
         * Identical code does not automatically mean the same delegate reference.
         */

        Console.WriteLine("\n===== Correct Lambda Removal =====");

        // Store a delegate reference and use the SAME reference for removal.
        OrderEvent storedLambda = id =>
            Console.WriteLine($"Stored Lambda: {id}");

        lambdaHandler += storedLambda;

        Console.WriteLine("\nAfter adding stored lambda:");
        lambdaHandler("ORD105");

        // Because we stored the original delegate reference,
        // we can successfully remove that exact handler.
        lambdaHandler -= storedLambda;

        Console.WriteLine("\nAfter removing stored lambda:");
        lambdaHandler("ORD106");
    }
}