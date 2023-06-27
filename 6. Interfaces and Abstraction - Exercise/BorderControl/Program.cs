using System;
using System.Collections.Generic;

namespace BorderControl
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<IIdable> list = new List<IIdable>();   

            string input = Console.ReadLine();

            while (input != "End")
            {
                string[] arr = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (arr[0] == "Citizen")
                {
                    IIdable sth = new Citizen(arr[1], int.Parse(arr[2]), arr[3], arr[4]);
                    list.Add(sth);
                }
                else if (arr[0] == "Pet")
                {
                    IIdable sth = new Pet(arr[1], arr[2]);
                    list.Add(sth);
                }

                input = Console.ReadLine();
            }

            string check = Console.ReadLine();

            foreach (var it in list)
            {
                if (it.IsFake(check))
                {
                    Console.WriteLine(it.BDay);
                }
            }
        }
    }
}
