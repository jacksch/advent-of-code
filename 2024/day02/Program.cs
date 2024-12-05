
static class Program {

    static void Main() {
        var input = ReadInput();
        var inputList = input.ToListOfIntArray();

        var partOne = PartOne(inputList);
        var partTwo = PartTwo(inputList);

        Console.WriteLine($"Part One answer: {partOne}"); // 407
        Console.WriteLine($"Part Two answer: {partTwo}"); // 459
    }

    static int PartOne(List<int[]> input) {
        int total = 0;
        foreach (var report in input) {
            if (!IsSorted(report)) { // If not ascending or descending
                continue;
            }
            int reportTotal = 1;
            int prevLevel = report[0];
            for (int i = 1; i < report.Length; i++) {
                int level = report[i];
                if (prevLevel == level) continue;

                if (Math.Abs(level - prevLevel) <= 3) {
                    reportTotal++;
                }

                prevLevel = level;
            }

            if (reportTotal == report.Length) {
                total++;
            }
        }
        return total;
    }

    static int PartTwo(List<int[]> input) {
        int total = 0;
        foreach (var report in input) {
            if (IsSafe(report.ToList())) {
                total++;
            }
        }
        return total;
    }

    static bool IsSafe(List<int> report, bool isTolerated = false) {
        bool isAscending = IsAscending(report);
        int reportTotal = 1;
        int prevLevel = report.First();
        var reportLessFirst = report.Skip(1).ToList();

        int i = 0;
        foreach (var level in reportLessFirst) {
            int difference = level - prevLevel;
            if (IsSafeDifference(isAscending, difference)) {
                reportTotal++;
            }
            else if (!isTolerated) {
                List<int> toleratedReport = new(report);
                toleratedReport.RemoveAt(i);
                if (IsSafe(toleratedReport, true)) {
                    return true;
                }

                toleratedReport = new(report);
                toleratedReport.RemoveAt(i + 1);
                if (IsSafe(toleratedReport, true)) {
                    return true;
                }

                if (i > 0) {
                    toleratedReport = new(report);
                    toleratedReport.RemoveAt(i - 1);
                    if (IsSafe(toleratedReport, true)) {
                        return true;
                    }
                }

                isTolerated = true;
            }
            else {
                break;
            }

            prevLevel = level;
            i++;
        }
        if (reportTotal == report.Count()) {
            return true;
        }
        return false;
    }

    static bool IsSorted(int[] arr) {
        var list = arr.ToList();
        if (list.Order().SequenceEqual(list) || list.OrderDescending().SequenceEqual(list)) {
            return true;
        }
        return false;
    }

    static bool IsAscending(List<int> report) {
        int total = 0;
        int prevValue = report.First();
        foreach (var num in report.Skip(1).ToList()) {
            total += num - prevValue;
            prevValue = num;
        }
        if (total > 0) {
            return true;
        }
        return false;
    }

    static bool IsSafeDifference(bool isAscending, int difference) =>
        (isAscending && difference < 4 && difference > 0) ||
        (!isAscending && difference > -4 && difference < 0)
            ? true
            : false;

    static List<int[]> ToListOfIntArray(this List<string> input) =>
        input.Select(line =>
            Array.ConvertAll(
                line.Split(' ', StringSplitOptions.RemoveEmptyEntries),
                s => int.Parse(s))
        ).ToList();

    static List<string> ReadInput() =>
        File.ReadAllLines(@"./input.txt").ToList();
}
