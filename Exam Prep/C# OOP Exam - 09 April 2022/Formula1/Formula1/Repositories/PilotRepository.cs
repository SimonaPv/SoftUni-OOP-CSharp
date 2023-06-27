using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formula1.Repositories
{
    public class PilotRepository : IRepository<IPilot>
    {
        private readonly List<IPilot> pilots;

        public PilotRepository()
        {
            pilots = new List<IPilot>();
        }

        public IReadOnlyCollection<IPilot> Models => this.pilots;

        public void Add(IPilot pilot)
        {
            pilots.Add(pilot);
        }

        public IPilot FindByName(string name)
        {
            IPilot pilot = pilots.FirstOrDefault(x => x.FullName == name);
            if (pilot == null) return null;
            return pilot;
        }

        public bool Remove(IPilot pilot)
        {
            int count = pilots.Count;
            pilots.Remove(pilot);
            if (count == pilots.Count) return false;
            return true;
        }
    }
}
