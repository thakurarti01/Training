// ### Lab 4 — `Stack<T>` and `Queue<T>` in Action

// Build **two** small simulations:

// **4A — Balanced Parentheses Checker (Stack)**
// Write `bool IsBalanced(string expression)` that checks whether `(`, `{`, `[` are correctly matched/nested in a string like `"{[a+(b*c)]-d}"`. Use `Stack<char>`.

// **4B — Print Job Queue (Queue)**
// Simulate a printer queue:
// 1. `PrintJob { string DocumentName; int Pages; }`
// 2. Enqueue 5 print jobs.
// 3. Process jobs one at a time with `Dequeue()`, printing "Printing X (Y pages)..." with a `Peek()` before each dequeue to show "Now printing next: ...".
// 4. Add a "priority interrupt" feature: if a high-priority job arrives, it should be processed before non-priority jobs already queued (hint: you'll need to think about whether `Queue<T>` alone is sufficient, or whether you need two queues / a different structure — justify your choice in a code comment).

// **Deliverable:** Console app with both simulations and sample runs shown in output/comments.

// ---

using System;
using System.Collections.Generic;


// =============================================================
// 4A - BALANCED PARENTHESES CHECKER
// =============================================================

class ParenthesesChecker
{
    public static bool IsBalanced(string expression)
    {
        // Stack is suitable because brackets follow LIFO:
        // the last opening bracket must be closed first.
        Stack<char> stack = new Stack<char>();


        // Check every character in the expression.
        foreach (char ch in expression)
        {
            // Opening brackets are stored in the stack.
            if (ch == '(' || ch == '{' || ch == '[')
            {
                stack.Push(ch);
            }


            // Closing brackets must match the most recent
            // opening bracket stored at the top of the stack.
            else if (ch == ')' || ch == '}' || ch == ']')
            {
                // If there is no opening bracket,
                // the expression is immediately unbalanced.
                if (stack.Count == 0)
                {
                    return false;
                }


                // Remove the most recent opening bracket.
                char openingBracket = stack.Pop();


                // Check whether the opening and closing brackets match.
                if (!IsMatchingPair(openingBracket, ch))
                {
                    return false;
                }
            }
        }


        // If the stack is empty, every opening bracket
        // had a matching closing bracket.
        return stack.Count == 0;
    }


    // Checks the three valid types of bracket pairs.
    private static bool IsMatchingPair(
        char opening,
        char closing)
    {
        return
            (opening == '(' && closing == ')') ||
            (opening == '{' && closing == '}') ||
            (opening == '[' && closing == ']');
    }
}


// =============================================================
// 4B - PRINT JOB QUEUE
// =============================================================

class PrintJob
{
    public string DocumentName { get; set; }
    public int Pages { get; set; }
    public bool IsPriority { get; set; }


    // Constructor initializes the print job details.
    public PrintJob(
        string documentName,
        int pages,
        bool isPriority = false)
    {
        DocumentName = documentName;
        Pages = pages;
        IsPriority = isPriority;
    }
}


class Printer
{
    // Normal Queue follows FIFO, so normal jobs are printed
    // in the same order in which they arrive.
    private Queue<PrintJob> normalQueue =
        new Queue<PrintJob>();


    // A second queue is used for priority jobs.
    // Priority jobs are always processed before normal jobs.
    //
    // Queue<T> alone cannot efficiently insert a new job
    // at the front, so two queues are used.
    private Queue<PrintJob> priorityQueue =
        new Queue<PrintJob>();


    // Adds a job to the appropriate queue.
    public void AddJob(PrintJob job)
    {
        if (job.IsPriority)
        {
            priorityQueue.Enqueue(job);
        }
        else
        {
            normalQueue.Enqueue(job);
        }
    }


    // Processes all print jobs.
    public void ProcessJobs()
    {
        while (priorityQueue.Count > 0 ||
               normalQueue.Count > 0)
        {
            PrintJob nextJob;


            // Always process priority jobs first.
            if (priorityQueue.Count > 0)
            {
                // Peek shows the next job without removing it.
                Console.WriteLine(
                    "Now printing next: " +
                    priorityQueue.Peek().DocumentName);

                // Dequeue removes that job from the queue.
                nextJob = priorityQueue.Dequeue();
            }
            else
            {
                // Show the next normal job before removing it.
                Console.WriteLine(
                    "Now printing next: " +
                    normalQueue.Peek().DocumentName);

                nextJob = normalQueue.Dequeue();
            }


            // Print the selected document.
            Console.WriteLine(
                $"Printing {nextJob.DocumentName} " +
                $"({nextJob.Pages} pages)...");
        }
    }
}


// =============================================================
// MAIN PROGRAM
// =============================================================

class Program
{
    static void Main()
    {
        // =====================================================
        // 4A - Test Balanced Parentheses
        // =====================================================

        Console.WriteLine(
            "===== 4A: BALANCED PARENTHESES =====");


        string expression1 = "{[a+(b*c)]-d}";
        string expression2 = "{[a+(b*c)]-d";


        Console.WriteLine(
            $"Expression: {expression1}");

        Console.WriteLine(
            "Balanced: " +
            ParenthesesChecker.IsBalanced(expression1));


        Console.WriteLine();


        Console.WriteLine(
            $"Expression: {expression2}");

        Console.WriteLine(
            "Balanced: " +
            ParenthesesChecker.IsBalanced(expression2));


        // =====================================================
        // 4B - Print Job Queue
        // =====================================================

        Console.WriteLine(
            "\n===== 4B: PRINT JOB QUEUE =====");


        Printer printer = new Printer();


        // Add five normal print jobs to the queue.
        printer.AddJob(
            new PrintJob("Document1.pdf", 5));

        printer.AddJob(
            new PrintJob("Document2.pdf", 10));

        printer.AddJob(
            new PrintJob("Document3.pdf", 3));

        printer.AddJob(
            new PrintJob("Document4.pdf", 7));

        printer.AddJob(
            new PrintJob("Document5.pdf", 2));


        // Add a high-priority job.
        // It will be processed before the normal jobs.
        printer.AddJob(
            new PrintJob(
                "Urgent.pdf",
                1,
                true));


        // Process all jobs.
        printer.ProcessJobs();
    }
}