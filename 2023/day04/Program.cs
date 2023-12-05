namespace day04;

class Program
{
    static void Main(string[] args)
    {
        string[] input = File.ReadAllLines(@"./input.txt");

        Solution solution = new(input);

        int partOneResult = solution.PartOne();
        int partTwoResult = solution.PartTwo();

        Console.WriteLine("Part One Solution: {0}", partOneResult);
        Console.WriteLine("Part Two Solution: {0}", partTwoResult);
    }
}

public class Solution
{
    private readonly string[] _input;
    private readonly int _inputLen;
    private readonly int[][] _cards;

    public Solution(string[] input)
    {
        _input = input;
        _inputLen = input.Length;
        _cards = ParseGames();
    }

    public int PartOne()
    {
        int result = 0;

        for (int i = 0; i < _cards.Length; i += 2)
        {
            int matches = _cards[i].Intersect(_cards[i + 1]).Count();
            result += (int) Math.Pow(2, matches - 1);
        }

        return result;
    }

    public int PartTwo()
    {
        int result = 0;
        int[] cardsCounts = Enumerable.Repeat(1, _cards.Length).ToArray();

        for (int i = 0; i < _cards.Length; i += 2)
        {
            int cardsCount = cardsCounts[(i + 2) / 2];
            result += cardsCount;

            while (cardsCount > 0)
            {
                int matches = _cards[i].Intersect(_cards[i + 1]).Count();
                int j = 0;

                while (j < matches)
                {
                    cardsCounts[(i + 2) / 2 + j + 1] += 1;
                    j++;
                }
                cardsCount--;
            }
        }

        return result;
    }

    public int[][] ParseGames()
    {
        List<int[]> cards = new();
        for (int i = 0; i < _inputLen; i++)
        {
            string[] card = _input[i].Split(":", StringSplitOptions.RemoveEmptyEntries);
            string[] numbers = card[1].Split("|");

            string[] winningNums = numbers[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            cards.Add(winningNums.Select(x => int.Parse(x)).ToArray());


            string[] gameNums = numbers[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            cards.Add(gameNums.Select(x => int.Parse(x)).ToArray());
        }

        return cards.ToArray();
    }
}
