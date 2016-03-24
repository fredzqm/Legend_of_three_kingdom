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
            Assert.AreEqual(PhaseType.JudgePhase , g.curPhase.type );
            g.nextStage();
            Assert.AreEqual(PhaseType.DrawingPhase , g.curPhase.type );
            g.nextStage();
            Assert.AreEqual(PhaseType.ActionPhase , g.curPhase.type );
            g.nextStage();
            Assert.AreEqual(PhaseType.DiscardPhase , g.curPhase.type );
        }

        [TestMethod]
        public void EightPeopleGameCycleTest()
        {
            Game g = new Game(8 , null);
            for (int j = 0; j < 1; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    Assert.AreEqual(PhaseType.JudgePhase, g.curPhase.type);
                    Assert.AreEqual(g.curPhase.playerID, i);
                    g.nextStage();
                    Assert.AreEqual(PhaseType.DrawingPhase, g.curPhase.type);
                    Assert.AreEqual(g.curPhase.playerID, i);
                    g.nextStage();
                    Assert.AreEqual( PhaseType.ActionPhase , g.curPhase.type );
                    Assert.AreEqual(g.curPhase.playerID, i);
                    g.nextStage();
                    Assert.AreEqual( PhaseType.DiscardPhase , g.curPhase.type );
                    Assert.AreEqual(g.curPhase.playerID, i);
                    g.nextStage();
                }
            }
        }

        [TestMethod]
        public void CurrentPlayerTest()
        {
            Game g = new Game(8, null);
            for (int j = 0; j < 1; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    Assert.AreEqual( PhaseType.JudgePhase , g.curPhase.type );
                    Assert.AreEqual(g.curRoundPlayer, i);
                    g.nextStage();
                    Assert.AreEqual( PhaseType.DrawingPhase , g.curPhase.type );
                    Assert.AreEqual(g.curRoundPlayer, i);
                    g.nextStage();
                    Assert.AreEqual( PhaseType.ActionPhase , g.curPhase.type );
                    Assert.AreEqual(g.curRoundPlayer, i);
                    g.nextStage();
                    Assert.AreEqual( PhaseType.DiscardPhase , g.curPhase.type );
                    Assert.AreEqual(g.curRoundPlayer, i);
                    g.nextStage();
                }
            }
        }

        [TestMethod]
        public void UserResponseYES_OR_NOTest()
        {
            Game g = new Game(5, null);
            Assert.AreEqual(PhaseType.JudgePhase, g.curPhase.type);
            // advance
            g.userResponse(new UserAction(UserActionType.YES_OR_NO, 0));
            Assert.AreEqual(PhaseType.DrawingPhase, g.curPhase.type);
            // advance
            g.userResponse(new UserAction(UserActionType.YES_OR_NO, 0));
            Assert.AreEqual(PhaseType.ActionPhase, g.curPhase.type);
            // not advance
            g.userResponse(new UserAction(UserActionType.YES_OR_NO, 1));
            Assert.AreEqual(PhaseType.ActionPhase, g.curPhase.type);
            // advance
            g.userResponse(new UserAction(UserActionType.YES_OR_NO, 0));
            Assert.AreEqual(PhaseType.DiscardPhase, g.curPhase.type);
            // advance
            g.userResponse(new UserAction(UserActionType.YES_OR_NO, 1));
            Assert.AreEqual(PhaseType.JudgePhase, g.curPhase.type);
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
