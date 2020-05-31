using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace PopulationCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<Villager>();

            for (int i = 0; i < 5; i++)
            {
                list.Add(new Villager { Age = 14, IsFemale = true });
                list.Add(new Villager { Age = 14, IsFemale = false });
            }
            float probability = 0.1f;
            var rand = new Random();

            bool Chance(float chance)
                => rand.Next(0, 101) / 100f <= chance;
            using var fs = File.OpenWrite(Path.Combine(".", "test.csv"));
            using var writer = new StreamWriter(fs);
            writer.WriteLine($"Tick,Jahr,Frauen,Männer,Tote,Geburten");
            var newList = new List<Villager>();
            var deadList = new List<Villager>();
            for (int i = 0; i < 1001; i++)
            {
                var tmpList = list.ToList();
                newList.Clear();
                deadList.Clear();


                foreach (var villager in tmpList)
                {
                    if (villager.IsFemale && villager.CanBeMadePregnant)
                    {
                        if (!villager.IsPregnant)
                        {
                            var chance = rand.Next(0, 101) / 100f;
                            villager.IsPregnant = Chance(probability);
                        }
                        else
                        {
                            villager.PregnantTime++;

                            if (villager.PregnantTime >= 3)
                            {
                                villager.IsPregnant = false;
                                villager.PregnantTime = 0;
                                if (Chance(0.35f))
                                {
                                    var child = new Villager { Age = 0, IsFemale = Chance(0.5f) };
                                    newList.Add(child);
                                }
                            }
                        }
                    }

                    villager.GetOlder();

                    if (villager.IsDead)
                    {
                        deadList.Add(villager);
                        list.Remove(villager);
                    }
                }

                list.AddRange(newList);
                list.RemoveAll(deadList.Contains);
                writer.WriteLine($"{i},{i / 4},{list.Where(x => x.IsFemale).Count()},{list.Where(x => !x.IsFemale).Count()},{deadList.Count()},{newList.Count}");

                if (i % 100 == 0)
                {


                    Console.WriteLine($"Ticks: {i} => {i / 4} Jahre");
                    Console.WriteLine("Frauen: " + list.Where(x => x.IsFemale).Count());
                    Console.WriteLine("Männer: " + list.Where(x => !x.IsFemale).Count());
                }
            }
            Console.WriteLine($"Finish: The village have now {list.Count} villagers");
            Console.ReadKey();
        }


        private class Villager
        {
            public int Age { get; set; }
            public bool IsFemale { get; set; }
            public int PregnantTime { get; set; }
            public bool IsPregnant { get; set; }
            public bool IsDead => Age > 40;
            public bool CanBeMadePregnant => Age > 15;

            private int tick;
            public void GetOlder()
            {
                tick++;

                if (tick < 4)
                    return;

                Age++;
                tick = 0;

            }
        }
    }
}
