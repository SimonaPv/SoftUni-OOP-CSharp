namespace Robots.Tests
{
    using NUnit.Framework;
    using System;

    public class RobotsTests
    {
        private const string NAME = "MoniBonboni";
        private const int MAXIMUM_BATTERY = 100;

        private const int CAPACITY = 2;

        private Robot robot;
        private RobotManager robotManager;

        [SetUp]
        public void SetUp()
        {
            robot = new Robot(NAME, MAXIMUM_BATTERY);
            robotManager = new RobotManager(CAPACITY);
        }

        [Test]
        public void Constructor_RobotTest()
        {
            Assert.AreEqual(NAME, robot.Name);
            Assert.AreEqual(MAXIMUM_BATTERY, robot.Battery);
            Assert.AreEqual(MAXIMUM_BATTERY, robot.MaximumBattery);
        }

        [Test]
        public void Constructor_RobotManagerTest()
        {
            Assert.AreEqual(CAPACITY, robotManager.Capacity);
            Assert.AreEqual(0, robotManager.Count);
        }

        [Test]
        public void Capacity_ExcepTest()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                robotManager = new RobotManager(-1);
            });
        }

        [Test]
        public void Add_HappyTest()
        {
            robotManager.Add(robot);
            Assert.AreEqual(1, robotManager.Count);
        }

        [Test]
        public void Add_AlrdExistsExcepTest()
        {
            robotManager.Add(robot);
            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Add(robot);
            });
        }


        [Test]
        public void Add_NotEnoughSpaceTest()
        {
            robotManager = new RobotManager(1);
            robotManager.Add(robot);
            Assert.Throws<InvalidOperationException>(() =>
            {
                Robot r = new Robot("test", 79);
                robotManager.Add(r);
            });
        }

        [Test]
        public void Remove_HappyTest()
        {
            robotManager.Add(robot);
            robotManager.Remove(NAME);
            Assert.AreEqual(0, robotManager.Count);
        }

        [Test]
        public void Remove_ExcepTest()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Remove(null);
            });
        }

        [Test]
        public void Work_HappyTest()
        {
            Robot r = new Robot("test", 100);
            r.Battery -= 20;
            robotManager.Add(robot);
            robotManager.Work(NAME, "job", 20);

            Assert.AreEqual(r.Battery, robot.Battery);
        }

        [Test]
        public void Work_NullTest()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Work("isadufg", "job", 20);
            });
        }

        [Test]
        public void Work_ExcepTest()
        {
            robot = new Robot(NAME, 30);
            robotManager.Add(robot);
            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Work(NAME, "job", 40);
            });
        }

        [Test]
        public void Charge_HappyTest()
        {
            Robot r = new Robot(NAME, 100);

            Robot robot = new Robot(NAME, 100);
            robotManager.Add(robot);
            robotManager.Work(NAME, "job", 20);

            robotManager.Charge(NAME);

            Assert.AreEqual(r.Battery, robot.Battery);
        }

        [Test]
        public void Charge_ExcepTest()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Charge(NAME);
            });
        }
    }
}
