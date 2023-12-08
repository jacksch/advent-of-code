namespace day05;

class Program
{
    static void Main(string[] args)
    {
        string[] input = File.ReadAllLines(@"./sampleinput.txt"); long maxSize = 100;
        // string[] input = File.ReadAllLines(@"./input.txt"); long maxSize = 10_000_000_000;

        Solution solution = new(input, maxSize);

        int partOneResult = solution.PartOne();
        int partTwoResult = solution.PartTwo();

        Console.WriteLine("Part One Solution: {0}", partOneResult);
        Console.WriteLine("Part Two Solution: {0}", partTwoResult);
    }
}

public class Solution
{
    private readonly string[] _input;
    private readonly long _maxSize;
    private readonly long[] _seeds;
    private long[] _seedsMap;
    public Solution(string[] input, long maxSize)
    {
        _input = input;
        _maxSize = maxSize;
        _seeds = ParseSeeds();
        _seedsMap = new long[maxSize];
    }

    public int PartOne()
    {

        for (int i = 2; i < _input.Length; i++)
        {
            
        }

        // long[] seedToSoil = TestParseMap();

        

        return 0;
    }

    public long[] TestParseMap()
    {


        return [];
    }

    public long[] ParseSeeds() =>
        _input[0].Split(": ")[1].Split(" ").Select(long.Parse).ToArray();

    public int PartTwo() =>
        0;
    
}
