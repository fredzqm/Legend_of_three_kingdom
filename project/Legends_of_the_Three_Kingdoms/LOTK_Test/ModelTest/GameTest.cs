using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Model;

namespace LOTK_Test.ModelTest
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void GameConstructTest()
        {
            Game g = new Game(5);
        }

        [TestMethod]
        public void FourStageTest()
        {
            Game g = new Game(5);
            Assert.IsTrue(g.currentStage is PlayerTurn);
            g.nextStage();
            Assert.IsTrue(g.currentStage is JudgePhase);
            g.nextStage();
            Assert.IsTrue(g.currentStage is DrawingPhase);
            g.nextStage();
            Assert.IsTrue(g.currentStage is ActionPhase);
            g.nextStage();
            Assert.IsTrue(g.currentStage is DiscardPhase);
        }

        [TestMethod]
        public void EightPeopleGameCycleTest()
        {
            Game g = new Game(8);
            for (int i = 0; i < 8; i++)
            {
                Assert.IsTrue(g.currentStage is PlayerTurn);
                Assert.AreEqual(g.currentStage.playerID, i);
                g.nextStage();
                Assert.IsTrue(g.currentStage is JudgePhase);
                Assert.AreEqual(g.currentStage.playerID, i);
                g.nextStage();
                Assert.IsTrue(g.currentStage is DrawingPhase);
                Assert.AreEqual(g.currentStage.playerID, i);
                g.nextStage();
                Assert.IsTrue(g.currentStage is ActionPhase);
                Assert.AreEqual(g.currentStage.playerID, i);
                g.nextStage();
                Assert.IsTrue(g.currentStage is DiscardPhase);
                Assert.AreEqual(g.currentStage.playerID, i);
            }
        }
    }
}
