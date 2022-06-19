using System.Collections.Generic;
using System.Text;

namespace KursovaPP._2
{
    class Planet
    {
            public string Name { get; private set; }
            public string PlanetType { get; private set; }

            private bool supportLife;
            public bool IsSupportLife
            {
               get { return supportLife; }

               set { supportLife = value; }

            }

            public List<Moon> Moons = new List<Moon>();
            public Planet(string name, string planetType, string supportLife)
            {
                this.Name = name;
                this.PlanetType = planetType;
                if(supportLife.Equals("yes"))
                   this.IsSupportLife = true;
                else
                   this.IsSupportLife = false;
            }

            public void AddMoon(string name)
            {
               Moons.Add(new Moon(name));
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
