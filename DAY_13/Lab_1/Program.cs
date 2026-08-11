using System;

// 1. Define a struct RgbColor
public struct RgbColor
{
    public byte R;
    public byte G;
    public byte B;

    // Constructor
    public RgbColor(byte r, byte g, byte b)
    {
        R = r;
        G = g;
        B = b;
    }

    // 5. Override ToString()
    public override string ToString()
    {
        return $"#{R:X2}{G:X2}{B:X2}";
    }
}

// 2. Define enum NamedColor
public enum NamedColor
{
    Red,
    Green,
    Blue,
    White,
    Black
}

// Method to convert NamedColor to RgbColor
public class ColorHelper
{
    public static RgbColor FromNamed(NamedColor name)
    {
        switch (name)
        {
            case NamedColor.Red:
                return new RgbColor(255, 0, 0);

            case NamedColor.Green:
                return new RgbColor(0, 255, 0);

            case NamedColor.Blue:
                return new RgbColor(0, 0, 255);

            case NamedColor.White:
                return new RgbColor(255, 255, 255);

            case NamedColor.Black:
                return new RgbColor(0, 0, 0);

            default:
                return new RgbColor(0, 0, 0);
        }
    }
}

// 4. Define Pixel class
public class Pixel
{
    public RgbColor Color;
}

class Program
{
    static void Main()
    {
        // -----------------------------------
        // STRUCT COPY
        // -----------------------------------

        RgbColor a = new RgbColor(255, 0, 0);

        // Struct is copied by value
        RgbColor b = a;

        // Modify b
        b.R = 1;

        Console.WriteLine("----- struct copy -----");
        Console.WriteLine($"a = {a}");
        Console.WriteLine($"b = {b}");

        // -----------------------------------
        // CLASS / REFERENCE COPY
        // -----------------------------------

        Pixel p1 = new Pixel();
        p1.Color = new RgbColor(0, 255, 0);

        // Reference is copied
        Pixel p2 = p1;

        // Modify p2.Color
        p2.Color = new RgbColor(0, 255, 0);

        Console.WriteLine();
        Console.WriteLine("----- class/reference copy -----");
        Console.WriteLine($"p1.Color = {p1.Color}");
        Console.WriteLine($"p2.Color = {p2.Color}");

        // Demonstrate that p1 and p2 refer to the same object
        Console.WriteLine();
        Console.WriteLine($"Are p1 and p2 the same object? {ReferenceEquals(p1, p2)}");
    }
}