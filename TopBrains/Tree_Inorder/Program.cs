using System;

class Node
{
    public int data;
    public Node left;
    public Node right;

    public Node(int value)
    {
        data = value;
        left = null;
        right = null;
    }
}

class Program
{
    // Inorder Traversal
    static void InOrder(Node root)
    {
        if (root == null)
            return;

        InOrder(root.left);          // Visit Left
        Console.Write(root.data + " "); // Visit Root
        InOrder(root.right);         // Visit Right
    }

    static void Main(string[] args)
    {
        // Creating the tree
        Node root = new Node(1);

        root.left = new Node(2);
        root.right = new Node(3);

        root.left.left = new Node(4);
        root.left.right = new Node(5);

        Console.WriteLine("Inorder Traversal:");
        InOrder(root);
    }
}