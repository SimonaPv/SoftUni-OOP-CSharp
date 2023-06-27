using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ShoppingSpree
{
    public class Person
    {
        private string name;
        private int money;

        public Person(string name, int money)
        {
            this.Name = name;
            this.Money = money;
        }
        
        public string Name
        {
            get { return this.name; }
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"Name cannot be empty");
                }
                this.name = value;
            }
        }
        public int Money
        {
            get { return this.money; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"Money cannot be negative");
                }
                this.money = value;
            }
        }
        public List<Product> BagOfProd { get; set; } = new List<Product>();

        public override string ToString()
        {
            return $"{this.Name} - {string.Join(", ", BagOfProd.Select(x => x.Name))}";
        }
    }
}
