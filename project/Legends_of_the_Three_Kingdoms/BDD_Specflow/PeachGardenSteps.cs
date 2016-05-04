using LOTK.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace BDD_Specflow
{
    [Binding]
    public class PeachGardenSteps
    {
        public Player playerA, playerB;
        public IGame game;
        public PeachGarden c;
        [Given(@"There is a game of player A with (.*) health and player B has (.*) health2")]
        public void GivenThereIsAGameOfPlayerAWithHealthAndPlayerBHasHealth(int p0, int p1)
        {
            playerA = new Player(0, "PA", "Player A", p0+1);
            playerB = new Player(1, "PB", "Player B", p1);
            Player[] players = new Player[2];
            players[0] = playerA;
            players[1] = playerB;
            List<Card> ls = new List<Card>();

            c = new PeachGarden(CardSuit.Heart, 3);
            ls.Add(c);
            ls.Add(c);
            ls.Add(c);
            ls.Add(c);
            game = new Game(players, new FakeCardSet(ls));
            game.start(0);
        }
        public ActionPhase actionPhase;
        [Given(@"At Player A's actionPhase3")]
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
        
        [When(@"Player A uses Peachgarden")]
        public void WhenPlayerAUsesPeachgarden()
        {
            game.processUserInput(0, new UseCardAction(c));

        }

        [Then(@"Player A should has (.*) health2")]
        public void ThenPlayerAShouldHasHealth(int p0)
        {
            Assert.AreEqual(p0, playerA.health);
        }
        
        [Then(@"Player B should has (.*) health2")]
        public void ThenPlayerBShouldHasHealth(int p0)
        {
            Assert.AreEqual(p0, playerB.health);
        }
    }
}
