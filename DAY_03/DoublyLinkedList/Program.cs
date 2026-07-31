using System;

class Node
{
	public int Data;
	public Node Prev;
	public Node Next;

	public Node(int data)
	{
		Data = data;
		Prev = null;
		Next = null;
	}
}

class DoublyLinkedList
{
	Node head;

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
		newNode.Prev = temp;
	}

	public void DisplayForward()
	{
		Node temp = head;

		while (temp != null)
		{
			Console.Write(temp.Data + " <-> ");
			temp = temp.Next;
		}

		Console.WriteLine("NULL");
	}

	static void Main()
	{
		DoublyLinkedList list = new DoublyLinkedList();

		list.Insert(10);
		list.Insert(20);
		list.Insert(30);
		list.Insert(40);

		Console.WriteLine("Doubly Linked List:");
		list.DisplayForward();
	}
}
