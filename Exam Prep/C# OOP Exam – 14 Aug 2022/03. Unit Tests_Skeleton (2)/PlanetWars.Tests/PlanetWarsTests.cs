using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace PlanetWars.Tests
{
    public class Tests
    {
        [TestFixture]
        public class PlanetWarsTests
        {
            private const string NAME = "gun";
            private const double PRICE = 25.5;
            private const int DESTRUCTION_LEVEL = 2;

            private const string NAME_PLANET = "Earth";
            private const double BUDGET = 150.5;

            private Weapon weapon;
            private Planet planet;

            [SetUp]
            public void SetUp()
            {
                weapon = new Weapon(NAME, PRICE, DESTRUCTION_LEVEL);
                planet = new Planet(NAME_PLANET, BUDGET);
            }

            [TestCase(9)]
            public void IsNuclearFalseTest(int d)
            {
                weapon = new Weapon("blabla", 20.2, d);
                bool exp = false;
                bool test = weapon.IsNuclear;
                Assert.AreEqual(exp, test);
            }

            [TestCase(10)]
            [TestCase(11)]
            public void IsNuclearTrueTest(int d)
            {
                weapon = new Weapon("blabla", 20.2, d);
                bool exp = true;
                bool test = weapon.IsNuclear;
                Assert.AreEqual(exp, test);
            }

            [Test]
            public void DestructOpponentMessageTest()
            {
                planet.AddWeapon(weapon);
                Planet opponent = new Planet("Mars", 250);
                string output = planet.DestructOpponent(opponent);
                Assert.AreEqual($"{opponent.Name} is destructed!", output);
            }

            [Test]
            public void DestructOpponentExcept()
            {
                weapon = new Weapon(NAME, PRICE, 3);
                Weapon weapon2 = new Weapon("d", PRICE, 2);
                Weapon weapon3 = new Weapon("m", PRICE, 4);

                Planet opp = new Planet("Jupiter", 100);
                opp.AddWeapon(weapon);
                opp.AddWeapon(weapon2);
                opp.AddWeapon(weapon3);

                planet.AddWeapon(weapon3);
                planet.AddWeapon(weapon2);

                Assert.Throws<InvalidOperationException>(() =>
                {
                    planet.DestructOpponent(opp);
                });
            }

            [Test]
            public void UpgradeWeaponElseTest()
            {
                int sum = DESTRUCTION_LEVEL + 1;
                planet.AddWeapon(weapon);
                planet.UpgradeWeapon("gun");
                Weapon w = planet.Weapons.FirstOrDefault(x => x.Name == "gun");

                Assert.AreEqual(sum, weapon.DestructionLevel);
            }

            [Test]
            public void UpgradeWeaponIfTest()
            {
                planet.AddWeapon(weapon);
                Assert.Throws<InvalidOperationException>(() =>
                {
                    planet.UpgradeWeapon("d");
                });
            }

            [Test]
            public void RemoveWeaponTets()
            {
                List<Weapon> list = new List<Weapon>();
                weapon = new Weapon(NAME, PRICE, 3);
                Weapon weapon2 = new Weapon("d", PRICE, 2);

                list.Add(weapon);
                list.Add(weapon2);
                planet.AddWeapon(weapon);
                planet.AddWeapon(weapon2);

                list.Remove(weapon2);
                planet.RemoveWeapon("d");

                Assert.AreEqual(list, planet.Weapons);
            }

            [Test]
            public void AddWeaponExceptTest()
            {
                planet.AddWeapon(weapon);
                Assert.Throws<InvalidOperationException>(() =>
                {
                    planet.AddWeapon(weapon);
                });
            }

            [Test]
            public void AddWeaponHappyTest()
            {
                List<Weapon> list = new List<Weapon>();
                list.Add(weapon);
                planet.AddWeapon(weapon);

                Assert.AreEqual(list, planet.Weapons);
            }

            [Test]
            public void SpendFundsExceptTest()
            {
                Assert.Throws<InvalidOperationException>(() =>
                {
                    planet.SpendFunds(151);
                });
            }

            [Test]
            public void SpendFundsTest()
            {
                double sum = BUDGET - 50;
                planet.SpendFunds(50);
                Assert.AreEqual(sum, planet.Budget);
            }

            [Test]
            public void ProfitTest()
            {
                double sum = BUDGET + 5;
                planet.Profit(5);
                Assert.AreEqual(sum, planet.Budget);
            }

            [Test]
            public void MilitaryPowerRatioTest()
            {
                List<Weapon> list = new List<Weapon>(); 
                weapon = new Weapon(NAME, PRICE, 3);
                Weapon weapon2 = new Weapon("d", PRICE, 2);
                Weapon weapon3 = new Weapon("m", PRICE, 4);

                list.Add(weapon);
                list.Add(weapon2);
                list.Add(weapon3);
                planet.AddWeapon(weapon);
                planet.AddWeapon(weapon2);
                planet.AddWeapon(weapon3);

                int sum = list.Sum(w => w.DestructionLevel);

                Assert.AreEqual(sum, planet.MilitaryPowerRatio);

            }

            [Test]
            public void BudgetExceptTest()
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    planet = new Planet(NAME_PLANET, -1);
                });
            }

            [TestCase(null)]
            [TestCase("")]
            public void NameExceptTest(string name)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    planet = new Planet(name, BUDGET);
                });
            }

            [Test]
            public void ConstructorPlanetTest()
            {
                List<Weapon> list = new List<Weapon>(); 
                Assert.AreEqual(NAME_PLANET, planet.Name);
                Assert.AreEqual(BUDGET, planet.Budget);
                Assert.AreEqual(list, planet.Weapons);
            }

            [Test]
            public void ConstructorWeaponTest()
            {
                Assert.AreEqual(NAME, weapon.Name);
                Assert.AreEqual(PRICE, weapon.Price);
                Assert.AreEqual(PRICE, weapon.Price);
            }

            [Test]
            public void PriceExceptTest()
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    weapon = new Weapon(NAME, -1, DESTRUCTION_LEVEL);
                });
            }

            [Test]
            public void IncreaseDestructionLevelTest()
            {
                weapon.IncreaseDestructionLevel();
                Assert.AreEqual(3, weapon.DestructionLevel);
            }
        }
    }
}
