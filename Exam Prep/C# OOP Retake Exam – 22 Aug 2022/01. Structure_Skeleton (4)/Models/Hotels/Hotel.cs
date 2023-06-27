using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories;
using BookingApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BookingApp.Models.Hotels
{
    public class Hotel : IHotel
    {
        private string fullName;
        private int categogry;
        private IRepository<IRoom> roomRepository;
        private IRepository<IBooking> bookingRepository;

        public Hotel(string fullName, int category)
        {
            roomRepository = new RoomRepository();
            bookingRepository = new BookingRepository();
            this.FullName = fullName;
            this.Category = category;   
        }

        public string FullName
        {
            get => this.fullName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Hotel name cannot be null or empty!");
                }
                this.fullName = value;
            }
        }

        public int Category
        {
            get => this.categogry;
            private set
            {
                if (value < 1 || value > 5)
                {
                    throw new ArgumentException("Category should be between 1 and 5 stars!");
                }
                this.categogry = value;
            }
        }

        public double Turnover => this.TurnoverSum();

        public IRepository<IRoom> Rooms => this.roomRepository;

        public IRepository<IBooking> Bookings => this.bookingRepository;

        private double TurnoverSum()
        {
            double sum = 0;

            foreach (var item in this.bookingRepository.All())
            {
                sum += item.ResidenceDuration * item.Room.PricePerNight;
            }
            return Math.Round(sum, 2);
        }
    }
}
