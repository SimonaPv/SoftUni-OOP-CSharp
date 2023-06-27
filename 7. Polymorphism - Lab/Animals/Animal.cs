using System;
using System.Collections.Generic;
using System.Text;

namespace Animals
{
    public abstract class Animal
    {
        public Animal(string name, string favouriteFood)
        {
            Name = name;
            FavouriteFood = favouriteFood;
        }

        public abstract string Name { get; set; }
        public abstract string FavouriteFood { get; set; }
        public abstract string ExplainSelf();
    }
}
