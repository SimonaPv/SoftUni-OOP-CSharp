using CarRacing.Models.Cars.Contracts;
using CarRacing.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarRacing.Repositories
{
    public class CarRepository : IRepository<ICar>
    {
        private readonly List<ICar> models;

        public CarRepository()
        {
            models = new List<ICar>();
        }

        public IReadOnlyCollection<ICar> Models => this.models;

        public void Add(ICar model)
        {
            if (model == null)
            {
                throw new ArgumentException("Cannot add null in Car Repository");
            }
            models.Add(model);
        }

        public ICar FindBy(string property)
        {
            ICar c = models.FirstOrDefault(x => x.VIN == property);
            if (c == null) return null;
            return c;
        }

        public bool Remove(ICar model)
        {
            ICar c = models.FirstOrDefault(x => x.VIN == model.VIN);
            if (c == null) return false;

            models.Remove(model);
            return true;
        }
    }
}
