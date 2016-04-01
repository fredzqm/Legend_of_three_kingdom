﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Model;
using System.Collections.Generic;
using LOTK.Controller;

namespace LOTK_Test.ModelTest
{
    /// <summary>
    /// This is an integrated test for the model
    /// It involves cards, 
    /// </summary>
    [TestClass]
    public class GameIntegratedTest
    {

        private ICollection<Card> cardList;
        private Player[] players;
        private Player[] players8;
        private IGame game;
        private IGame game8;


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

            players = new Player[5];
            for (int i = 0; i < 5; i++)
            {
                players[i] = new Player(i);
            }
            game = new Game(players, cardList);

            players8 = new Player[8];
            for (int i = 0; i < 8; i++)
            {
                players8[i] = new Player(i);
            }
            game8 = new Game(players8, cardList);
        }

        [TestMethod]
        public void FourStageTest()
        {
            Assert.AreEqual(typeof(JudgePhase) , game.curPhase.GetType() );
            Assert.AreEqual(0, game.curPhase.playerID);
            game.nextStage(null);
            Assert.AreEqual(typeof(DrawingPhase) , game.curPhase.GetType() );
            Assert.AreEqual(0, game.curPhase.playerID);
            game.nextStage(null);
            Assert.AreEqual(typeof(ActionPhase) , game.curPhase.GetType() );
            Assert.AreEqual(0, game.curPhase.playerID);
            game.nextStage(new YesOrNoAction(false));
            Assert.AreEqual(typeof(DiscardPhase) , game.curPhase.GetType() );
            Assert.AreEqual(0, game.curPhase.playerID);
            game.nextStage(new YesOrNoAction(false));
            Assert.AreEqual(typeof(JudgePhase), game.curPhase.GetType());
            Assert.AreEqual(1, game.curPhase.playerID);
        }

        [TestMethod]
        public void EightPeopleGameCycleTest()
        {
            for (int j = 0; j < 1; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    Assert.AreEqual(typeof(JudgePhase), game8.curPhase.GetType());
                    Assert.AreEqual(game8.curPhase.playerID, i);
                    game8.nextStage(null);
                    Assert.AreEqual(typeof(DrawingPhase), game8.curPhase.GetType());
                    Assert.AreEqual(game8.curPhase.playerID, i);
                    game8.nextStage(null);
                    Assert.AreEqual( typeof(ActionPhase) , game8.curPhase.GetType() );
                    Assert.AreEqual(game8.curPhase.playerID, i);
                    game8.nextStage(new YesOrNoAction(false));
                    Assert.AreEqual( typeof(DiscardPhase) , game8.curPhase.GetType() );
                    Assert.AreEqual(game8.curPhase.playerID, i);
                    game8.nextStage(new YesOrNoAction(false));
                }
            }
        }

        [TestMethod]
        public void CurrentPlayerTest()
        {
            for (int j = 0; j < 1; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    Assert.AreEqual( typeof(JudgePhase) , game8.curPhase.GetType() );
                    Assert.AreEqual(game8.curRoundPlayer, i);
                    game8.nextStage(null);
                    Assert.AreEqual( typeof(DrawingPhase) , game8.curPhase.GetType() );
                    Assert.AreEqual(game8.curRoundPlayer, i);
                    game8.nextStage(null);
                    Assert.AreEqual( typeof(ActionPhase) , game8.curPhase.GetType() );
                    Assert.AreEqual(game8.curRoundPlayer, i);
                    game8.nextStage(new YesOrNoAction(false));
                    Assert.AreEqual( typeof(DiscardPhase) , game8.curPhase.GetType() );
                    Assert.AreEqual(game8.curRoundPlayer, i);
                    game8.nextStage(new YesOrNoAction(false));
                }
            }
        }

        [TestMethod]
        public void UserResponseYES_OR_NOTest()
        {
            Assert.AreEqual(typeof(JudgePhase), game.curPhase.GetType());
            // advance
            game.nextStage(new YesOrNoAction(false));
            Assert.AreEqual(typeof(DrawingPhase), game.curPhase.GetType());
            // advance
            game.nextStage(new YesOrNoAction(false));
            Assert.AreEqual(typeof(ActionPhase), game.curPhase.GetType());
            // not advance
            game.nextStage(new YesOrNoAction(true));
            Assert.AreEqual(typeof(ActionPhase), game.curPhase.GetType());
            // advance
            game.nextStage(new YesOrNoAction(false));
            Assert.AreEqual(typeof(DiscardPhase), game.curPhase.GetType());
            // advance
            game.nextStage(new YesOrNoAction(true));
            Assert.AreEqual(typeof(JudgePhase), game.curPhase.GetType());
        }
       
        [TestMethod]
        public void AutoAdvancedTest()
         {
            Assert.AreEqual(typeof(JudgePhase), game.curPhase.GetType());
            game.tick();
            Assert.AreEqual(typeof(JudgePhase), game.curPhase.GetType());
            game.tick();
            Assert.AreEqual(typeof(DrawingPhase), game.curPhase.GetType());
            game.tick();
            Assert.AreEqual(typeof(DrawingPhase), game.curPhase.GetType());
            game.tick();
            Assert.AreEqual(typeof(ActionPhase), game.curPhase.GetType());
            game.tick();
            Assert.AreEqual(typeof(ActionPhase), game.curPhase.GetType());
            game.tick();
            Assert.AreEqual(typeof(ActionPhase), game.curPhase.GetType());
            game.nextStage(new YesOrNoAction(false));
            Assert.AreEqual(typeof(DiscardPhase), game.curPhase.GetType());
            game.tick();
            Assert.AreEqual(typeof(DiscardPhase), game.curPhase.GetType());
            game.tick();
            Assert.AreEqual(typeof(DiscardPhase), game.curPhase.GetType());
        }

    }
}
