using System;

class Node
{
    public int Data;
    public Node Next;

    public Node(int data)
    {
        Data = data;
        Next = null;
    }
}

class CircularLinkedList
{
    Node head;

    public void Insert(int data)
    {
        Node newNode = new Node(data);

        if(head == null)
        {
            head = newNode;
            newNode.Next = head;
            return;
        }

        Node temp = head;

        while(temp.Next != head)
        {
            temp = temp.Next;
        }

        temp.Next = newNode;
        newNode.Next = head;
    }

    public void Display()
    {
        if(head == null)
        {
            return;
        }

        Node temp = head;

        do
        {
            Console.Write(temp.Data + "->");
            temp = temp.Next;
        }

        while(temp != head);

        Console.Write(" Back to Head");
    }

    static void Main()
    {
        CircularLinkedList list = new CircularLinkedList();

        list.Insert(10);
        list.Insert(20);
        list.Insert(30);
        list.Insert(40);

        Console.Write("Circular Linked List: ");
        list.Display();
    }
}