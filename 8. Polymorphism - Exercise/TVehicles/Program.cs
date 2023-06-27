using System;
using System.Collections.Generic;

namespace TVehicles
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string[] carInfo = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string[] truckInfo = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries); 
            string[] busInfo = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            Car car = new Car(double.Parse(carInfo[1]), double.Parse(carInfo[2]), double.Parse(carInfo[3]));
            Truck truck = new Truck(double.Parse(truckInfo[1]), double.Parse(truckInfo[2]), double.Parse(truckInfo[3]));
            Bus bus = new Bus(double.Parse(busInfo[1]), double.Parse(busInfo[2]), double.Parse(busInfo[3]));

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] command = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string action = command[0];
                string vehicle = command[1];
                double value = double.Parse(command[2]);

                Vehicle currV = null;

                if (vehicle == "Car")
                {
                    currV = car;

                    if (action == "Drive")
                    {
                        if (currV.CanDrive(value))
                        {
                            currV.Drive(value);
                            Console.WriteLine($"Car travelled {value} km");
                        }
                        else
                        {
                            Console.WriteLine("Car needs refueling");
                        }
                    }
                    else if (action == "Refuel")
                    {
                        currV.Refuel(value);
                    }
                }

                else if(vehicle == "Truck")
                {
                    currV= truck;

                    if (action == "Drive")
                    {
                        if (currV.CanDrive(value))
                        {
                            currV.Drive(value);
                            Console.WriteLine($"Truck travelled {value} km");
                        }
                        else
                        {
                            Console.WriteLine("Truck needs refueling");
                        }
                    }
                    else if (action == "Refuel")
                    {
                        currV.Refuel(value);
                    }
                }

                else
                {
                    currV = bus;

                    if (action == "Drive")
                    {
                        if (currV.CanDrive(value))
                        {
                            currV.Drive(value);
                            Console.WriteLine($"Bus travelled {value} km");
                        }
                        else
                        {
                            Console.WriteLine("Bus needs refueling");
                        }
                    }
                    else if (action == "Refuel")
                    {
                        currV.Refuel(value);
                    }
                    else if (action == "DriveEmpty")
                    {
                        currV.Consumption -= 1.4;

                        if (currV.CanDrive(value))
                        {
                            currV.Drive(value);
                            Console.WriteLine($"Bus travelled {value} km");
                        }
                        else
                        {
                            Console.WriteLine("Bus needs refueling");
                        }
                    }
                }
            }

            Console.WriteLine(car.ToString());
            Console.WriteLine(truck.ToString());
            Console.WriteLine(bus.ToString());
        }
    }
}
