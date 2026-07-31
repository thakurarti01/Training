using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Stack<string> history = new Stack<string>();

        while (true)
        {
            Console.WriteLine("1. Visit Page");
            Console.WriteLine("2. Back");
            Console.WriteLine("3. Current Page");
            Console.WriteLine("4. Display History");
            Console.WriteLine("5. Clear History");
            Console.WriteLine("6. Total Pages");
            Console.WriteLine("7. Exit");

            Console.Write("\nEnter Choice : ");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter Website : ");
                    string website = Console.ReadLine();
                    history.Push(website);
                    Console.WriteLine("Page Visited Successfully.");
                    break;

                case 2:
                    if (history.Count == 0)
                    {
                        Console.WriteLine("No pages in history.");
                    }
                    else
                    {
                        Console.WriteLine("Going Back from: " + history.Pop());

                        if (history.Count > 0)
                            Console.WriteLine("Current Page : " + history.Peek());
                        else
                            Console.WriteLine("No current page.");
                    }
                    break;

                case 3:
                    if (history.Count == 0)
                        Console.WriteLine("No current page.");
                    else
                        Console.WriteLine("Current Page : " + history.Peek());
                    break;

                case 4:
                    if (history.Count == 0)
                    {
                        Console.WriteLine("History is empty.");
                    }
                    else
                    {
                        Console.WriteLine("\nBrowsing History:");
                        foreach (string page in history)
                        {
                            Console.WriteLine(page);
                        }
                    }
                    break;

                case 5:
                    history.Clear();
                    Console.WriteLine("History Cleared Successfully.");
                    break;

                case 6:
                    Console.WriteLine("Total Pages : " + history.Count);
                    break;

                case 7:
                    Console.WriteLine("Exiting...");
                    return;

                default:
                    Console.WriteLine("Invalid Choice!");
                    break;
            }
        }
    }
}