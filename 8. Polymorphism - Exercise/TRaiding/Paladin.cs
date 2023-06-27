using System;
using System.Collections.Generic;
using System.Text;

namespace TRaiding
{
    public class Paladin : BaseHero
    {
        public Paladin (string name)
        {
            this.Name = name;
            this.Power = 100;
        }

        public override string Name { get; set; }
        public override int Power { get; set; }

        public override string CastAbility()
        {
            return $"{GetType().Name} - {Name} healed for {Power}";
        }
    }
}
