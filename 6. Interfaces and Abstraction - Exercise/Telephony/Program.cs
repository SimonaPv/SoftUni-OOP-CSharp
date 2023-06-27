using System;
using System.Linq;

namespace Telephony
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Smartphone sp = new Smartphone();
            StationaryPhone stp = new StationaryPhone();

            string[] phoneNums = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string[] sites = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            foreach (var num in phoneNums)
            {
                if (!num.All(x => char.IsDigit(x)))
                {
                    Console.WriteLine("Invalid number!");
                    continue;
                }

                if (num.Length == 7)
                {
                    stp.Number = num;
                    stp.Calling();
                }
                else if (num.Length == 10)
                {
                    sp.Number = num;
                    sp.Calling();
                }
            }

            foreach (var url in sites)
            {
                if (!url.All(x => !char.IsDigit(x)))
                {
                    Console.WriteLine("Invalid URL!");
                }
                else
                {
                    sp.Site = url;
                    sp.Browsing();
                }
            }
        }
    }
}
