using ChristmasPastryShop.Models.Delicacies.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Delicacies
{
    public abstract class Delicacy : IDelicacy
    {
        private string name;
        private double price;

        protected Delicacy(string delicacyName, double price)
        {
            this.Name = delicacyName;
            this.Price = price;
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(Utilities.Messages.ExceptionMessages.NameNullOrWhitespace.TrimEnd()));
                }
                this.name = value;
            }
        }

        public double Price
        {
            get => this.price;
            private set => this.price = value;
        }

        public override string ToString()
        {
            return $"{this.Name} - {this.Price:f2} lv".TrimEnd();
        }
    }
}
