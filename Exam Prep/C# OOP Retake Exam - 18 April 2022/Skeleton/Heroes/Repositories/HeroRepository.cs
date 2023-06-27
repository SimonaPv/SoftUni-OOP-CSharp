using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using Heroes.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Heroes.Repositories
{
    public class HeroRepository : IRepository<IHero>
    {
        private readonly List<IHero> heroes;

        public HeroRepository()
        {
            this.heroes = new List<IHero>();
        }

        public IReadOnlyCollection<IHero> Models => this.heroes;

        public void Add(IHero model)
        {
            heroes.Add(model);
        }

        public IHero FindByName(string name)
        {
            IHero hero = heroes.FirstOrDefault(x => x.Name == name);

            if (hero == null) return null;
            else return hero;
        }

        public bool Remove(IHero model)
        {
            IHero hero = heroes.FirstOrDefault(x => x.Name == model.Name);

            if (hero == null) return false;
            else
            {
                heroes.Remove(model);
                return true;
            }
        }
    }
}
