using KursovaPP._2.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursovaPP._2
{
    public class Validation
    {
        public void IsExist(IGalaxible obj)
        {
            if (obj == null)
            {
                Console.WriteLine($"The {obj.GetType} '{obj.Name}' doesnt exist!");
                Program.DataProcess();
            }
        }
        public void IsExist(string type, string name,List<Galaxy>db)
        {
            var rezult = () => type switch
            {
                "galaxy" => db.Any(galaxy => galaxy.Name.Equals(name)),

                "star" => db.Any(galaxy => galaxy.stars
                                  .Any(star => star.Name.Equals(name))),

                "planet" => db.Any(galaxy => galaxy.stars
                                    .Any(star => star.planets
                                    .Any(planet => planet.Name.Equals(name)))),

                "moon" => db.Any(galaxy => galaxy.stars
                                  .Any(star => star.planets
                                  .Any(planet => planet.moons
                                  .Any(moon => moon.Name.Equals(name)))))
            };
            if (rezult.Invoke())
            {
                Console.WriteLine($"The name '{name}' for {type} is already exist!");
                Program.DataProcess();
            }
        }
        public void ValidateStarClass(char starClass)
        {
            if (starClass == '0')
            {
                Console.WriteLine("Incorrect data, re-enter");


                Program.DataProcess();
            }
        }
        public double [] ValidateAndParseToDouble(params string [] strings)
        {
            var culture = CultureInfo.InvariantCulture;
            var style = NumberStyles.Currency;
            double [] methodRezult = new double [strings.Length];
            double tryParseRezult = 0;
            for (int i = 0; i < methodRezult.Length; i++)
            {
                bool isParsed = double.TryParse(strings[i],style,culture, out tryParseRezult);
                if (!isParsed)
                {
                    Console.WriteLine("Incorrect data for Star stats!");
                    Program.DataProcess();
                }
                methodRezult[i] = tryParseRezult;
            }
            return methodRezult;
        }
    }


}
