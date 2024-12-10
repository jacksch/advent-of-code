
static class Program {

    static void Main() {
        var input = ReadInput();
        var rules = ParseRules(input);

        var partOne = PartOne(input, rules);
        var partTwo = PartTwo(input, rules);

        Console.WriteLine($"Part One answer: {partOne}"); // 5964
        Console.WriteLine($"Part Two answer: {partTwo}"); // 4719
    }

    static int PartOne(string[] input, List<(int, int)> rules) =>
        ParseUpdateLists(input)
            .Select( list =>
                IsInOrder(list, rules)
                    ? list[list.Count() / 2]
                    : 0)
            .Sum();

    static int PartTwo(string[] input, List<(int, int)> rules) =>
        ParseUpdateLists(input)
            .Where(list => !IsInOrder(list, rules))
            .Select(list => {
                while (true) {
                    var failingRule = FailingRule(list.ToArray(), rules);
                    list.Move(failingRule.Item1, list.IndexOf(failingRule.Item2));

                    if (IsInOrder(list, rules)) {
                        return list[list.Count() / 2];
                    }
                }
            }).Sum();

    static bool IsInOrder(List<int> pageUpdateList, List<(int, int)> rules) {
        var pageUpdateArr = pageUpdateList.ToArray();
        foreach (var pageUpdate in pageUpdateList) {
            var pageRules = rules.Where( x => x.Item1 == pageUpdate );
            if (!IsCompliant(pageUpdateArr, pageRules)) {
                return false;
            }
        }
        return true;
    }

    static (int, int) FailingRule(int[] pageUpdateArr, IEnumerable<(int, int)> rules) {
        foreach (var pageUpdate in pageUpdateArr) {
            var pageRules = rules.Where( x => x.Item1 == pageUpdate );
            foreach (var pageRule in pageRules) {
                var lIndex = Array.IndexOf(pageUpdateArr, pageRule.Item1);
                var rIndex = Array.IndexOf(pageUpdateArr, pageRule.Item2);

                if (rIndex == -1) {
                    continue;
                }

                if (lIndex >= rIndex) {
                    return pageRule;
                }
            }
        }

        return (0,0);
    }

    static bool IsCompliant(int[] update, IEnumerable<(int, int)> pageRules) {
        foreach (var pageRule in pageRules) {
            var lIndex = Array.IndexOf(update, pageRule.Item1);
            var rIndex = Array.IndexOf(update, pageRule.Item2);

            if (rIndex == -1) {
                continue;
            }

            if (lIndex >= rIndex) {
                return false;
            }
        }

        return true;
    }

    static List<(int, int)> ParseRules(string[] input) =>
        input.Take(Array.IndexOf(input, String.Empty))
            .Select(rules => rules.Split('|'))
            .Select(rules => (int.Parse(rules[0]), int.Parse(rules[1])))
            .ToList();

    static List<List<int>> ParseUpdateLists(string[] input) {
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

    static void Move<T>(this List<T> list, T item, int newIndex) {
        if (item != null) {
            var oldIndex = list.IndexOf(item);
            if (oldIndex > -1) {
                list.RemoveAt(oldIndex);
                if (newIndex > oldIndex) {
                    newIndex--;
                }

                list.Insert(newIndex, item);
            }
        }
    }

    static string ToString(this List<int> list) {
        return string.Join(", ", list);
    }

    static string[] ReadInput() =>
        File.ReadAllLines(@"./input.txt");
}

