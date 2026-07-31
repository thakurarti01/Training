using System;
using System.Collections.Generic;

public class BTreeNode
{
    public List<int> Keys { get; set; }
    public List<BTreeNode> Children { get; set; }
    public bool IsLeaf { get; set; }

    public BTreeNode(bool isLeaf)
    {
        Keys = new List<int>();
        Children = new List<BTreeNode>();
        IsLeaf = isLeaf;
    }
}

public class BTree
{
    private BTreeNode root;
    private int degree;
    private int maxKeys;
    private int minKeys;

    public BTree(int degree)
    {
        this.degree = degree;
        maxKeys = 2 * degree - 1;
        minKeys = degree - 1;
        root = new BTreeNode(true);
    }

    // Insert a key
    public void Insert(int key)
    {
        if (root.Keys.Count == maxKeys)
        {
            BTreeNode newRoot = new BTreeNode(false);
            newRoot.Children.Add(root);

            SplitChild(newRoot, 0);

            root = newRoot;
        }

        InsertNonFull(root, key);
    }

    private void InsertNonFull(BTreeNode node, int key)
    {
        int i = node.Keys.Count - 1;

        if (node.IsLeaf)
        {
            node.Keys.Add(0);

            while (i >= 0 && key < node.Keys[i])
            {
                node.Keys[i + 1] = node.Keys[i];
                i--;
            }

            node.Keys[i + 1] = key;
        }
        else
        {
            while (i >= 0 && key < node.Keys[i])
                i--;

            i++;

            if (node.Children[i].Keys.Count == maxKeys)
            {
                SplitChild(node, i);

                if (key > node.Keys[i])
                    i++;
            }

            InsertNonFull(node.Children[i], key);
        }
    }

    private void SplitChild(BTreeNode parent, int index)
    {
        BTreeNode fullChild = parent.Children[index];
        BTreeNode newChild = new BTreeNode(fullChild.IsLeaf);

        int middleKey = fullChild.Keys[degree - 1];

        for (int j = degree; j < fullChild.Keys.Count; j++)
            newChild.Keys.Add(fullChild.Keys[j]);

        fullChild.Keys.RemoveRange(degree - 1, fullChild.Keys.Count - (degree - 1));

        if (!fullChild.IsLeaf)
        {
            for (int j = degree; j < fullChild.Children.Count; j++)
                newChild.Children.Add(fullChild.Children[j]);

            fullChild.Children.RemoveRange(degree, fullChild.Children.Count - degree);
        }

        parent.Children.Insert(index + 1, newChild);
        parent.Keys.Insert(index, middleKey);
    }

    // Inorder traversal
    public void Traverse()
    {
        Traverse(root);
        Console.WriteLine();
    }

    private void Traverse(BTreeNode node)
    {
        int i;

        for (i = 0; i < node.Keys.Count; i++)
        {
            if (!node.IsLeaf)
                Traverse(node.Children[i]);

            Console.Write(node.Keys[i] + " ");
        }

        if (!node.IsLeaf)
            Traverse(node.Children[i]);
    }
}

class Program
{
    static void Main()
    {
        BTree tree = new BTree(3);

        int[] values = { 10, 20, 5, 6, 12, 30, 7, 17 };

        foreach (int value in values)
            tree.Insert(value);

        Console.WriteLine("B-Tree Traversal:");
        tree.Traverse();
    }
}