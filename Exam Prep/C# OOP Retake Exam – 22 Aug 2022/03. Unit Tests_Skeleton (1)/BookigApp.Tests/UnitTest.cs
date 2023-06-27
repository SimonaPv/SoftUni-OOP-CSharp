using FrontDeskApp;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookigApp.Tests
{
    public class Tests
    {
        private const int BED_CAPACITY = 2;
        private const double PRICE_PER_NIGHT = 78.5;
        private const int RESIDENCE_DURATION = 1;
        private const int BOOKING_NUMBER = 0;
        private const string FULL_NAME = "Simona";
        private const int CATEGORY = 3;
        private Room room;
        private Booking booking;
        private Hotel hotel;

        [SetUp]
        public void Setup()
        {
            room = new Room(BED_CAPACITY, PRICE_PER_NIGHT);
            booking = new Booking(BOOKING_NUMBER, room, RESIDENCE_DURATION);
            hotel = new Hotel(FULL_NAME, CATEGORY);
        }

        [Test]
        public void BookRoomBedCapacityTest()
        {
            Room r = new Room(3, 20);
            hotel.AddRoom(r);
            hotel.BookRoom(1, 1, 4, 200.0);
            Assert.AreEqual(1, hotel.Bookings.Count);
        }

        [Test]
        public void Hotel_BookRoom_DoesntBookIfBedCapacityIsLowerThanBedsNeeded()
        {
            hotel.AddRoom(room);
            hotel.BookRoom(2, 1, 4, 200.0);
            Assert.AreEqual(hotel.Bookings.Count, 0);
        }

        [Test]
        public void Hotel_BookRoom_ProperlyGeneratesTurnover()
        {
            int residenceDuration = 2;
            double pricePerNight = room.PricePerNight;
            double expectedTurnover = residenceDuration * pricePerNight;

            hotel.AddRoom(room);
            hotel.BookRoom(1, 1, residenceDuration, 200.0);
            Assert.AreEqual(expectedTurnover, hotel.Turnover);
        }

        [TestCase(0)]
        public void BookRoomResidenceDurationTest(int residenceDuration)
        {
            int a = 3;
            int ch = 3;
            double b = 10;
            Assert.Throws<ArgumentException>(() =>
            {
                hotel.BookRoom(a, ch, residenceDuration, b);
            });
        }

        [TestCase(-1)]
        public void BookRoomKidsTest(int children)
        {
            int a = 3;
            int r = 3;
            double b = 10;
            Assert.Throws<ArgumentException>(() =>
            {
                hotel.BookRoom(a, children, r, b);
            });
        }


        [TestCase(0)]
        [TestCase(-1)]
        public void BookRoomAdultsTest(int adults)
        {
            int ch = 2;
            int r = 3;
            double b = 10;
            Assert.Throws<ArgumentException>(() =>
            {
                hotel.BookRoom(adults, ch, r, b);
            });
        }

        [Test]
        public void AddRoomTest()
        {
            List<Room> list = new List<Room>();
            list.Add(room);
            hotel.AddRoom(room);
            Assert.AreEqual(list, hotel.Rooms);
        }

        [TestCase(null)]
        [TestCase(" ")]
        public void FullNameExceptTest(string name)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                hotel = new Hotel(name, CATEGORY);
            });
        }

        [TestCase(0)]
        [TestCase(6)]
        public void CategoryExceptTest(int cat)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                hotel = new Hotel(FULL_NAME, cat);
            });
        }

        [Test]
        public void ConstructorHotelTest()
        {
            List<Room> rooml = new List<Room>();
            List<Booking> bookingl = new List<Booking>();

            Assert.AreEqual(FULL_NAME, hotel.FullName);
            Assert.AreEqual(CATEGORY, hotel.Category);
            Assert.AreEqual(rooml, hotel.Rooms);
            Assert.AreEqual(bookingl, hotel.Bookings);
        }

        [Test]
        public void ConstructorRoomTest()
        {
            Assert.AreEqual(BED_CAPACITY, room.BedCapacity);
            Assert.AreEqual(PRICE_PER_NIGHT, room.PricePerNight);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void BedCapacityExceptTest(int bedCapacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                room = new Room(bedCapacity, PRICE_PER_NIGHT);
            });
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void PriceExceptTest(int pricePerNight)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                room = new Room(BED_CAPACITY, pricePerNight);
            });
        }

        [Test]
        public void ConstructorBookingTest()
        {
            Assert.AreEqual(BOOKING_NUMBER, booking.BookingNumber);
            Assert.AreEqual(room, booking.Room);
            Assert.AreEqual(RESIDENCE_DURATION, booking.ResidenceDuration);
        }
    }
}