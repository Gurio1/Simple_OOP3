using System.Text;

namespace KursovaPP._2
{
    class Moon: Service
    {
            public string Name { get; private set; }
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
