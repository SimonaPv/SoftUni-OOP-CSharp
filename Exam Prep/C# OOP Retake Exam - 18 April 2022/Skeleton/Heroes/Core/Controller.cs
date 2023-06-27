using Heroes.Core.Contracts;
using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using Heroes.Models.Map;
using Heroes.Models.Weapons;
using Heroes.Repositories;
using Heroes.Repositories.Contracts;
using System;
using System.Linq;
using System.Text;

namespace Heroes.Core
{
    public class Controller : IController
    {
        private IRepository<IHero> heroes;
        private IRepository<IWeapon> weapons;

        public Controller()
        {
            this.heroes = new HeroRepository();
            this.weapons = new WeaponRepository();
        }

        public string AddWeaponToHero(string weaponName, string heroName)
        {
            IWeapon weapon = weapons.FindByName(weaponName);
            IHero hero = heroes.FindByName(heroName);

            if (weapon == null)
            {
                throw new InvalidOperationException($"Weapon {weaponName} does not exist.");
            }
            if (hero == null)
            {
                throw new InvalidOperationException($"Hero {heroName} is well-armed.");
            }
            if (hero.Weapon != null)
            {
                throw new InvalidOperationException($"Hero {heroName} is well-armed.");
            }

            hero.AddWeapon(weapon);
            weapons.Remove(weapon);
            return $"Hero {heroName} can participate in battle using a {weapon.GetType().Name.ToLower()}.";
        }

        public string CreateHero(string type, string name, int health, int armour)
        {
            Hero hero;

            if (heroes.Models.FirstOrDefault(x => x.Name == name) == null)
            {
                if (type == "Knight")
                {
                    hero = new Knight(name, health, armour);
                    heroes.Add(hero);
                    return $"Successfully added Sir {name} to the collection.";
                }
                else if (type == "Barbarian")
                {
                    hero = new Barbarian(name, health, armour);
                    heroes.Add(hero);
                    return $"Successfully added Barbarian {name} to the collection.";

                }
                else
                {
                    throw new InvalidOperationException("Invalid hero type.");
                }
            }
            else
            {
                throw new InvalidOperationException($"The hero {name} already exists.");
            }
        }

        public string CreateWeapon(string type, string name, int durability)
        {
            Weapon weapon;

            if (weapons.Models.FirstOrDefault(x => x.Name == name) == null)
            {
                if (type == "Claymore")
                {
                    weapon = new Claymore(name, durability);
                }
                else if (type == "Mace")
                {
                    weapon = new Mace(name, durability);
                }
                else
                {
                    throw new InvalidOperationException("Invalid weapon type.");
                }
            }
            else
            {
                throw new InvalidOperationException($"The weapon {name} already exists.");
            }

            weapons.Add(weapon);
            return $"A {type.ToLower()} {name} is added to the collection.";
        }

        public string HeroReport()
        {
            var list = heroes.Models
                .OrderBy(x => x.GetType().Name)
                .ThenByDescending(x => x.Health)
                .ThenBy(x => x.Name);

            StringBuilder sb = new StringBuilder();

            foreach (var hero in list)
            {
                if (hero.Weapon == null)
                {
                    sb.AppendLine($"{hero.GetType().Name}: {hero.Name}{Environment.NewLine}" +
                                $"--Health: {hero.Health}{Environment.NewLine}" +
                                $"--Armour: {hero.Armour}{Environment.NewLine}" +
                                $"--Weapon: Unarmed");
                }
                else
                {
                    sb.AppendLine($"{hero.GetType().Name}: {hero.Name}{Environment.NewLine}" +
                                $"--Health: {hero.Health}{Environment.NewLine}" +
                                $"--Armour: {hero.Armour}{Environment.NewLine}" +
                                $"--Weapon: {hero.Weapon.Name}");
                }
            }

            return sb.ToString().TrimEnd();   
        }

        public string StartBattle()
        {
            Map map = new Map();
            var players = heroes.Models.Where(x => x.Weapon != null && x.IsAlive).ToList();
            return map.Fight(players);
        }
    }
}
