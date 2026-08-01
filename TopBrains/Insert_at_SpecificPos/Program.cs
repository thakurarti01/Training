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

    // Insert at Tail (to create the list)
    public void InsertAtTail(int value)
    {
        Node newNode = new Node(value);

        if (head == null)
        {
            head = newNode;
            return;
        }

        Node temp = head;

        while (temp.next != null)
        {
            temp = temp.next;
        }

        temp.next = newNode;
    }

    // Insert at Specific Position
    public void InsertAtPosition(int value, int position)
    {
        Node newNode = new Node(value);

        // Insert at Head
        if (position == 0)
        {
            newNode.next = head;
            head = newNode;
            return;
        }

        Node temp = head;

        // Move to the node before the required position
        for (int i = 0; i < position - 1; i++)
        {
            temp = temp.next;
        }

        newNode.next = temp.next;
        temp.next = newNode;
    }

    // Display Linked List
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

        Console.WriteLine("Original List:");
        list.Display();

        list.InsertAtPosition(25, 2);

        Console.WriteLine("After Inserting 25 at Position 2:");
        list.Display();
    }
}