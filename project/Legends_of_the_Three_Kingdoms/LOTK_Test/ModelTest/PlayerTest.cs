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
            PhaseList ls = p.handlePhase(new PlayerTurn(0), );
            Assert.IsInstanceOfType(ls.pop(), typeof(JudgePhase));
            Assert.IsInstanceOfType(ls.pop(), typeof(PlayerTurn));
        }
    }
}