namespace Database.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class DatabaseTests
    {
        private Database database;

        [Test]
        public void ConstructorThrowsExceptTest()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                database = new Database(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17);
            });
        }

        [Test]
        public void AddWithoutExceptTest()
        {
            database = new Database(1, 2, 3);
            database.Add(4);
            Assert.AreEqual(4, database.Count);
        }

        [Test]
        public void AddWithExceptTest()
        {
            database = new Database(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
            Assert.Throws<InvalidOperationException>(() =>
            {
                database.Add(18);
            });
        }

        [Test]
        public void RemoveWithoutExceptTest()
        {
            database = new Database(1, 2, 3, 4, 5);
            database.Remove();
            Assert.AreEqual(4, database.Count);
        }

        [Test]
        public void RemoveWithExceptTest()
        {
            database = new Database();
            Assert.Throws<InvalidOperationException>(() =>
            {
                database.Remove();
            });
        }

        [Test]
        public void FetchTest()
        {
            database = new Database(1, 2, 3, 4, 5);
            int[] expectedArr = new int[] { 1, 2, 3, 4, 5 };
            int[] test = database.Fetch();
            Assert.AreEqual(expectedArr, test, "Fetch doesn't add right");
        }
    }
}
