using System.Text;

namespace KursovaPP._2.Models
{
    public class Moon : IGalaxible
    {
        public string Name { get; private set; }

        public string Type => "Moon";

        public Moon(string name)
        {
            Name = name;
        }
        public StringBuilder Print()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"\t\t\t@ Name: {Name}");
            return sb;
        }
    }
}
