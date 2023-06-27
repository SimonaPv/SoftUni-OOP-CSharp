using System;
using System.Collections.Generic;
using System.Text;

namespace Telephony
{
    public class StationaryPhone : IStationaryPhone
    {
        public string Number { get; set; }

        public void Calling()
        {
            Console.WriteLine($"Dialing... {this.Number}");
        }
    }
}
