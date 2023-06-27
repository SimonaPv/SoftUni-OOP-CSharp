using System;
using System.Collections.Generic;
using System.Text;

namespace TVehicles
{
    public class Truck : Vehicle
    {
        public Truck(double quantity, double consumption, double capacity) : base(quantity, consumption, capacity)
        {
        }

        public override double Consumption
            => base.Consumption + 1.6;

        public override void Refuel(double fuel)
        {
            if (base.Quantity + fuel > base.Capacity)
            {
                Console.WriteLine($"Cannot fit {fuel} fuel in the tank");
            }
            else
            {
                fuel *= 0.95;
                base.Refuel(fuel);
            }
        }
    }
}
