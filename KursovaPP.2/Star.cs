using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace KursovaPP._2
{
    
    class Star: Service
    {
        public string Name { get; private set; }
        public double Mass { get; private set; }
        public double Size { get; private set; }
        public double Temp { get; private set; }
        public double Luminosity { get; private set; }
        public char starClass { get; private set; }

        public List<Planet> planets = new List<Planet>();
        public Star(string name, double mass, double size, double temp, double luminosity,char starClass)
        {
            Name = name;
            Mass = mass;
            Size = size/2;
            Temp = temp;
            Luminosity = luminosity;
            this.starClass = starClass;

        }
        public  StringBuilder Print()
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
