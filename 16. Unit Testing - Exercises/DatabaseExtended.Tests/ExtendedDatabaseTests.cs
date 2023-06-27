namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using System;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private Database database;
        private Person[] person;

        [SetUp]
        public void SetUp()
        {
            database = new Database();

            person = new Person[13];
            for (int i = 0; i < person.Length; i++)
            {
                person[i] = new Person(i, ((char)('A' + i)).ToString());
            }
        }

        [Test]
        public void AddRangeWithExcepTest()
        {
            List<Person> list = new List<Person>();

            for (int i = 0; i < 17; i++)
            {
                Person pr = new Person(i, "Lucho");
                list.Add(pr);
            }

            Person[] arr = list.ToArray();

            Assert.Throws<ArgumentException>(() =>
            {
                database = new Database(arr);
            });
        }

        [Test]
        public void AddHappyTest()
        {
            Person pTest = new Person(1, "Moni");
            database.Add(pTest);

            Assert.AreEqual(1, database.Count);
        }

        [Test]
        public void AddIsEqualExcepTest()
        {
            person = new Person[16];
            for (int i = 0; i < person.Length; i++)
            {
                person[i] = new Person(i, ((char)('A' + i)).ToString());
            }

            database = new Database(person);
            Person pTest = new Person(57, "Moni");

            Assert.Throws<InvalidOperationException>(() =>
            {
                database.Add(pTest);
            });
        }

        [Test]
        public void AddSameNameExcepTest()
        {
            Person pTest = new Person(1, "A");
            database = new Database(person);

            Assert.Throws<InvalidOperationException>(() =>
            {
                database.Add(pTest);
            });
        }

        [Test]
        public void AddSameArray()
        {
            Person p = new Person(57, "Moni");
            database.Add(p);
            Person exp = database.FindById(57);
            Assert.AreEqual(p, exp);
        }

        [Test]
        public void AddSameIdExcepTest()
        {
            Person pTest = new Person(0, "S");
            database = new Database(person);

            Assert.Throws<InvalidOperationException>(() =>
            {
                database.Add(pTest);
            });
        }

        [Test]
        public void RemoveHappyTest()
        {
            database = new Database(person);
            int exp = database.Count - 1;
            database.Remove();
            Assert.AreEqual(exp, database.Count);
        }

        [Test]
        public void Add_CannotExceedMaximumArrayCount()
        {
            database = new Database(person);
            database.Add(new Person(14, "Fourteen"));
            database.Add(new Person(15, "Fifteen"));
            database.Add(new Person(16, "Sixteen"));

            Assert.Throws<InvalidOperationException>(() =>
            {
                database.Add(new Person(17, "Error"));
            });
        }

        [Test]
        public void RemoveSameArrayTest()
        {
            database = new Database();
            Person p1 = new Person(1, "S");
            Person p2 = new Person(2, "K");
            database.Add(p1);
            database.Add(p2);
            database.Remove();

            Assert.Throws<InvalidOperationException>(() =>
            {
                database.FindById(2);
            });
        }

        [Test]
        public void RemoveWithExcepTest()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                database.Remove();
            });
        }

        [Test]
        public void FindByUsernameHappyTest()
        {
            database = new Database(person);
            Person pFound = person.FirstOrDefault(x => x.UserName == "A");
            Assert.AreEqual(pFound, database.FindByUsername("A"));
        }

        [TestCase(null)]
        [TestCase("")]
        public void FindByUsername_UserCantBeNull(string username)
        {
            database = new Database(person);
            Assert.Throws<ArgumentNullException>(() =>
            {
                database.FindByUsername(username);
            });
        }

        [Test]
        public void FindByUsernameWrongTest()
        {
            database = new Database(person);
            Assert.Throws<InvalidOperationException>(() =>
            {
                database.FindByUsername("Moni");
            });
        }

        [Test]
        public void FindByIdHappyTest()
        {
            database = new Database(person);
            Person pFound = person.FirstOrDefault(x => x.Id == 7);
            Assert.AreEqual(pFound, database.FindById(7));
        }

        [Test]
        public void FindByIdNegativeTest()
        {
            database = new Database(person);
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                database.FindById(-2);
            });
        }

        [Test]
        public void FindByIdWrongTest()
        {
            database = new Database(person);
            Assert.Throws<InvalidOperationException>(() =>
            {
                database.FindById(43);
            });
        }
    }
}