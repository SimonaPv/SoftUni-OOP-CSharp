using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Models.Fish
{
    public class SaltwaterFish : Fish
    {
        //            Can only live in SaltwaterAquarium!!!!!!
        public SaltwaterFish(string name, string species, decimal price) : base(name, species, price)
        {
        }

        public override void Eat()
        {
            this.Size += 2;
        }

        public override int Size => 5;
    }
}
