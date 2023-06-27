using System;
using System.Collections.Generic;
using System.Text;

namespace TRaiding
{
    public class Warrior : BaseHero
    {
        public Warrior(string name)
        {
            this.Name = name;
            this.Power = 100;
        }

        public override string Name { get; set; }
        public override int Power { get; set; }

        public override string CastAbility()
        {
            return $"{GetType().Name} - {Name} hit for {Power} damage";
        }
    }
}
