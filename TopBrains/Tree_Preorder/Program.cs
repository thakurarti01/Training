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
    // Preorder Traversal
    static void PreOrder(Node root)
    {
        if (root == null)
            return;

        Console.Write(root.data + " "); // Visit Root
        PreOrder(root.left);            // Visit Left
        PreOrder(root.right);           // Visit Right
    }

    static void Main(string[] args)
    {
        // Creating the tree
        Node root = new Node(1);

        root.left = new Node(2);
        root.right = new Node(3);

        root.left.left = new Node(4);
        root.left.right = new Node(5);

        Console.WriteLine("Preorder Traversal:");
        PreOrder(root);
    }
}