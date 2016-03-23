using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Model;
using System.Collections.Generic;

namespace LOTK_Test.ModelTest
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void PlayerConstructTest()
        {
            Player p = new Player(0);

        }

        [TestMethod]
        public void PlayerFiveBasicPhaseTest()
        {
            Player p = new Player(0);
            IGame testgame = new TestGame(5);
            PhaseList ls;
            ls= p.handlePhase(new Phase(0, PhaseType.PlayerTurn), testgame);
            Assert.AreEqual(ls.pop().type , PhaseType.PlayerTurn);
            Assert.AreEqual(ls.pop().type , PhaseType.PlayerTurn);
            Assert.IsTrue(ls.isEmpty());

            ls = p.handlePhase(new Phase(0, PhaseType.JudgePhase), testgame);
            Assert.AreEqual(ls.pop().type , PhaseType.DrawingPhase);
            Assert.AreEqual(ls.pop().type , PhaseType.ActionPhase);
            Assert.IsTrue(ls.isEmpty());

            ls = p.handlePhase(new Phase(0, PhaseType.DrawingPhase), testgame);
            Assert.IsTrue(ls.isEmpty());

            ls = p.handlePhase(new Phase(0, PhaseType.ActionPhase), testgame);
            Assert.AreEqual(ls.pop().type , PhaseType.DiscardPhase);
            Assert.IsTrue(ls.isEmpty());

            ls = p.handlePhase(new Phase(0, PhaseType.DiscardPhase), testgame);
            Assert.IsTrue(ls.isEmpty());

        }
        

    }

    internal class TestGame : IGame
    {
        public int Num_Player {get;}

        public TestGame(int n)
        {
            Num_Player = n;
        }
    }
}