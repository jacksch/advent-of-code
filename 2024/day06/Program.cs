
static class Program {

    static void Main() {
        var input = ReadInput();

        var partOne = PartOne(input);
        var partTwo = PartTwo(input);

        Console.WriteLine($"Part One answer: {partOne}"); // 5269
        Console.WriteLine($"Part Two answer: {partTwo}"); // Between 1734 and 2054 - Not 1867, 1921, or 2296 (too high)
    }

    static int PartOne(string[] input) {
        var start = FindStart(input);
        // Console.WriteLine("Start: {0}", start);

        // Up: 0, -1
        // Right: 1, 0
        // Down: 0, 1
        // Left: -1, 0
        var dir = (X: 0, Y: -1);
        var currentPos = start;
        List<(int, int)> positions = new();
        while (true) {
            if (!positions.Contains(currentPos)) {
                positions.Add(currentPos);
            }

            (int X, int Y) nextPos = (currentPos.X + dir.X, currentPos.Y + dir.Y);
            if (IsOutOfBounds(input, nextPos)) {
                break;
            }

            char nextPosChar = input[nextPos.Y][nextPos.X];
            // Console.WriteLine("Current: {0} - Next: {1} - NextChar: {2} - Dir: {3}", currentPos, nextPos, nextPosChar, dir);
            if (nextPosChar == '.' || nextPosChar == '^') {
                currentPos = nextPos;
                continue;
            }

            dir = TurnRight(dir);
        }

        return positions.Count();
    }

    static int PartTwo(string[] input) {
        var start = FindStart(input);
        // Console.WriteLine("Start: {0}", start);

        var dir = (X: 0, Y: -1); // X, Y
        var currentPos = start;
        int loopCount = 0;

        // List<(int, int)> positions = new();
        List<string> positions = new();
        while (true) {
            // if (!positions.Contains(currentPos)) {
            //     positions.Add(currentPos);
            // }
            (int X, int Y) nextPos = (currentPos.X + dir.X, currentPos.Y + dir.Y);
            if (IsOutOfBounds(input, nextPos)) {
                break;
            }

            // Up: 0, -1
            // Right: 1, 0
            // Down: 0, 1
            // Left: -1, 0
            char nextPosChar = input[nextPos.Y][nextPos.X]; // 1, 2, 

            // Console.WriteLine("Current: {0} - Next: {1} - NextChar: {2} - Dir: {3}", currentPos, nextPos, nextPosChar, dir);
            if (nextPosChar == '.' || nextPosChar == '^') {
                if (IsLoop(input, currentPos, dir)) {
                    if (!positions.Contains($"{currentPos},{dir}")) { // TODO: Include dir
                        // Console.WriteLine($"{currentPos},{dir}");
                        positions.Add($"{currentPos},{dir}");
                        loopCount++;
                        // Console.WriteLine("Loop -- Current: {0} - Next: {1} - NextChar: {2} - Dir: {3}", currentPos, nextPos, nextPosChar, dir);
                    }
                }
                currentPos = nextPos;
                continue;
            }

            dir = TurnRight(dir);
        }

        return loopCount;
    }

    // Up: 0, -1
    // Right: 1, 0
    // Down: 0, 1
    // Left: -1, 0
    static bool IsLoop(string[] input, (int, int) position, (int X, int Y) dir) {
        // if (position == (8, 4)) {
        //     Console.WriteLine("Loop check. Pos: {0} - Dir {1}", position, dir);
        // }
        int turnsCount = 1;
        bool dirChange = true;
        dir = TurnRight(dir);
        (int X, int Y) currentPos = position;
        // int currentSteps = 0;
        while (true) {
            (int X, int Y) nextPos = (currentPos.X + dir.X, currentPos.Y + dir.Y);
            if (IsOutOfBounds(input, nextPos)) {
                return false;
            }

            char nextPosChar = input[nextPos.Y][nextPos.X];
            // if (position == (8, 4)) {
            //     Console.WriteLine("Loop -- Current: {0} - Next: {1} - NextChar: {2} - Dir: {3} - StartPos: {4}", currentPos, nextPos, nextPosChar, dir, position);
            // }
            if (currentPos == position && !dirChange) {
                // if (position == (8, 4)) {
                //     Console.WriteLine("here?");
                // }
                return true;
            }
            if (nextPosChar == '.' || nextPosChar == '^') {
                currentPos = nextPos;
                dirChange = false;
                // currentSteps++;
                continue;
            }
            // if (position == (8, 4)) {
            //     Console.WriteLine("here?");
            // }

            // if (currentSteps < 3) {
            //     return false;
            // }
            dir = TurnRight(dir);
            dirChange = true;
            // if (currentPos == position && currentSteps >= 1) {
            //     return true;
            // }
            turnsCount++;
            if (turnsCount > 10_000) {
                return true;
            }
            // currentSteps = 0;
        }

        // return false;
    }

    static bool IsOutOfBounds(string[] input, (int X, int Y) pos) =>
        pos.X < 0 || pos.X == input[0].Length || pos.Y < 0 || pos.Y == input.Length
            ? true
            : false;


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
            _ => throw new ArgumentException($"Not a valid direction: {dir}")
        };

    static string[] ReadInput() =>
        File.ReadAllLines(@"./input.txt");
        // File.ReadAllLines(@"./sampleinput.txt");
}

