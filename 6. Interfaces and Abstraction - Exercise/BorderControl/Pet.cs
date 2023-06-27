using System;
using System.Collections.Generic;
using System.Text;

namespace BorderControl
{
    public class Pet : IIdable
    {
        public Pet(string name, string bDay)
        {
            this.Name = name;
            this.BDay = bDay;
        }

        public string Name { get; set; }
        public string BDay { get; set; }

        public bool IsFake(string check)
        {
            if (check.Length <= BDay.Length)
            {
                string year = BDay.Substring(BDay.Length - 4);
                if (year == check) { return true; }
                return false;
            }
            return false;
        }
    }
}
