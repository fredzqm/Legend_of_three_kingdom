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
            Assert.AreEqual(typeof(JudgePhase) , g.curPhase.GetType() );
            Assert.AreEqual(0, g.curPhase.playerID);
            g.nextStage(null);
            Assert.AreEqual(typeof(DrawingPhase) , g.curPhase.GetType() );
            Assert.AreEqual(0, g.curPhase.playerID);
            g.nextStage(null);
            Assert.AreEqual(typeof(ActionPhase) , g.curPhase.GetType() );
            Assert.AreEqual(0, g.curPhase.playerID);
            g.nextStage(new UserActionYesOrNo(false));
            Assert.AreEqual(typeof(DiscardPhase) , g.curPhase.GetType() );
            Assert.AreEqual(0, g.curPhase.playerID);
            g.nextStage(new UserActionYesOrNo(false));
            Assert.AreEqual(typeof(JudgePhase), g.curPhase.GetType());
            Assert.AreEqual(1, g.curPhase.playerID);
        }

        [TestMethod]
        public void EightPeopleGameCycleTest()
        {
            Game g = new Game(8 , null);
            for (int j = 0; j < 1; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    Assert.AreEqual(typeof(JudgePhase), g.curPhase.GetType());
                    Assert.AreEqual(g.curPhase.playerID, i);
                    g.nextStage(null);
                    Assert.AreEqual(typeof(DrawingPhase), g.curPhase.GetType());
                    Assert.AreEqual(g.curPhase.playerID, i);
                    g.nextStage(null);
                    Assert.AreEqual( typeof(ActionPhase) , g.curPhase.GetType() );
                    Assert.AreEqual(g.curPhase.playerID, i);
                    g.nextStage(new UserActionYesOrNo(false));
                    Assert.AreEqual( typeof(DiscardPhase) , g.curPhase.GetType() );
                    Assert.AreEqual(g.curPhase.playerID, i);
                    g.nextStage(new UserActionYesOrNo(false));
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
                    Assert.AreEqual( typeof(JudgePhase) , g.curPhase.GetType() );
                    Assert.AreEqual(g.curRoundPlayer, i);
                    g.nextStage(null);
                    Assert.AreEqual( typeof(DrawingPhase) , g.curPhase.GetType() );
                    Assert.AreEqual(g.curRoundPlayer, i);
                    g.nextStage(null);
                    Assert.AreEqual( typeof(ActionPhase) , g.curPhase.GetType() );
                    Assert.AreEqual(g.curRoundPlayer, i);
                    g.nextStage(new UserActionYesOrNo(false));
                    Assert.AreEqual( typeof(DiscardPhase) , g.curPhase.GetType() );
                    Assert.AreEqual(g.curRoundPlayer, i);
                    g.nextStage(new UserActionYesOrNo(false));
                }
            }
        }

        [TestMethod]
        public void UserResponseYES_OR_NOTest()
        {
            Game g = new Game(5, null);
            Assert.AreEqual(typeof(JudgePhase), g.curPhase.GetType());
            // advance
            g.nextStage(new UserActionYesOrNo(false));
            Assert.AreEqual(typeof(DrawingPhase), g.curPhase.GetType());
            // advance
            g.nextStage(new UserActionYesOrNo(false));
            Assert.AreEqual(typeof(ActionPhase), g.curPhase.GetType());
            // not advance
            g.nextStage(new UserActionYesOrNo(true));
            Assert.AreEqual(typeof(ActionPhase), g.curPhase.GetType());
            // advance
            g.nextStage(new UserActionYesOrNo(false));
            Assert.AreEqual(typeof(DiscardPhase), g.curPhase.GetType());
            // advance
            g.nextStage(new UserActionYesOrNo(true));
            Assert.AreEqual(typeof(JudgePhase), g.curPhase.GetType());
        }

        [TestMethod]
        public void CardSetDrawTest()
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

        [TestMethod]
        public void AutoAdvancedTest()
        {
            Game g = new Game(5, null);
            Assert.AreEqual(typeof(JudgePhase), g.curPhase.GetType());
            g.tick();
            Assert.AreEqual(typeof(JudgePhase), g.curPhase.GetType());
            g.tick();
            Assert.AreEqual(typeof(DrawingPhase), g.curPhase.GetType());
            g.tick();
            Assert.AreEqual(typeof(DrawingPhase), g.curPhase.GetType());
            g.tick();
            Assert.AreEqual(typeof(ActionPhase), g.curPhase.GetType());
            g.tick();
            Assert.AreEqual(typeof(ActionPhase), g.curPhase.GetType());
            g.tick();
            Assert.AreEqual(typeof(ActionPhase), g.curPhase.GetType());
            g.nextStage(new UserActionYesOrNo(false));
            Assert.AreEqual(typeof(DiscardPhase), g.curPhase.GetType());
            g.tick();
            Assert.AreEqual(typeof(DiscardPhase), g.curPhase.GetType());
            g.tick();
            Assert.AreEqual(typeof(DiscardPhase), g.curPhase.GetType());
        }
    }
}
