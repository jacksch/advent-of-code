
static class Program {

    static void Main() {
        var input = ReadInput();

        var partOne = PartOne(input);
        var partTwo = PartTwo(input);

        Console.WriteLine($"Part One answer: {partOne}"); // 175015740
        Console.WriteLine($"Part Two answer: {partTwo}"); // 112272912
    }

    static int PartOne(string input) {
        int total = 0;

        var instructions = input.Split("mul");
        foreach (var inst in instructions) {
            Mul mul;
            bool success = TryParseInstruction(inst, out mul);
            if (!success) {
                continue;
            }
            total += (mul.X * mul.Y);
        }

        return total;
    }

    static int PartTwo(string input) {
        int total = 0;

        var dos = input.Split("do");
        foreach (var region in dos) {
            if (region.Substring(0,3) == "n't") {
                continue;
            }

            var instructions = region.Split("mul");
            foreach (var inst in instructions) {
                Mul mul;
                bool success = TryParseInstruction(inst, out mul);
                if (!success) {
                    continue;
                }
                total += (mul.X * mul.Y);
            }
        }

        return total;
    }

    static bool TryParseInstruction(string inst, out Mul result) {
        if (inst[0] != '(' || !inst.Contains(',')) {
            result = default;
            return false;
        }

        inst = inst.Substring(1, inst.Length - 1);
        var mults = inst.Split(',');
        if (mults[0].Length > 3) {
            result = default;
            return false;
        }

        string yStr = string.Empty;
        int x;
        if (!int.TryParse(mults[0], out x)) {
            result = default;
            return false;
        }

        string yRaw = mults[1];
        for (int i = 0; i < yRaw.Length - 1; i++) { // Trim the junk off the end of Y
            if (int.TryParse(yRaw[i].ToString(), out _)) {
                yStr += yRaw[i];
            }
            else if (yRaw[i] != ')') {
                result = default;
                return false;
            }
            else {
                break;
            }
        }

        if (yStr.Length == 0) {
            result = default;
            return false;
        }
        int y = int.Parse(yStr);

        result = new(x, y);
        return true;
    }

    static string ReadInput() =>
        string.Join(' ', File.ReadAllLines(@"./input.txt"));
}

struct Mul {
    public int X { get; }
    public int Y { get; }

    public Mul(int x, int y) {
        X = x;
        Y = y;
    }
}
