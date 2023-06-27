using Gym.Models.Athletes;
using Gym.Models.Equipment.Contracts;
using Gym.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Repositories
{
    public class EquipmentRepository : IRepository<IEquipment>
    {
        private readonly List<IEquipment> equipment;

        public EquipmentRepository()
        {
            this.equipment = new List<IEquipment>();
        }

        public IReadOnlyCollection<IEquipment> Models => this.equipment;

        public void Add(IEquipment model)
        {
            equipment.Add(model);
        }

        public IEquipment FindByType(string type)
        {
            IEquipment e = this.equipment.FirstOrDefault(x => x.GetType().Name == type);
            if (e == null) return null;
            return e;
        }

        public bool Remove(IEquipment model)
        {
            if (!equipment.Any(x => x.Price == model.Price))
            {
                return false;
            }

            equipment.Remove(model);
            return true;
        }
    }
}
