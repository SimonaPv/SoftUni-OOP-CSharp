namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class WarriorTests
    {
        private const string NAME = "Moni";
        private const int DAMAGE = 5;
        private const int HP = 45;
        private Warrior warrior;

        [SetUp]
        public void SetUp()
        {
            warrior = new Warrior(NAME, DAMAGE, HP);    
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.AreEqual(NAME, warrior.Name, "NAME ERROR");
            Assert.AreEqual(DAMAGE, warrior.Damage, "NAME ERROR");
            Assert.AreEqual(HP, warrior.HP, "NAME ERROR");
        }

        [TestCase(null)]
        [TestCase(" ")]
        public void NameExceptTest(string name)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                warrior = new Warrior(name, DAMAGE, HP);
            });
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void DamageExceptTest(int damage)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                warrior = new Warrior(NAME, damage, HP);
            });
        }

        [TestCase(-1)]
        public void HPExceptTest(int hp)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                warrior = new Warrior(NAME, DAMAGE, hp);
            });
        }

        [Test]
        public void AttackElseTest()
        {
            Warrior wBeat = new Warrior("Second", 7, 35);
            int expHP = HP - 7;
            int expWBeat = 35 - DAMAGE;

            warrior.Attack(wBeat);

            Assert.AreEqual(expHP, warrior.HP);
            Assert.AreEqual(expWBeat, wBeat.HP);
        }

        [Test]
        public void AttackIfTest()
        {
            warrior = new Warrior("Blabla", 32, 40);
            Warrior wBeat = new Warrior("Second", 7, 31);

            int expThisHP = warrior.HP - 7;
            
            warrior.Attack(wBeat);

            Assert.AreEqual(expThisHP, warrior.HP);
            Assert.AreEqual(0, wBeat.HP);
        }

        [TestCase(29)]
        [TestCase(30)]
        public void AttactThisMinAttack(int hp)
        {
            warrior = new Warrior("This", 32, hp);
            Warrior wBeat = new Warrior("Warrior", 7, 31);

            Assert.Throws<InvalidOperationException>(() =>
            {
                warrior.Attack(wBeat);
            });
        }

        [TestCase(29)]
        [TestCase(30)]
        public void AttactOtherMinAttack(int hp)
        {
            warrior = new Warrior("This", 32, 33);
            Warrior wBeat = new Warrior("Warrior", 7, hp);

            Assert.Throws<InvalidOperationException>(() =>
            {
                warrior.Attack(wBeat);
            });
        }

        [Test]
        public void AttactEachOtherExcep()
        {
            warrior = new Warrior("This", 32, 31);
            Warrior wBeat = new Warrior("Warrior", 32, 31);

            Assert.Throws<InvalidOperationException>(() =>
            {
                warrior.Attack(wBeat);
            });
        }
    }
}