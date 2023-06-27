using System;
using System.Collections.Generic;
using System.Text;

namespace TRaiding
{
    public class Druid : BaseHero
    {
        public Druid(string name)
        {
            this.Name = name;
            this.Power = 80;
        }

        public override string Name { get; set; }
        public override int Power { get; set; }

        public override string CastAbility()
        {
            return $"{GetType().Name} - {Name} healed for {Power}";
        }
    }
}
