using Easter.Models.Bunnies.Contracts;
using Easter.Models.Eggs.Contracts;
using Easter.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easter.Repositories
{
    public class EggRepository : IRepository<IEgg>
    {
        private readonly List<IEgg> eggList;

        public EggRepository()
        {
            this.eggList = new List<IEgg>();
        }

        public IReadOnlyCollection<IEgg> Models => this.eggList;

        public void Add(IEgg model)
        {
            eggList.Add(model);
        }

        public IEgg FindByName(string name)
        {
            IEgg e = eggList.FirstOrDefault(x => x.Name == name);
            if (e == null) return null;
            return e;
        }

        public bool Remove(IEgg model)
        {
            IEgg e = eggList.FirstOrDefault(x => x.Name == model.Name);
            if (e == null) return false;
            eggList.Remove(model);
            return true;
        }
    }
}
