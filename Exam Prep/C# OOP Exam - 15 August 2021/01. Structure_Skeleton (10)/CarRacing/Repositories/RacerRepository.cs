using CarRacing.Models.Cars.Contracts;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarRacing.Repositories
{
    public class RacerRepository : IRepository<IRacer>
    {
        private readonly List<IRacer> models;

        public RacerRepository()
        {
            models = new List<IRacer>();
        }

        public IReadOnlyCollection<IRacer> Models => this.models;

        public void Add(IRacer model)
        {
            if (model == null)
            {
                throw new ArgumentException("Cannot add null in Racer Repository");
            }
            models.Add(model);
        }

        public IRacer FindBy(string property)
        {
            IRacer c = models.FirstOrDefault(x => x.Username == property);
            if (c == null) return null;
            return c;
        }

        public bool Remove(IRacer model)
        {
            IRacer c = models.FirstOrDefault(x => x.Username == model.Username);
            if (c == null) return false;

            models.Remove(model);
            return true;
        }
    }
}
