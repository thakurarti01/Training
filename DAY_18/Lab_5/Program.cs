// ### Lab 5 — `HashSet<T>` Set Operations

// Build a **Customer Overlap Analyzer** for a marketing team.

// 1. Two `HashSet<string>` of customer emails: `NewsletterSubscribers` and `AppUsers`.
// 2. Compute and print:
//    - Customers who are **both** subscribers and app users (`IntersectWith`)
//    - Customers who are subscribers but **not** app users (`ExceptWith`)
//    - All unique customers across both lists (`UnionWith`)
//    - Whether `NewsletterSubscribers` is a subset of `AppUsers` (`IsSubsetOf`)
// 3. Deduplicate a `List<string>` of 100 randomly generated emails (with intentional duplicates) into a `HashSet<string>` and report how many duplicates were removed.

// **Deliverable:** Console app printing each computed set clearly labeled.

// ---

using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // =====================================================
        // 1. Create two HashSets of customer emails
        // =====================================================

        // HashSet is used because it stores only unique values
        // and provides built-in set operations.
        HashSet<string> newsletterSubscribers =
            new HashSet<string>
            {
                "arti@gmail.com",
                "rahul@gmail.com",
                "neha@gmail.com",
                "aman@gmail.com",
                "riya@gmail.com"
            };


        HashSet<string> appUsers =
            new HashSet<string>
            {
                "rahul@gmail.com",
                "neha@gmail.com",
                "vikas@gmail.com",
                "rohit@gmail.com"
            };


        // =====================================================
        // 2. Customers who are BOTH subscribers and app users
        // =====================================================

        // Create a copy so the original HashSet is not changed.
        HashSet<string> both =
            new HashSet<string>(newsletterSubscribers);

        // IntersectWith keeps only values present in both sets.
        both.IntersectWith(appUsers);

        Console.WriteLine(
            "===== CUSTOMERS IN BOTH SETS =====");

        foreach (string email in both)
        {
            Console.WriteLine(email);
        }


        // =====================================================
        // 3. Subscribers but NOT app users
        // =====================================================

        // Again, create a copy to preserve the original set.
        HashSet<string> subscribersOnly =
            new HashSet<string>(newsletterSubscribers);

        // ExceptWith removes all values that exist in appUsers.
        subscribersOnly.ExceptWith(appUsers);

        Console.WriteLine(
            "\n===== SUBSCRIBERS BUT NOT APP USERS =====");

        foreach (string email in subscribersOnly)
        {
            Console.WriteLine(email);
        }


        // =====================================================
        // 4. All unique customers
        // =====================================================

        // Start with newsletter subscribers.
        HashSet<string> allCustomers =
            new HashSet<string>(newsletterSubscribers);

        // UnionWith adds app users while automatically
        // ignoring duplicate email addresses.
        allCustomers.UnionWith(appUsers);

        Console.WriteLine(
            "\n===== ALL UNIQUE CUSTOMERS =====");

        foreach (string email in allCustomers)
        {
            Console.WriteLine(email);
        }


        // =====================================================
        // 5. Check whether NewsletterSubscribers is a subset
        //    of AppUsers
        // =====================================================

        // IsSubsetOf checks whether every subscriber
        // is also present in the app users set.
        bool isSubset =
            newsletterSubscribers.IsSubsetOf(appUsers);

        Console.WriteLine(
            "\n===== SUBSET CHECK =====");

        Console.WriteLine(
            "Newsletter subscribers are a subset of " +
            $"app users: {isSubset}");


        // =====================================================
        // 6. Generate 100 emails with duplicates
        // =====================================================

        // List is intentionally used here because the question
        // asks us to start with a List containing duplicates.
        List<string> emails = new List<string>();

        string[] sampleEmails =
        {
            "a@gmail.com",
            "b@gmail.com",
            "c@gmail.com",
            "d@gmail.com",
            "e@gmail.com"
        };

        Random random = new Random();

        // Randomly select emails 100 times.
        // Because the sample contains only 5 emails,
        // duplicates will intentionally occur.
        for (int i = 0; i < 100; i++)
        {
            emails.Add(
                sampleEmails[
                    random.Next(sampleEmails.Length)]);
        }


        // =====================================================
        // 7. Remove duplicates using HashSet
        // =====================================================

        // HashSet automatically removes duplicate values.
        HashSet<string> uniqueEmails =
            new HashSet<string>(emails);

        // Original count - unique count = duplicates removed.
        int duplicatesRemoved =
            emails.Count - uniqueEmails.Count;


        Console.WriteLine(
            "\n===== DUPLICATE REMOVAL =====");

        Console.WriteLine(
            "Original email count: " + emails.Count);

        Console.WriteLine(
            "Unique email count: " + uniqueEmails.Count);

        Console.WriteLine(
            "Duplicates removed: " + duplicatesRemoved);
    }
}