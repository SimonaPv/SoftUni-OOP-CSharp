using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace SmartphoneShop.Tests
{
    [TestFixture]
    public class SmartphoneShopTests
    {
        private const string MODEL_NAME = "iphone";
        private const int MAX_BATTERY_CHARGE = 89;
        private const int CURRENT_BATTERY_CHARGE = MAX_BATTERY_CHARGE;

        private const int CAPACITY = 10;

        private Smartphone smartphone;
        private Shop shop;

        [SetUp]
        public void SetUp()
        {
            smartphone = new Smartphone(MODEL_NAME, MAX_BATTERY_CHARGE);
            shop = new Shop(CAPACITY);
        }

        [Test]
        public void Constructor_SmartphoneTest()
        {
            Assert.AreEqual(MODEL_NAME, smartphone.ModelName);
            Assert.AreEqual(MAX_BATTERY_CHARGE, smartphone.MaximumBatteryCharge);
            Assert.AreEqual(CURRENT_BATTERY_CHARGE, smartphone.CurrentBateryCharge);
        }

        [Test]
        public void Constructor_ShopTest()
        {
            Assert.AreEqual(CAPACITY, shop.Capacity);
        }

        [Test]
        public void Capacity_Test()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                shop = new Shop(-1);
            });
        }

        [Test]
        public void Add_HappyTest()
        {
            shop.Add(smartphone);
            Assert.AreEqual(1, shop.Count);
        }

        [Test]
        public void Add_IfExceptTest()
        {
            shop.Add(smartphone);
            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.Add(smartphone);
            });
        }

        [Test]
        public void Add_ElseIfExcepTest()
        {
            shop = new Shop(1);
            shop.Add(smartphone);
            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.Add(smartphone);
            });
        }

        [Test]
        public void Remove_ExceptTest()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.Remove("test");
            });
        }

        [Test]
        public void Remove_HappyTest()
        {
            shop = new Shop(1);
            shop.Add(smartphone);
            shop.Remove(MODEL_NAME);
            Assert.AreEqual(0, shop.Count);
        }

        [Test]
        public void TestPhone_HappyTest()
        {
            shop.Add(smartphone);
            shop.TestPhone(MODEL_NAME, 10);
            Assert.AreEqual(79, smartphone.CurrentBateryCharge);
        }

        [Test]
        public void TestPhone_IfExceptTest()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.TestPhone("test", 10);
            });
        }

        [Test]
        public void TestPhone_ElseIfExceptTest()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.TestPhone(MODEL_NAME, 90);
            });
        }

        [Test]
        public void ChargePhone_ExceptTest()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.ChargePhone("test");
            });
        }

        [Test]
        public void ChargePhone_HappyTest()
        {
            smartphone = new Smartphone(MODEL_NAME, 89);
            shop.Add(smartphone);
            shop.TestPhone(MODEL_NAME, 10);
            shop.ChargePhone(MODEL_NAME);
            Assert.AreEqual(MAX_BATTERY_CHARGE, smartphone.CurrentBateryCharge);    
        }
    }
}