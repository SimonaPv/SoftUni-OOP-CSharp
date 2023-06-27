using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace RepairShop.Tests
{
    public class Tests
    {
        public class RepairsShopTests
        {
            private const string CAR_MODEL = "audi";
            private const int NUMBER_OF_ISSUES = 3;
            private const string NAME = "lucho";
            private const int MECHANICS_AVAILABLE = 2;

            private Car car;
            private Garage garage;

            [SetUp]
            public void SetUp()
            {
                car = new Car(CAR_MODEL, NUMBER_OF_ISSUES);
                garage = new Garage(NAME, MECHANICS_AVAILABLE);
            }

            [Test]
            public void ReportTest()
            {
                //garage = new Garage("la", 5);

                Car c1 = new Car(CAR_MODEL, NUMBER_OF_ISSUES);
                Car c4 = new Car("ford", 2);

                List<Car> list = new List<Car>();
                list.Add(c1);
                list.Add(c4);

                garage.AddCar(c1);
                garage.AddCar(c4);

                var names = list.Select(f => f.CarModel).ToList();

                string exp = $"There are {list.Count} which are not fixed: {string.Join(", ", names)}.";
                Assert.AreEqual(exp, garage.Report());
            }

            [Test]
            public void RemoveFixedCarExcepTest()
            {
                garage = new Garage("la", 5);

                Car c1 = new Car(CAR_MODEL, NUMBER_OF_ISSUES);
                Car c4 = new Car("ford", 2);
                garage.AddCar(c1);
                garage.AddCar(c4);

                Assert.Throws<InvalidOperationException>(() =>
                {
                    garage.RemoveFixedCar();
                });
            }

            [Test]
            public void RemoveFixedCarHappyTest()
            {
                garage = new Garage("la", 5);

                Car c1 = new Car(CAR_MODEL, NUMBER_OF_ISSUES);
                Car c2 = new Car("bmw", 0);
                Car c3 = new Car("mercedes", 0);
                Car c4 = new Car("ford", 2);
                garage.AddCar(c1);
                garage.AddCar(c2);
                garage.AddCar(c3);
                garage.AddCar(c4);

                Assert.AreEqual(2, garage.RemoveFixedCar());
            }

            [Test]
            public void FixCarExceptTest()
            {
                Assert.Throws<InvalidOperationException>(() =>
                {
                    garage.FixCar("bmw");
                });
            }

            [Test]
            public void FixCarHappyTest()
            {
                garage.AddCar(car);
                Car test = garage.FixCar(CAR_MODEL);
                Assert.AreEqual(0, test.NumberOfIssues);
            }

            [Test]
            public void AddCarExceptTest()
            {
                garage = new Garage(NAME, 1);
                garage.AddCar(car);
                Assert.Throws<InvalidOperationException>(() =>
                {
                    garage.AddCar(car);
                });
            }

            [Test]
            public void AddCarHappyTest()
            {
                List<Car> list = new List<Car>();
                list.Add(car);
                garage.AddCar(car);
                Assert.AreEqual(list.Count, garage.CarsInGarage);
            }

            [Test]
            public void CarsInGarageTest()
            {
                garage.AddCar(car);
                Assert.AreEqual(1, garage.CarsInGarage);
            }

            [TestCase(0)]
            [TestCase(-1)]
            public void MechanicsAvailableExceptTest(int ma)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    garage = new Garage(NAME, ma);
                });
            }

            [TestCase(null)]
            [TestCase("")]
            public void NameExceptTest(string name)
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    garage = new Garage(name, MECHANICS_AVAILABLE);
                });
            }

            [Test]
            public void ConstructorGarageTest() 
            {
                Assert.AreEqual(NAME, garage.Name);
                Assert.AreEqual(MECHANICS_AVAILABLE, garage.MechanicsAvailable);
            }

            //class car
            [Test]
            public void ConstructorCarTest()
            {
                Assert.AreEqual(CAR_MODEL, car.CarModel);
                Assert.AreEqual(NUMBER_OF_ISSUES, car.NumberOfIssues);
            }

            [Test]
            public void IsFixedTrueTest()
            {
                bool exp = false;
                bool test = car.IsFixed;
                Assert.AreEqual(exp, test);
            }

            [Test]
            public void IsFixedFalseTest()
            {
                car = new Car(CAR_MODEL, 0);
                bool exp = true;
                bool test = car.IsFixed;
                Assert.AreEqual(exp, test);
            }
        }
    }
}