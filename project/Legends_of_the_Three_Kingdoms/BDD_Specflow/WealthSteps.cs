using LOTK.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace BDD_Specflow
{
    [Binding]
    public class WealthSteps
    {
        public Player playerA;
        public IGame game;
        public Wealth c;
        [Given(@"There is a game of player A")]
        public void GivenThereIsAGameOfPlayerA()
        {
            playerA = new Player(0, "PA", "Player A",4);
            Player[] players = new Player[1];
            players[0] = playerA;
            List<Card> ls = new List<Card>();
            
            c = new Wealth(CardSuit.Heart, 3);
            ls.Add(c);
            ls.Add(c);
            ls.Add(c);
            ls.Add(c);
            game = new Game(players, new FakeCardSet(ls));
            game.start(0);
        }
        public ActionPhase actionPhase;
        [Given(@"At Player A's actionPhase2")]
        public void GivenAtPlayerASActionPhase()
        {

            actionPhase = game.curPhase as ActionPhase;
            while (actionPhase == null || actionPhase.player != playerA)
            {
                game.processUserInput(0, new YesOrNoAction(false));
                game.processUserInput(1, new YesOrNoAction(false));
                actionPhase = game.curPhase as ActionPhase;
            }
        }
        
        [When(@"Player A uses Wealth")]
        public void WhenPlayerAUsesWealth()
        {
            game.processUserInput(0, new UseCardAction(c));
        }
        
        [Then(@"Player A has (.) cards")]
        public void ThenPlayerAHasCards(int p0)
        {
            Assert.AreEqual(p0, playerA.handCards.Count);
        }
    }
}
