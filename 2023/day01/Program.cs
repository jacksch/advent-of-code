using System.Text;

namespace day01;

class Program
{
    static void Main(string[] args)
    {
        string[] input = File.ReadAllLines(@"./input.txt");

        int partOneResult = PartOne(input);
        int partTwoResult = PartTwo(input);

        Console.WriteLine("Part One Solution: {0}", partOneResult);
        Console.WriteLine("Part Two Solution: {0}", partTwoResult);
    }

    static int PartOne(string[] input)
    {
        int total = 0;
        for (int i = 0; i <= input.Length - 1; i++)
        {
            string line = input[i];
            StringBuilder sb = new();

            foreach (char c in line)
            {
                if (char.IsDigit(c))
                    sb.Append(c);
            }

            if (sb.Length > 2)
                sb.Remove(1, sb.Length - 2);

            if (sb.Length < 2)
                sb.Append(sb[0]);

            total += int.Parse(sb.ToString());
        }

        return total;
    }

    static int PartTwo(string[] input)
    {
        Dictionary<string,int> numbers = new(){
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 }
        };

        int total = 0;
        for (int i = 0; i <= input.Length - 1; i++)
        {
            string line = input[i];
            StringBuilder sb = new();

            for (int c = 0; c <= line.Length - 1;)
            {
                if (char.IsDigit(line[c]))
                    sb.Append(line[c]);

                int prevSbLen = sb.ToString().Length;
                foreach (KeyValuePair<string,int> number in numbers)
                {
                    int lookAhead = number.Key.Length;
                    if (c + lookAhead > line.Length)
                        continue;

                    int strIndex = line.IndexOf(number.Key, c, lookAhead);

                    if (strIndex >= 0)
                    {
                        sb.Append(number.Value);
                        c += lookAhead - 1;
                        break;
                    }
                }
                if (prevSbLen == sb.ToString().Length)
                    c++;
            }

            if (sb.Length > 2)
                sb.Remove(1, sb.Length - 2);

            if (sb.Length < 2)
                sb.Append(sb[0]);

            total += int.Parse(sb.ToString());
        }

        return total;
    }
}
