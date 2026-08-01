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

    // Insert at Head
    public void InsertAtHead(int value)
    {
        Node newNode = new Node(value);

        newNode.next = head;
        head = newNode;
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

        list.InsertAtHead(10);
        list.InsertAtHead(20);
        list.InsertAtHead(30);
        list.InsertAtHead(40);

        Console.WriteLine("Linked List:");
        list.Display();
    }
}