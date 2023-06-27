using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;

namespace FootballTeam.Tests
{
    public class Tests
    {
        private const string PLAYER_NAME = "NEYMAR";
        private const int PLAYER_NUMBER = 10;
        private const string POSITION = "Goalkeeper";

        private const string TEAM_NAME = "BRC";
        private const int CAPACITY = 17;
        private List<FootballPlayer> list;


        private FootballPlayer player;
        private FootballTeam team;

        [SetUp]
        public void Setup()
        {
            player = new FootballPlayer(PLAYER_NAME, PLAYER_NUMBER, POSITION);
            team = new FootballTeam(TEAM_NAME, CAPACITY);
            list = new List<FootballPlayer>();
        }

        [Test]
        public void Score_HappyTest()
        {
            FootballPlayer p = new FootballPlayer(PLAYER_NAME, PLAYER_NUMBER, POSITION);
            p.Score();

            string exp = $"{PLAYER_NAME} scored and now has 1 for this season!";
            team.AddNewPlayer(player);

            Assert.AreEqual(exp, team.PlayerScore(PLAYER_NUMBER));
            Assert.AreEqual(1, player.ScoredGoals);
        }

        [Test]
        public void PickPlayer_Test()
        {
            team.AddNewPlayer(player);
            Assert.AreEqual(player, team.PickPlayer(PLAYER_NAME));
        }

        [Test]
        public void Add_ExcepTest()
        {
            FootballPlayer f1 = new FootballPlayer("a87wged", PLAYER_NUMBER, POSITION);
            FootballPlayer f2 = new FootballPlayer("ycu", PLAYER_NUMBER, POSITION);
            FootballPlayer f3 = new FootballPlayer("sdrg", PLAYER_NUMBER, POSITION);
            FootballPlayer f4 = new FootballPlayer("aoiehf", PLAYER_NUMBER, POSITION);
            FootballPlayer f5 = new FootballPlayer("c[ew", PLAYER_NUMBER, POSITION);
            FootballPlayer f6 = new FootballPlayer("pcoe", PLAYER_NUMBER, POSITION);
            FootballPlayer f7 = new FootballPlayer("owidhp", PLAYER_NUMBER, POSITION);
            FootballPlayer f8 = new FootballPlayer("qcei ", PLAYER_NUMBER, POSITION);
            FootballPlayer f9 = new FootballPlayer("oeiwdh0", PLAYER_NUMBER, POSITION);
            FootballPlayer f10 = new FootballPlayer("pwejfew", PLAYER_NUMBER, POSITION);
            FootballPlayer f11 = new FootballPlayer("ayugef", PLAYER_NUMBER, POSITION);
            FootballPlayer f12 = new FootballPlayer("apdjc", PLAYER_NUMBER, POSITION);
            FootballPlayer f13 = new FootballPlayer("qoriv", PLAYER_NUMBER, POSITION);
            FootballPlayer f14 = new FootballPlayer("eqori", PLAYER_NUMBER, POSITION);
            FootballPlayer f15 = new FootballPlayer("qoefh", PLAYER_NUMBER, POSITION);
            FootballPlayer f16 = new FootballPlayer("qiofhr", PLAYER_NUMBER, POSITION);
            FootballPlayer f17 = new FootballPlayer("qoifhro", PLAYER_NUMBER, POSITION);

            team.AddNewPlayer(f1);
            team.AddNewPlayer(f2);
            team.AddNewPlayer(f3);
            team.AddNewPlayer(f4);
            team.AddNewPlayer(f5);
            team.AddNewPlayer(f6);
            team.AddNewPlayer(f7);
            team.AddNewPlayer(f8);
            team.AddNewPlayer(f9);
            team.AddNewPlayer(f10);
            team.AddNewPlayer(f11);
            team.AddNewPlayer(f12);
            team.AddNewPlayer(f13);
            team.AddNewPlayer(f14);
            team.AddNewPlayer(f15);
            team.AddNewPlayer(f16);
            team.AddNewPlayer(f17);

            Assert.AreEqual("No more positions available!", team.AddNewPlayer(player));
        }

        [Test]
        public void AddPlayer_HappyTest()
        {
            string exp = $"Added player {PLAYER_NAME} in position {POSITION} with number {PLAYER_NUMBER}";

            //team.AddNewPlayer(player);
            Assert.AreEqual(exp, team.AddNewPlayer(player));
            Assert.AreEqual(1, team.Players.Count);
        }

        [Test]
        public void Capacity_ExcepTest()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                team = new FootballTeam(TEAM_NAME, 14);
            });
        }

        [TestCase(null)]
        [TestCase("")]
        public void Name_TeamExceptTest(string n)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                team = new FootballTeam(n, CAPACITY);
            });
        }

        [Test]
        public void Constructor_TeamTest()
        {
            Assert.AreEqual(TEAM_NAME, team.Name);
            Assert.AreEqual(CAPACITY, team.Capacity);
            Assert.AreEqual(list, team.Players);
        }

        [Test]
        public void Constructor_PLayerTest()
        {
            Assert.AreEqual(PLAYER_NAME, player.Name);
            Assert.AreEqual(PLAYER_NUMBER, player.PlayerNumber);
            Assert.AreEqual(POSITION, player.Position);
            Assert.AreEqual(0, player.ScoredGoals);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Name_ExceptTest(string n)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                player = new FootballPlayer(n, PLAYER_NUMBER, POSITION);
            });
        }

        [TestCase(0)]
        [TestCase(22)]
        public void Number_ExceptTest(int n)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                player = new FootballPlayer(PLAYER_NAME, n, POSITION);
            });
        }

        [TestCase("utdj")]
        public void Position_ExceptTest(string n)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                player = new FootballPlayer(PLAYER_NAME, PLAYER_NUMBER, n);
            });
        }

        [Test]
        public void Goals_Test()
        {
            player.Score();
            Assert.AreEqual(1, player.ScoredGoals);
        }
    }
}