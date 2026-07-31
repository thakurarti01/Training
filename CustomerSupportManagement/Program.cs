using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string[] tickets =
        {
            "T001|John|Login Issue",
            "T002|Alice|Payment Failed",
            "T003|David|Account Locked",
            "T004|Emma|Refund Request",
            "T005|James|Password Reset"
        };

        Queue<string> queue = new Queue<string>();

        // Adding tickets to the queue
        foreach (string ticket in tickets)
        {
            queue.Enqueue(ticket);
        }

        Console.WriteLine("Customer Support Ticket Management\n");

        // ------------------------------------------------------------------------

        Console.WriteLine("Task 1: Ticket IDs");

        foreach (string ticket in queue)
        {
            string[] details = ticket.Split('|');
            Console.WriteLine(details[0]);
        }

        // ------------------------------------------------------------------------

        Console.WriteLine("\nTask 2: All Tickets");

        foreach (string ticket in queue)
        {
            string[] details = ticket.Split('|');

            Console.Write(details[0] + " ");
            Console.Write(details[1] + " ");
            Console.WriteLine(details[2]);
        }

        // ------------------------------------------------------------------------

        Console.WriteLine("\nTask 3: Display First Ticket");

        string[] data = queue.Peek().Split('|');

        Console.WriteLine(data[0] + " " + data[1] + " " + data[2]);

        // ------------------------------------------------------------------------

        Console.WriteLine("\nTask 4: View Next Ticket");
        queue.Dequeue();
        data = queue.Peek().Split('|');
        Console.WriteLine(data[0] + " " + data[1] + " " + data[2]);

        // ------------------------------------------------------------------------
        Console.WriteLine("\nTask 5: Count of Pending Tickets");
        Console.WriteLine("Pending Tickets: " + queue.Count);

        // ------------------------------------------------------------------------
        Console.WriteLine("\nTask 6: Search Ticket by ID");

        Console.Write("Enter Ticket ID: ");
        string searchId = Console.ReadLine();

        bool found = false;

        foreach (string ticket in queue)
        {
            string[] details = ticket.Split('|');

            if (details[0] == searchId)
            {
                Console.WriteLine("\nTicket Found");
                Console.WriteLine("Customer : " + details[1]);
                Console.WriteLine("Issue : " + details[2]);
                found = true;
                break;
            }
        }

        if (!found)
        {
            Console.WriteLine("\nTicket Not Found");
        }

        // ------------------------------------------------------------------------
        Console.WriteLine("\nTask 7: Counting Tickets by Issue");

        int login = 0;
        int payment = 0;
        int refund = 0;

        foreach(string ticket in tickets)
        {
            string[] details = ticket.Split('|');
            if(details[2] == "Login Issue")
            {
                login++;
            }
            else if(details[2] == "Payment Failed")
            {
                payment++;
            }
            else if(details[2] == "Refund Request")
            {
                refund++;
            }
        }
        Console.WriteLine("Login Issue: " + login);
        Console.WriteLine("Payment Failed: " + payment);
        Console.WriteLine("Refund Request: " + refund);
    }
}