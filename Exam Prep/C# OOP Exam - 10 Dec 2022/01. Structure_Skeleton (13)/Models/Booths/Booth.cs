using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Booths
{
    public class Booth : IBooth
    {
        private int boothId;
        private int capacity;
        private DelicacyRepository delicacyMenu;
        private CocktailRepository cocktailMenu;
        private double currentBill;
        private double turnover;
        private bool isReserved;

        public Booth(int boothId, int capacity)
        {
            this.BoothId = boothId;
            this.Capacity = capacity;
            this.delicacyMenu = new DelicacyRepository();
            this.cocktailMenu = new CocktailRepository();
            this.CurrentBill = 0.0;
            this.Turnover = 0.0;
            this.IsReserved = false;
        }

        public int BoothId
        {
            get => this.boothId;
            private set => this.boothId = value;
        }

        public int Capacity
        {
            get => this.capacity;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(Utilities.Messages.ExceptionMessages.CapacityLessThanOne).TrimEnd());
                }
                this.capacity = value;
            }
        }

        public IRepository<IDelicacy> DelicacyMenu => this.delicacyMenu;

        public IRepository<ICocktail> CocktailMenu => this.cocktailMenu;

        public double CurrentBill
        {
            get => this.currentBill;
            private set => this.currentBill = value;
        }

        public double Turnover
        {
            get => this.turnover;
            private set => this.turnover = value;
        }

        public bool IsReserved
        {
            get => this.isReserved; 
            private set => this.isReserved = value;
        }

        public void ChangeStatus()
        {
            if (this.IsReserved == true)
            {
                this.IsReserved = false;
            }
            else
            {
                this.IsReserved = true;
            }
        }

        public void Charge()
        {
            this.Turnover = this.CurrentBill;
            this.CurrentBill = 0.0;
        }

        public void UpdateCurrentBill(double amount)
        {
            this.CurrentBill += amount;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Booth: {this.BoothId}".TrimEnd());
            sb.AppendLine($"Capacity: {this.Capacity}".TrimEnd());
            sb.AppendLine($"Turnover: {this.Turnover:f2} lv".TrimEnd());

            sb.AppendLine($"-Cocktail menu:".TrimEnd());

            foreach (ICocktail c in cocktailMenu.Models)
            {
                sb.AppendLine($"--{c.ToString()}".TrimEnd());
            }

            sb.AppendLine($"-Delicacy menu:".TrimEnd());

            foreach (IDelicacy d in delicacyMenu.Models)
            {
                sb.AppendLine($"--{d.ToString()}".TrimEnd());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
