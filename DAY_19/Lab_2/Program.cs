// ### Lab 2 — Scenario-Driven Collection Choice

// For each scenario below, pick the correct collection (`HashSet<T>`, `Queue<T>`, `Stack<T>`, or `LinkedList<T>`), implement it, and justify your choice in a code comment.

// 1. **Undo stack for a text editor** — supports `RecordAction(string action)` and `Undo()` returning the most recent action, or `null` if none remain.
// 2. **Customer support ticket queue** — supports `SubmitTicket(string ticketId)` and `ProcessNext()` returning the oldest unprocessed ticket.
// 3. **Unique daily active user tracker** — supports `RecordVisit(int userId)` and `UniqueVisitorCount()`; must handle the same user visiting multiple times without double-counting.
// 4. **Music playlist with fast insert/remove at an arbitrary position** — supports `InsertAfter(string afterSong, string newSong)` and `Remove(string song)`.

// **Deliverable:** Four small classes/methods, each with a short `Main`-driven demonstration and a one-line justification comment.

// ---

using System;
using System.Collections.Generic;

class Program
{
    // =========================================================
    // 1. UNDO STACK - Text Editor
    // =========================================================

    class UndoStack
    {
        // Stack follows LIFO:
        // Last action recorded is the first action undone.
        private Stack<string> actions = new Stack<string>();

        // Adds a new action to the top of the stack.
        public void RecordAction(string action)
        {
            actions.Push(action);
        }

        // Removes and returns the most recent action.
        // Returns null when there is nothing left to undo.
        public string? Undo()
        {
            if (actions.Count == 0)
            {
                return null;
            }

            return actions.Pop();
        }
    }


    // =========================================================
    // 2. CUSTOMER SUPPORT TICKET QUEUE
    // =========================================================

    class SupportTicketQueue
    {
        // Queue follows FIFO:
        // The first ticket submitted is processed first.
        private Queue<string> tickets = new Queue<string>();

        // Adds a new ticket at the end of the queue.
        public void SubmitTicket(string ticketId)
        {
            tickets.Enqueue(ticketId);
        }

        // Removes and returns the oldest ticket.
        public string? ProcessNext()
        {
            if (tickets.Count == 0)
            {
                return null;
            }

            return tickets.Dequeue();
        }
    }


    // =========================================================
    // 3. UNIQUE DAILY ACTIVE USERS
    // =========================================================

    class ActiveUserTracker
    {
        // HashSet stores only unique values.
        // Therefore, the same user ID cannot be counted twice.
        private HashSet<int> users = new HashSet<int>();

        // Adds the user ID to the set.
        // If the user already exists, HashSet ignores the duplicate.
        public void RecordVisit(int userId)
        {
            users.Add(userId);
        }

        // Returns the number of unique users.
        public int UniqueVisitorCount()
        {
            return users.Count;
        }
    }


    // =========================================================
    // 4. MUSIC PLAYLIST
    // =========================================================

    class MusicPlaylist
    {
        // LinkedList is suitable because it allows efficient
        // insertion and removal once the required node is found.
        private LinkedList<string> songs = new LinkedList<string>();

        // Adds the first song directly to the playlist.
        public void AddSong(string song)
        {
            songs.AddLast(song);
        }

        // Inserts a new song immediately after a particular song.
        public void InsertAfter(string afterSong, string newSong)
        {
            // Find the node containing the requested song.
            LinkedListNode<string>? node = songs.Find(afterSong);

            // Only insert if the song exists.
            if (node != null)
            {
                songs.AddAfter(node, newSong);
            }
        }

        // Removes a song from the playlist.
        public void Remove(string song)
        {
            songs.Remove(song);
        }

        // Displays all songs in the playlist.
        public void Display()
        {
            foreach (string song in songs)
            {
                Console.Write(song + " -> ");
            }

            Console.WriteLine("END");
        }
    }


    // =========================================================
    // MAIN METHOD - DEMONSTRATION
    // =========================================================

    static void Main()
    {
        Console.WriteLine("==========================================");
        Console.WriteLine("LAB 2 - SCENARIO-DRIVEN COLLECTION CHOICE");
        Console.WriteLine("==========================================");


        // =====================================================
        // 1. UNDO STACK
        // =====================================================

        Console.WriteLine("\n1. Text Editor Undo Stack");

        // Stack is used because undo should happen in LIFO order:
        // Last action performed must be undone first.
        UndoStack undoStack = new UndoStack();

        undoStack.RecordAction("Typed Hello");
        undoStack.RecordAction("Typed World");
        undoStack.RecordAction("Deleted World");

        Console.WriteLine("Undo: " + undoStack.Undo());
        Console.WriteLine("Undo: " + undoStack.Undo());


        // =====================================================
        // 2. SUPPORT TICKET QUEUE
        // =====================================================

        Console.WriteLine("\n2. Customer Support Ticket Queue");

        // Queue is used because tickets should be processed
        // in the same order in which they were submitted (FIFO).
        SupportTicketQueue ticketQueue = new SupportTicketQueue();

        ticketQueue.SubmitTicket("T001");
        ticketQueue.SubmitTicket("T002");
        ticketQueue.SubmitTicket("T003");

        Console.WriteLine("Processing: " + ticketQueue.ProcessNext());
        Console.WriteLine("Processing: " + ticketQueue.ProcessNext());


        // =====================================================
        // 3. UNIQUE ACTIVE USERS
        // =====================================================

        Console.WriteLine("\n3. Unique Daily Active Users");

        // HashSet is used because it automatically prevents
        // duplicate user IDs.
        ActiveUserTracker tracker = new ActiveUserTracker();

        tracker.RecordVisit(101);
        tracker.RecordVisit(102);
        tracker.RecordVisit(101); // Duplicate visit
        tracker.RecordVisit(103);
        tracker.RecordVisit(102); // Duplicate visit

        Console.WriteLine(
            "Unique visitors: " + tracker.UniqueVisitorCount()
        );


        // =====================================================
        // 4. MUSIC PLAYLIST
        // =====================================================

        Console.WriteLine("\n4. Music Playlist");

        // LinkedList is used because it supports insertion/removal
        // around a particular node without shifting other elements.
        MusicPlaylist playlist = new MusicPlaylist();

        playlist.AddSong("Song A");
        playlist.AddSong("Song B");
        playlist.AddSong("Song D");

        Console.WriteLine("Original playlist:");
        playlist.Display();

        // Insert Song C after Song B.
        playlist.InsertAfter("Song B", "Song C");

        Console.WriteLine("After inserting Song C:");
        playlist.Display();

        // Remove Song B from the playlist.
        playlist.Remove("Song B");

        Console.WriteLine("After removing Song B:");
        playlist.Display();
    }
}