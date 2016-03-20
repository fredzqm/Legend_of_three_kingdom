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
            for (int j = 0; j < 1; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    Assert.IsTrue(g.currentStage is JudgePhase);
                    Assert.AreEqual(g.currentStage.player, i);
                    g.nextStage();
                    Assert.IsTrue(g.currentStage is DrawingPhase);
                    Assert.AreEqual(g.currentStage.player, i);
                    g.nextStage();
                    Assert.IsTrue(g.currentStage is ActionPhase);
                    Assert.AreEqual(g.currentStage.player, i);
                    g.nextStage();
                    Assert.IsTrue(g.currentStage is DiscardPhase);
                    Assert.AreEqual(g.currentStage.player, i);
                    g.nextStage();
                }
            }
        }

        [TestMethod]
        public void CurrentPlayerTest()
        {
            Game g = new Game(8);
            for (int j = 0; j < 1; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    Assert.IsTrue(g.currentStage is JudgePhase);
                    Assert.AreEqual(g.curRoundPlayer, i);
                    g.nextStage();
                    Assert.IsTrue(g.currentStage is DrawingPhase);
                    Assert.AreEqual(g.curRoundPlayer, i);
                    g.nextStage();
                    Assert.IsTrue(g.currentStage is ActionPhase);
                    Assert.AreEqual(g.curRoundPlayer, i);
                    g.nextStage();
                    Assert.IsTrue(g.currentStage is DiscardPhase);
                    Assert.AreEqual(g.curRoundPlayer, i);
                    g.nextStage();
                }
            }
        }

        [TestMethod]
        public void UserInputYES_OR_NOTest()
        {
            Game g = new Game(5);
            Assert.IsTrue(g.currentStage is JudgePhase);
            Assert.IsFalse(g.userResponse(new UserAction(UserActionType.YES_OR_NO, 0)));
            Assert.IsFalse(g.userResponse(new UserAction(UserActionType.YES_OR_NO, 1)));
            g.nextStage();
            Assert.IsTrue(g.currentStage is DrawingPhase);
            Assert.IsFalse(g.userResponse(new UserAction(UserActionType.YES_OR_NO, 0)));
            Assert.IsFalse(g.userResponse(new UserAction(UserActionType.YES_OR_NO, 1)));
            g.nextStage();
            Assert.IsTrue(g.currentStage is ActionPhase);
            Assert.IsFalse(g.userResponse(new UserAction(UserActionType.YES_OR_NO, 0)));
            Assert.IsTrue(g.userResponse(new UserAction(UserActionType.YES_OR_NO, 1)));
            g.nextStage();
            Assert.IsTrue(g.currentStage is DiscardPhase);
            Assert.IsFalse(g.userResponse(new UserAction(UserActionType.YES_OR_NO, 0)));
            Assert.IsTrue(g.userResponse(new UserAction(UserActionType.YES_OR_NO, 1)));
        }
    }
}
