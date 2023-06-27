using System;
using System.Collections.Generic;
using System.Text;

namespace TRaiding
{
    public class HeroFactory
    {
        public static BaseHero GetHero(string type, string name)
        {
            if (type == "Druid")
            {
                return new Druid(name);
            }
            else if (type == "Paladin")
            {
                return new Paladin(name);
            }
            else if (type == "Rogue")
            {
                return new Rogue(name);
            }
            else if (type == "Warrior")
            {
                return new Warrior(name);
            }
            else
            {
                throw new ArgumentException($"Invalid hero!");
            }
        }
    }
}
