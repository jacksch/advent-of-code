
static class Program {

    static void Main() {
        var input = ReadInput();

        var partOne = PartOne(input);
        var partTwo = PartTwo(input);

        Console.WriteLine($"Part One answer: {partOne}"); // 2358
        Console.WriteLine($"Part Two answer: {partTwo}"); // 1737
    }

    static int PartOne(string[] input) =>
        input.Select((y, yIndex) =>
            y.Select((x, xIndex) =>
                XmasCount(ref input, xIndex, yIndex))
                .Sum())
            .Sum();

    static int PartTwo(string[] input) {
        int total = 0;

        for (int y = 1; y < input.Length - 1; y++) {
            for (int x = 1; x < input[y].Length - 1; x++) {
                if (IsXmasX(ref input, x, y)) {
                    total++;
                }
            }
        }
        return total;
    }

    static int XmasCount(ref string[] input, int x, int y) {
        if (input[y][x] != 'X') return 0;
        int total = 0;

        for (int ySearch = y - 1; ySearch <= y + 1; ySearch++) {
            if (ySearch < 0 || ySearch == input.Length) continue; // Out of bounds checking

            for (int xSearch = x - 1; xSearch <= x + 1; xSearch++) {
                if (xSearch < 0 || xSearch == input[y].Length) continue;

                if (input[ySearch][xSearch] == 'M' && IsXmasInDirection(ref input, x, y, xSearch, ySearch)) {
                    total++;
                }
            }
        }

        return total;
    }

    static bool IsXmasInDirection(ref string[] input, int x, int y, int xOffset, int yOffset) {
        int letters = 1;
        int xDir = xOffset - x;
        int yDir = yOffset - y;

        for (int i = 0; i < 3; i++) {
            if (yOffset < 0 || yOffset == input.Length)
                continue;

            if (xOffset < 0 || xOffset == input[y].Length)
                continue;

            if (input[yOffset][xOffset] == 'M' && letters == 1) {
                yOffset = yOffset + yDir;
                xOffset = xOffset + xDir;
                letters++;
            }
            else if (input[yOffset][xOffset] == 'A' && letters == 2) {
                yOffset = yOffset + yDir;
                xOffset = xOffset + xDir;
                letters++;
            }
            else if (input[yOffset][xOffset] == 'S' && letters == 3) {
                yOffset = yOffset + yDir;
                xOffset = xOffset + xDir;
                letters++;
            }
        }

        if (letters == 4)
            return true;

        return false;
    }

    static bool IsXmasX(ref string[] input, int x, int y) {
        if (input[y][x] != 'A')
            return false;

        int masCount = 0;
        if (input[y - 1][x - 1] == 'M' && input[y + 1][x + 1] == 'S') { // Top-left to bottom-right
            masCount++;
        }
        if (input[y - 1][x + 1] == 'M' && input[y + 1][x - 1] == 'S') { // Top-right to bottom-left
            masCount++;
        }
        if (input[y + 1][x - 1] == 'M' && input[y - 1][x + 1] == 'S') { // Bottom-left to top-right
            masCount++;
        }
        if (input[y + 1][x + 1] == 'M' && input[y - 1][x - 1] == 'S') { // Bottom-right to top-left
            masCount++;
        }

        if (masCount == 2)
            return true;

        return false;
    }


    static string[] ReadInput() =>
        File.ReadAllLines(@"./input.txt");
}

