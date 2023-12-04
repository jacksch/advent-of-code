using System.Text;

namespace day03;

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
        int result = 0;
        for (int i = 0; i <= input.Length - 1; i++)
        {
            StringBuilder sb = new();
            string line = input[i];

            for (int c = 0; c <= line.Length - 1; c++)
            {
                if (char.IsDigit(line[c]))
                {
                    sb.Append(line[c]);
                    if (c + 1 == line.Length || !char.IsDigit(line[c + 1]))
                    {
                        string numString = sb.ToString();
                        if (IsPartNumber(input, numString.Length, i, c - numString.Length + 1))
                            result += int.Parse(sb.ToString());
                    }
                }
                else if (c > 0)
                {
                    if (char.IsDigit(line[c - 1]))
                        sb = new StringBuilder();
                }

            }
        }

        return result;
    }

    public static int PartTwo(string[] input)
    {
        int result = 0;
        for (int i = 0; i < input.Length - 1; i++)
        {
            string line = input[i];

            for (int c = 0; c < line.Length - 1; c++)
            {
                if (line[c].Equals('*') && IsGear(input, i, c))
                    result += GetGearRatio(input, i, c);
            }
        }

        return result;
    }

    public static bool IsGear(string[] input, int lineIndex, int charIndex)
    {
        int li = lineIndex;
        int ci = charIndex;
        int count = 0;
        for (int i = -1; i <= 1; i++)
        {
            bool isNumber = false;
            for (int j = -1; j <= 1; j++)
            {
                char currentChar = input[li + i][ci + j];

                if (char.IsDigit(currentChar))
                {
                    isNumber = true;
                }
                if (!char.IsDigit(currentChar) && isNumber)
                {
                    isNumber = false;
                    count++;
                }
            }
            if (isNumber)
                count++;
        }
        if (count == 2)
            return true;

        return false;
    }

    public static int GetGearRatio(string[] input, int lineIndex, int charIndex)
    {
        int li = lineIndex;
        int ci = charIndex;

        int result = 1;
        for (int i = -1; i <= 1; i++)
        {
            bool buildingNumber = false;
            bool numberFound = false;
            StringBuilder sb = new();
            for (int j = -1; j <= 1; j++)
            {
                if (char.IsDigit(input[li + i][ci + j]) && !buildingNumber)
                    numberFound = true;

                while (numberFound)
                {
                    if (ci + j == 0 || !char.IsDigit(input[li + i][ci + j - 1]))
                    {
                        numberFound = false;
                        buildingNumber = true;

                        break;
                    }
                    j--;
                }

                while (buildingNumber)
                {
                    sb.Append(input[li + i][ci + j]);
                    if (ci + j == input[li].Length - 1 || !char.IsDigit(input[li + i][ci + j + 1]))
                    {
                        buildingNumber = false;
                        result *= int.Parse(sb.ToString());
                        sb = new();
                        break;
                    }
                    j++;
                }
            }
        }

        return result;
    }

    public static bool IsPartNumber (string[] input, int numLength, int lineNumber, int startIndex)
    {
        char[] symbols = ['@', '#', '$', '%', '&', '*', '-', '+', '=', '/'];

        int i = lineNumber;
        int ll = input[i].Length - 1;
        int j = startIndex;
        int e = startIndex + numLength - 1;

        if (j > 0 && symbols.Contains(input[i][j - 1])) // Check left
            return true;

        if (e < ll && symbols.Contains(input[i][e + 1])) // Check right
            return true;

        for (int k = 0; k <= numLength - 1; k++) // Check above
            if (i > 0 && symbols.Contains(input[i - 1][j + k]))
                return true;

        for (int k = 0; k <= numLength - 1; k++) // Check underneath
            if (i < input.Length - 1 && symbols.Contains(input[i + 1][j + k]))
                return true;

        if (j > 0 && i > 0 && symbols.Contains(input[i - 1][j - 1])) // Check top-left
            return true;

        if (e < ll && i > 0 && symbols.Contains(input[i - 1][e + 1])) // Check top-right
            return true;

        if (j > 0 && i < input.Length - 1 && symbols.Contains(input[i + 1][j - 1])) // Check bottom-left
            return true;

        if (e < ll && i < input.Length - 1 && symbols.Contains(input[i + 1][e + 1])) // Check bottom-right
            return true;

        return false;
    }
}
