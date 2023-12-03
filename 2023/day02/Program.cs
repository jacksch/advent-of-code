using System.Numerics;

namespace day02;

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
        Dictionary<string, int> cubeColours = new() {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 }
        };

        int result = 0;
        for (int i = 0; i <= input.Length - 1; i++)
        {
            List<bool> gamePossible = [];
            string line = input[i];
            string[] game = line.Split(":");

            int gameId = int.Parse(game[0].Split(" ")[1]);
            string[] plays = game[1].Split(";");

            foreach (string play in plays)
            {
                string[] colourCounts = play.Split(",");
                foreach (string colourCount in colourCounts)
                {
                    string[] countStr = colourCount.Trim().Split();

                    string colour = countStr[1];
                    int count = int.Parse(countStr[0]);

                    if (count <= cubeColours[colour])
                        gamePossible.Add(true);
                    else
                        gamePossible.Add(false);
                }
            }

            if (!gamePossible.Contains(false))
                result += gameId;
        }

        return result;
    }

    static int PartTwo(string[] input)
    {
        int result = 0;
        for (int i = 0; i <= input.Length - 1; i++)
        {
            string line = input[i];
            string[] game = line.Split(":");

            int gameId = int.Parse(game[0].Split(" ")[1]);
            string[] plays = game[1].Split(";");

            int redMin = 0;
            int greenMin = 0;
            int blueMin = 0;

            foreach (string play in plays)
            {
                string[] colourCounts = play.Split(",");
                foreach (string colourCount in colourCounts)
                {
                    string[] countStr = colourCount.Trim().Split();

                    string colour = countStr[1];
                    int count = int.Parse(countStr[0]);
                    switch (colour)
                    {
                        case "red":
                            if (count > redMin)
                                redMin = count;
                            break;
                        case "green":
                            if (count > greenMin)
                                greenMin = count;
                            break;
                        case "blue":
                            if (count > blueMin)
                                blueMin = count;
                            break;
                    }
                }
            }

            result += redMin * greenMin * blueMin;
        }

        return result;
    }
}
