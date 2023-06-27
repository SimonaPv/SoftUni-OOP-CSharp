using PlanetWars.Models.MilitaryUnits.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanetWars.Models.MilitaryUnits
{
    public abstract class MilitaryUnit : IMilitaryUnit
    {
        private double cost;
        private int enduranceLevel;

        protected MilitaryUnit(double cost)
        {
            this.Cost = cost;
            this.EnduranceLevel = 1;
        }

        public double Cost
        {
            get => this.cost;
            private set => this.cost = value;
        }

        public int EnduranceLevel
        {
            get => this.enduranceLevel;
            private set => this.enduranceLevel = value;
        }

        public void IncreaseEndurance()
        {
            if (this.EnduranceLevel + 1 >= 21)
            {
                this.EnduranceLevel = 20;
                throw new ArgumentException("Endurance level cannot exceed 20 power points.");
            }

            this.EnduranceLevel++;
        }
    }
}
