using System;
using System.IO;
using System.Linq;

string[] input = File.ReadAllLines(@"./input.txt");

int currentTotal = 0;
List<int> totals = new();

for (int i = 0; i < input.Count(); i++)
{
    if (input[i] != "")
    {
        currentTotal += Convert.ToInt32(input[i]);
    }
    else if (input[i] == "" || i == input.Count() -1)
    {
        totals.Add(currentTotal);
        currentTotal = 0;
    }
}

int topThreeTotal = 0;

var topThree = (from i in totals
                orderby i descending
                select i).Take(3);

foreach (var x in topThree)
{
    topThreeTotal += x;
}

Console.WriteLine("Part One Solution: {0}", totals.Max());
Console.WriteLine("Part Two Solution: {0}", topThreeTotal);