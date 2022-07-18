using KursovaPP._2.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KursovaPP._2
{
    public class GalaxyRepository
    {
        private static List<Galaxy> galaxyes = new List<Galaxy>();
        private static Validation _validator;
        private static Stats _printer = new Stats();
        public GalaxyRepository( Validation validation)
        {
            _validator = validation;
        }
        public void AddGalaxy(string name,string type,string age)
        {
            galaxyes.Add(new Galaxy(name, type, age));
        }
        public void AddStar(string nameOfGalaxy, string nameOfStar, double mass, double size, double temp, double luminosity)
        {
            char starClass = Star.DefineStarClass(mass, size / 2, temp, luminosity);
            _validator.ValidateStarClass(starClass);

            var rezult = galaxyes.FirstOrDefault(x => x.Name.Equals(nameOfGalaxy));
            _validator.IsExist(rezult);
            rezult.stars.Add(new Star(nameOfStar, mass, size, temp, luminosity, starClass));
        }
        public void AddPlanet(string nameOfStar, string name, string planetType, string isSupportLife)
        {
            var rezult = galaxyes.SelectMany(x => x.stars)
                                 .FirstOrDefault(star => star.Name.Equals(nameOfStar));
            _validator.IsExist(rezult);
            rezult.planets.Add(new Planet(name, planetType, isSupportLife));
        }
        public void AddMoon(string nameOfPlanet, string name)
        {
            var rezult = galaxyes.SelectMany(x => x.stars)
                                 .SelectMany(stars => stars.planets)
                                 .FirstOrDefault(planet => planet.Name.Equals(nameOfPlanet));
            _validator.IsExist(rezult);
            rezult.moons.Add(new Moon(name));
        }
        public void IsExist(string type, string name)
        {
            _validator.IsExist(type, name, galaxyes);
        }
        public void PrintStats() => _printer.PrintStats(galaxyes);
        public void ListOF(string objects) => _printer.ListOF(objects, galaxyes);
        public Galaxy FindGalaxy(Func<Galaxy, bool> condition)
        {
            var galaxy = galaxyes.FirstOrDefault(condition);
            if(galaxy is null)
            {
                Console.WriteLine("This galaxy does not exist!");
                Program.DataProcess();
            }
            return galaxy;
        }
    }
}
