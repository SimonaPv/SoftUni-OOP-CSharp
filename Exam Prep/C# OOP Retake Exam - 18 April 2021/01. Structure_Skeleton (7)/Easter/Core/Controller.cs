using Easter.Core.Contracts;
using Easter.Models.Bunnies;
using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes;
using Easter.Models.Dyes.Contracts;
using Easter.Models.Eggs;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops.Contracts;
using Easter.Models.Workshops;
using Easter.Repositories;
using Easter.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easter.Core
{
    public class Controller : IController
    {
        private BunnyRepository bunnies;
        private EggRepository eggs;

        public Controller()
        {
            bunnies = new BunnyRepository();
            eggs = new EggRepository();
        }

        public string AddBunny(string bunnyType, string bunnyName)
        {
            if (bunnyType != "HappyBunny" && bunnyType != "SleepyBunny")
            {
                throw new InvalidOperationException("Invalid bunny type.");
            }

            IBunny bunny;
            if (bunnyType == "HappyBunny")
            {
                bunny = new HappyBunny(bunnyName);
            }
            else
            {
                bunny = new SleepyBunny(bunnyName);
            }

            bunnies.Add(bunny);
            return $"Successfully added {bunnyType} named {bunnyName}.";
        }

        public string AddDyeToBunny(string bunnyName, int power)
        {
            if (bunnies.FindByName(bunnyName) == null)
            {
                throw new InvalidOperationException("The bunny you want to add a dye to doesn't exist!");
            }

            IBunny bunny = bunnies.FindByName(bunnyName);

            IDye dye = new Dye(power);
            bunny.AddDye(dye);

            return $"Successfully added dye with power {power} to bunny {bunnyName}!";
        }

        public string AddEgg(string eggName, int energyRequired)
        {
            IEgg e = new Egg(eggName, energyRequired);
            eggs.Add(e);
            return $"Successfully added egg: {eggName}!";
        }

        public string ColorEgg(string eggName)
        {
            IEgg egg = eggs.FindByName(eggName);
            var selectedBunnies = bunnies.Models.OrderByDescending(x => x.Energy).TakeWhile(x => x.Energy >= 50);

            if (!selectedBunnies.Any())
            {
                throw new InvalidOperationException("There is no bunny ready to start coloring!");
            }

            IWorkshop workshop = new Workshop();
            foreach (var bunny in selectedBunnies)
            {
                workshop.Color(egg, bunny);
                if (bunny.Energy == 0)
                {
                    bunnies.Remove(bunny);
                }
            }

            if (egg.IsDone())
            {
                return $"Egg {eggName} is done.";
            }
            else
            {
                return $"Egg {eggName} is not done.";
            }
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{eggs.Models.Select(x => x.IsDone() == true).Count()} eggs are done!");
            sb.AppendLine("Bunnies info:");

            foreach (var b in bunnies.Models)
            {
                sb.AppendLine($"Name: {b.Name}");
                sb.AppendLine($"Energy: {b.Energy}");
                sb.AppendLine($"Dyes: {b.Dyes.Select(x => x.IsFinished() == false).Count()} not finished");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
