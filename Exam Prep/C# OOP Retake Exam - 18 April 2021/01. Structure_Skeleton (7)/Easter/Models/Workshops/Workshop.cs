using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes;
using Easter.Models.Dyes.Contracts;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easter.Models.Workshops
{
    public class Workshop : IWorkshop
    {
        public void Color(IEgg egg, IBunny bunny)
        {
            while (!egg.IsDone() && bunny.Energy > 0 && bunny.Dyes.Any(x => !x.IsFinished()))
            {
                IDye dye = bunny.Dyes.FirstOrDefault(x => !x.IsFinished());
                dye.Use();
                egg.GetColored();
                bunny.Work();
            }
        }
    }
}
