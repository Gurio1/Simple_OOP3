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
        public static Validation validator = new Validation();
        private static GalaxyRepository db = new GalaxyRepository(validator);
        private delegate void AddComand(string[] args);
        private static Dictionary<string, AddComand> AddComands = new Dictionary<string, AddComand>();
        static Program()
        {
            #region Add commands to the Dictionary
            AddComands.Add("add galaxy", args =>
            {
                db.IsExist(args[1].ToLower(), args[2]);
                db.AddGalaxy(args[2], args[3], args[4]);
            });
            AddComands.Add("add star", args =>
            {
                db.IsExist(args[1].ToLower(), args[3]);
                var culture = CultureInfo.InvariantCulture;
                double[] parsedProps = validator.ValidateAndParseToDouble(args[4], args[5], args[6], args[7]);
                db.AddStar(args[2], args[3], parsedProps[0], parsedProps[1], parsedProps[2], parsedProps[3]);
            });
            AddComands.Add("add planet", args =>
            {
                db.IsExist(args[1].ToLower(), args[3]);
                db.AddPlanet(args[2], args[3], args[4], args[5]);
            });
            AddComands.Add("add moon", args =>
            {
                db.IsExist(args[1].ToLower(), args[3]);
                db.AddMoon(args[2], args[3]);
            });
            #endregion
        }
        public static void DataProcess()
        {
            bool inProgress = true;
            //using (StreamReader sr = new StreamReader("./text.txt"))
            //{
            while (inProgress)
            {
                //string temp = sr.ReadLine();
                //if (temp == null)
                //    break;
                //string[] input = temp.Filter();
                string[] input = Console.ReadLine().Filter();
                switch (input[0].ToLower())
                {
                    case "add":
                        string key = (input[0] + " " + input[1]).ToLower();
                        AddComands[key](input);
                        break;
                    case "stats":
                        db.PrintStats();
                        break;
                    case "list":
                        db.ListOF(input[1].ToLower());
                        break;
                    case "print":
                        db.FindGalaxy(x => x.Name.Equals(input[1])).PrintGalaxyStats();
                        break;
                    case "exit":
                        inProgress = false;
                        break;
                    default:
                        Console.WriteLine("Upps,something wrong");
                        break;
                }
                // }
            }
        }
        static void Main(string[] args)
        {
            DataProcess();
        }
    }
}
