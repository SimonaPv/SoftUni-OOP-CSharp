using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gyms.Tests
{
    public class GymsTests
    {
        private const string FULL_NAME = "MoniBonboni";
        private const string GYM_NAME = "LuchosGym";
        private const int CAPACITY = 5;

        private Athlete athlete;
        private Gym gym;

        [SetUp]
        public void SetUp()
        {
            athlete = new Athlete(FULL_NAME);
            gym = new Gym(GYM_NAME, CAPACITY);
        }

        [Test]
        public void Constructor_AthleteTest()
        {
            Assert.AreEqual(FULL_NAME, athlete.FullName);
            Assert.AreEqual(false, athlete.IsInjured);
        }

        [Test]
        public void Constructor_GymTest()
        {
            Assert.AreEqual(GYM_NAME, gym.Name);
            Assert.AreEqual(CAPACITY, gym.Capacity);
            Assert.AreEqual(0, gym.Count);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Name_ExceptTest(string n)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                gym = new Gym(n, CAPACITY);
            });
        }

        [Test]
        public void Capacity_ExceptTest()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                gym = new Gym(GYM_NAME, -1);
            });
        }

        [Test]
        public void AddAthlete_HappyTest()
        {
            gym.AddAthlete(athlete);
            Assert.AreEqual(1, gym.Count);
        }

        [Test]
        public void AddAthlete_ExceptTest()
        {
            gym = new Gym(GYM_NAME, 0);
            Assert.Throws<InvalidOperationException>(() =>
            {
                gym.AddAthlete(athlete);
            });
        }

        [Test]
        public void RemoveAthlete_HappyTest()
        {
            gym.AddAthlete(athlete);
            gym.RemoveAthlete(FULL_NAME);
            Assert.AreEqual(0, gym.Count);
        }

        [Test]
        public void RemoveAthlete_ExceptTest()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                gym.RemoveAthlete(null);
            });
        }

        [Test]
        public void InjureAthlete_HappyBoolTest()
        {
            gym.AddAthlete(athlete);
            gym.InjureAthlete(FULL_NAME);
            Assert.AreEqual(true, athlete.IsInjured);
        }

        [Test]
        public void InjureAthlete_HappyReturnTest()
        {
            List<Athlete> list = new List<Athlete>();
            list.Add(athlete);

            Athlete a = list.FirstOrDefault(x => x.FullName == FULL_NAME);
            a.IsInjured = true;

            gym.AddAthlete(athlete);

            Assert.AreEqual(a, gym.InjureAthlete(FULL_NAME));
        }

        [Test]
        public void InjureAthlete_ExcepTest()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                gym.InjureAthlete(null);
            });
        }

        [Test]
        public void Report_Test()
        {
            string exp = $"Active athletes at {GYM_NAME}: {FULL_NAME}";
            gym.AddAthlete(athlete);
            string result = gym.Report();
            Assert.AreEqual(exp, result);   
        }
    }   
}
