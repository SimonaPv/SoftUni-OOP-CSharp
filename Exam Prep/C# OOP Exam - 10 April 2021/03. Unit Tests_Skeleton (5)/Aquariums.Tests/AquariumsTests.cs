namespace Aquariums.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AquariumsTests
    {
        private const string FISH_NAME = "Bibi";
        private const bool AVAILABLE = true;
        private const string AQUARIUM_NAME = "Ribarnik";
        private const int CAPACITY = 8;

        private Fish fish;
        private Aquarium aquarium;

        [SetUp]
        public void SetUp()
        {
            fish = new Fish(FISH_NAME);
            aquarium = new Aquarium(AQUARIUM_NAME, CAPACITY);
        }

        [Test]
        public void Constructor_FishTest()
        {
            Assert.AreEqual(FISH_NAME, fish.Name);
            Assert.AreEqual(AVAILABLE, fish.Available);
        }

        [Test]
        public void Constructor_AquariumTest()
        {
            Assert.AreEqual(AQUARIUM_NAME, aquarium.Name);
            Assert.AreEqual(CAPACITY, aquarium.Capacity);
            Assert.AreEqual(0, aquarium.Count);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Name_ExceptTest(string name)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                aquarium = new Aquarium(name, 7);
            });
        }

        [Test]
        public void Capacity_ExceptTest()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                aquarium = new Aquarium(AQUARIUM_NAME, -1);
            });
        }

        [Test]
        public void Add_HappyTest()
        {
            aquarium.Add(fish);
            Assert.AreEqual(1, aquarium.Count);
        }

        [Test]
        public void Add_ExceptTest()
        {
            aquarium = new Aquarium(AQUARIUM_NAME, 0);
            Assert.Throws<InvalidOperationException>(() =>
            {
                aquarium.Add(fish);
            });
        }

        [Test]
        public void Remove_HappyTest()
        {
            aquarium.Add(fish);
            aquarium.RemoveFish(FISH_NAME);
            Assert.AreEqual(0, aquarium.Count);
        }

        [Test]
        public void Remove_ExceptTest()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                aquarium.RemoveFish("Test");
            });
        }

        [Test]
        public void SellFish_HappyTest()
        {
            aquarium.Add(fish);
            aquarium.SellFish(FISH_NAME);
            Assert.AreEqual(false, fish.Available);
        }

        [Test]
        public void SellFish_BoolTest()
        {
            List<Fish> list = new List<Fish>();
            list.Add(fish);

            Fish f = list.FirstOrDefault(x => x.Name == FISH_NAME);
            f.Available = false;

            aquarium.Add(fish);

            Assert.AreEqual(f, aquarium.SellFish(FISH_NAME));
        }

        [Test]
        public void SellFish_ExceptTest()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                aquarium.SellFish("Test");
            });
        }

        [Test]
        public void Report_Test()
        {
            List<Fish> list = new List<Fish>();

            Fish f1 = new Fish("uyhif");
            Fish f2 = new Fish("dfghj");
            Fish f3 = new Fish("lkjhg");

            list.Add(f1);
            list.Add(f2);
            list.Add(f3);

            aquarium.Add(f1);
            aquarium.Add(f2);
            aquarium.Add(f3);

            string fishNames = string.Join(", ", list.Select(x => x.Name));
            string output = $"Fish available at {aquarium.Name}: {fishNames}";

            Assert.AreEqual(output, aquarium.Report());
        }
    }
}
