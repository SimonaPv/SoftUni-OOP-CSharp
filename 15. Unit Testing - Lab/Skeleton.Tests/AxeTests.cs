using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class AxeTests
    {
        private int attack;
        private int durability;
        private int brokenD;
        private Axe axe;
        private Axe brokenAxe;
        private Dummy dummy;


        [SetUp]
        public void SetUp()
        {
            attack = 5;
            durability = 15;
            brokenD = -1;
            dummy = new Dummy(30, 20);
            axe = new Axe(attack, durability); 
            brokenAxe = new Axe(attack, brokenD);
        }

        [Test]
        public void AttackWithoutExcepTest()
        {
            axe.Attack(dummy);
            Assert.AreEqual(14, axe.DurabilityPoints);
        }

        [Test]
        public void AttackWithExcepTest()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                brokenAxe.Attack(dummy);
            });
        }
    }
}