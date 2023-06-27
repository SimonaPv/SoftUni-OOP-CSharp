namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    [TestFixture]
    public class ArenaTests
    {
        private Warrior first;
        private Warrior second;

        [SetUp]
        public void SetUp()
        {
            first = new Warrior("First", 23, 31);
            second = new Warrior("Second", 21, 33);
            Arena arena = new Arena();
        }

        [Test]
        public void ConstructorTest()
        {
            List<Warrior> list = new List<Warrior>();
            Arena arena = new Arena();
            Assert.AreEqual(list, arena.Warriors);
        }

        [Test]
        public void EnrollHappyTest()
        {
            List<Warrior> list = new List<Warrior>();
            list.Add(first);

            Arena arena = new Arena();
            arena.Enroll(first);

            Assert.AreEqual(list, arena.Warriors);
        }

        [Test]
        public void EnrollExceptTest()
        {
            Arena arena = new Arena();
            arena.Enroll(first);
            arena.Enroll(second);

            Assert.Throws<InvalidOperationException>(() =>
            {
                arena.Enroll(first);
            });
        }

        [Test]
        public void Fight()
        {
            Arena arena = new Arena();
            arena.Enroll(first);
            arena.Enroll(second);

            int expectedHpFirst = first.HP - second.Damage;
            int expectedHpSecond = second.HP - first.Damage;

            arena.Fight(first.Name, second.Name);

            Assert.AreEqual(expectedHpFirst, first.HP);
            Assert.AreEqual(expectedHpSecond, second.HP);
        }

        [Test]
        public void Fight_AttackerNotPresentInTheArenaCantFight()
        {
            Arena arena = new Arena();
            arena.Enroll(first);
            arena.Enroll(second);

            Assert.Throws<InvalidOperationException>(() =>
            {
                arena.Fight(null, second.Name);
            });
        }

        [Test]
        public void Fight_DefenderNotPresentInTheArenaCantFight()
        {
            Arena arena = new Arena();
            arena.Enroll(first);
            arena.Enroll(second);

            Assert.Throws<InvalidOperationException>(() =>
            {
                arena.Fight(first.Name, null);
            });
        }
    }
}
