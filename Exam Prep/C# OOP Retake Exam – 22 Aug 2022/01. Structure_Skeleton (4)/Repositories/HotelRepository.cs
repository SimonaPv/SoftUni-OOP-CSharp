using BookingApp.Models.Hotels.Contacts;
using BookingApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookingApp.Repositories
{
    public class HotelRepository : IRepository<IHotel>
    {
        private List<IHotel> hotelList;

        public HotelRepository()
        {
            hotelList = new List<IHotel>();
        }

        public void AddNew(IHotel model)
        {
            hotelList.Add(model);
        }

        public IReadOnlyCollection<IHotel> All()
        {
            return hotelList.AsReadOnly();
        }

        public IHotel Select(string criteria)
        {
            IHotel h = hotelList.FirstOrDefault(x => x.FullName == criteria);
            return h;
        }
    }
}
