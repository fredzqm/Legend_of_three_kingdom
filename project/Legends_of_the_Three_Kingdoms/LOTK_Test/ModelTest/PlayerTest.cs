﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Model;
using System.Collections.Generic;

namespace LOTK_Test.ModelTest
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void HarmTest()
        {
            IGame game = new TestGame(1);
            int health = 5;
            int harm = 2;
            Player p = new Player(0, "Name", "Descript", health);
            PhaseList ret = p.harm(new HarmPhase(p, null, harm), game);
            Assert.IsTrue(ret.isEmpty());

            Assert.AreEqual(health - harm , p.health);
        }

        [TestMethod]
        public void HarmDyingTest()
        {
            IGame game = new TestGame(1);
            int health = 5;
            int harm = 10;
            Player p = new Player(0, "Name", "Descript", health);
            PhaseList ret = p.harm(new HarmPhase(p, null, harm), game);

            //Phase x = ret.pop();
            Assert.IsInstanceOfType(x, typeof(askForHelpPhase));
            Assert.IsTrue(ret.isEmpty());

            Assert.AreEqual(health - harm, p.health);
        }

    }

    internal class TestGame : IGame
    {
        public int Num_Player { get; }
        public Player[] players { get; set; }
        public CardSet cards { get; set; }
        public Phase curPhase { get; }
        public Player curRoundPlayer { get; }
        public TestGame(int n)
        {
            Num_Player = n;
        }

        public TestGame(int n, params Player[] players) : this(n)
        {
            this.players = players;
        }

        public bool tick()
        {
            throw new NotImplementedException();
        }

        public void nextStage(UserAction yesOrNoAction)
        {
            throw new NotImplementedException();
        }

        public List<Card> drawCard(int v)
        {
            throw new NotImplementedException();
        }

        public Player nextPlayer(int curPlayer, int count)
        {
            return players[(curPlayer + count) % Num_Player];
        }
    }
}