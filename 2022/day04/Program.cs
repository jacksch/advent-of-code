using System;
using System.IO;

class Program
{
    static void Main()
    {
        string[] input = File.ReadAllLines(@"./input.txt");

        int partOneCount = 0;
        int partTwoCount = 0;
        for (int i = 0; i < input.Count(); i++)
        {
            string[] elfPair = input[i].Split(",");

            string[] elf1 = elfPair[0].Split("-");
            string[] elf2 = elfPair[1].Split("-");

            int[] elf1arr = GetArray(elf1[0],elf1[1]);
            int[] elf2arr = GetArray(elf2[0],elf2[1]);

            if (ContainsTheOther(elf1arr, elf2arr)) partOneCount++;
            if (Overlaps(elf1arr,elf2arr)) partTwoCount++;
        }
        
        Console.WriteLine("Part One Solution: {0}", partOneCount.ToString());
        Console.WriteLine("Part Two Solution: {0}", partTwoCount.ToString());
    }

    static int[] GetArray(string startStr, string endStr)
    {
            int start = Convert.ToInt32(startStr);
            int end   = Convert.ToInt32(endStr);

            return Enumerable.Range(start, end - start + 1).ToArray();
    }

    static bool ContainsTheOther(int[] arr1, int[] arr2)
    {
        if (arr1[0] >= arr2[0] && arr1.Last() <= arr2.Last() ||
            arr1[0] <= arr2[0] && arr1.Last() >= arr2.Last()) return true;
        return false;
    }

    static bool Overlaps(int[] arr1, int[] arr2)
    {
        var sameValue = String.Concat(from x in arr1
                                      where arr2.Contains(x)
                                      select x);

        if (!String.IsNullOrWhiteSpace(sameValue)) return true;
        return false;
    }
}