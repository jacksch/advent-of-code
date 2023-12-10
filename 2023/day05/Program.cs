namespace day05;

class Program
{
    static void Main(string[] args)
    {
        string[] input = File.ReadAllLines(@"./sampleinput.txt");
        // string[] input = File.ReadAllLines(@"./input.txt");

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
    private readonly uint[] _seeds;
    public Solution(string[] input)
    {
        _input = input;
        _seeds = ParseSeeds();
    }

    public int PartOne()
    {
        foreach (uint seed in _seeds)
        {
            for (int i = 2; i < _input.Length; i++)
            {
                if (seed != 79) continue; // Testing
                int m = 0;
                string[] line = _input[i].Split(" ");
                if (String.IsNullOrWhiteSpace(_input[i]) && m == 2)
                {
                    break; // Testing
                    m++;
                }
                if (String.IsNullOrWhiteSpace(_input[i]))
                {
                    Console.WriteLine(_input[i + 1]);
                    continue;
                }
                

                if (line.Length == 2)
                    continue;
                
                uint destIdx = uint.Parse(line[0]);
                uint srcIdx = uint.Parse(line[1]);
                uint range = uint.Parse(line[2]);

                Console.WriteLine("Seed: {0}, Dest: {1}, Src: {2}, Range {3}", seed, destIdx, srcIdx, range);

                // if src - range - 1 == seed, etc
                if (srcIdx < seed && srcIdx + range - 1 > seed)
                {
                    // Console.WriteLine("We have something?");
                    long offset = (long)destIdx - (long)srcIdx;
                    long newSeed = seed + offset;
                    Console.WriteLine(newSeed);
                }
                else
                {
                    // Should stay the same?
                }

            }
        }
        return 0;
    }

    public uint[] ParseSeeds() =>
        _input[0].Split(": ")[1].Split(" ").Select(uint.Parse).ToArray();

    public int PartTwo() =>
        0;
    
}
