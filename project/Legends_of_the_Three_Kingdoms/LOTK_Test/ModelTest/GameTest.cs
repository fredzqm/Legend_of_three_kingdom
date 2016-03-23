using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Model;
using System.Collections.Generic;

namespace LOTK_Test.ModelTest
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void GameConstructTest()
        {
            Game g = new Game(5 , null);
        }

        [TestMethod]
        public void FourStageTest()
        {
            Game g = new Game(5 , null);
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
            Game g = new Game(8 , null);
            for (int j = 0; j < 1; j++)
            {
                for (int i = 0; i < 8; i++)
                {
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
                    g.nextStage();
                }
            }
        }

        [TestMethod]
        public void CurrentPlayerTest()
        {
            Game g = new Game(8 , null);
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
            Game g = new Game(5 , null);
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

        [TestMethod]
        public void GameCarSetDrawTest()
        {
            List<Card> ls = new List<Card>();
            ls.Add(new Card(CardSuit.Club, CardType.Attack, 0));
            ls.Add(new Card(CardSuit.Club, CardType.Miss, 1));
            ls.Add(new Card(CardSuit.Diamond, CardType.Miss, 2));
            ls.Add(new Card(CardSuit.Spade, CardType.Attack, 3));
            ls.Add(new Card(CardSuit.Club, CardType.Wine, 4));
            ls.Add(new Card(CardSuit.Spade, CardType.Attack, 5));
            Game g = new Game(5, ls);

            for (int i = 0; i < ls.Count; i += 2)
            {
                List<Card> drawn = g.drawCard(2);
                Assert.AreEqual(2, drawn.Count);
                Assert.IsTrue(ls.Contains(drawn[0]));
                Assert.IsTrue(ls.Contains(drawn[1]));
                ls.Remove(drawn[0]);
                ls.Remove(drawn[1]);
            }
        }
    }
}
