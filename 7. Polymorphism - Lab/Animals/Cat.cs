using System;
using System.Collections.Generic;
using System.Text;

namespace Animals
{
    public class Cat : Animal
    {
        public Cat(string name, string favouriteFood) : base(name, favouriteFood)
        { }

        public override string Name { get; set; }
        public override string FavouriteFood { get; set; }

        public override string ExplainSelf()
        {
            return $"I am {Name} and my favourite food is {FavouriteFood}{Environment.NewLine}" +
            "MEEOW";
        }
    }
}
