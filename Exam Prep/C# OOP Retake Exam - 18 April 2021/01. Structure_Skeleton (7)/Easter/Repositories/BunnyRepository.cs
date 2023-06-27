using Easter.Models.Bunnies.Contracts;
using Easter.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Easter.Repositories
{
    public class BunnyRepository : IRepository<IBunny>
    {
        private readonly List<IBunny> bunnyList;

        public BunnyRepository()
        {
            this.bunnyList = new List<IBunny>();
        }

        public IReadOnlyCollection<IBunny> Models => this.bunnyList;

        public void Add(IBunny model)
        {
            bunnyList.Add(model);
        }

        public IBunny FindByName(string name)
        {
            IBunny b = bunnyList.FirstOrDefault(x => x.Name == name);
            if (b == null) return null;
            return b;
        }

        public bool Remove(IBunny model)
        {
            IBunny b = bunnyList.FirstOrDefault(x => x.Name == model.Name);
            if (b == null) return false;
            bunnyList.Remove(model);
            return true;
        }
    }
}
