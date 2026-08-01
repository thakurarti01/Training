using System;

class Node
{
    public int data;
    public Node next;

    public Node(int value)
    {
        data = value;
        next = null;
    }
}

class LinkedList
{
    public Node head;

    // Insert at Tail
    public void InsertAtTail(int value)
    {
        Node newNode = new Node(value);

        // If the list is empty
        if (head == null)
        {
            head = newNode;
            return;
        }

        // Traverse to the last node
        Node temp = head;
        while (temp.next != null)
        {
            temp = temp.next;
        }

        // Link the last node to the new node
        temp.next = newNode;
    }

    // Display the linked list
    public void Display()
    {
        Node temp = head;

        while (temp != null)
        {
            Console.Write(temp.data + " ");
            temp = temp.next;
        }

        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        LinkedList list = new LinkedList();

        list.InsertAtTail(10);
        list.InsertAtTail(20);
        list.InsertAtTail(30);
        list.InsertAtTail(40);

        Console.WriteLine("Linked List:");
        list.Display();
    }
}