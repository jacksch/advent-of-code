using System;
using System.IO;
using System.Linq;

string[] input = File.ReadAllLines(@".\input.txt");

// Part 1: Get the elf with the most calories
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

// Part 2: Get the total calories for the top 3 elves
int topThreeTotal = 0;

var topThree = (from i in totals
                orderby i descending
                select i).Take(3);

foreach (var x in topThree)
{
    topThreeTotal += x;
}

Console.WriteLine("The elf with the most calories of food has: {0}",totals.Max());
Console.WriteLine("The top three elves has a total calories of: {0}",topThreeTotal);