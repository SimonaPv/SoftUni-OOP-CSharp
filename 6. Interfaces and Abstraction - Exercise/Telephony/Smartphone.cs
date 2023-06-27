using System;
using System.Collections.Generic;
using System.Text;

namespace Telephony
{
    public class Smartphone : ISmartphone
    {
        public string Number { get; set; }
        public string Site { get; set; }

        public void Browsing()
        {
            Console.WriteLine($"Browsing: {this.Site}!");
        }

        public void Calling()
        {
            Console.WriteLine($"Calling... {this.Number}");
        }
    }
}
