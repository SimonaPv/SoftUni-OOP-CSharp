using ChristmasPastryShop.Models.Cocktails.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Cocktails
{
    public abstract class Cocktail : ICocktail
    {
        private string name;
        private string size;
        private double price;

        protected Cocktail(string cocktailName, string size, double price)
        {
            this.Name = cocktailName;
            this.Size = size;
            this.Price = price;
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(Utilities.Messages.ExceptionMessages.NameNullOrWhitespace.TrimEnd());
                }
                this.name = value;
            }
        }

        public string Size
        {
            get => this.size;
            private set => this.size = value;
        }

        public double Price
        {
            get => this.price;
            private set
            {
                if (this.Size == "Large")
                {
                    this.price = value;
                }
                else if (this.Size == "Middle")
                {
                    value = 2 * (value / 3);
                    this.price = value;
                }
                else
                {
                    value /= 3;
                    this.price = value;
                }
            }
        }

        public override string ToString()
        {
            return $"{this.Name} ({this.Size}) - {this.Price:f2} lv".TrimEnd();
        }
    }
}
