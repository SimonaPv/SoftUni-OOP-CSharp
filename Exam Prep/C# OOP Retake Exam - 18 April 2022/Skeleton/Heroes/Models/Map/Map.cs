using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Models.Map
{
    public class Map : IMap
    {
        public string Fight(ICollection<IHero> players)
        {
            var knigths = new List<Knight>();
            var barbarians = new List<Barbarian>();

            foreach (var player in players)
            {
                if (player.IsAlive)
                {
                    if (player is Knight kn)
                    {
                        knigths.Add(kn);
                    }
                    else if (player is Barbarian bar)
                    {
                        barbarians.Add(bar);
                    }
                }
            }

            bool continueBattle = true;
            bool knWin = false;

            while (continueBattle)
            {
                foreach (var kn in knigths.Where(x => x.IsAlive))
                {
                    foreach (var bar in barbarians.Where(x => x.IsAlive))
                    {
                        bar.TakeDamage(kn.Weapon.DoDamage());

                        if (barbarians.All(x => x.IsAlive == false))
                        {
                            continueBattle = false;
                            knWin = true;
                        }
                    }
                }

                if (continueBattle)
                {
                    foreach (var bar in barbarians.Where(x => x.IsAlive))
                    {
                        foreach (var kn in knigths.Where(x => x.IsAlive))
                        {
                            kn.TakeDamage(bar.Weapon.DoDamage());

                            if (knigths.All(x => x.IsAlive == false))
                            {
                                continueBattle = false;
                            }
                        }
                    }
                }
            }

            if (knWin)
            {
                return $"The knights took {knigths.Where(x => x.IsAlive == false).Count()} casualties but won the battle.";
            }
            else
            {
                return $"The barbarians took {barbarians.Where(x => x.IsAlive == false).Count()} casualties but won the battle.";
            }
        }
    }
}
