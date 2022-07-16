using KursovaPP._2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursovaPP._2
{
    public class Validation
    {
        private IEnumerable<Galaxy> _db;
        public Validation(IEnumerable<Galaxy> DbGalaxy)
        {
            _db = DbGalaxy;
        }
        public void IsExist(IGalaxible obj)
        {
            if (obj == null)
            {
                Console.WriteLine($"The {obj.GetType} '{obj.Name}' doesnt exist!");
                Program.DataProcess();
            }
        }
        public void IsExist(string type, string name)
        {
            var rezult = () => type switch
            {
                "galaxy" => _db.Any(galaxy => galaxy.Name.Equals(name)),

                "star" => _db.Any(galaxy => galaxy.stars
                                  .Any(star => star.Name.Equals(name))),

                "planet" => _db.Any(galaxy => galaxy.stars
                                    .Any(star => star.planets
                                    .Any(planet => planet.Name.Equals(name)))),

                "moon" => _db.Any(galaxy => galaxy.stars
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
    }


}
