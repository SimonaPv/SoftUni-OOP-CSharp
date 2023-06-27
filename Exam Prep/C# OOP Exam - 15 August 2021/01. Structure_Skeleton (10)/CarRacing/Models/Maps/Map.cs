using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Maps
{
    public class Map : IMap
    {
        public string StartRace(IRacer racerOne, IRacer racerTwo)
        {
            if (racerOne.IsAvailable() == false && racerTwo.IsAvailable() == false)
            {
                return "Race cannot be completed because both racers are not available!";
            }
            else if (racerOne.IsAvailable() == false)
            {
                return $"{racerTwo.Username} wins the race! {racerOne.Username} was not available to race!";
            }
            else if (racerTwo.IsAvailable() == false)
            {
                return $"{racerOne.Username} wins the race! {racerTwo.Username} was not available to race!";
            }

            double racerOneSum = 0;
            double racerTwoSum = 0;

            racerOne.Race();
            racerTwo.Race();

            if (racerOne.RacingBehavior == "strict")
            {
                racerOneSum = racerOne.Car.HorsePower * racerOne.DrivingExperience * 1.2;
            }
            else
            {
                racerOneSum = racerOne.Car.HorsePower * racerOne.DrivingExperience * 1.1;
            }

            if (racerTwo.RacingBehavior == "strict")
            {
                racerTwoSum = racerOne.Car.HorsePower * racerOne.DrivingExperience * 1.2;
            }
            else
            {
                racerTwoSum = racerOne.Car.HorsePower * racerOne.DrivingExperience * 1.1;
            }

            if (racerOneSum > racerTwoSum)
            {
                return $"{racerOne.Username} has just raced against {racerTwo.Username}! {racerOne.Username} is the winner!";
            }
            else
            {
                return $"{racerOne.Username} has just raced against {racerTwo.Username}! {racerTwo.Username} is the winner!";
            }
        }
    }
}
