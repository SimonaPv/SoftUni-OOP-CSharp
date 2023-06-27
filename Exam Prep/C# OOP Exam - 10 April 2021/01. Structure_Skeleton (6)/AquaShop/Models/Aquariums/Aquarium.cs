using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Models.Aquariums
{
    public abstract class Aquarium : IAquarium
    {
        private string name;
        private int capacity;
        private List<IDecoration> decorations;
        private List<IFish> fishes;

        protected Aquarium(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.decorations = new List<IDecoration>();
            this.fishes = new List<IFish>();
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Aquarium name cannot be null or empty.");
                }
                this.name = value;
            }
        }

        public int Capacity
        {
            get => this.capacity;
            private set => this.capacity = value;
        }

        public int Comfort => this.ComfortSum();

        public ICollection<IDecoration> Decorations => this.decorations;

        public ICollection<IFish> Fish => this.fishes;

        public void AddDecoration(IDecoration decoration)
        {
            this.Decorations.Add(decoration);   
        }

        public void AddFish(IFish fish)
        {
            if (this.Capacity <= this.Fish.Count)
            {
                throw new InvalidOperationException("Not enough capacity.");
            }
            this.Fish.Add(fish);
        }

        public void Feed()
        {
            foreach (var f in this.Fish)
            {
                f.Eat();
            }
        }

        public string GetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{this.Name} ({this.GetType().Name}):");
            sb.Append("Fish: ");
            sb.AppendLine(this.Fish.Count == 0 ? "none" : string.Join(", ", this.Fish.Select(x => x.Name)));
            sb.AppendLine($"Decorations: {this.Decorations.Count}");
            sb.AppendLine($"Comfort: {this.Comfort}");
            return sb.ToString().TrimEnd();
        }

        public bool RemoveFish(IFish fish)
        {
            IFish f = this.Fish.FirstOrDefault(x => x.Name == fish.Name);
            if (f == null) return false;
            else
            {
                this.Fish.Remove(f);
                return true;
            }
        }

        private int ComfortSum()
        {
            int sum = 0;
            foreach (var d in this.Decorations)
            {
                sum += d.Comfort;
            }
            return sum;
        }
    }
}
