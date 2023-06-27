using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingSpree
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Person> pers = new List<Person>();
            List<Product> products = new List<Product>();

            try
            {
                string[] arr = Console.ReadLine()
                .Split(";");
                for (int i = 0; i < arr.Length; i++)
                {
                    string[] arr2 = arr[i].Split("=", StringSplitOptions.RemoveEmptyEntries);

                    Person person = new Person(arr2[0], int.Parse(arr2[1]));
                    pers.Add(person);
                }

                string[] arr3 = Console.ReadLine()
                    .Split(";");
                for (int i = 0; i < arr.Length; i++)
                {
                    string[] arr2 = arr3[i].Split("=", StringSplitOptions.RemoveEmptyEntries);
                    Product prod = new Product(arr2[0], int.Parse(arr2[1]));

                    products.Add(prod);
                }

                string input = Console.ReadLine();
                while (input != "END")
                {
                    string[] purchace = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    Person per = pers.FirstOrDefault(x => x.Name == purchace[0]);
                    Product prod = products.FirstOrDefault(x => x.Name == purchace[1]);

                    if (per.Money - prod.Price >= 0)
                    {
                        per.BagOfProd.Add(prod);
                        per.Money -= prod.Price;

                        Console.WriteLine($"{per.Name} bought {prod.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"{per.Name} can't afford {prod.Name}");
                    }

                    input = Console.ReadLine();
                }

                foreach (var p in pers)
                {
                    if (p.BagOfProd.Count == 0)
                    {
                        Console.WriteLine($"{p.Name} - Nothing bought");
                    }
                    else
                    {
                        Console.WriteLine(p.ToString().TrimEnd());
                    }
                }
            }
            catch (ArgumentException ae)
            {

                Console.WriteLine(ae.Message); 
            }
        }
    }
}
