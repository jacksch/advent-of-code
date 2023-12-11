using System.Data;
using System.Runtime.InteropServices;

namespace day05;

class Program
{
    static void Main(string[] args)
    {
        // string[] input = File.ReadAllLines(@"./sampleinput.txt");
        string[] input = File.ReadAllLines(@"./input.txt");

        Solution solution = new(input);

        uint partOneResult = solution.PartOne();
        // uint partTwoResult = solution.PartTwo();

        Console.WriteLine("Part One Solution: {0}", partOneResult);
        // Console.WriteLine("Part Two Solution: {0}", partTwoResult);
    }
}

public class Solution
{
    private readonly string[] _input;
    private readonly uint[] _seeds;

    private readonly List<List<uint[]>> _maps;

    public Solution(string[] input)
    {
        _input = input;
        _seeds = ParseSeeds();
        _maps = ParseMaps();
    }

    public uint PartOne()
    {
        uint lowestLocation = uint.MaxValue;
        foreach (uint currentSeed in _seeds)
        {
            uint seed = currentSeed;
            bool breakLine = false;
            for (int i = 2; i < _input.Length; i++)
            {
                string[] line = _input[i].Split(" ");

                if (String.IsNullOrWhiteSpace(_input[i]))
                {
                    breakLine = false;
                    continue;
                }

                if (line.Length == 2 || breakLine)
                    continue;
                
                uint destIdx = uint.Parse(line[0]);
                uint srcIdx = uint.Parse(line[1]);
                uint range = uint.Parse(line[2]);

                if (srcIdx < seed && srcIdx + range > seed)
                {
                    if (srcIdx < destIdx)
                        seed += destIdx - srcIdx;
                    else
                        seed -= srcIdx - destIdx;

                    breakLine = true;
                }
            }
            if (seed < lowestLocation)
                lowestLocation = seed;
        }

        return lowestLocation;
    }

    // public uint PartTwo()
    // {
    //     for (int i = 0; i < _seeds.Length; i += 2)
    //     {
    //         uint seedNumber = _seeds[i];
    //         uint seedRange = _seeds[i + 1];
    //     }

    //     return 0;
    // }


    public List<List<uint[]>> ParseMaps()
    {
        List<List<uint[]>> mapList = [];
        List<uint[]> map = [];
        for (int i = 2; i < _input.Length; i++)
        {
            string[] line = _input[i].Split(" ");

            if (String.IsNullOrWhiteSpace(_input[i]) || line.Length == 2)
            {
                if (map.Count > 0)
                    mapList.Add(map);

                map = [];
                continue;
            }

            uint destIdx = uint.Parse(line[0]);
            uint srcIdx = uint.Parse(line[1]);
            uint range = uint.Parse(line[2]);

            map.Add([destIdx, srcIdx, range]);
        }
        mapList.Add(map);

        return mapList;
    }


    public uint[] ParseSeeds() =>
        _input[0].Split(": ")[1].Split(" ").Select(uint.Parse).ToArray();
}
