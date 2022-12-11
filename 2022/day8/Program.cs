using System;
using System.Text;
using System.IO;

class Program
{
    static void Main()
    {
        string[] input = File.ReadAllLines(@"./input.txt");
        
        int visableCount = 0;
        List<int> scores = new();

        for (int x = 0; x < input[0].Length; x++)
        {
            for (int y = 0; y < input.Count(); y++)
            {
                if (IsVisable(input, x, y)) 
                {
                    visableCount++;
                }
                scores.Add(GetScore(input, x, y));
            }
        }

        Console.WriteLine("Part One Solution: {0}", visableCount);
        Console.WriteLine("Part Two Solution: {0}", scores.Max());
    }
    static bool IsVisable(string[] grid, int x, int y)
    {
        // if tree is on the border
        if (x == 0 ||
            y == 0 ||
            x == grid.Count() - 1 ||
            y == grid.Length - 1) return true;

        if (VisableToNorth(grid, x, y)) return true;
        if (VisableToSouth(grid, x, y)) return true;
        if (VisableToEast(grid, x, y)) return true;
        if (VisableToWest(grid, x, y)) return true;

        return false;
    }
    static bool VisableToNorth(string[] grid, int x, int y)
    {
        int treeHeight = grid[y][x];
        for (int i = 1; i <= y; i++)
        {
            if (treeHeight <= grid[y - i][x]) return false;
        }
        return true;
    }
    static bool VisableToSouth(string[] grid, int x, int y)
    {
        int treeHeight = grid[y][x];
        for (int i = 1; (y + i) < grid.Count(); i++)
        {
            if (treeHeight <= grid[y + i][x]) return false;
        }
        return true;
    }
    static bool VisableToEast(string[] grid, int x, int y)
    {
        int treeHeight = grid[y][x];
        for (int i = 1; (x + i) < grid.Length; i++)
        {
            if (treeHeight <= grid[y][x + i]) return false;
        }
        return true;
    }
    static bool VisableToWest(string[] grid, int x, int y)
    {
        int treeHeight = grid[y][x];
        for (int i = 1; i <= x; i++)
        {
            if (treeHeight <= grid[y][x - i]) return false;
        }
        return true;
    }
    static int GetScore(string[] grid, int x, int y)
    {
        int northScore = GetNorthScore(grid, x, y);
        int southScore = GetSouthScore(grid, x, y);
        int eastScore = GetEastScore(grid, x, y);
        int westScore = GetWestScore(grid, x, y);

        return northScore * westScore * southScore * eastScore;
    }
    static int GetNorthScore(string[] grid, int x, int y)
    {
        int treeHeight = grid[y][x];
        int score = 0;

        if (y == 0) return 0;

        for (int i = 1; i <= y; i++)
        {
            if (treeHeight > grid[y - i][x])
            {
                score++;
                continue;
            }
            else if (treeHeight >= grid[y - 1][x])
            {
                score++;
                break;
            }
            else break;
        }
        return score;
    }
    static int GetSouthScore(string[] grid, int x, int y)
    {
        int treeHeight = grid[y][x];
        int score = 0;

        if (x == 0) return 0;

        for (int i = 1; (y + i) < grid.Count(); i++)
        {
            if (treeHeight > grid[y + i][x])
            {
                score++;
                continue;
            }
            else if (treeHeight <= grid[y + i][x])
            {
                score++;
                break;
            }
            else break;
            
        }
        return score;
    }
    static int GetEastScore(string[] grid, int x, int y)
    {
        int treeHeight = grid[y][x];
        int score = 0;

        if (y == grid.Length - 1) return score;

        for (int i = 1; (x + i) < grid.Length; i++)
        {
            if (treeHeight > grid[y][x + i])
            {
                score++;
                continue;
            }
            else if (treeHeight <= grid[y][x + i])
            {
                score++;
                break;
            }
            else break;
        }
        return score;
    }
    static int GetWestScore(string[] grid, int x, int y)
    {
        int treeHeight = grid[y][x];
        int score = 0;

        if (x == grid.Count() - 1) return score;

        for (int i = 1; i <= x; i++)
        {
            if (treeHeight > grid[y][x - i])
            {
                score++;
                continue;
            }
            else if (treeHeight <= grid[y][x - i])
            {
                score++;
                break;
            }
            else break;

        }
        return score;
    }
}