using System.Text;

static class Program {

    static void Main() {
        var input = ReadInput();

        var partOnePositions = PartOne(input);
        var partTwo          = PartTwo(input, partOnePositions);

        Console.WriteLine($"Part One answer: {partOnePositions.Count()}"); // 5269
        Console.WriteLine($"Part Two answer: {partTwo}");                  // 1957
    }

    static List<(int, int)> PartOne(string[] input) {
        var startPos = FindStart(input);

        var dir = (X: 0, Y: -1);
        var currentPos = startPos;
        List<(int X, int Y)> pathPositions = new();
        while (true) {
            if (!pathPositions.Contains(currentPos)) {
                pathPositions.Add(currentPos);
            }

            (int X, int Y) nextPos = (currentPos.X + dir.X, currentPos.Y + dir.Y);
            if (IsOutOfBounds(input, nextPos)) {
                break;
            }

            if (IsEmptySpace(input, nextPos)) {
                currentPos = nextPos;
                continue;
            }

            dir = TurnRight(dir);
        }

        return pathPositions;
    }

    static int PartTwo(string[] input, List<(int X, int Y)> pathPositions) {
        List<(int, int)> obstaclePositions = new();
        foreach (var position in pathPositions) {
            string[] inputWithObstacle = (string[]) input.Clone();

            StringBuilder sb = new(inputWithObstacle[position.Y]); // Add the obstacle
            sb[position.X] = 'O';
            inputWithObstacle[position.Y] = sb.ToString();

            int turnsCount = 0; // For finding deadend loops
            bool isLoop = false;

            var currentPos = FindStart(inputWithObstacle);
            var dir = (X: 0, Y: -1);
            while (true) {
                (int X, int Y) nextPos = (currentPos.X + dir.X, currentPos.Y + dir.Y);
                if (IsOutOfBounds(inputWithObstacle, nextPos)) {
                    break;
                }

                if (IsEmptySpace(inputWithObstacle, nextPos)) {
                    currentPos = nextPos;
                    continue;
                }

                dir = TurnRight(dir);

                turnsCount++;
                if (turnsCount > 1_000) {
                    isLoop = true;
                    break;
                }
            }
            if (isLoop) {
                obstaclePositions.Add(position);
            }
        }

        return obstaclePositions.Count();
    }

    static bool IsOutOfBounds(string[] input, (int X, int Y) pos) =>
        pos.X < 0 || pos.X == input[0].Length || pos.Y < 0 || pos.Y == input.Length
            ? true
            : false;

    static bool IsEmptySpace(string[] input, (int X, int Y) pos) =>
        input[pos.Y][pos.X] == '.' || input[pos.Y][pos.X] == '^'
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
}

