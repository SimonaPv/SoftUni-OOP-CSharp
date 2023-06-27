using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using Heroes.Models.Weapons;
using Heroes.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Repositories
{
    public class WeaponRepository : IRepository<IWeapon>
    {
        private readonly List<IWeapon> weapons;

        public WeaponRepository()
        {
            weapons = new List<IWeapon>();
        }

        public IReadOnlyCollection<IWeapon> Models => this.weapons;

        public void Add(IWeapon model)
        {
            weapons.Add(model);
        }

        public IWeapon FindByName(string name)
        {
            IWeapon weapon = weapons.FirstOrDefault(x => x.Name == name);

            if (weapon == null) return null;
            else return weapon;
        }

        public bool Remove(IWeapon model)
        {
            IWeapon weapon = weapons.FirstOrDefault(x => x.Name == model.Name);

            if (weapon == null) return false;
            else
            {
                weapons.Remove(model);
                return true;
            }
            throw new NotImplementedException();
        }
    }
}
