using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Repositories
{
    public class WeaponRepository : IRepository<IWeapon>
    {
        private readonly List<IWeapon> weapons;

        public WeaponRepository()
        {
            this.weapons = new List<IWeapon>();
        }

        public IReadOnlyCollection<IWeapon> Models => this.weapons;

        public void AddItem(IWeapon model)
        {
            weapons.Add(model);
        }

        public IWeapon FindByName(string name)
        {
            IWeapon weapon = weapons.FirstOrDefault(x => x.GetType().Name == name);
            if (weapon == null) return null;
            return weapon;
        }

        public bool RemoveItem(string name)
        {
            IWeapon weapon = weapons.FirstOrDefault(x => x.GetType().Name == name);
            if (weapon == null) return false;

            weapons.Remove(weapon);
            return true;
        }
    }
}
