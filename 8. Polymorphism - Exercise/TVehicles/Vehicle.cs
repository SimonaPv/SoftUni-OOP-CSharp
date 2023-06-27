using System;
using System.Collections.Generic;
using System.Text;

namespace TVehicles
{
    public abstract class Vehicle
    {
        public Vehicle()
        {

        }
        protected Vehicle(double quantity, double consumption, double capacity)
        {
            this.Quantity = quantity;
            this.Consumption = consumption;
            this.Capacity = capacity;

            if (this.Quantity > this.Capacity)
            {
                this.Quantity = 0;
            }
        }

        public double Quantity { get; set; }
        public virtual double Consumption { get; set; }
        public double Capacity { get; set; }

        public bool CanDrive(double km)
           => this.Quantity - km * this.Consumption >= 0; //return true

        public void Drive(double km)
        {
            if (CanDrive(km))
            {
                this.Quantity -= km * this.Consumption;
            }
        }

        public virtual void Refuel(double fuel)
        {
            if (fuel > 0)
            {
                if (this.Quantity + fuel > this.Capacity)
                {
                    Console.WriteLine($"Cannot fit {fuel} fuel in the tank");
                }
                else { this.Quantity += fuel; }
            }
            else
            {
                Console.WriteLine("Fuel must be a positive number");
            }
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}: {this.Quantity:f2}";
        }
    }
}
