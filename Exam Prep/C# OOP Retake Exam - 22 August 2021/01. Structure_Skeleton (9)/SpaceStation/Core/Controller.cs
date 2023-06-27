using SpaceStation.Core.Contracts;
using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission;
using SpaceStation.Models.Planets;
using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Core
{
    public class Controller : IController
    {
        private AstronautRepository astronautRepository;
        private PlanetRepository planetRepository;
        private int explored;

        public Controller()
        {
            this.astronautRepository = new AstronautRepository();
            this.planetRepository = new PlanetRepository();
        }

        public string AddAstronaut(string type, string astronautName)
        {
            if (type != "Meteorologist" && type != "Geodesist" && type != "Biologist")
            {
                throw new InvalidOperationException("Astronaut type doesn't exists!");
            }

            IAstronaut a;
            if (type == "Meteorologist")
            {
                a = new Meteorologist(astronautName);
            }
            else if (type == "Geodesist")
            {
                a = new Geodesist(astronautName);
            }
            else
            {
                a = new Biologist(astronautName);
            }

            astronautRepository.Add(a);
            return $"Successfully added {type}: {astronautName}!";
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            IPlanet p = new Planet(planetName);

            foreach (var i in items)
            {
                p.Items.Add(i);
            }
            planetRepository.Add(p);

            return $"Successfully added Planet: {planetName}!";
        }

        public string ExplorePlanet(string planetName)
        {
            List<IAstronaut> sortedList = astronautRepository.Models.Where(x => x.Oxygen > 60).ToList();

            if (sortedList.Count() == 0)
            {
                throw new InvalidOperationException("You need at least one astronaut to explore the planet!");
            }

            IPlanet p = planetRepository.FindByName(planetName);

            Mission mission = new Mission();
            mission.Explore(p, sortedList);
            explored++;

            return $"Planet: {planetName} was explored! Exploration finished with {sortedList.Count(x => x.CanBreath == false)} dead astronauts!";
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{explored} planets were explored!");
            sb.AppendLine("Astronauts info:");

            foreach (var ast in astronautRepository.Models)
            {
                sb.AppendLine($"Name: {ast.Name}");
                sb.AppendLine($"Oxygen: {ast.Oxygen}");
                sb.Append("Bag items: ");

                sb.AppendLine(ast.Bag.Items.Count == 0
                    ? "none"
                    : string.Join(", ", ast.Bag.Items));
            }

            return sb.ToString().TrimEnd(); 
        }

        public string RetireAstronaut(string astronautName)
        {
            IAstronaut a = astronautRepository.FindByName(astronautName);

            if (a == null)
            {
                throw new InvalidOperationException($"Astronaut {astronautName} doesn't exists!");
            }

            astronautRepository.Remove(a);
            return $"Astronaut {astronautName} was retired!";
        }
    }
}
