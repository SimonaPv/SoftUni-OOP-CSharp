using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Runtime.CompilerServices;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DummyTests
    {
        private Dummy dummy;
        private Dummy deadDummy;
        private int health;
        private int deadH;
        private int experience;
        private int attack;

        [SetUp]
        public void SetUp()
        {
            health = 30;
            deadH = -1;
            experience = 20;
            attack = 5;

            dummy = new Dummy(health, experience);
            deadDummy = new Dummy(deadH, experience);
        }

        [Test]
        public void TakeAttackWithoutExceptTest()
        {
            dummy.TakeAttack(attack);
            Assert.AreEqual(25, dummy.Health);
        }

        [Test]
        public void TakeAttackWithExceptTest()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                deadDummy.TakeAttack(attack);
            });
        }

        [Test]
        public void GiveExperienceWithoutExceptTest()
        {//must be dead
            deadDummy.GiveExperience();
            Assert.AreEqual(20, deadDummy.GiveExperience());
        }

        [Test]
        public void GiveExperienceWithExceptTest()
        { 
            Assert.Throws<InvalidOperationException>(() =>
            {
                dummy.GiveExperience();
                Assert.AreEqual(20, dummy.GiveExperience());
            });
        }
    }
}