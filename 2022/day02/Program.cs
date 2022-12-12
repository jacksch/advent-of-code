using System;
using System.IO;

class Program
{
    static void Main()
    {
        string[] input = File.ReadAllLines(@".\input.txt");

        int partOneScore = 0;
        int partTwoScore = 0;
        for (int i = 0; i < input.Count(); i++)
        {
            string[] hands = input[i].Split(" ");

            partOneScore += GameScoreResult(hands[0],hands[1]);
            partTwoScore += SpecifyResult(hands[0],hands[1]);
        }

        Console.WriteLine("Part One Solution: {0}", partOneScore);
        Console.WriteLine("Part Two Solution: {0}", partTwoScore);
    }

    static int GameScoreResult(string p1Hand, string p2Hand)
    {
        // Score matrix - Opponent X axis, You Y axis.
        int[,] matrix = new int[3,3] { { 4, 8, 3 }, { 1, 5, 9 }, { 7, 2, 6 } };
        int player1 = 0;
        int player2 = 0;

        switch (p1Hand)
        {
            case "A": player1 = 0; break;
            case "B": player1 = 1; break;
            case "C": player1 = 2; break;
        }

        switch (p2Hand)
        {
            case "X": player2 = 0; break;
            case "Y": player2 = 1; break;
            case "Z": player2 = 2; break;
        }

        return matrix[player1,player2];
    }

    static int SpecifyResult(string p1Hand, string p2Hand)
    {
        string newP2Hand = "";
        if (p1Hand == "A" && p2Hand == "X") newP2Hand = "Z";
        if (p1Hand == "A" && p2Hand == "Y") newP2Hand = "X";
        if (p1Hand == "A" && p2Hand == "Z") newP2Hand = "Y";

        if (p1Hand == "B" && p2Hand == "X") newP2Hand = "X";
        if (p1Hand == "B" && p2Hand == "Y") newP2Hand = "Y";
        if (p1Hand == "B" && p2Hand == "Z") newP2Hand = "Z";

        if (p1Hand == "C" && p2Hand == "X") newP2Hand = "Y";
        if (p1Hand == "C" && p2Hand == "Y") newP2Hand = "Z";
        if (p1Hand == "C" && p2Hand == "Z") newP2Hand = "X";

        return GameScoreResult(p1Hand, newP2Hand);
    }
}



