using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formula1.Repositories
{
    public class FormulaOneCarRepository : IRepository<IFormulaOneCar>
    {
        private readonly List<IFormulaOneCar> cars;

        public FormulaOneCarRepository()
        {
            cars = new List<IFormulaOneCar>();
        }

        public IReadOnlyCollection<IFormulaOneCar> Models => this.cars;

        public void Add(IFormulaOneCar model)
        {
            cars.Add(model);
        }

        public IFormulaOneCar FindByName(string name)
        {
            IFormulaOneCar car = cars.FirstOrDefault(x => x.Model == name);
            if (car == null) return null;
            return car;
        }

        public bool Remove(IFormulaOneCar model)
        {
            int count = cars.Count;
            cars.Remove(model);
            if (count == cars.Count) return false;
            return true;
        }
    }
}
