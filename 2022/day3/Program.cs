using System;
using System.Text;
using System.IO;

class Program
{
    static void Main()
    {
        string[] input = File.ReadAllLines(@".\input.txt");

        int prioritySum = 0;
        int badgePrioritySum = 0;
        for (int i = 0; i < input.Count(); i++)
        {
            // Part One
            string firstHalf = input[i].Substring(0, input[i].Length / 2);
            string secondHalf = input[i].Substring(input[i].Length / 2);

            var duplicateItem = String.Concat(from x in secondHalf
                                            where firstHalf.Contains(x)
                                            select x);

            prioritySum += SortPriority(duplicateItem);

            // Part Two
            if ((i + 1) % 3 == 0)
            {
                var badgeLetter = String.Concat((from y in input[i]
                                  where input[i-1].Contains(y) && input[i-2].Contains(y)
                                  select y).Distinct());

                badgePrioritySum += SortPriority(badgeLetter);
            }   
        }

        Console.WriteLine("Part One Solution: {0}", prioritySum.ToString());
        Console.WriteLine("Part Two Solution: {0}", badgePrioritySum.ToString());
    }
    static int SortPriority(string letter)
    {
        // Use ASCII values to get an easy letter order.
        // a-z: -96
        // A-Z: -38 (not 64 because of the lowercase 1-26 offset)
        int letterASCIIDecimal = (int)Encoding.ASCII.GetBytes(letter)[0];

        if (letter == letter.ToLower()) return letterASCIIDecimal -= 96;
        return letterASCIIDecimal -= 38;
    }
}