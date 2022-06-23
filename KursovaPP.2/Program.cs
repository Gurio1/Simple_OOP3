using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace KursovaPP._2
{
    class Program
    {
        private static List<Galaxy> galaxyes = new List<Galaxy>();

        private delegate void AddComand(string[] args);
        private static Dictionary<string, AddComand> AddComands = new Dictionary<string, AddComand>();
        static Program()
        {
            #region Add commands to the Dictionary
            AddComands.Add("add galaxy", args =>
            {
                IsExist(args[1].ToLower(), args[2]);
                galaxyes.Add(new Galaxy(args[2], args[3], args[4]));
            });
            AddComands.Add("add star", args =>
            {
                IsExist(args[1].ToLower(), args[3]);
                var culture = CultureInfo.InvariantCulture;
                AddStar(args[2], args[3], Convert.ToDouble(args[4], culture), Convert.ToDouble(args[5], culture), Convert.ToDouble(args[6], culture), Convert.ToDouble(args[7], culture));
            });
            AddComands.Add("add planet", args =>
            {
                IsExist(args[1].ToLower(), args[3]);
                AddPlanet(args[2], args[3], args[4], args[5]);
            });
            AddComands.Add("add moon", args =>
            {
                IsExist(args[1].ToLower(), args[3]);
                AddMoon(args[2], args[3]);
            });
            #endregion
        }

        private static void IsExistRelationalObject(string type, string name, Service obj)
        {
            if (obj == null)
            {
                Console.WriteLine($"The {type} '{name}' doesnt exist!");
                DataProcess();
            }
        }
        private static void AddStar(string nameOfGalaxy, string nameOfStar, double mass, double size, double temp, double luminosity)
        {
            char starClass = DefineStarClass(mass, size / 2, temp, luminosity);
            if (starClass == '0')
            {
                Console.WriteLine("Incorrect data, re-enter");
                DataProcess();
            }

            var rezult = galaxyes.Find(x => x.Name.Equals(nameOfGalaxy));
            IsExistRelationalObject("galaxy", nameOfGalaxy, rezult);
        }
        private static void AddPlanet(string nameOfStar, string name, string planetType, string isSupportLife)
        {
            var rezult = galaxyes.SelectMany(x => x.stars)
                                 .FirstOrDefault(star => star.Name.Equals(nameOfStar));
            IsExistRelationalObject("sun", nameOfStar, rezult);
            rezult.planets.Add(new Planet(name, planetType, isSupportLife));
        }
        private static void AddMoon(string nameOfPlanet, string name)
        {
            var rezult = galaxyes.SelectMany(x => x.stars)
                                 .SelectMany(stars => stars.planets)
                                 .FirstOrDefault(planet => planet.Name.Equals(nameOfPlanet));
            IsExistRelationalObject("planet", nameOfPlanet, rezult);
            rezult.moons.Add(new Moon(name));
        }
        private static void IsExist(string type, string name)
        {
            var rezult = () => type switch
           {
               "galaxy" => galaxyes.Any(galaxy => galaxy.Name.Equals(name)),

               "star" => galaxyes.Any(galaxy => galaxy.stars
                                 .Any(star => star.Name.Equals(name))),

               "planet" => galaxyes.Any(galaxy => galaxy.stars
                                   .Any(star => star.planets
                                   .Any(planet => planet.Name.Equals(name)))),

               "moon" => galaxyes.Any(galaxy => galaxy.stars
                                 .Any(star => star.planets
                                 .Any(planet => planet.moons
                                 .Any(moon => moon.Name.Equals(name)))))
           };
            if (rezult.Invoke())
            {
                Console.WriteLine($"The name '{name}' for {type} is already exist!");
                DataProcess();
            }
        }       
        private static void PrintStats()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("--- Stats ---\n");
            sb.Append($"Galaxies: {galaxyes.Count}\n");
            sb.Append($"Stars: {galaxyes.Sum(x => x.stars.Count)}\n");
            sb.Append($"Planets: {galaxyes.SelectMany(galaxys => galaxys.stars).Sum(x => x.planets.Count)}\n");
            sb.Append($"Moons: {galaxyes.SelectMany(galaxys => galaxys.stars).SelectMany(stars => stars.planets).Sum(planets => planets.moons.Count)}\n");
            sb.AppendLine("--- End of stats ---");
            Console.WriteLine(sb);
        }
        private static void ListOF(string objects)
        {
            switch (objects)
            {
                case "galaxies":
                    PrintList(galaxyes.Select(x => x.Name).ToArray(), objects);
                    break;
                case "stars":
                    PrintList(galaxyes.SelectMany(x => x.stars.Select(a => a.Name)).ToArray(), objects);
                    break;
                case "planets":
                    PrintList(galaxyes.SelectMany(x => x.stars.SelectMany(a => a.planets.Select(c => c.Name))).ToArray(), objects);
                    break;
                case "moons":
                    PrintList(galaxyes.SelectMany(x => x.stars.SelectMany(a => a.planets.SelectMany(c => c.moons.Select(p => p.Name)))).ToArray(), objects);
                    break;
            }
        }
        static private void PrintList(string[] names, string obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"--- List of all researched {obj} ---");
            sb.AppendJoin(",", names);
            sb.AppendLine("");
            sb.AppendLine($"--- End of {obj} list ---");
            Console.WriteLine(sb);
        }
        private static char DefineStarClass(double mass, double size, double temp, double luminosity) =>
            (mass, size, temp, luminosity) switch
            {
                ( >= 0.08 and < 0.45, <= 0.7, >= 2400 and < 3700, <= 0.08) => 'M',
                ( >= 0.45 and < 0.8, > 0.7 and <= 0.96, >= 3700 and < 5200, > 0.08 and <= 0.6) => 'K',
                ( >= 0.8 and < 1.04, > 0.96 and <= 1.15, >= 5200 and < 60000, > 0.6 and <= 1.5) => 'G',
                ( >= 1.04 and < 1.4, > 1.15 and <= 1.4, >= 6000 and < 7500, > 1.5 and <= 5) => 'F',
                ( >= 1.4 and < 2.1, > 1.4 and <= 1.8, >= 7500 and < 10000, > 5 and <= 25) => 'A',
                ( >= 2.1 and < 16, > 1.8 and <= 6.6, >= 10000 and < 30000, > 25 and <= 30000) => 'B',
                ( >= 16, > 6.6, >= 30000, > 30000) => 'O',
                _ => '0'
            };
        public static void DataProcess()
        {

            bool inProgress = true;
            //using (StreamReader sr = new StreamReader("./text.txt"))
            //{
            while (inProgress)
            {
                //        string temp = sr.ReadLine();
                //        if (temp == null)
                //            break;
                //        string[] input = temp.Filter();
                string[] input = Console.ReadLine().Filter();
                switch (input[0].ToLower())
                {
                    case "add":
                        string key = (input[0] + " " + input[1]).ToLower();
                        AddComands[key](input);
                        break;
                    case "stats":
                        PrintStats();
                        break;
                    case "list":
                        ListOF(input[1].ToLower());
                        break;
                    case "print":
                        galaxyes.First(x => x.Name.Equals(input[1])).PrintGalaxyStats();
                        break;
                    case "exit":
                        inProgress = false;
                        break;
                    default:
                        Console.WriteLine("Upps,something wrong");
                        break;
                }
                //}
            }
        }
        static void Main(string[] args)
        {

            DataProcess();

        }
    }
}
