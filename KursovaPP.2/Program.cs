using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace KursovaPP._2
{
    class Program
    {
        private static List<Galaxy> Galaxyes = new List<Galaxy>();
        private static void AddStar(string nameOfGalaxy, string nameOfStar, double mass, double size, double temp, double luminosity)
        {
            char StarClass = SetStarClass(mass, size / 2, temp, luminosity);
            if (StarClass == '0')
            {
                Console.WriteLine("Incorrect data, re-enter");
                DataProcess();
            }

            Galaxyes.Find(x => x.Name.Equals(nameOfGalaxy))?.AddStar(nameOfStar, mass, size, temp, luminosity, StarClass);
        }
        private static void Add(string[] input)
        {
            switch (input[1].ToLower())
            {

                case "galaxy":
                    Galaxyes.Add(new Galaxy(input[2], input[3], input[4]));
                    break;
                case "star":
                    var culture = CultureInfo.InvariantCulture;
                    AddStar(input[2], input[3], Convert.ToDouble(input[4], culture), Convert.ToDouble(input[5], culture), Convert.ToDouble(input[6], culture), Convert.ToDouble(input[7], culture));
                    break;
                case "planet":
                    Galaxyes.SelectMany(x => x.Stars)
                            .First(star => star.Name.Equals(input[2]))
                            .Planets.Add(new Planet(input[3], input[4], input[5]));
                    break;
                case "moon":
                    Galaxyes.SelectMany(x => x.Stars)
                            .SelectMany(stars => stars.Planets)
                            .First(planet => planet.Name.Equals(input[2]))
                            .Moons.Add(new Moon(input[3]));
                    break;
                default:
                    Console.WriteLine("Uuups,something wrong");
                    break;
            }
        }
        private static void PrintStats()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("--- Stats ---\n");
            sb.Append($"Galaxies: {Galaxyes.Count}\n");
            sb.Append($"Stars: {Galaxyes.Sum(x => x.Stars.Count)}\n");
            sb.Append($"Planets: {Galaxyes.SelectMany(galaxys => galaxys.Stars).Sum(x => x.Planets.Count)}\n");
            sb.Append($"Moons: {Galaxyes.SelectMany(galaxys => galaxys.Stars).SelectMany(stars => stars.Planets).Sum(planets => planets.Moons.Count)}\n");
            sb.AppendLine("--- End of stats ---");
            Console.WriteLine(sb);
        }
        private static void ListOF(string objects)
        {
            switch (objects)
            {
                case "galaxies":
                    PrintList(Galaxyes.Select(x => x.Name).ToArray(), objects);
                    break;
                case "stars":
                    PrintList(Galaxyes.SelectMany(x => x.Stars.Select(a => a.Name)).ToArray(), objects);
                    break;
                case "planets":
                    PrintList(Galaxyes.SelectMany(x => x.Stars.SelectMany(a => a.Planets.Select(c => c.Name))).ToArray(), objects);
                    break;
                case "moons":
                    PrintList(Galaxyes.SelectMany(x => x.Stars.SelectMany(a => a.Planets.SelectMany(c => c.Moons.Select(p => p.Name)))).ToArray(), objects);
                    break;
            }
        }
        static private void PrintList(string[] Names, string obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"--- List of all researched {obj} ---");
            sb.AppendJoin(",", Names);
            sb.AppendLine("");
            sb.AppendLine($"--- End of {obj} list ---");
            Console.WriteLine(sb);
        }
        private static char SetStarClass(double mass, double size, double temp, double luminosity) =>
            (mass, size, temp, luminosity) switch
            {
                (>= 0.08 and  < 0.45 , <= 0.7,  >= 2400 and < 3700,  <= 0.08) => 'M',
                ( >= 0.45 and  < 0.8,  > 0.7 and  <= 0.96,  >= 3700 and  < 5200,  > 0.08 and  <= 0.6) => 'K',
                ( >= 0.8 and  < 1.04,  > 0.96 and  <= 1.15,  >= 5200 and  < 60000,  > 0.6 and  <= 1.5) => 'G',
                ( >= 1.04 and  < 1.4,  > 1.15 and  <= 1.4,  >= 6000 and  < 7500,  > 1.5 and  <= 5) => 'F',
                ( >= 1.4 and  < 2.1,  > 1.4 and  <= 1.8,  >= 7500 and  < 10000,  > 5 and  <= 25) => 'A',
                ( >= 2.1 and  < 16, > 1.8 and  <= 6.6,  >= 10000 and  < 30000,  > 25 and  <= 30000) => 'B',
                (>= 16 , > 6.6 , >= 30000, > 30000) => 'O',
                _ => '0'
            };
        public static void DataProcess()
        {

            bool noExit = true;
            using (StreamReader sr = new StreamReader("./text.txt"))
            {
                while (noExit)
                {
                    string temp = sr.ReadLine();
                    if (temp == null)
                        break;
                    string[] input = temp.Filter();
                    // string[] input = Console.ReadLine().Filter();
                    switch (input[0].ToLower())
                    {
                        case "add":
                            Add(input);
                            break;
                        case "stats":
                            PrintStats();
                            break;
                        case "list":
                            ListOF(input[1].ToLower());
                            break;
                        case "print":
                            Galaxyes.First(x => x.Name.Equals(input[1])).PrintGalaxyStats();
                            break;
                        case "exit":
                            noExit = false;
                            break;
                        default:
                            Console.WriteLine("Upps,something wrong");
                            break;
                    }
                }
            }
        }
        static void Main(string[] args)
        {

            DataProcess();

        }
    }
}
