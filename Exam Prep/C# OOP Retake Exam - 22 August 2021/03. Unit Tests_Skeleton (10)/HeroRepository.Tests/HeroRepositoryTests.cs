using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;

public class HeroRepositoryTests
{
    private const string NAME = "MoniBonboni";
    private const int LEVEL = 8;

    private Hero hero;
    private HeroRepository repository;

    [SetUp]
    public void SetUp()
    {
        hero = new Hero(NAME, LEVEL);
        repository = new HeroRepository();
    }

    [Test]
    public void Constructor_HeroTest()
    {
        Assert.AreEqual(NAME, hero.Name);
        Assert.AreEqual(LEVEL, hero.Level);
    }

    [Test]
    public void Constructor_HeroRepositoryTest()
    {
        List<Hero> list = new List<Hero>();
        Assert.AreEqual(list, repository.Heroes);
    }

    [Test]
    public void Create_HappyTest()
    {
        string exp = "Successfully added hero MoniBonboni with level 8";
        Assert.AreEqual(exp, repository.Create(hero));
        Assert.AreEqual(1, repository.Heroes.Count);
    }

    [Test]
    public void Create_ExistsTest()
    {
        repository.Create(hero);
        Assert.Throws<InvalidOperationException>(() => repository.Create(hero));
    }

    [Test]
    public void Create_NullTest()
    {
        Assert.Throws<ArgumentNullException>(() => repository.Create(null));
    }

    [Test]
    public void Remove_HappyTest()
    {
        repository.Create(hero);
        Assert.AreEqual(true, repository.Remove(NAME));
        Assert.AreEqual(0, repository.Heroes.Count);
    }

    [TestCase(null)]
    [TestCase(" ")]
    public void Remove_NullTest(string n)
    {
        Assert.Throws<ArgumentNullException>(() => repository.Remove(n));
    }

    [Test]
    public void GetHeroWithHighestLevel_HappyTest()
    {
        Hero h1 = new Hero("C", 7);
        Hero h2 = new Hero("A", 6);
        Hero h3 = new Hero("B", 5);

        repository.Create(h1);
        repository.Create(h2);
        repository.Create(h3);

        Assert.AreEqual(h1, repository.GetHeroWithHighestLevel());
    }

    [Test]
    public void GetHero_HappyTest()
    {
        Hero h1 = new Hero("C", 7);
        Hero h2 = new Hero("A", 6);
        Hero h3 = new Hero("B", 5);

        repository.Create(h1);
        repository.Create(h2);
        repository.Create(h3);

        Assert.AreEqual(h2, repository.GetHero("A"));
    }
}