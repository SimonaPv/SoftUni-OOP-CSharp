using System;
using System.Collections.Generic;
using System.Text;

namespace TVehicles
{
    public class Bus : Vehicle
    {
        public Bus(double quantity, double consumption, double capacity) : base(quantity, consumption, capacity)
        {
        }

        public override double Consumption 
            => base.Consumption + 1.4;
    }
}
