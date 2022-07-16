using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace KursovaPP._2.Models
{

    public class Star : IGalaxible
    {
        public string Name { get; private set; }
        public double Mass { get; private set; }
        public double Size { get; private set; }
        public double Temp { get; private set; }
        public double Luminosity { get; private set; }
        public char starClass { get; private set; }
        public string Type => "Star";

        public List<Planet> planets = new List<Planet>();
        public Star(string name, double mass, double size, double temp, double luminosity, char starClass)
        {
            Name = name;
            Mass = mass;
            Size = size / 2;
            Temp = temp;
            Luminosity = luminosity;
            this.starClass = starClass;

        }

        public static char DefineStarClass(double mass, double size, double temp, double luminosity) =>
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
        public StringBuilder Print()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\t-Name: {Name}");
            sb.AppendLine($"\tClass: {starClass}({Mass},{Size},{Temp},{Luminosity})");
            sb.AppendLine($"\tPlanets:");
            foreach (var a in planets)
            {
                sb.AppendLine(a.Print().ToString());
            }
            if (planets.Count == 0)
                sb.AppendLine("\t\to none");
            return sb;
        }


    }
}
