using PlanetWars.Core.Contracts;
using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Core
{
    public class Controller : IController
    {
        private PlanetRepository planetRepository;

        public Controller()
        {
            this.planetRepository = new PlanetRepository();
        }

        public string AddUnit(string unitTypeName, string planetName)
        {
            IMilitaryUnit unit;
            if (unitTypeName == "AnonymousImpactUnit")
            {
                unit = new AnonymousImpactUnit();
            }
            else if (unitTypeName == "SpaceForces")
            {
                unit = new SpaceForces();
            }
            else if (unitTypeName == "StormTroopers")
            {
                unit = new StormTroopers();
            }
            else
            {
                throw new InvalidOperationException($"{unitTypeName} still not available!");
            }

            IPlanet planet = this.planetRepository.Models.FirstOrDefault(x => x.Name == planetName);
            if (planet == null)
            {
                throw new InvalidOperationException($"Planet {planetName} does not exist!");
            }

            if (!planet.Army.Any(x => x.GetType().Name == unit.GetType().Name))
            {
                planet.AddUnit(unit);
                planet.Spend(unit.Cost);
                return $"{unitTypeName} added successfully to the Army of {planetName}!";
            }
            else
            {
                throw new InvalidOperationException($"{unitTypeName} already added to the Army of {planetName}!");
            }
        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            IWeapon weapon;
            if (weaponTypeName == "SpaceMissiles")
            {
                weapon = new SpaceMissiles(destructionLevel);
            }
            else if (weaponTypeName == "NuclearWeapon")
            {
                weapon = new NuclearWeapon(destructionLevel);
            }
            else if (weaponTypeName == "BioChemicalWeapon")
            {
                weapon = new BioChemicalWeapon(destructionLevel);
            }
            else
            {
                throw new InvalidOperationException($"{weaponTypeName} still not available!");
            }

            IPlanet planet = this.planetRepository.Models.FirstOrDefault(x => x.Name == planetName);
            if (planet == null)
            {
                throw new InvalidOperationException($"Planet {planetName} does not exist!");
            }

            if (!planet.Weapons.Any(x => x.GetType().Name == weaponTypeName))
            {
                planet.AddWeapon(weapon);
                planet.Spend(weapon.Price);
                return $"{planetName} purchased {weaponTypeName}!";
            }
            else
            {
                throw new InvalidOperationException($"{weaponTypeName} already added to the Weapons of {planetName}!");
            }
        }

        public string CreatePlanet(string name, double budget)
        {
            if (this.planetRepository.FindByName(name) == null)
            {
                IPlanet planet = new Planet(name, budget);
                this.planetRepository.AddItem(planet);

                return $"Successfully added Planet: {name}";
            }

            return $"Planet {name} is already added!";
        }

        public string ForcesReport()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("***UNIVERSE PLANET MILITARY REPORT***");

            foreach (var item in this.planetRepository.Models
                .OrderByDescending(x => x.MilitaryPower)
                .ThenBy(x => x.Name))
            {
                sb.AppendLine(item.PlanetInfo());
            }

            return sb.ToString().TrimEnd();
        }

        public string SpaceCombat(string planetOne, string planetTwo)
        {
            IPlanet firstPlanet = this.planetRepository.FindByName(planetOne);
            IPlanet secondPlanet = this.planetRepository.FindByName(planetTwo);

            double firstSum = firstPlanet.MilitaryPower;
            double secondSum = secondPlanet.MilitaryPower;

            if (firstSum == secondSum)
            {
                if (firstPlanet.Weapons.Any(x => x.GetType().Name == "NuclearWeapon") || secondPlanet.Weapons.Any(x => x.GetType().Name == "NuclearWeapon"))
                {
                    if (firstPlanet.Weapons.Any(x => x.GetType().Name == "NuclearWeapon") && secondPlanet.Weapons.Any(x => x.GetType().Name == "NuclearWeapon"))
                    {
                        firstPlanet.Spend(firstPlanet.Budget / 2);
                        secondPlanet.Spend(secondPlanet.Budget / 2);
                        return "The only winners from the war are the ones who supply the bullets and the bandages!";
                    }
                    else if (firstPlanet.Weapons.Any(x => x.GetType().Name == "NuclearWeapon"))
                    {
                        firstPlanet.Spend(firstPlanet.Budget / 2);

                        double amount = secondPlanet.Budget / 2;
                        firstPlanet.Profit(amount);

                        double sumWeaponsAndArmy = secondPlanet.Weapons.Sum(x => x.Price) + secondPlanet.Army.Sum(x => x.Cost);
                        firstPlanet.Profit(sumWeaponsAndArmy);

                        this.planetRepository.RemoveItem(planetTwo);

                        return $"{planetOne} destructed {planetTwo}!";
                    }
                    else
                    {
                        secondPlanet.Spend(secondPlanet.Budget / 2);

                        double amount = firstPlanet.Budget / 2;
                        secondPlanet.Profit(amount);

                        double sumWeaponsAndArmy = firstPlanet.Weapons.Sum(x => x.Price) + firstPlanet.Army.Sum(x => x.Cost);
                        secondPlanet.Profit(sumWeaponsAndArmy);

                        this.planetRepository.RemoveItem(planetOne);

                        return $"{planetTwo} destructed {planetOne}!";
                    }
                }
                else
                {
                    firstPlanet.Spend(firstPlanet.Budget / 2);
                    secondPlanet.Spend(secondPlanet.Budget / 2);
                    return "The only winners from the war are the ones who supply the bullets and the bandages!";
                }
            }
            else if (firstSum > secondSum)
            {
                firstPlanet.Spend(firstPlanet.Budget / 2);

                double amount = secondPlanet.Budget / 2;
                firstPlanet.Profit(amount);

                double sumWeaponsAndArmy = secondPlanet.Weapons.Sum(x => x.Price) + secondPlanet.Army.Sum(x => x.Cost);
                firstPlanet.Profit(sumWeaponsAndArmy);

                this.planetRepository.RemoveItem(planetTwo);

                return $"{planetOne} destructed {planetTwo}!";
            }
            else
            {
                secondPlanet.Spend(secondPlanet.Budget / 2);

                double amount = firstPlanet.Budget / 2;
                secondPlanet.Profit(amount);

                double sumWeaponsAndArmy = firstPlanet.Weapons.Sum(x => x.Price) + firstPlanet.Army.Sum(x => x.Cost);
                secondPlanet.Profit(sumWeaponsAndArmy);

                this.planetRepository.RemoveItem(planetOne);

                return $"{planetTwo} destructed {planetOne}!";
            }
        }

        public string SpecializeForces(string planetName)
        {
            IPlanet planet = this.planetRepository.Models.FirstOrDefault(x => x.Name == planetName);
            if (planet == null)
            {
                throw new InvalidOperationException($"Planet {planetName} does not exist!");
            }

            if (planet.Army.Count == 0)
            {
                throw new InvalidOperationException("No units available for upgrade!");
            }

            planet.Spend(1.25);
            planet.TrainArmy();
            return $"{planetName} has upgraded its forces!";
        }
    }
}
