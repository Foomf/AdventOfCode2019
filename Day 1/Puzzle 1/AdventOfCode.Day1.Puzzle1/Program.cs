using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Day1.Puzzle1
{
    class Program
    {
        private const string InputName = "input.txt";

        static async Task Main(string[] args)
        {
            var numbers = await ReadInputAsync();
            var totalFuel = numbers
                .Select(GetRequiredFuel)
                .Aggregate(0, (total, reqFuel) => total + reqFuel);
            Console.WriteLine($"The total fuel needed is {totalFuel}");
        }

        public static int GetRequiredFuel(int mass)
        {
            var val = mass / 3.0D;
            val = Math.Floor(val);
            val -= 2;
            return (int) val;
        }

        public static async Task<List<int>> ReadInputAsync()
        {
            var lines = await File.ReadAllLinesAsync(InputName);
            return lines.Select(int.Parse).ToList();
        }
    }
}
