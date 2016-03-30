using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Model;
using System.Collections.Generic;
using LOTK.Controller;

namespace LOTK_Test.ModelTest
{
    [TestClass]
    public class GameTest
    {

        private ICollection<Card> cardList;

        [TestInitialize()]
        public void initialize()
        {
            cardList = new List<Card>();
            cardList.Add(new Attack(CardSuit.Club, 2));
            cardList.Add(new Attack(CardSuit.Club, 3));
            cardList.Add(new Attack(CardSuit.Club, 4));
            cardList.Add(new Attack(CardSuit.Club, 5));
            cardList.Add(new Attack(CardSuit.Club, 6));
            cardList.Add(new Attack(CardSuit.Club, 7));
            cardList.Add(new Attack(CardSuit.Club, 8));
            cardList.Add(new Attack(CardSuit.Club, 9));

            cardList.Add(new Attack(CardSuit.Spade, 2));
            cardList.Add(new Attack(CardSuit.Spade, 3));
            cardList.Add(new Attack(CardSuit.Spade, 4));
            cardList.Add(new Attack(CardSuit.Spade, 5));
            cardList.Add(new Attack(CardSuit.Spade, 6));
            cardList.Add(new Attack(CardSuit.Spade, 7));
            cardList.Add(new Attack(CardSuit.Spade, 8));
            cardList.Add(new Attack(CardSuit.Spade, 9));

            cardList.Add(new Miss(CardSuit.Diamond, 2));
            cardList.Add(new Miss(CardSuit.Diamond, 3));
            cardList.Add(new Miss(CardSuit.Diamond, 4));
            cardList.Add(new Miss(CardSuit.Diamond, 5));
            cardList.Add(new Miss(CardSuit.Diamond, 6));
            cardList.Add(new Miss(CardSuit.Diamond, 7));
            cardList.Add(new Miss(CardSuit.Diamond, 8));
            cardList.Add(new Miss(CardSuit.Diamond, 9));

            cardList.Add(new Peach(CardSuit.Heart, 2));
            cardList.Add(new Peach(CardSuit.Heart, 3));
            cardList.Add(new Peach(CardSuit.Heart, 4));
            cardList.Add(new Peach(CardSuit.Heart, 5));
            cardList.Add(new Peach(CardSuit.Heart, 6));
            cardList.Add(new Peach(CardSuit.Heart, 7));
            cardList.Add(new Peach(CardSuit.Heart, 8));
            cardList.Add(new Peach(CardSuit.Heart, 9));
        }

        [TestMethod]
        public void GameConstructTest()
        {
            IGame g = new Game(5 , cardList);
        }

        [TestMethod]
        public void FourStageTest()
        {
            IGame g = new Game(5 , cardList);
            Assert.AreEqual(typeof(JudgePhase) , g.curPhase.GetType() );
            Assert.AreEqual(0, g.curPhase.playerID);
            g.nextStage(null);
            Assert.AreEqual(typeof(DrawingPhase) , g.curPhase.GetType() );
            Assert.AreEqual(0, g.curPhase.playerID);
            g.nextStage(null);
            Assert.AreEqual(typeof(ActionPhase) , g.curPhase.GetType() );
            Assert.AreEqual(0, g.curPhase.playerID);
            g.nextStage(new YesOrNoAction(false));
            Assert.AreEqual(typeof(DiscardPhase) , g.curPhase.GetType() );
            Assert.AreEqual(0, g.curPhase.playerID);
            g.nextStage(new YesOrNoAction(false));
            Assert.AreEqual(typeof(JudgePhase), g.curPhase.GetType());
            Assert.AreEqual(1, g.curPhase.playerID);
        }

        [TestMethod]
        public void EightPeopleGameCycleTest()
        {
            IGame g = new Game(8 , cardList);
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
                    g.nextStage(new YesOrNoAction(false));
                    Assert.AreEqual( typeof(DiscardPhase) , g.curPhase.GetType() );
                    Assert.AreEqual(g.curPhase.playerID, i);
                    g.nextStage(new YesOrNoAction(false));
                }
            }
        }

        [TestMethod]
        public void CurrentPlayerTest()
        {
            IGame g = new Game(8, cardList);
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
                    g.nextStage(new YesOrNoAction(false));
                    Assert.AreEqual( typeof(DiscardPhase) , g.curPhase.GetType() );
                    Assert.AreEqual(g.curRoundPlayer, i);
                    g.nextStage(new YesOrNoAction(false));
                }
            }
        }

        [TestMethod]
        public void UserResponseYES_OR_NOTest()
        {
            IGame g = new Game(5, cardList);
            Assert.AreEqual(typeof(JudgePhase), g.curPhase.GetType());
            // advance
            g.nextStage(new YesOrNoAction(false));
            Assert.AreEqual(typeof(DrawingPhase), g.curPhase.GetType());
            // advance
            g.nextStage(new YesOrNoAction(false));
            Assert.AreEqual(typeof(ActionPhase), g.curPhase.GetType());
            // not advance
            g.nextStage(new YesOrNoAction(true));
            Assert.AreEqual(typeof(ActionPhase), g.curPhase.GetType());
            // advance
            g.nextStage(new YesOrNoAction(false));
            Assert.AreEqual(typeof(DiscardPhase), g.curPhase.GetType());
            // advance
            g.nextStage(new YesOrNoAction(true));
            Assert.AreEqual(typeof(JudgePhase), g.curPhase.GetType());
        }

       
        [TestMethod]
        public void AutoAdvancedTest()
         {
            IGame g = new Game(5, cardList);
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
            g.nextStage(new YesOrNoAction(false));
            Assert.AreEqual(typeof(DiscardPhase), g.curPhase.GetType());
            g.tick();
            Assert.AreEqual(typeof(DiscardPhase), g.curPhase.GetType());
            g.tick();
            Assert.AreEqual(typeof(DiscardPhase), g.curPhase.GetType());
        }
    }
}
