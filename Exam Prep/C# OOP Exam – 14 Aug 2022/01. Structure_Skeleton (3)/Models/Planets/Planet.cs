using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Models.Planets
{
    public class Planet : IPlanet
    {
        private UnitRepository unitRepository;
        private WeaponRepository weaponRepository;
        private string name;
        private double budget;

        public Planet(string name, double budget)
        {
            unitRepository = new UnitRepository();
            weaponRepository = new WeaponRepository();
            this.Name = name;
            this.Budget = budget;
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Planet name cannot be null or empty.");
                }
                this.name = value;
            }
        }

        public double Budget
        {
            get => this.budget;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Budget's amount cannot be negative.");
                }
                this.budget = value;
            }
        }


        public double MilitaryPower => this.MilitaryPowerSum();

        public IReadOnlyCollection<IMilitaryUnit> Army => this.unitRepository.Models;

        public IReadOnlyCollection<IWeapon> Weapons => this.weaponRepository.Models;

        public void AddUnit(IMilitaryUnit unit)
        {
            this.unitRepository.AddItem(unit);
        }

        public void AddWeapon(IWeapon weapon)
        {
            this.weaponRepository.AddItem(weapon);
        }

        public string PlanetInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Planet: {this.Name}");
            sb.AppendLine($"--Budget: {this.Budget} billion QUID");
            sb.Append($"--Forces: ");
            sb.AppendLine(this.unitRepository.Models.Count == 0 ? "No units" : string.Join(", ", this.unitRepository.Models.Select(x => x.GetType().Name)));
            sb.Append($"--Combat equipment: ");
            sb.AppendLine(this.weaponRepository.Models.Count == 0 ? "No weapons" : string.Join(", ", this.weaponRepository.Models.Select(x=>x.GetType().Name)));
            sb.AppendLine($"--Military Power: {this.MilitaryPower}");

            return sb.ToString().TrimEnd();
        }

        public void Profit(double amount)
        {
            this.Budget += amount;
        }

        public void Spend(double amount)
        {
            if (this.Budget < amount)
            {
                throw new InvalidOperationException("Budget too low!");
            }

            this.Budget -= amount;
        }

        public void TrainArmy()
        {
            foreach (var item in Army)
            {
                item.IncreaseEndurance();
            }
        }

        private double MilitaryPowerSum()
        {
            double totalAmount = (this.unitRepository.Models.Sum(x => x.EnduranceLevel) + this.weaponRepository.Models.Sum(x => x.DestructionLevel));

            IMilitaryUnit unit = this.unitRepository.Models.FirstOrDefault(x => x.GetType().Name == "AnonymousImpactUnit");
            if (unit != null)
            {
                totalAmount += totalAmount * 0.3;
            }

            IWeapon weapon = this.weaponRepository.Models.FirstOrDefault(x => x.GetType().Name == "NuclearWeapon");
            if (weapon != null)
            {
                totalAmount += totalAmount * 0.45;
            }

            return Math.Round(totalAmount, 3);
        }
    }
}
