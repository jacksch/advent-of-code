using System;
using System.Text;
using System.IO;

class Program2
{
    public static List<string> tPositions = new();
    static void Main()
    {
        string[] input = File.ReadAllLines(@"./input.txt");

        List<string[]> motions = ParseMotions(input);
        int[] pos = { 0, 0, 0, 0 };
        int[,] knotPos = new int[10,2];

        foreach (var motion in motions)
        {
            pos = MoveRope(motion, pos[0], pos[1], pos[2], pos[3]);
        }        

        int tPositionsCount = (from x in tPositions
                               select x).Distinct().Count();

        Console.WriteLine("Part One Solution: {0}", tPositionsCount);
    }

    static List<string[]> ParseMotions(string[] input)
    {
        List<string[]> motions = new();

        foreach (var motion in input)
        {
            string[] motionArr = motion.Split(" ");
            motions.Add(motionArr);
        }

        return motions;
    }
    static int[] MoveRope(string[] motion, int headX, int headY, int tailX, int tailY)
    {
        string direction = motion[0];
        int distance = Convert.ToInt32(motion[1]);

        for (int i = 0; i < distance; i++)
        {
            int[] newHeadPos = MoveHead(motion, headX, headY);
            headX = newHeadPos[0];
            headY = newHeadPos[1];

            int[] newTailPos = MoveKnot(motion, headX, headY, tailX, tailY);
            tailX = newTailPos[0];
            tailY = newTailPos[1];

            tPositions.Add($"{tailX},{tailY}");
        }

        return new int[] { headX, headY, tailX, tailY };
    }
    static int[] MoveHead(string[] motion, int headX, int headY)
    {
        string direction = motion[0];

        // Move head in direction
        if (direction == "U") headY++;
        else if (direction == "D") headY--;
        else if (direction == "L") headX--;
        else if (direction == "R") headX++;

        return new int[] { headX, headY };
    }
    static int[] MoveKnot(string[] motion, int leadX, int leadY, int trailX, int trailY)
    {
        string direction = motion[0];

        // Tail is on same direction axis as head
        if (direction == "U" && leadX == trailX && leadY + 1 != trailY && leadY - 1 != trailY && leadY > trailY) trailY++;
        else if (direction == "D" && leadX == trailX && leadY - 1 != trailY && leadY + 1 != trailY && leadY < trailY) trailY--;
        else if (direction == "L" && leadY == trailY && leadX + 1 != trailX && leadX + 1 != trailX && leadX < trailX) trailX--;
        else if (direction == "R" && leadY == trailY && leadX - 1 != trailX && leadX - 1 != trailX && leadX > trailX) trailX++;

        // Tail is no longer on the same direction axes as head 
        else if (direction == "U" && leadX != trailX && leadY != trailY && leadY != trailY + 1)
        {
            trailY++;
            if (leadX > trailX) trailX++;
            if (leadX < trailX) trailX--;
        }
        else if (direction == "D" && leadX != trailX && leadY != trailY && leadY != trailY - 1)
        {
            trailY--;
            if (leadX > trailX) trailX++;
            if (leadX < trailX) trailX--;
        }
        else if (direction == "L" && leadY != trailY && leadX != trailX && leadX != trailX - 1)
        {
            trailX--;
            if (leadY > trailY) trailY++;
            if (leadY < trailY) trailY--;
        }
        else if (direction == "R" && leadY != trailY && leadX != trailX && leadX != trailX + 1)
        {
            trailX++;
            if (leadY > trailY) trailY++;
            if (leadY < trailY) trailY--;
        }

        return new int[] { trailX, trailY };
    }
}