using System;

static class StringToolkit
{
    public static string ToTitleCase(string input)
    {
        string[] words = input.ToLower().Split(' ');

        string result = "";

        foreach (string word in words)
        {
            if (word.Length > 0)
            {
                result += char.ToUpper(word[0]) + word.Substring(1);
                result += " ";
            }
        }

        return result.Trim();
    }
}