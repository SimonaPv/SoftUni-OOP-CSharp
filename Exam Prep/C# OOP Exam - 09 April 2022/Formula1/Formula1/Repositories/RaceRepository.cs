using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formula1.Repositories
{
    public class RaceRepository : IRepository<IRace>
    {
        private readonly List<IRace> races;

        public RaceRepository()
        {
            races = new List<IRace>();
        }

        public IReadOnlyCollection<IRace> Models => this.races;

        public void Add(IRace race)
        {
            races.Add(race);
        }

        public IRace FindByName(string raceName)
        {
            IRace car = races.FirstOrDefault(x => x.RaceName == raceName);
            if (car == null) return null;
            return car;
        }

        public bool Remove(IRace race)
        {
            int count = races.Count;
            races.Remove(race);
            if (count == races.Count) return false;
            return true;
        }
    }
}
