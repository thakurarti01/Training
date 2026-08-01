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
    // Postorder Traversal
    static void PostOrder(Node root)
    {
        if (root == null)
            return;

        PostOrder(root.left);          // Visit Left
        PostOrder(root.right);         // Visit Right
        Console.Write(root.data + " "); // Visit Root
    }

    static void Main(string[] args)
    {
        // Creating the tree
        Node root = new Node(1);

        root.left = new Node(2);
        root.right = new Node(3);

        root.left.left = new Node(4);
        root.left.right = new Node(5);

        Console.WriteLine("Postorder Traversal:");
        PostOrder(root);
    }
}