using KursovaPP._2.Models;
using System.Collections.Generic;
using System.Linq;

namespace KursovaPP._2
{
    public class GalaxyContext
    {
       static IEnumerable<Galaxy> _db;
       static Validation validator;
        public GalaxyContext(IEnumerable<Galaxy> db, Validation validation)
        {
            _db = db;
            validator = validation;
        }

        public void AddStar(string nameOfGalaxy, string nameOfStar, double mass, double size, double temp, double luminosity)
        {
            char starClass = Star.DefineStarClass(mass, size / 2, temp, luminosity);
            validator.ValidateStarClass(starClass);

            var rezult = _db.FirstOrDefault(x => x.Name.Equals(nameOfGalaxy));
            validator.IsExist(rezult);
            rezult.stars.Add(new Star(nameOfStar, mass, size, temp, luminosity, starClass));
        }
        public void AddPlanet(string nameOfStar, string name, string planetType, string isSupportLife)
        {
            var rezult = _db.SelectMany(x => x.stars)
                                 .FirstOrDefault(star => star.Name.Equals(nameOfStar));
            validator.IsExist(rezult);
            rezult.planets.Add(new Planet(name, planetType, isSupportLife));
        }
        public void AddMoon(string nameOfPlanet, string name)
        {
            var rezult = _db.SelectMany(x => x.stars)
                                 .SelectMany(stars => stars.planets)
                                 .FirstOrDefault(planet => planet.Name.Equals(nameOfPlanet));
            validator.IsExist(rezult);
            rezult.moons.Add(new Moon(name));
        }
    }
}
