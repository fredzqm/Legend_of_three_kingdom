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
        public void PlayerTurnPhaseTest()
        {
            Player p = new Player(0);
            IGame testgame = new TestGame(5);
            PhaseList ls = p.handlePhase(new PlayerTurn(0), testgame);
            Assert.IsInstanceOfType(ls.pop(), typeof(JudgePhase));
            Assert.IsInstanceOfType(ls.pop(), typeof(PlayerTurn));
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