using System;

class String_Handling
{
    static string[] orders =
    {
        "ORD1001|John Smith|Laptop|1200|Delivered",
        "ORD1002|Alice Brown|Mobile|800|Pending",
        "ORD1003|David Wilson|Keyboard|150|Shipped",
        "ORD1004|Emma Davis|Monitor|300|Delivered",
        "ORD1005|Chris Lee|Mouse|50|Pending"
    };

    public static void Handle()
    {
        Console.WriteLine("===== E-Commerce Order Management =====\n");

        DisplayOrders();

        Console.WriteLine("\n----------------------------");
        DisplayUpperCaseNames();

        Console.WriteLine("\n----------------------------");
        DisplayDeliveredOrders();

        Console.WriteLine("\n----------------------------");
        CountOrders();

        Console.WriteLine("\n----------------------------");
        SearchOrder();

        Console.WriteLine("\n----------------------------");
        ExtractPriceDetails();
    }

    static void DisplayOrders()
    {
        Console.WriteLine("TASK 1 : Display All Orders\n");

        foreach (string order in orders)
        {
            string[] data = order.Split('|');

            Console.WriteLine("Order ID : " + data[0]);
            Console.WriteLine("Customer : " + data[1]);
            Console.WriteLine("Product  : " + data[2]);
            Console.WriteLine("Price    : $" + data[3]);
            Console.WriteLine("Status   : " + data[4]);
            Console.WriteLine();
        }
    }

    static void DisplayUpperCaseNames()
    {
        Console.WriteLine("TASK 2 : Customer Names in Uppercase\n");

        foreach (string order in orders)
        {
            string[] data = order.Split('|');
            Console.WriteLine(data[1].ToUpper());
        }
    }

    static void DisplayDeliveredOrders()
    {
        Console.WriteLine("TASK 3 : Display Delivered Orders\n");

        foreach (string order in orders)
        {
            string[] data = order.Split('|');

            if (data[4] == "Delivered")
            {
                Console.WriteLine(data[0]);
            }
        }
    }

    static void CountOrders()
    {
        Console.WriteLine("TASK 4 : Count Total Orders\n");

        Console.WriteLine("Total Orders: " + orders.Length);
    }

    static void SearchOrder()
    {
        Console.WriteLine("TASK 5 : Search Order by ID\n");
        Console.Write("Enter Order ID: ");

        string orderId = Console.ReadLine();

        bool orderFound = false;

        foreach (string order in orders)
        {
            string[] data = order.Split('|');

            if (data[0] == orderId)
            {
                Console.WriteLine("\nOrder Found");
                Console.WriteLine("Order ID : " + data[0]);
                Console.WriteLine("Customer : " + data[1]);
                Console.WriteLine("Product  : " + data[2]);
                Console.WriteLine("Price    : $" + data[3]);
                Console.WriteLine("Status   : " + data[4]);

                orderFound = true;
                break;
            }
        }

        if (!orderFound)
        {
            Console.WriteLine("Order with ID " + orderId + " not found.");
        }
    }

    static void ExtractPriceDetails()
    {
        Console.WriteLine("TASK 6 : Extract Price\n");

        foreach (string order in orders)
        {
            string[] data = order.Split('|');
            Console.WriteLine(data[3]);
        }
    }
}