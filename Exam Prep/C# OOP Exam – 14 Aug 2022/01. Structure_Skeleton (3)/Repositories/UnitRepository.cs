using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Repositories
{
    public class UnitRepository : IRepository<IMilitaryUnit>
    {
        private readonly List<IMilitaryUnit> militaryUnits;

        public UnitRepository()
        {
            this.militaryUnits = new List<IMilitaryUnit>();
        }

        public IReadOnlyCollection<IMilitaryUnit> Models => this.militaryUnits;

        public void AddItem(IMilitaryUnit model)
        {
            militaryUnits.Add(model);
        }

        public IMilitaryUnit FindByName(string name)
        {
            IMilitaryUnit unit = militaryUnits.FirstOrDefault(x => x.GetType().Name == name);
            if (unit == null) return null;
            return unit;

        }

        public bool RemoveItem(string name)
        {
            IMilitaryUnit unit = militaryUnits.FirstOrDefault(x => x.GetType().Name == name);
            if (unit == null) return false;

            militaryUnits.Remove(unit);
            return true;
        }
    }
}
