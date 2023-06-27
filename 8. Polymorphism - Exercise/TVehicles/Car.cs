using System;
using System.Collections.Generic;
using System.Text;

namespace TVehicles
{
    public class Car : Vehicle
    {
        public Car(double quantity, double consumption, double capacity) : base(quantity, consumption, capacity)
        {
        }

        public override double Consumption 
            => base.Consumption + 0.9;
    }
}
