using System;
using System.Collections.Generic;
using System.Text;

namespace KursovaPP._2.Models
{
    public class Galaxy : IGalaxible
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string Age { get; private set; }

        public string TypeOfObject => "Galaxy";

        public List<Star> stars = new List<Star>();
        public Galaxy(string name, string type, string age)
        {
            Name = name;
            Type = type;
            Age = age;
        }
        public void PrintGalaxyStats()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"--- Data for {Name} galaxy ---");
            sb.AppendLine($"Type: {Type}");
            sb.AppendLine($"Age: {Age}");
            sb.AppendLine("Stars:");
            foreach (var a in stars)
            {
                sb.AppendLine(a.Print().ToString());
            }
            if (stars.Count == 0)
                sb.AppendLine("\t-none");
            sb.AppendLine($"--- End of data for {Name} galaxy ---");
            Console.WriteLine(sb);
        }

    }
}
