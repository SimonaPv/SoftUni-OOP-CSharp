namespace Book.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using NUnit.Framework;

    public class Tests
    {
        private const string BOOK_NAME= "VARITY";
        private const string AUTHOR = "HOOVER";

        private Book book;

        [SetUp]
        public void SetUp()
        {
            book = new Book(BOOK_NAME, AUTHOR);
        }

        [Test]
        public void Constructor_Test()
        {
            Assert.AreEqual(BOOK_NAME, book.BookName);
            Assert.AreEqual(AUTHOR, book.Author);
            Assert.AreEqual(0, book.FootnoteCount);
        }

        [TestCase("")]
        [TestCase(null)]
        public void BookName_ExceptTest(string n)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                book = new Book(n, AUTHOR);
            });
        }

        [TestCase("")]
        [TestCase(null)]
        public void Author_ExceptTest(string a)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                book = new Book(BOOK_NAME, a);
            });
        }

        [Test]
        public void Add_HappyTest()
        {
            Dictionary<int, string> d = new Dictionary<int, string>();
            d.Add(2, "text");

            book.AddFootnote(2, "text");
            Assert.AreEqual(d.Count, book.FootnoteCount);
        }

        [Test]
        public void Add_ExceptTest()
        {
            book.AddFootnote(2, "text");
            Assert.Throws<InvalidOperationException>(() =>
            {
                book.AddFootnote(2, "text");
            });
        }

        [Test]
        public void FindFootnote_HappyTest()
        {
            book.AddFootnote(2, "text");
            string text = "Footnote #2: text";
            Assert.AreEqual(text, book.FindFootnote(2));
        }

        [Test]
        public void FindFootnote_ExceptTest()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                book.FindFootnote(2);
            });
        }

        [Test]
        public void AlterFootnote_HappyTest()
        {
            string exp = "Footnote #2: diffText";

            book.AddFootnote(2, "text");
            book.AlterFootnote(2, "diffText");
            string text = book.FindFootnote(2);

            Assert.AreEqual(exp, text);
        }

        [Test]
        public void AlterFootnote_ExceptTest()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                book.AlterFootnote(2, "text");
            });
        }
    }
}