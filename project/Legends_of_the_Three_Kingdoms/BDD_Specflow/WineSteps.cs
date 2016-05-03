using LOTK.Controller;
using LOTK.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;

namespace LOTK_Test.BDD
{
    [Binding]
    public class WineSteps
    {
        public Player playerA, playerB;
        public IGame game;
        [Given(@"There is a game of player A with (.) health and player B has (.) health")]
        public void GivenThereIsAGameOfPlayerAWithHealthAndPlayerBHasHealth(int p0, int p1)
        {
            playerA = new Player(0, "PA", "Player A", p0);
            playerB = new Player(1, "PB", "Player B", p1);
            Player[] players = new Player[2];
            players[0] = playerA;
            players[1] = playerB;
            game = new Game(players, GameController.initialLizeCardSet());
            game.start();
        }

        public ActionPhase actionPhase;
        [Given(@"At Player A's actionPhase")]
        public void GivenAtPlayerASActionPhase()
        {
            actionPhase = game.curPhase as ActionPhase;
            while (actionPhase == null || actionPhase.player != playerA)
            {
                game.tick();
                actionPhase = game.curPhase as ActionPhase;
            }
        }

        public Wine wine;
        [Given(@"Player A uses Wine")]
        public void GivenPlayerAUsesWine()
        {
            wine = new Wine(CardSuit.Club, 1);
            game.processUserInput(0, new UseCardAction(wine));
        }

        public Attack attack;
        [Given(@"Player A attack Player B")]
        public void GivenPlayerAAttackPlayerB()
        {
            attack = new Attack(CardSuit.Club, 2);
            game.processUserInput(0, new UseCardAction(attack, playerB));
        }

        [When(@"Player B response with cancel")]
        public void WhenPlayerBResponseWithCancel()
        {
            game.processUserInput(1, new YesOrNoAction(false));
        }

        [Then(@"Player B should has (.) health")]
        public void ThenPlayerBShouldHasHealth(int p0)
        {
            Assert.AreEqual(p0, playerB.health);
        }
    }
}
