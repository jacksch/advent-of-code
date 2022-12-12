using System;
using System.Text;
using System.IO;

class Program
{
    static void Main()
    {
        string[] input = File.ReadAllLines(@"./input.txt");

        List<string> stacks = ParseStacks(input);
        List<int[]> instructions = ParseInstructions(input);

        Console.WriteLine("Part One Solution: {0}", TopCratesPartOne(stacks, instructions));
        Console.WriteLine("Part Two Solution: {0}", TopCratesPartTwo(stacks, instructions));
    }
    static List<string> ParseStacks(string[] input)
    {
        List<string> stacks = new();

        for (int x = 1; x < input[0].Length; x += 4)
        {
            StringBuilder currentStack = new();

            for (int y = 0; y < input.Count(); y++)
            {
                if (input[y + 1].Length == 0) break;
                if (Char.IsWhiteSpace(input[y][x])) continue;

                currentStack.Append(input[y][x]);
            }
            string stack = currentStack.ReverseString();

            stacks.Add(stack);
        }
        return stacks;
    }
    static List<int[]> ParseInstructions(string[] input)
    {
        List<int[]> instructionsList = new();
        bool atInstructions = false;

        for (int i = 0; i < input.Count(); i++)
        {
            if (atInstructions)
            {
                string[] instructionStr = input[i].Split(" ");
                int numOfCrates = Convert.ToInt32(instructionStr[1]);
                int srcStack    = Convert.ToInt32(instructionStr[3]);
                int destStack   = Convert.ToInt32(instructionStr[5]);
                int[] instruction = { numOfCrates, srcStack, destStack };
                instructionsList.Add(instruction);
            }
            
            if (input[i].Length == 0) atInstructions = true;
        }

        return instructionsList;
    }
    static string TopCratesPartOne(List<string> stacks, List<int[]> instructions)
    {
        List<StringBuilder> stacksSb = ConvertToSb(stacks);

        foreach (var instruction in instructions)
        {
            int numOfCrates = instruction[0];
            int sourceStackId = instruction[1] - 1;
            int destStackId   = instruction[2] - 1;

            for (int x = 1; x <= numOfCrates; x++)
            {
                char ltr = stacksSb[sourceStackId].ToString().Last();

                stacksSb[sourceStackId].Length -= 1;
                stacksSb[destStackId].Append(ltr);
            }
        }

        return GetResult(stacksSb);
    }
    static string TopCratesPartTwo(List<string> stacks, List<int[]> instructions)
    {
        List<StringBuilder> stacksSb = ConvertToSb(stacks);

        foreach (var instruction in instructions)
        {
            int numOfCrates     = instruction[0];
            int sourceStackId   = instruction[1] - 1;
            int destStackId     = instruction[2] - 1;
            string sourceStack  = stacksSb[sourceStackId].ToString();
            
            string charsToMove = sourceStack.Substring(sourceStack.Length - numOfCrates);

            stacksSb[sourceStackId].Length -= numOfCrates;
            stacksSb[destStackId].Append(charsToMove);
        }

        return GetResult(stacksSb);
    }
    static List<StringBuilder> ConvertToSb(List<string> strList)
    {
        List<StringBuilder> list = new();
        for (int i = 0; i < strList.Count(); i++)
        {
            StringBuilder stack = new StringBuilder(strList[i]);
            list.Add(stack);
        }

        return list;
    }
    static string GetResult(List<StringBuilder> stacks)
    {
        StringBuilder result = new();

        foreach (StringBuilder stack in stacks)
        {
            string stackStr = stack.ToString();

            result.Append(stackStr.Last());
        }
        return result.ToString();
    }
}

static class Extensions
{
    public static string ReverseString(this StringBuilder sb)
    {
        string str = sb.ToString();
        char[] chars = str.ToCharArray();
        Array.Reverse(chars);

        return new string(chars);
    }
}