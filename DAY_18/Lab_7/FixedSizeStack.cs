using System;
using System.Collections;
using System.Collections.Generic;


// Generic stack that can store any data type.
public class FixedSizeStack<T> : IReadOnlyCollection<T>
{
    // Array is used because the stack must have a fixed capacity.
    private T[] items;

    // 'top' stores the position where the next item will be added.
    private int top;


    // Count exposes the current number of elements.
    public int Count => top;


    // Constructor creates the stack with a fixed capacity.
    public FixedSizeStack(int capacity)
    {
        // Capacity must be positive.
        if (capacity <= 0)
        {
            throw new ArgumentException(
                "Capacity must be greater than zero.");
        }

        items = new T[capacity];

        // Initially the stack contains zero elements.
        top = 0;
    }


    // =========================================================
    // Push
    // =========================================================

    public void Push(T item)
    {
        // A fixed-size stack cannot accept more items
        // once its capacity has been reached.
        if (top == items.Length)
        {
            throw new InvalidOperationException(
                "Stack is full.");
        }

        // Store the item at the current top position.
        items[top] = item;

        // Move top to the next available position.
        top++;
    }


    // =========================================================
    // Pop
    // =========================================================

    public T Pop()
    {
        // We cannot remove an item from an empty stack.
        if (top == 0)
        {
            throw new InvalidOperationException(
                "Stack is empty.");
        }

        // Move top back to the last stored element.
        top--;

        // Store the item that will be removed.
        T item = items[top];

        // Clear the reference/value from the array.
        items[top] = default;

        return item;
    }


    // =========================================================
    // Peek
    // =========================================================

    public T Peek()
    {
        // We cannot peek when the stack is empty.
        if (top == 0)
        {
            throw new InvalidOperationException(
                "Stack is empty.");
        }

        // Return the top element without removing it.
        return items[top - 1];
    }


    // =========================================================
    // IEnumerable<T>
    // =========================================================

    public IEnumerator<T> GetEnumerator()
    {
        // Start from the top so foreach displays
        // the stack in top-to-bottom order.
        for (int i = top - 1; i >= 0; i--)
        {
            yield return items[i];
        }
    }


    // Non-generic IEnumerable implementation.
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}


// =============================================================
// Extension Method
// =============================================================

public static class StackExtensions
{
    // Converts any IEnumerable<T> into a FixedSizeStack<T>.
    public static FixedSizeStack<T> ToFixedSizeStack<T>(
        this IEnumerable<T> source,
        int capacity)
    {
        // Create a new fixed-size stack.
        FixedSizeStack<T> stack =
            new FixedSizeStack<T>(capacity);


        // Add each source item to the stack.
        foreach (T item in source)
        {
            stack.Push(item);
        }

        return stack;
    }
}