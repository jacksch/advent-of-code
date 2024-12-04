
static class Program {

    static void Main() {
        var input = ReadInput();
        var inputList = input.ToListOfIntArray();

        var partOne = PartOne(inputList);
        var partTwo = PartTwo(inputList);

        Console.WriteLine($"Part One answer: {partOne}"); // 1197984
        Console.WriteLine($"Part Two answer: {partTwo}"); // 23387399
    }

    static int PartOne(List<int[]> input) {
        var c1 = input.GetColumn(0).OrderBy(x => x);
        var c2 = input.GetColumn(1).OrderBy(x => x);

        int total = c1.Zip(c2)
            .Select(nums =>
                Math.Abs(nums.First - nums.Second))
            .Sum();

        return total;
    }

    static int PartTwo(List<int[]> input) {
        var c1 = input.GetColumn(0);
        var c2 = input.GetColumn(1);
        var c2grp = c2.GroupBy(x => x); // Precompute number of instances in second column

        int score = c1.Select(n1 =>
            n1 * c2grp.Where(n2 =>
                n2.Key == n1).Select(n2 =>
                    n2.Count())
                .FirstOrDefault())
            .Sum();

        return score;
    }

    static List<int> GetColumn(this List<int[]> input, int column) {
        return input.Select(x => x[column]).ToList();
    }

    static List<int[]> ToListOfIntArray(this List<string> input) =>
        input.Select(line =>
            Array.ConvertAll(
                line.Split(' ', StringSplitOptions.RemoveEmptyEntries),
                s => int.Parse(s))
        ).ToList();

    static List<string> ReadInput() =>
        File.ReadAllLines(@"./input.txt").ToList();
}
