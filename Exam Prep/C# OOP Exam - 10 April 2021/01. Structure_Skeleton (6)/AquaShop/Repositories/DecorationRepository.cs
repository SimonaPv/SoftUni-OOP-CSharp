using AquaShop.Models.Decorations.Contracts;
using AquaShop.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AquaShop.Repositories
{
    public class DecorationRepository : IRepository<IDecoration>
    {
        private readonly List<IDecoration> decorations;

        public DecorationRepository()
        {
            this.decorations = new List<IDecoration>();
        }

        public IReadOnlyCollection<IDecoration> Models => this.decorations;

        public void Add(IDecoration model)
        {
            decorations.Add(model);
        }

        public IDecoration FindByType(string type)
        {
            IDecoration d = this.decorations.FirstOrDefault(x => x.GetType().Name == type);
            if (d == null) return null;
            return d;
        }

        public bool Remove(IDecoration model)
        {
            IDecoration d = this.decorations.FirstOrDefault(x => x.Price == model.Price);
            if (d == null) return false;
            this.decorations.Remove(d);
            return true;
        }
    }
}
