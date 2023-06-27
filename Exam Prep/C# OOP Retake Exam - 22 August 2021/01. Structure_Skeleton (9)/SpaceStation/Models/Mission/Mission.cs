using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Planets.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Models.Mission
{
    public class Mission : IMission
    {
        public void Explore(IPlanet planet, ICollection<IAstronaut> astronauts)
        {
            foreach (var ast in astronauts)
            {
                while (planet.Items.Count > 0 && ast.CanBreath)
                {
                    string item = planet.Items.First();
                    ast.Breath();
                    ast.Bag.Items.Add(item);
                    planet.Items.Remove(item);
                }
            }
        }
    }
}
