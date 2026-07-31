using System;

class Stack_Array
{
    int[] stack = new int[5];
    int top = -1;

    public void Push(int value)
    {
        if (top == stack.Length - 1)
        {
            Console.WriteLine("Stack Overflow");
            return;
        }

        stack[++top] = value;
    }

    public void Pop()
    {
        if (top == -1)
        {
            Console.WriteLine("Stack Underflow");
            return;
        }

        Console.WriteLine("Deleted: " + stack[top--]);
    }

    public void Display()
    {
        if (top == -1)
        {
            Console.WriteLine("Stack is Empty");
            return;
        }

        Console.WriteLine("Stack Elements:");

        for (int i = top; i >= 0; i--)
        {
            Console.WriteLine(stack[i]);
        }
    }

    public static void Stack()
    {
        Stack_Array s = new Stack_Array();

        s.Push(10);
        s.Push(20);
        s.Push(30);

        s.Display();

        s.Pop();

        Console.WriteLine("\nAfter Pop");

        s.Display();
    }
}