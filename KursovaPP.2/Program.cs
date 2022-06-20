using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.IO;

namespace KursovaPP._2
{
   static class Program
        {
        private static List<Galaxy> Galaxyes = new List<Galaxy>();
        private static void AddStar(string nameOfGalaxy,string nameOfStar,double mass,double size,double temp, double luminosity)
        {
            char StarClass = filters.Select(filter => filter(mass, size / 2, temp, luminosity)).First(x => x.Item1).Item2;
            if (StarClass == ('0'))
            {
                Console.WriteLine("Incorrect data, re-enter");
                DataProcess();
            }
            else
            {
                Galaxyes.Find(x => x.Name.Equals(nameOfGalaxy)).AddStar(nameOfStar, mass, size, temp, luminosity, StarClass);
            }
        }
        private static void Add(string [] input)
        {
            switch (input[1].ToLower())
            {

                case "galaxy":
                    Galaxyes.Add(new Galaxy(input[2], input[3], input[4]));
                    break;
                case "star":
                    var culture = CultureInfo.InvariantCulture;
                    AddStar(input[2], input[3],Convert.ToDouble(input[4], culture), Convert.ToDouble(input[5], culture), Convert.ToDouble(input[6], culture), Convert.ToDouble(input[7], culture));
                    break;
                case "planet":
                    Galaxyes.SelectMany(x => x.Stars).First(star => star.Name.Equals(input[2])).AddPlanet(input[3], input[4], input[5]);
                    break;
                case "moon":
                    Galaxyes.SelectMany(x => x.Stars).SelectMany(stars => stars.Planets).First(planet => planet.Name.Equals(input[2])).AddMoon(input[3]);
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

        #region Construction like if,to set class for Star
        delegate Tuple<bool, char> Filter(double mass, double size, double temp, double luminosity);

        static List<Filter> filters = new List<Filter>()
        {
            (mass,size,temp,luminosity)=>new Tuple<bool,char>((mass>=0.08 && mass<0.45)&&(size <= 0.7)&&(temp>=2400 && temp<3700) &&(luminosity<= 0.08),'M'),
            (mass,size,temp,luminosity)=>new Tuple<bool,char>((mass>=0.45 && mass<0.8)&&(size > 0.7 && size<=0.96)&&(temp>=3700 && temp<5200) &&(luminosity> 0.08 && luminosity<=0.6),'K'),
            (mass,size,temp,luminosity)=>new Tuple<bool,char>((mass>=0.8 && mass<1.04)&&(size > 0.96 && size<=1.15)&&(temp>=5200 && temp<6000) &&(luminosity> 0.6 && luminosity<=1.5),'G'),
            (mass,size,temp,luminosity)=>new Tuple<bool,char>((mass>=1.04 && mass<1.4)&&(size > 1.15 && size<=1.4)&&(temp>=6000 && temp<7500) &&(luminosity> 1.5 && luminosity<=5),'F'),
            (mass,size,temp,luminosity)=>new Tuple<bool,char>((mass>=1.4 && mass<2.1)&&(size > 1.4 && size<=1.8)&&(temp>=7500 && temp<10000) &&(luminosity> 5 && luminosity<=25),'A'),
            (mass,size,temp,luminosity)=>new Tuple<bool,char>((mass>=2.1 && mass<16)&&(size > 1.8 && size<=6.6)&&(temp>=10000 && temp<30000) &&(luminosity> 25 && luminosity<=30000),'B'),
            (mass,size,temp,luminosity)=>new Tuple<bool,char>(mass>=16 &&size > 6.6&&temp>=30000 &&luminosity> 30000 ,'O'),
            (mass,size,temp,luminosity)=>new Tuple<bool,char>(true ,'0')

        };
        #endregion
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
