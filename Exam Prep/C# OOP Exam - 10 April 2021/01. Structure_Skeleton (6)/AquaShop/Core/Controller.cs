using AquaShop.Core.Contracts;
using AquaShop.Models.Aquariums;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Core
{
    public class Controller : IController
    {
        private DecorationRepository decorationRepository;
        private List<IAquarium> aquariums;

        public Controller()
        {
            decorationRepository = new DecorationRepository();
            aquariums = new List<IAquarium>();
        }

        public string AddAquarium(string aquariumType, string aquariumName)
        {
            if (aquariumType != "SaltwaterAquarium" && aquariumType != "FreshwaterAquarium")
            {
                throw new InvalidOperationException("Invalid aquarium type.");
            }

            IAquarium aquarium;
            if (aquariumType == "FreshwaterAquarium")
            {
                aquarium = new FreshwaterAquarium(aquariumName);
            }
            else
            {
                aquarium = new SaltwaterAquarium(aquariumName);
            }

            aquariums.Add(aquarium);
            return $"Successfully added {aquariumType}.";
        }

        public string AddDecoration(string decorationType)
        {
            if (decorationType != "Ornament" && decorationType != "Plant")
            {
                throw new InvalidOperationException("Invalid decoration type.");
            }

            IDecoration decoration;
            if (decorationType == "Ornament")
            {
                decoration = new Ornament();
            }
            else
            {
                decoration = new Plant();
            }

            decorationRepository.Add(decoration);
            return $"Successfully added {decorationType}.";
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            if (fishType != "FreshwaterFish" && fishType != "SaltwaterFish")
            {
                throw new InvalidOperationException("Invalid fish type.");
            }

            IAquarium aquarium = aquariums.FirstOrDefault(x => x.Name == aquariumName);

            IFish fish;

            if (fishType == "FreshwaterFish")
            {
                fish = new FreshwaterFish(fishName, fishSpecies, price);

                if (aquarium.GetType().Name == "SaltwaterAquarium")
                {
                    return "Water not suitable.";
                }
                else
                {
                    foreach (var a in aquariums)
                    {
                        if (a.Name == aquariumName)
                        {
                            a.AddFish(fish);
                        }
                    }
                }
            }
            else
            {
                fish = new SaltwaterFish(fishName, fishSpecies, price);

                if (aquarium.GetType().Name == "FreshwaterAquarium")
                {
                    return "Water not suitable.";
                }
                else
                {
                    foreach (var a in aquariums)
                    {
                        if (a.Name == aquariumName)
                        {
                            a.AddFish(fish);
                        }
                    }
                }
            }

            return $"Successfully added {fishType} to {aquariumName}.";
        }

        public string CalculateValue(string aquariumName)
        {
            IAquarium aquarium = aquariums.FirstOrDefault(x => x.Name == aquariumName);

            decimal sumFish = 0m;
            foreach (var f in aquarium.Fish)
            {
                sumFish += f.Price;
            }

            decimal sumDec = 0m;
            foreach (var d in aquarium.Decorations)
            {
                sumDec += d.Price;
            }

            decimal sum = sumFish + sumDec;
            return $"The value of Aquarium {aquariumName} is {sum:f2}.";
        }

        public string FeedFish(string aquariumName)
        {
            int count = 0;

            foreach (var a in aquariums)
            {
                if (a.Name == aquariumName)
                {
                    a.Feed();
                    count = a.Fish.Count;
                }
            }

            return $"Fish fed: {count}";
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            IDecoration d = decorationRepository.FindByType(decorationType);
            if (d == null)
            {
                throw new InvalidOperationException($"There isn't a decoration of type {decorationType}.");
            }

            foreach (var a in aquariums)
            {
                if (a.Name == aquariumName)
                {
                    a.AddDecoration(d);
                    break;
                }
            }

            decorationRepository.Remove(d);
            return $"Successfully added {decorationType} to {aquariumName}.";
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var a in aquariums)
            {
                sb.AppendLine(a.GetInfo());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
