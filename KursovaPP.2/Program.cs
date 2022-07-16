using KursovaPP._2.Models;
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
        private static List<Galaxy> galaxyes = new List<Galaxy>();
        public static Validation validator = new Validation(galaxyes);
        private static GalaxyContext db = new GalaxyContext(galaxyes,validator);
        public static Stats print = new Stats(galaxyes);
        private delegate void AddComand(string[] args);
        private static Dictionary<string, AddComand> AddComands = new Dictionary<string, AddComand>();

        static Program()
        {
            #region Add commands to the Dictionary
            AddComands.Add("add galaxy", args =>
            {
                validator.IsExist(args[1].ToLower(), args[2]);
                galaxyes.Add(new Galaxy(args[2], args[3], args[4]));
            });
            AddComands.Add("add star", args =>
            {
                validator.IsExist(args[1].ToLower(), args[3]);
                var culture = CultureInfo.InvariantCulture;
                db.AddStar(args[2], args[3], Convert.ToDouble(args[4], culture), Convert.ToDouble(args[5], culture), Convert.ToDouble(args[6], culture), Convert.ToDouble(args[7], culture));
            });
            AddComands.Add("add planet", args =>
            {
                validator.IsExist(args[1].ToLower(), args[3]);
                db.AddPlanet(args[2], args[3], args[4], args[5]);
            });
            AddComands.Add("add moon", args =>
            {
                validator.IsExist(args[1].ToLower(), args[3]);
                db.AddMoon(args[2], args[3]);
            });
            #endregion
        }
        public static void DataProcess()
        {

            bool inProgress = true;
            using (StreamReader sr = new StreamReader("./text.txt"))
            {
                while (inProgress)
            {
                    string temp = sr.ReadLine();
                    if (temp == null)
                        break;
                    string[] input = temp.Filter();
                    //string[] input = Console.ReadLine().Filter();
                switch (input[0].ToLower())
                {
                    case "add":
                        string key = (input[0] + " " + input[1]).ToLower();
                        AddComands[key](input);
                        break;
                    case "stats":
                        print.PrintStats();
                        break;
                    case "list":
                        print.ListOF(input[1].ToLower());
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
                }
            }
        }
        static void Main(string[] args)
        {

            DataProcess();

        }
    }
}
