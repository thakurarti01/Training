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
    // Function to find height
    static int Height(Node root)
    {
        if (root == null)
            return -1;

        int leftHeight = Height(root.left);
        int rightHeight = Height(root.right);

        return Math.Max(leftHeight, rightHeight) + 1;
    }

    static void Main(string[] args)
    {
        // Creating the tree
        Node root = new Node(1);

        root.left = new Node(2);
        root.right = new Node(3);

        root.left.left = new Node(4);
        root.left.right = new Node(5);

        Console.WriteLine("Height of Tree: " + Height(root));
    }
}