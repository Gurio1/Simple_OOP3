using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace KursovaPP._2
{
    
    class Star
    {
        public string Name { get; private set; }
        public double Mass { get; private set; }
        public double Size { get; private set; }
        public double Temp { get; private set; }
        public double Luminosity { get; private set; }

        public char Class { get; private set; }

        public List<Planet> Planets = new List<Planet>();
        public Star(string name, double mass, double size, double temp, double luminosity,char clas)
        {
            this.Name = name;
            this.Mass = mass;
            this.Size = size/2;
            this.Temp = temp;
            this.Luminosity = luminosity;
            this.Class = clas;

        }

        public void AddPlanet(string name, string planetType, string supportLife)
        {
            Planets.Add(new Planet(name, planetType, supportLife));
        }

        public  StringBuilder Print()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\t-Name: {Name}");
            sb.AppendLine($"\tClass: {Class}({Mass},{Size},{Temp},{Luminosity})");
            sb.AppendLine($"\tPlanets:");
            foreach (var a in Planets)
            {
                sb.AppendLine(a.Print().ToString());
            }
            if (Planets.Count == 0)
                sb.AppendLine("\t\to none");
            return sb;
        }

        
    }
}
