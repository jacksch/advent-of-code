using System;
using System.Text;
using System.IO;
using System.Linq;

class Program
{
    public static int x = 1;
    public static int n = 0;
    public static int xPixel = 0;
    public static int[] checks = { 20, 60, 100, 140, 180, 220 };
    public static int cycleProductSum = 0;
    
    static void Main()
    {
        string[] input = File.ReadAllLines(@"./input.txt");

        Console.WriteLine("Part Two Solution:");

        for (int i = 0; i < input.Count(); i++)
        {
            string[] instruction = input[i].Split(" ");

            ExecuteInstruction(instruction);
        }

        Console.WriteLine(" ");
        Console.WriteLine("Part One Solution: {0}", cycleProductSum);
    }
    static void ExecuteInstruction(string[] instruction)
    {
        if (instruction[0] == "noop")
        {
            n++;

            DrawPixel();
            CycleCheck();
        }
        if (instruction[0] == "addx")
        {
            n++;

            DrawPixel();
            CycleCheck();

            n++;
            
            DrawPixel();
            CycleCheck();

            x += int.Parse(instruction[1]);               
        }
    }
    static void DrawPixel()
    {      
        if (xPixel == 39)
        {
            if (x == xPixel || x + 1 == xPixel || x - 1 == xPixel)
                Console.WriteLine("#");
            else
                Console.WriteLine(" ");

            xPixel = 0;
        }
        else
        {
            if (x == xPixel || x + 1 == xPixel || x - 1 == xPixel)
                Console.Write("#");
            else
                Console.Write(" ");

            xPixel++;
        }
    }
    static bool CycleCheck()
    {
        var check = Array.Exists(checks, x => x == n);

        if (check) cycleProductSum += n * x;

        return check;
    }
}