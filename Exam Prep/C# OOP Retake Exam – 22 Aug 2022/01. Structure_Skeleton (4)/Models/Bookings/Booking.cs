using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Rooms;
using BookingApp.Models.Rooms.Contracts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BookingApp.Models.Bookings
{
    public class Booking : IBooking
    {
        private IRoom room;
        private int residenceDuration;
        private int adultsCount;
        private int childrenCount;
        private int bookingNumber;

        public Booking(IRoom room, int residenceDuration, int adultsCount, int childrenCount, int bookingNumber)
        {
            this.room = room;
            this.ResidenceDuration = residenceDuration;
            this.AdultsCount = adultsCount;
            this.ChildrenCount = childrenCount;
            this.bookingNumber = bookingNumber;
        }

        public IRoom Room => this.room;

        public int ResidenceDuration
        {
            get => this.residenceDuration;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Duration cannot be negative or zero!");
                }
                this.residenceDuration = value;
            }
        }

        public int AdultsCount
        {
            get => this.adultsCount;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Adults count cannot be negative or zero!");
                }
                this.adultsCount = value;
            }
        }

        public int ChildrenCount
        {
            get => this.childrenCount;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Children count cannot be negative!");
                }
                this.childrenCount = value;
            }
        }

        public int BookingNumber => this.bookingNumber;

        public string BookingSummary()
        {
            return $"Booking number: {this.bookingNumber}{Environment.NewLine}" +
                $"Room type: {this.room.GetType().Name}{Environment.NewLine}" +
                $"Adults: {this.AdultsCount} Children: {this.ChildrenCount}{Environment.NewLine}" +
                $"Total amount paid: {this.TotalPaid():f2} $";
        }

        private double TotalPaid()
        {
            return Math.Round(ResidenceDuration * room.PricePerNight, 2);
        }
    }
}
