using BookingApp.Core.Contracts;
using BookingApp.Models.Bookings;
using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Hotels;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Rooms;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories;
using BookingApp.Repositories.Contracts;
using BookingApp.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookingApp.Core
{
    public class Controller : IController
    {
        private HotelRepository hotelRepository;

        public Controller()
        {
            hotelRepository = new HotelRepository();
        }

        public string AddHotel(string hotelName, int category)
        {
            IHotel hotel = hotelRepository.All().FirstOrDefault(x => x.FullName == hotelName);
            if (hotel != null)
            {
                return $"Hotel {hotelName} is already registered in our platform.";
            }

            hotel = new Hotel(hotelName, category);
            hotelRepository.AddNew(hotel);
            return $"{category} stars hotel {hotelName} is registered in our platform and expects room availability to be uploaded.";
        }

        public string BookAvailableRoom(int adults, int children, int duration, int category)
        {
            if (!hotelRepository.All().Any(x => x.Category == category))
            {
                return $"{category} star hotel is not available in our platform.";
            }

            var list = new Dictionary<IRoom, string>();

            foreach (var hotel in hotelRepository.All()
                .Where(x => x.Category == category)
                .OrderBy(x => x.FullName))
            {
                foreach (var r in hotel.Rooms.All())
                {
                    if (r.PricePerNight > 0)
                    {
                        list.Add(r, hotel.FullName);
                    }
                }
            }

            Dictionary<IRoom, string> ascendingList = list
                .OrderBy(x => x.Key.BedCapacity)
                .ToDictionary(x => x.Key, y => y.Value);

            IRoom room = null;
            string hotelName = string.Empty;
            int people = adults + children;
            
            foreach (var r in ascendingList)
            {
                if (r.Key.BedCapacity >= people)
                {
                    room = r.Key;
                    hotelName = r.Value;
                    break;
                }
            }

            if (room == null)
            {
                return "We cannot offer appropriate room for your request.";
            }

            IHotel selectedHotel = hotelRepository.Select(hotelName);
            int bookingNumber = selectedHotel.Bookings.All().Count + 1;
            Booking booking = new Booking(room, duration, adults, children, bookingNumber);

            selectedHotel.Bookings.AddNew(booking);
            
            return $"Booking number {bookingNumber} for {hotelName} hotel is successful!";
        }

        public string HotelReport(string hotelName)
        {
            IHotel hotel = hotelRepository.All().FirstOrDefault(x => x.FullName == hotelName);
            if (hotel == null)
            {
                return $"Profile {hotelName} doesn’t exist!";
            }

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Hotel name: {hotelName}");
            sb.AppendLine($"--{hotel.Category} star hotel");
            sb.AppendLine($"--Turnover: {hotel.Turnover:f2} $");
            sb.AppendLine("--Bookings:");
            sb.AppendLine();

            if (hotel.Bookings.All().Count == 0)
            {
                sb.AppendLine("none");
            }

            foreach (var booking in hotel.Bookings.All())
            {
                sb.AppendLine(booking.BookingSummary());
                sb.AppendLine();
            }

            return sb.ToString().TrimEnd();
        }

        public string SetRoomPrices(string hotelName, string roomTypeName, double price)
        {
            IHotel hotel = hotelRepository.All().FirstOrDefault(x => x.FullName == hotelName);
            if (hotel == null)
            {
                return $"Profile {hotelName} doesn’t exist!";
            }

            IRoom room = hotel.Rooms.All().FirstOrDefault(x => x.GetType().Name == roomTypeName);
            if (room == null)
            {
                return "Room type is not created yet!";
            }

            if (room.PricePerNight == 0)
            {
                room.SetPrice(price);
                return $"Price of {roomTypeName} room type in {hotelName} hotel is set!";
            }

            return "Price is already set!";
        }

        public string UploadRoomTypes(string hotelName, string roomTypeName)
        {
            IHotel hotel = hotelRepository.All().FirstOrDefault(x => x.FullName == hotelName);
            if (hotel == null)
            {
                return $"Profile {hotelName} doesn’t exist!";
            }

            IRoom r = hotel.Rooms.All().FirstOrDefault(x => x.GetType().Name == roomTypeName);
            if (r != null)
            {
                return "Room type is already created!";
            }

            IRoom room;
            if (roomTypeName == "Studio")
            {
                room = new Studio();
            }
            else if (roomTypeName == "DoubleBed")
            {
                room = new DoubleBed();
            }
            else if (roomTypeName == "Apartment")
            {
                room = new Apartment();
            }
            else
            {
                throw new ArgumentException("Incorrect room type!");
            }

            hotel.Rooms.AddNew(room);
            return $"Successfully added {roomTypeName} room type in {hotelName} hotel!";
        }
    }
}
