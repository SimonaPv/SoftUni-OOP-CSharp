using BookingApp.Models.Bookings.Contracts;
using BookingApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookingApp.Repositories
{
    public class BookingRepository : IRepository<IBooking>
    {
        private List<IBooking> bookingsList;

        public BookingRepository()
        {
            bookingsList = new List<IBooking>();
        }

        public void AddNew(IBooking model)
        {
            bookingsList.Add(model);
        }

        public IReadOnlyCollection<IBooking> All()
        {
            return bookingsList.AsReadOnly();
        }

        public IBooking Select(string criteria)
        {
            IBooking b = bookingsList.FirstOrDefault(x => x.GetType().Name == criteria);
            return b;
        }
    }
}
