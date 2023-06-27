using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodShortage
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<IBuyer> list = new List<IBuyer>();

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] arr = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (arr.Length == 4)
                {
                    Citizen citizen = new Citizen(arr[0], int.Parse(arr[1]), arr[2], arr[3]);
                    list.Add(citizen);
                }
                else if (arr.Length == 3)
                {
                    Rebel rebel = new Rebel(arr[0], int.Parse(arr[1]), arr[2]);
                    list.Add(rebel);
                }
            }

            string input = Console.ReadLine();

            while (input != "End")
            {
                IBuyer buyer = list.FirstOrDefault(x => x.Name == input);
                if (buyer == null)
                {
                    input = Console.ReadLine();
                }
                else
                {
                    buyer.BuyFood();
                    input = Console.ReadLine();
                }
            }

            int food = 0;
            foreach (var it in list)
            {
                food += it.Food;
            }

            Console.WriteLine(food);
        }
    }
}
