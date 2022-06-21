using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace KursovaPP._2
{
    class Galaxy
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string Age { get; private set; }

        public List<Star> Stars = new List<Star>();

        public Galaxy(string name, string type, string age)
        {
            Name = name;
            Type = type;
            Age = age;
        }

        public void AddStar(string name, double mass, double size, double temp, double luminosity,char clas)
        {
            Stars.Add(new Star(name, mass, size, temp, luminosity,clas));
            
        }

        public void PrintGalaxyStats()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"--- Data for {Name} galaxy ---");
            sb.AppendLine($"Type: {Type}");
            sb.AppendLine($"Age: {Age}");
            sb.AppendLine("Stars:");
            foreach(var a in Stars)
            {
                sb.AppendLine(a.Print().ToString());
            }
            if (Stars.Count == 0)
                sb.AppendLine("\t-none");
            sb.AppendLine($"--- End of data for {Name} galaxy ---");
            Console.WriteLine(sb);
        }

    }
}
