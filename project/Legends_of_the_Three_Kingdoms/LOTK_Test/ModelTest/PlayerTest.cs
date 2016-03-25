using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Model;
using System.Collections.Generic;
using Legends_of_the_Three_Kingdoms.Model;

namespace LOTK_Test.ModelTest
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void PlayerConstructTest()
        {
            Player p = new Player(0, "Player Name", "Player Description");
        }

        [TestMethod]
        public void PlayerFiveBasicPhaseTest()
        {
            Player p = new Player(0, "Player Name", "Player Description");
            IGame testgame = new TestGame(5);
            PhaseList ls;
            ls = p.handlePhase(new Phase(0, PhaseType.PlayerTurn), testgame);
            Assert.AreEqual(PhaseType.JudgePhase, ls.pop().type);
            Assert.AreEqual(PhaseType.PlayerTurn, ls.pop().type);
            Assert.IsTrue(ls.isEmpty());

            ls = p.handlePhase(new Phase(0, PhaseType.JudgePhase), testgame);
            Assert.AreEqual(PhaseType.DrawingPhase, ls.pop().type);
            Assert.AreEqual(PhaseType.ActionPhase, ls.pop().type);
            Assert.IsTrue(ls.isEmpty());

            ls = p.handlePhase(new Phase(0, PhaseType.DrawingPhase), testgame);
            Assert.IsTrue(ls.isEmpty());

            ls = p.handlePhase(new Phase(0, PhaseType.ActionPhase), testgame);
            Assert.AreEqual(ls.pop().type, PhaseType.DiscardPhase);
            Assert.IsTrue(ls.isEmpty());

            ls = p.handlePhase(new Phase(0, PhaseType.DiscardPhase), testgame);
            Assert.IsTrue(ls.isEmpty());

        }

        [TestMethod]
        public void PlayerFiveAdvancePhaseTest()
        {
            Player p = new Player(0, "Player Name", "Player Description");
            IGame testgame = new TestGame(5);
            PhaseList ls;
            ls = p.handlePhase(new Phase(0, PhaseType.PlayerTurn), testgame);
            Assert.AreEqual(PhaseType.JudgePhase, ls.pop().type);
            Assert.AreEqual(PhaseType.PlayerTurn, ls.pop().type);
            Assert.IsTrue(ls.isEmpty());

            ls = p.handlePhase(new Phase(0, PhaseType.JudgePhase), testgame);
            Assert.AreEqual(PhaseType.DrawingPhase, ls.pop().type);
            Assert.AreEqual(PhaseType.ActionPhase, ls.pop().type);
            Assert.IsTrue(ls.isEmpty());

            ls = p.handlePhase(new Phase(0, PhaseType.DrawingPhase), testgame);
            Assert.IsTrue(ls.isEmpty());

            ls = p.handlePhase(new Phase(0, PhaseType.ActionPhase), testgame);
            Assert.AreEqual(ls.pop().type, PhaseType.DiscardPhase);
            Assert.IsTrue(ls.isEmpty());

            ls = p.handlePhase(new Phase(0, PhaseType.DiscardPhase), testgame);
            Assert.IsTrue(ls.isEmpty());
            try { ls.pop(); }
            catch (EmptyException e)
            {
                Console.WriteLine("Empty Exception caught.", e);
            }

        }

        [TestMethod]
        public void UserInputYES_OR_NOTest()
        {
            Player g = new Player(0, "Player Name", "Player Description");
            Assert.IsTrue(g.UserInputYesOrNo(new Phase(0, PhaseType.JudgePhase), 0);
            Assert.IsTrue(g.UserInputYesOrNo(new Phase(0, PhaseType.JudgePhase), 1);
            Assert.IsTrue(g.UserInputYesOrNo(new Phase(0, PhaseType.DrawingPhase), 0);
            Assert.IsTrue(g.UserInputYesOrNo(new Phase(0, PhaseType.DrawingPhase), 1);
            Assert.IsTrue(g.UserInputYesOrNo(new Phase(0, PhaseType.ActionPhase), 0);
            Assert.IsFalse(g.UserInputYesOrNo(new Phase(0, PhaseType.ActionPhase), 1);
            Assert.IsTrue(g.UserInputYesOrNo(new Phase(0, PhaseType.DiscardPhase), 0);
            Assert.IsTrue(g.UserInputYesOrNo(new Phase(0, PhaseType.DiscardPhase), 1);
        }

        [TestMethod]
        public void handlePhaseTest()
        {
            Player g = new Player(0, "Player Name", "Player Description");

        }


        internal class TestGame : IGame
        {
            public int Num_Player { get; }

            public TestGame(int n)
            {
                Num_Player = n;
            }
        }
    }
}