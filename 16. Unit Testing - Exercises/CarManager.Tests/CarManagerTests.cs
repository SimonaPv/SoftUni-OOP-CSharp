namespace CarManager.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CarManagerTests
    {
        private Car car;
        private const string MAKE = "BMW";
        private const string MODEL = "BMW";
        private const double FUEL_CONSUMPTION = 12.5;
        private const double FUEL_CAPACITY = 13.1;

        [SetUp]
        public void SetUp()
        {
            car = new Car(MAKE, MODEL, FUEL_CONSUMPTION, FUEL_CAPACITY);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.AreEqual(MAKE, car.Make, "Error - MAKE");
            Assert.AreEqual(MODEL, car.Model, "Error - MODEL");
            Assert.AreEqual(FUEL_CAPACITY, car.FuelCapacity, "Error - FUEL_CAPACITY");
            Assert.AreEqual(FUEL_CONSUMPTION, car.FuelConsumption, "Error - FUEL_CONSUMPTION");
            Assert.AreEqual(0, car.FuelAmount, "Error - FUEL_AMOUNT");
        }

        [TestCase("")]
        [TestCase(null)]
        public void MakeExceptTest(string make)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                car = new Car(make, MODEL, FUEL_CONSUMPTION, FUEL_CAPACITY);
            });
        }


        [TestCase("")]
        [TestCase(null)]
        public void ModelExceptTest(string model)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                car = new Car(MAKE, model, FUEL_CONSUMPTION, FUEL_CAPACITY);
            });
        }


        [TestCase(0.0)]
        [TestCase(-1.1)]
        public void FuelConsumptionExceptTest(double fuelConsumption)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                car = new Car(MAKE, MODEL, fuelConsumption, FUEL_CAPACITY);
            });
        }

        [TestCase(0.0)]
        [TestCase(-1.1)]
        public void FuelCapacityExceptTest(double fuelCapacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                car = new Car(MAKE, MODEL, FUEL_CONSUMPTION, fuelCapacity);
            });
        }

        [Test]
        public void RefuelHappyTest()
        {
            double exp = 7.5 + car.FuelAmount;
            car.Refuel(7.5);
            Assert.AreEqual(exp, car.FuelAmount);
        }

        [TestCase(0)]
        [TestCase(-1.1)]
        public void RefuelExceptZeroTest(double fuelRef)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                car.Refuel(fuelRef);
            });
        }

        [Test]
        public void RefuelEqualTest()
        {
            car.Refuel(13.2);
            Assert.AreEqual(13.1, car.FuelAmount);
        }

        [Test]
        public void DriveHappyTest()
        {
            double fuelNeeded = (20.0 / 100) * FUEL_CONSUMPTION;
            car.Refuel(10);
            double exp = car.FuelAmount - fuelNeeded;
            car.Drive(20);

            Assert.AreEqual(exp, car.FuelAmount);
        }

        [Test]
        public void DriveExcepTest()
        {
            car.Refuel(1);
            Assert.Throws<InvalidOperationException>(() =>
            {
                car.Drive(20);
            });
        }

        //[Test]
        //public void FuelAmountExceptTest()
        //{
        //    Assert.Throws<ArgumentException>(() =>
        //    {
        //
        //    });
        //}
    }
}