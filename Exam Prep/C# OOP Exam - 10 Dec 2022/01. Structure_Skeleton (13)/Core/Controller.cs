using ChristmasPastryShop.Core.Contracts;
using ChristmasPastryShop.Models.Booths;
using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ChristmasPastryShop.Core
{
    public class Controller : IController
    {
        private BoothRepository booths;

        public Controller()
        {
            booths = new BoothRepository();
        }

        public string AddBooth(int capacity)
        {
            IBooth b = new Booth(booths.Models.Count + 1, capacity);
            booths.AddModel(b);
            return string.Format(OutputMessages.NewBoothAdded, b.BoothId, b.Capacity);
        }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            if (cocktailTypeName != "MulledWine" && cocktailTypeName != "Hibernation")
            {
                return $"Cocktail type {cocktailTypeName} is not supported in our application!".TrimEnd();
            }

            if (size != "Small" && size != "Middle" && size != "Large")
            {
                return $"{size} is not recognized as valid cocktail size!".TrimEnd();
            }

            IBooth booth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            if (booth == null) return string.Format(OutputMessages.BoothNotFound, boothId);

            if (booth.CocktailMenu.Models.Any(x => x.Name == cocktailName && x.Size == size))
            {
                return $"{size} {cocktailName} is already added in the pastry shop!".TrimEnd();
            }

            ICocktail c;
            if (cocktailTypeName == "MulledWine")
            {
                c = new MulledWine(cocktailName, size);
            }
            else
            {
                c = new Hibernation(cocktailName, size);
            }

            booth.CocktailMenu.AddModel(c);
            return string.Format(OutputMessages.NewCocktailAdded, size, cocktailName, cocktailTypeName);
        }

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            if (delicacyTypeName != "Gingerbread" && delicacyTypeName != "Stolen")
            {
                return $"Delicacy type {delicacyTypeName} is not supported in our application!".TrimEnd();
            }

            IBooth booth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            if (booth == null) return string.Format(OutputMessages.BoothNotFound, boothId);

            if (booth.DelicacyMenu.Models.Any(x => x.Name == delicacyName))
            {
                return $"{delicacyName} is already added in the pastry shop!".TrimEnd();
            }

            IDelicacy d;
            if (delicacyTypeName == "Gingerbread")
            {
                d = new Gingerbread(delicacyName);
            }
            else
            {
                d = new Stolen(delicacyName);
            }

            booth.DelicacyMenu.AddModel(d);
            return $"{delicacyTypeName} {delicacyName} added to the pastry shop!".TrimEnd();
        }

        public string BoothReport(int boothId)
        {
            IBooth booth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            if (booth == null) return string.Format(OutputMessages.BoothNotFound, boothId);
            return booth.ToString();
        }

        public string LeaveBooth(int boothId)
        {
            IBooth booth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            booth.Charge();
            booth.ChangeStatus();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Bill {booth.Turnover:f2} lv".TrimEnd());
            sb.AppendLine($"Booth {boothId} is now available!".TrimEnd());

            return sb.ToString().TrimEnd();
        }

        public string ReserveBooth(int countOfPeople)
        {
            List<IBooth> b = booths.Models
                .Where(x => x.Capacity >= countOfPeople)
                .OrderBy(x => x.Capacity)
                .ThenByDescending(x => x.BoothId)
                .ToList();

            if (b.Count == 0)
            {
                return $"No available booth for {countOfPeople} people!".TrimEnd();
            }

            IBooth booth = b.First(x => x.IsReserved == false);
            booth.ChangeStatus();
            return $"Booth {booth.BoothId} has been reserved for {countOfPeople} people!".TrimEnd();
        }

        public string TryOrder(int boothId, string order)
        {
            string[] ord = order.Split("/", StringSplitOptions.RemoveEmptyEntries);

            string itemType = ord[0];
            string itemName = ord[1];
            int countOfOrderedPieces = int.Parse(ord[2]);
            string size = string.Empty;

            if (ord.Length == 4)
            {
                size = ord[3];
            }

            if (itemType != "Gingerbread" && itemType != "Stolen" && itemType != "MulledWine" && itemType != "Hibernation")
            {
                return $"{itemType} is not recognized type!".TrimEnd();
            }

            IBooth booth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);

            if (booth == null) return string.Format(OutputMessages.BoothNotFound, boothId);

            if (!booth.CocktailMenu.Models.Any(x => x.Name == itemName && x.Size == size) && !booth.DelicacyMenu.Models.Any(x => x.Name == itemName))
            {
                if (ord.Length == 3)
                {
                    return $"There is no {itemType} {itemName} available!".TrimEnd();
                }
                return $"There is no {size} {itemName} available!".TrimEnd();
            }

            if (itemType == "Gingerbread" || itemType == "Stolen")
            {
                IDelicacy deli = booth.DelicacyMenu.Models.FirstOrDefault(x => x.Name == itemName);
                booth.UpdateCurrentBill(deli.Price * countOfOrderedPieces);
                return $"Booth {boothId} ordered {countOfOrderedPieces} {itemName}!".TrimEnd();
            }
            else
            {
                ICocktail cock = booth.CocktailMenu.Models.FirstOrDefault(x => x.Name == itemName);
                booth.UpdateCurrentBill(cock.Price * countOfOrderedPieces);
                return $"Booth {boothId} ordered {countOfOrderedPieces} {itemName}!".TrimEnd();
            }
        }
    }
}
