
static class Program {

    static void Main() {
        var input = ReadInput();

        var partOne = PartOne(input);
        var partTwo = PartTwo(input);

        Console.WriteLine($"Part One answer: {partOne}"); // 5964
        Console.WriteLine($"Part Two answer: {partTwo}"); //
    }

    static int PartOne(string[] input) {
        var rules = ParseRules(input);
        var updates = ParseUpdates(input);

        int total = 0;
        foreach (var update in updates) {
            bool correctOrder = true;
            var updateArr = update.ToArray();
            foreach (var pageUpdate in update) {
                // var pageRules = rules.Where( x => x.Item1 == pageUpdate );
                // foreach (var pageRule in pageRules) {
                //     var lIndex = Array.IndexOf(updateArr, pageRule.Item1);
                //     var rIndex = Array.IndexOf(updateArr, pageRule.Item2);
                //
                //     if (rIndex == -1) {
                //         continue;
                //     }
                //
                //     if (lIndex < rIndex) {
                //         correctOrder = true;
                //     }
                //     else {
                //         correctOrder = false;
                //         break;
                //     }
                // }
                // if (!correctOrder) {
                //     break;
                // }
                var pageRules = rules.Where( x => x.Item1 == pageUpdate );
                if (IsCompliant(updateArr, pageRules)) {
                    correctOrder = true;
                }
                else {
                    correctOrder = false;
                    break;
                }
            }
            if (correctOrder) {
                total += update[update.Count() / 2];
            }
        }


        return total;
    }

    static int PartTwo(string[] input) {
        var rules = ParseRules(input);
        var updates = ParseUpdates(input);

        int total = 0;
        foreach (var update in updates) {
            bool correctOrder = false;
            var updateArr = update.ToArray();
            foreach (var pageUpdate in update) {
                var pageRules = rules.Where( x => x.Item1 == pageUpdate );
                if (IsCompliant(updateArr, pageRules)) {
                    correctOrder = true;
                }
                else {
                    correctOrder = false;
                    break;
                }
            }
            if (correctOrder) {
                total += update[update.Count() / 2];
            }
        }

        return total;
    }

    static bool IsCompliant(int[] update, IEnumerable<(int, int)> pageRules) {
        // bool isCompliant = false;
        foreach (var pageRule in pageRules) {
            var lIndex = Array.IndexOf(update, pageRule.Item1);
            var rIndex = Array.IndexOf(update, pageRule.Item2);

            if (rIndex == -1) {
                continue;
            }

            // Console.WriteLine("{0}, {1}, {2}", pageRule, lIndex, rIndex);

            if (lIndex >= rIndex) {
                return false;
            }
        }

        return true;
        // Console.WriteLine("Update: {0} is {1}", update, isCompliant);
        // return isCompliant;
    }

    static List<(int, int)> ParseRules(string[] input) =>
        input.Take(Array.IndexOf(input, String.Empty))
            .Select(rules => rules.Split('|'))
            .Select(rules => (int.Parse(rules[0]), int.Parse(rules[1])))
            .ToList();

    static List<List<int>> ParseUpdates(string[] input) {
        List<List<int>> pages = new();

        foreach(var pageListStr in input) {
            if (pageListStr.Contains('|') || pageListStr.Length == 0) {
                continue;
            }

            pages.Add( pageListStr.Split(',')
                .Select( x => int.Parse(x) )
                .ToList());
        }

        return pages;
    }

    static string[] ReadInput() =>
        File.ReadAllLines(@"./input.txt");
        // File.ReadAllLines(@"./sampleinput.txt");
}

