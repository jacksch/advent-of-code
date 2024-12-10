
static class Program {

    static void Main() {
        var input = ReadInput();

        var partOne = PartOne(input);
        // var partTwo = PartTwo(input);

        Console.WriteLine($"Part One answer: {partOne}"); //
        // Console.WriteLine($"Part Two answer: {partTwo}"); //
    }

    static int PartOne(string[] input) {
        var start = FindStart(input);
        Console.WriteLine("Start: {0}", start);

        // Up: 0, -1
        // Right: 1, 0
        // Down: 0, 1
        // Left: -1, 0
        var dir = (X: 0, Y: -1); // X, Y
        var currentPos = start;
        int total = 0;
        while (true) {
            (int X, int Y) nextPos = (currentPos.X + dir.X, currentPos.Y + dir.Y);
            if (nextPos.X < 0 || nextPos.X == input[0].Length || nextPos.Y < 0 || nextPos.Y == input.Length) {
                break;
            }
            char nextPosChar = input[nextPos.Y][nextPos.X];
            total++; // TODO: Count distinct positions

            Console.WriteLine("Current: {0} - Next: {1} - NextChar: {2} - Dir: {3}", currentPos, nextPos, nextPosChar, dir);
            // Console.WriteLine(nextPosChar);
            if (nextPosChar == '.' || nextPosChar == '^') {
                currentPos = nextPos;
                continue;
            }
            
            dir = TurnRight(dir);
        }

        return total;
    }

    // Up: 0, -1
    // Right: 1, 0
    // Down: 0, 1
    // Left: -1, 0
    static (int X, int Y) TurnRight((int, int) dir) =>
        dir switch {
            (0, -1) => (1, 0),  // Up to Right
            (1, 0) => (0, 1),   // Right to Down
            (0, 1) => (-1, 0),  // Down to Left
            (-1, 0) => (0, -1), // Left to Up
            _ => throw new ArgumentException()
        };



    static int PartTwo(string[] input) {
        return 0;
    }

    static (int X, int Y) FindStart(string[] input) {
        for (int y = 0; y < input.Length; y++) {
            for (int x = 0; x < input[y].Length; x++) {
                if (input[y][x] == '^') {
                    return (x, y);
                }
            }
        }

        return (0, 0);
    }


    static string[] ReadInput() =>
        // File.ReadAllLines(@"./input.txt");
        File.ReadAllLines(@"./sampleinput.txt");
}

// struct Coord(int X, int Y) {
//     int X { get; } = X;
//     int Y { get; } = Y;
//
//     public override string ToString() {
//         return $"{X}, {Y}";
//     }
// }
//
