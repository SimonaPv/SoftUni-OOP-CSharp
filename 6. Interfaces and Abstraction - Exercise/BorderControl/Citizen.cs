using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace BorderControl
{
    public class Citizen : IIdable
    {
        public Citizen(string name, int age, string id, string bDay)
        {
            this.Name = name;
            this.Age = age;
            this.Id = id;
            this.BDay = bDay;
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public string Id { get; set; }
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
