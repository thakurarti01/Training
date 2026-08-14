using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        // TODO 1: Find all order numbers using IgnoreCase
        string text =
            "Order #4521 was shipped. order #99 is pending. ORDER #12345 was cancelled.";

        string orderPattern = @"Order\s+#(\d+)";

        MatchCollection orders = Regex.Matches(
            text,
            orderPattern,
            RegexOptions.IgnoreCase
        );

        Console.Write("Order numbers found: ");

        // Print only the captured order number
        for (int i = 0; i < orders.Count; i++)
        {
            Console.Write(orders[i].Groups[1].Value);

            if (i < orders.Count - 1)
                Console.Write(", ");
        }

        Console.WriteLine();


        // TODO 2: Mask a 16-digit card number except last 4 digits
        string card = "4521-1234-5678-1234";

        string cardPattern =
            @"\d{4}[- ]?\d{4}[- ]?\d{4}[- ]?(\d{4})";

        string maskedCard = Regex.Replace(
            card,
            cardPattern,
            "XXXX-XXXX-XXXX-$1"
        );

        Console.WriteLine("Masked card: " + maskedCard);


        // TODO 3: Change "lastname, firstname" to "firstname lastname"
        string names = "Smith, John";

        string namePattern = @"^\s*([^,]+),\s*(.+)\s*$";

        string reformattedName = Regex.Replace(
            names,
            namePattern,
            "$2 $1"
        );

        Console.WriteLine("Reformatted name: " + reformattedName);


        // TODO 4: Split tags using comma or semicolon
        string tags = "red, blue; green , yellow";

        string[] tagArray = Regex.Split(tags, @"[;,]");

        // Remove extra spaces from every tag
        for (int i = 0; i < tagArray.Length; i++)
        {
            tagArray[i] = tagArray[i].Trim();
        }

        Console.WriteLine(
            "Tags: [" + string.Join(", ", tagArray) + "]"
        );
    }
}