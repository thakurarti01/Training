using System;

class StringHandling
{
    public static void Main()
    {
        string[] orders =
        {
            "ORD1001|John Smith|Laptop|2|$1200|Delivered",
            "ORD1002|Alice Brown|Mobile|1|$800|Pending",
            "ORD1003|David Wilson|Keyboard|3|$150|Shipped",
            "ORD1004|Emma Davis|Monitor|2|$350|Delivered",
            "ORD1005|Sophia Lee|Mouse|5|$50|Pending"
        };

        Console.WriteLine("===== All Orders =====");
        foreach (string order in orders)
        {
            Console.WriteLine(order);
        }

        Console.WriteLine("\n===== Delivered Orders =====");
        foreach (string order in orders)
        {
            string[] details = order.Split('|');

            if (details[5] == "Delivered")
            {
                Console.WriteLine(details[0] + " - " + details[1]);
            }
        }

        Console.WriteLine("\n===== Customer Names =====");
        foreach (string order in orders)
        {
            string[] details = order.Split('|');
            Console.WriteLine(details[1]);
        }

        Console.WriteLine("\n===== Product Names =====");
        foreach (string order in orders)
        {
            string[] details = order.Split('|');
            Console.WriteLine(details[2]);
        }

        Console.WriteLine("\n===== Prices =====");
        foreach (string order in orders)
        {
            string[] details = order.Split('|');

            string price = details[4].Replace("$", "");

            Console.WriteLine(price);
        }

        Console.Write("\nEnter Order ID to Search: ");
        string searchId = Console.ReadLine();

        bool found = false;

        foreach (string order in orders)
        {
            string[] details = order.Split('|');

            if (details[0] == searchId)
            {
                Console.WriteLine("\nOrder Found");
                Console.WriteLine("Order ID : " + details[0]);
                Console.WriteLine("Customer : " + details[1]);
                Console.WriteLine("Product  : " + details[2]);
                Console.WriteLine("Quantity : " + details[3]);
                Console.WriteLine("Price    : " + details[4]);
                Console.WriteLine("Status   : " + details[5]);

                found = true;
                break;
            }
        }

        if (!found)
        {
            Console.WriteLine("Order Not Found");
        }

        int pendingCount = 0;

        foreach (string order in orders)
        {
            string[] details = order.Split('|');

            if (details[5] == "Pending")
            {
                pendingCount++;
            }
        }

        Console.WriteLine("\nPending Orders = " + pendingCount);
    }
}