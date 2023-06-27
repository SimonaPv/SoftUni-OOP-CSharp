using CarRacing.Core.Contracts;
using CarRacing.Models.Cars;
using CarRacing.Models.Cars.Contracts;
using CarRacing.Models.Maps;
using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarRacing.Core
{
    public class Controller : IController
    {
        private CarRepository cars;
        private RacerRepository racers;
        private IMap map;

        public Controller()
        {
            this.cars = new CarRepository();
            this.racers = new RacerRepository();
            this.map = new Map();
        }

        public string AddCar(string type, string make, string model, string VIN, int horsePower)
        {
            if (type != "TunedCar" && type != "SuperCar")
            {
                throw new ArgumentException("Invalid car type.");
            }

            ICar car;
            if (type == "TunedCar")
            {
                car = new TunedCar(make, model, VIN, horsePower);
            }
            else
            {
                car = new SuperCar(make, model, VIN, horsePower); 
            }

            cars.Add(car);
            return $"Successfully added car {make} {model} ({VIN}).";
        }

        public string AddRacer(string type, string username, string carVIN)
        {
            if (type != "StreetRacer" && type != "ProfessionalRacer")
            {
                throw new ArgumentException("Invalid racer type!");
            }

            ICar car = cars.FindBy(carVIN);
            if(car == null) return "Car cannot be found!";

            IRacer racer;
            if (type == "ProfessionalRacer")
            {
                racer = new ProfessionalRacer(username, car);
            }
            else
            {
                racer = new StreetRacer(username, car);
            }

            racers.Add(racer);
            return $"Successfully added racer {username}.";
        }

        public string BeginRace(string racerOneUsername, string racerTwoUsername)
        {
            IRacer racer1 = racers.FindBy(racerOneUsername);
            if (racer1 == null) return $"Racer {racerOneUsername} cannot be found!";

            IRacer racer2 = racers.FindBy(racerTwoUsername);
            if (racer2 == null) return $"Racer {racerTwoUsername} cannot be found!";

            return map.StartRace(racer1, racer2);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var rac in racers.Models.OrderByDescending(x => x.DrivingExperience).ThenBy(x => x.Username))
            {
                sb.AppendLine($"{rac.GetType().Name}: {rac.Username}");
                sb.AppendLine($"--Driving behavior: {rac.RacingBehavior}");
                sb.AppendLine($"--Driving experience: {rac.DrivingExperience}");
                sb.AppendLine($"--Car: {rac.Car.Make} {rac.Car.Model} ({rac.Car.VIN})");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
