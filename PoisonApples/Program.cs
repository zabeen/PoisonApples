using System;
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

            Console.WriteLine($"No. of poisoned apples: {apples.Count(a => a.Poisoned)}");

            var nextColour = apples
                .Where(a => a.Poisoned)
                .GroupBy(a => a.Colour)
                .OrderByDescending(c => c.Count())
                .SkipWhile(c => c.Key == "Red")
                .First()
                .Key;
            Console.WriteLine($"Next most common colour of poisoned apples after Red: {nextColour}");

            Console.WriteLine($"Max. no. of non-poisoned Red apples picked in succession: {MaxCountOfSuccessiveNonPoisonedRedApples(ref apples)}");

            Console.WriteLine($"Times two green apples picked in a row: {apples.Skip(1).Count(a => a.Colour == "Green" && apples[apples.IndexOf(a)-1].Colour == "Green")}");

            Console.ReadLine();
        }


        static int MaxCountOfSuccessiveNonPoisonedRedApples(ref List<ApplePoisoner.Apple> apples)
        {
            int maxCount = 0;

            for (int i = 0; i < apples.Count;)
            {
                int currentCount = 0;
                if (!apples[i].Poisoned && apples[i].Colour == "Red")
                    currentCount = apples.Skip(i).TakeWhile(a => !a.Poisoned && a.Colour == "Red").Count();

                maxCount = (currentCount > maxCount) ? currentCount : maxCount;

                i = i + currentCount + 1;
            }

            return maxCount;
        }
    }
}
