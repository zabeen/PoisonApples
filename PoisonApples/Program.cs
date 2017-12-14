﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoisonApples
{
    class Program
    {
        static void Main(string[] args)
        {
            var poisoner = new ApplePoisoner();
            var apples = poisoner.PickApples().Take(10000).ToList();

            WritePoisonedCount(apples);
            WriteNextMostCommonColour(apples);
            WriteSuccessivePickedMaxCount(apples);
            WriteTwoGreenPickedCount(apples);

            Console.ReadLine();
        }

        private static void WritePoisonedCount(List<ApplePoisoner.Apple> apples)
        {
            Console.WriteLine($"No. of poisoned apples: {apples.Count(a => a.Poisoned)}");
        }

        private static void WriteNextMostCommonColour(List<ApplePoisoner.Apple> apples)
        {
            var colour = apples
                .Where(a => a.Poisoned)
                .GroupBy(a => a.Colour)
                .OrderByDescending(c => c.Count())
                .SkipWhile(c => c.Key == "Red")
                .First()
                .Key;

            Console.WriteLine($"Next most common colour of poisoned apples after Red: {colour}");
        }

        private static void WriteSuccessivePickedMaxCount(List<ApplePoisoner.Apple> apples)
        {
            var count = apples
                .Select((a, i) => apples.Skip(i).TakeWhile(app => !app.Poisoned && app.Colour == "Red").Count())
                .Max();

            Console.WriteLine($"Max. no. of non-poisoned Red apples picked in succession: {count}");
        }

        private static void WriteTwoGreenPickedCount(List<ApplePoisoner.Apple> apples)
        {
            var count = apples
                .Skip(1)
                .Select((a, index) => new {a.Colour, index})
                .Count(a => a.Colour == "Green" && apples[a.index-1].Colour == "Green");

            Console.WriteLine($"Times two green apples picked in a row: {count}");
        }

    }
}
