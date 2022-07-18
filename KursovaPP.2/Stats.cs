using KursovaPP._2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursovaPP._2
{
    public class Stats
    {
        public void PrintStats(List<Galaxy> db)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("--- Stats ---\n");
            sb.Append($"Galaxies: {db.Count()}\n");
            sb.Append($"Stars: {db.Sum(x => x.stars.Count)}\n");
            sb.Append($"Planets: {db.SelectMany(galaxys => galaxys.stars).Sum(x => x.planets.Count)}\n");
            sb.Append($"Moons: {db.SelectMany(galaxys => galaxys.stars).SelectMany(stars => stars.planets).Sum(planets => planets.moons.Count)}\n");
            sb.AppendLine("--- End of stats ---");
            Console.WriteLine(sb);
        }
        public void ListOF(string objects, List<Galaxy> db)
        {
            switch (objects)
            {
                case "galaxies":
                    PrintList(db.Select(x => x.Name).ToArray(), objects);
                    break;
                case "stars":
                    PrintList(db.SelectMany(x => x.stars.Select(a => a.Name)).ToArray(), objects);
                    break;
                case "planets":
                    PrintList(db.SelectMany(x => x.stars.SelectMany(a => a.planets.Select(c => c.Name))).ToArray(), objects);
                    break;
                case "moons":
                    PrintList(db.SelectMany(x => x.stars.SelectMany(a => a.planets.SelectMany(c => c.moons.Select(p => p.Name)))).ToArray(), objects);
                    break;
            }
        }
        private void PrintList(string[] names, string obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"--- List of all researched {obj} ---");
            sb.AppendJoin(",", names);
            sb.AppendLine("");
            sb.AppendLine($"--- End of {obj} list ---");
            Console.WriteLine(sb);
        }

    }
}
