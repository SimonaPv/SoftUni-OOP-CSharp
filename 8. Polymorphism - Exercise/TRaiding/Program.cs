using System;
using System.Collections.Generic;

namespace TRaiding
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<BaseHero> list = new List<BaseHero>();
            int power = 0;
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i ++)
            {
                string hero = Console.ReadLine();
                string type = Console.ReadLine();

                try
                {
                    BaseHero bh = HeroFactory.GetHero(type, hero);
                    list.Add(bh);

                    power += bh.Power;
                }
                catch (ArgumentException ae)
                {

                    Console.WriteLine(ae.Message);
                }
            }

            int boss = int.Parse(Console.ReadLine());

            foreach (var item in list)
            {
                Console.WriteLine(item.CastAbility());
            }

            if (power >= boss) { Console.WriteLine("Victory!"); }
            else { Console.WriteLine("Defeat..."); }
        }
    }
}
