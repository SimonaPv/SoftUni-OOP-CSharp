using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Repositories
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        private readonly List<IPlanet> models;

        public PlanetRepository()
        {
            this.models = new List<IPlanet>();
        }

        public IReadOnlyCollection<IPlanet> Models => this.models;

        public void Add(IPlanet model)
        {
            this.models.Add(model);
        }

        public IPlanet FindByName(string name)
        {
            IPlanet p = this.models.FirstOrDefault(x => x.Name == name);
            return p;
        }

        public bool Remove(IPlanet model)
        {
            IPlanet planet = this.FindByName(model.Name);
            if (planet == null) return false;
            this.models.Remove(planet);
            return true;
        }
    }
}
