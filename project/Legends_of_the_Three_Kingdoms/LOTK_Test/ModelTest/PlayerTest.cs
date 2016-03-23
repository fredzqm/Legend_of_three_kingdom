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
            ls= p.handlePhase(new PlayerTurn(0), testgame);
            Assert.IsInstanceOfType(ls.pop(), typeof(JudgePhase));
            Assert.IsInstanceOfType(ls.pop(), typeof(PlayerTurn));
            Assert.IsTrue(ls.isEmpty());

            ls = p.handlePhase(new JudgePhase(0), testgame);
            Assert.IsInstanceOfType(ls.pop(), typeof(DrawingPhase));
            Assert.IsInstanceOfType(ls.pop(), typeof(ActionPhase));
            Assert.IsTrue(ls.isEmpty());

            ls = p.handlePhase(new DrawingPhase(0), testgame);
            Assert.IsTrue(ls.isEmpty());

            ls = p.handlePhase(new ActionPhase(0), testgame);
            Assert.IsInstanceOfType(ls.pop(), typeof(DiscardPhase));
            Assert.IsTrue(ls.isEmpty());

            ls = p.handlePhase(new DiscardPhase(0), testgame);
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