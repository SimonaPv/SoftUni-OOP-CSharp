using Gym.Core.Contracts;
using Gym.Models.Athletes;
using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms;
using Gym.Models.Gyms.Contracts;
using Gym.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Core
{
    public class Controller : IController
    {
        private EquipmentRepository equipment;
        private List<IGym> gyms;

        public Controller()
        {
            equipment = new EquipmentRepository();
            gyms = new List<IGym>();
        }

        public string AddAthlete(string gymName, string athleteType, string athleteName, string motivation, int numberOfMedals)
        {
            if (athleteType != "Boxer" && athleteType != "Weightlifter")
            {
                throw new InvalidOperationException("Invalid athlete type.");
            }

            IGym gym = this.gyms.FirstOrDefault(x => x.Name == gymName);

            IAthlete ath;
            if (athleteType == "Boxer")
            {
                ath = new Boxer(athleteName, motivation, numberOfMedals);
            }
            else
            {
                ath = new Weightlifter(athleteName, motivation, numberOfMedals);
            }

            if (athleteType == "Boxer" && gym.GetType().Name == "BoxingGym")
            {
                gym.AddAthlete(ath);
            }
            else if (athleteType == "Weightlifter" && gym.GetType().Name == "WeightliftingGym")
            {
                gym.AddAthlete(ath);
            }
            else
            {
                return "The gym is not appropriate.";
            }

            return $"Successfully added {athleteType} to {gymName}.";
        }

        public string AddEquipment(string equipmentType)
        {
            if (equipmentType != "BoxingGloves" && equipmentType != "Kettlebell")
            {
                throw new InvalidOperationException("Invalid equipment type.");
            }

            IEquipment equ;
            if (equipmentType == "BoxingGloves")
            {
                equ = new BoxingGloves();
            }
            else
            {
                equ = new Kettlebell();
            }

            equipment.Add(equ);
            return $"Successfully added {equipmentType}.";
        }

        public string AddGym(string gymType, string gymName)
        {
            if (gymType != "WeightliftingGym" && gymType != "BoxingGym")
            {
                throw new InvalidOperationException("Invalid gym type.");
            }

            IGym gym;
            if (gymType == "WeightliftingGym")
            {
                gym = new WeightliftingGym(gymName);
            }
            else
            {
                gym = new BoxingGym(gymName);
            }

            gyms.Add(gym);
            return $"Successfully added {gymType}.";
        }

        public string EquipmentWeight(string gymName)
        {
            IGym gym = this.gyms.FirstOrDefault(x => x.Name == gymName);
            return $"The total weight of the equipment in the gym {gymName} is {gym.EquipmentWeight:f2} grams.";


        }

        public string InsertEquipment(string gymName, string equipmentType)
        {
            IEquipment e = this.equipment.FindByType(equipmentType);
            if (e == null)
            {
                throw new InvalidOperationException($"There isn’t equipment of type {equipmentType}.");
            }

            IGym g = this.gyms.FirstOrDefault(x => x.Name == gymName);
            g.AddEquipment(e);
            equipment.Remove(e);
            return $"Successfully added {equipmentType} to {gymName}.";
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var g in gyms)
            {
                sb.AppendLine(g.GymInfo());
            }
            return sb.ToString().TrimEnd();
        }

        public string TrainAthletes(string gymName)
        {
            IGym gym = this.gyms.FirstOrDefault(x => x.Name == gymName);
            foreach (var a in gym.Athletes)
            {
                a.Exercise();
            }

            return $"Exercise athletes: {gym.Athletes.Count}.";
        }
    }
}
