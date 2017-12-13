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
            List<List<ApplePoisoner.Apple>> subLists = new List<List<ApplePoisoner.Apple>>();

            for (int i = 0; i < apples.Count;)
            {
                List<ApplePoisoner.Apple> subList = new List<ApplePoisoner.Apple>();
                if (!apples[i].Poisoned && apples[i].Colour == "Red")
                {
                    subList = apples.Skip(i).TakeWhile(a => !a.Poisoned && a.Colour == "Red").ToList();
                    subLists.Add(subList);
                }
                
                i = i + subList.Count + 1;
            }

            return subLists.Max(s => s.Count);
        }
    }
}
