using System;
using System.Text;
using System.IO;

class Program
{
    static void Main()
    {
        string[] input = File.ReadAllLines(@"./input.txt");
        string message = input[0];

        Console.WriteLine("Part One Solution: {0}", GetResult(message, 4));
        Console.WriteLine("Part Two Solution: {0}", GetResult(message, 14));
    }
    static string GetResult(string message, int num)
    {
        for (int i = 0; i < message.Length - (num - 1); i++)
        {
            var charArray = message.Substring(i, num).ToCharArray();
            var result = charArray.Distinct();

            if (result.Count() < num) continue;

            return (num + i).ToString();
        }
        return "No result";
    }
}