using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Repositories
{
    public class AstronautRepository : IRepository<IAstronaut>
    {
        private readonly List<IAstronaut> models;

        public AstronautRepository()
        {
            this.models = new List<IAstronaut>();
        }

        public IReadOnlyCollection<IAstronaut> Models => this.models;

        public void Add(IAstronaut model)
        {
            this.models.Add(model);
        }

        public IAstronaut FindByName(string name)
        {
            IAstronaut a = this.models.FirstOrDefault(x => x.Name == name);
            if (a == null) return null;
            return a;
        }

        public bool Remove(IAstronaut model)
        {
            IAstronaut astronaut = this.FindByName(model.Name);
            if (astronaut == null) return false;
            this.models.Remove(astronaut);
            return true;
        }
    }
}
