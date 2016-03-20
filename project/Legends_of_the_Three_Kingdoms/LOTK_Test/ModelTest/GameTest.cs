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
            Assert.IsTrue(g.currentStage is JudgePhase);
            g.nextStage();
            Assert.IsTrue(g.currentStage is JudgePhase);
            g.nextStage();
            Assert.IsTrue(g.currentStage is DrawingPhase);
            g.nextStage();
            Assert.IsTrue(g.currentStage is ActionPhase);
            g.nextStage();
            Assert.IsTrue(g.currentStage is DiscardPhase);
        }
    }
}
