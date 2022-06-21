using System.Collections.Generic;
using System.Text;

namespace KursovaPP._2
{
    class Planet
    {
            public string Name { get; private set; }
            public string PlanetType { get; private set; }

            private bool _supportLife;
            public bool IsSupportLife
            {
               get { return _supportLife; }

               set { _supportLife = value; }

            }

            public List<Moon> Moons = new List<Moon>();
            public Planet(string name, string planetType, string supportLife)
            {
                Name = name;
                PlanetType = planetType;
                if(supportLife.Equals("yes"))
                   IsSupportLife = true;
                else
                   IsSupportLife = false;
            }
        public StringBuilder Print()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\t\to Name: {Name}");
            sb.AppendLine($"\t\tType: {PlanetType}");
            sb.AppendLine($"\t\tSupport life: {IsSupportLife}");
            sb.AppendLine($"\t\tMoons:");
            foreach (var a in Moons)
            {
                sb.AppendLine(a.Print().ToString());
            }
            if (Moons.Count == 0)
                sb.AppendLine("\t\t\t@ none");
            return sb;
        }



    }
}
