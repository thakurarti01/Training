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

class SinglyLinkedList
{
	Node head = null;

	// Insert at End
	public void Insert(int data)
	{
		Node newNode = new Node(data);

		if (head == null)
		{
			head = newNode;
			return;
		}

		Node temp = head;

		while (temp.Next != null)
		{
			temp = temp.Next;
		}

		temp.Next = newNode;
	}

	// Display Linked List
	public void Display()
	{
		if (head == null)
		{
			Console.WriteLine("List is Empty");
			return;
		}

		Node temp = head;

		while (temp != null)
		{
			Console.Write(temp.Data + " -> ");
			temp = temp.Next;
		}

		Console.WriteLine("NULL");
	}
}

class Program
{
	static void Main()
	{
		SinglyLinkedList list = new SinglyLinkedList();

		list.Insert(10);
		list.Insert(20);
		list.Insert(30);
		list.Insert(40);

		Console.WriteLine("Linked List:");
		list.Display();
	}
}