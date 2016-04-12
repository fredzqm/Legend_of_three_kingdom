﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Model;
using System.Collections.Generic;
using Rhino.Mocks;
using System.Reflection;

namespace LOTK_Test.ModelTest
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class PlayerTest
    {
        private MockRepository mocks;
        [TestInitialize()]
        public void Initialize()
        {
            mocks = new MockRepository();
        }

        [TestMethod]
        public void HarmTest()
        { Attack card = new Attack(CardSuit.Club,1);
            IGame game = new TestGame(1);
            int health = 5;
            int harm = 2;
            Player p = new Player(0, "Name", "Descript", health);
            PhaseList ret = p.harm(new HarmPhase(p, null, harm, card), game);
            Assert.IsTrue(ret.isEmpty());

            Assert.AreEqual(health - harm , p.health);
        }

        [TestMethod]
        public void HarmDyingTest()
        {
            Attack card = new Attack(CardSuit.Club, 1);
            IGame game = new TestGame(1);
            int health = 5;
            int harm = 10;
            Player p = new Player(0, "Name", "Descript", health);
            PhaseList ret = p.harm(new HarmPhase(p, null, harm,card), game);

            Phase x = ret.pop();
            Assert.IsInstanceOfType(x, typeof(AskForHelpPhase));
            Assert.IsTrue(ret.isEmpty());

            Assert.AreEqual(health - harm, p.health);
        }

        [TestMethod]
        public void CaoCaoAbtestmock() 
        {
            
            Player p = new CaoCao(1);
            Player p2 = new ZhangFei(2);
            int harm = 1;
            Attack fakeCard = mocks.DynamicMock<Attack>(CardSuit.Club,(byte) 1);
            IGame fakeGame = mocks.DynamicMock<IGame>();
            HarmPhase fakeharm = mocks.DynamicMock<HarmPhase>(p,p2, harm, fakeCard);
            
           
            int old = p.handCards.Count;
            using (mocks.Ordered())
            {
                p.handCards.Add(fakeCard);
            }
            mocks.ReplayAll();
            PhaseList ret = p.harm(new HarmPhase(p, null, harm, fakeCard), fakeGame);
            int newc = p.handCards.Count;
            Assert.IsTrue(old != newc);
        }


        [TestMethod]
        public void ZhangFeiAbtestnomock()
        {
            {
                IGame game = new TestGame(1);
                Player p = new ZhangFei(1);
                Attack card = new Attack(CardSuit.Club, 1);
                ActionPhase acpha = new ActionPhase(p);
                Player[] ls = new Player[1];
                ls[0] = new LiuBei(2);

                AttackPhase pha = new AttackPhase(p,card,ls,acpha);
                pha.actionPhase.attackCount = 1;
                Assert.IsFalse(p.canNotAttack(pha, game));
            }
        }

        [TestMethod]
        public void LiuBeiAbtestnomock()
        {
            {
               Player p=new LiuBei(0);
                Player[] ls = new Player[1];
                ls[0] = new ZhangFei(1);
                Attack card = new Attack(CardSuit.Heart, (byte)1);
                p.handCards.Add(card);
                p.ability(new AbilityAction(card, ls),new TestGame(1));
                Assert.IsTrue(p.handCards.Count == 0);
                Assert.IsTrue(ls[0].handCards.Count == 1);

            }
        }
        [TestMethod]
        public void LiuBeiAbtestnomockBVA()
        {
            {
                Player p = new LiuBei(0);
                Player[] ls = new Player[1];
                ls[0] = new ZhangFei(1);
                Attack card = new Attack(CardSuit.Heart, (byte)1);
                p.handCards.Add(card);
                Assert.IsFalse(p.handCards.Count == 0);
                Assert.IsFalse(ls[0].handCards.Count == 1);

            }
        }

        [TestMethod]
        public void ZhangFeiAbtestnomockBVA()
        {
            {
                IGame game = new TestGame(1);
                Player p = new ZhangFei(1);
                Attack card = new Attack(CardSuit.Club, 1);
                ActionPhase acpha = new ActionPhase(p);
                Player[] ls = new Player[1];
                ls[0] = new LiuBei(2);

                AttackPhase pha = new AttackPhase(p, card, ls, acpha);
                pha.actionPhase.attackCount = 0;
                Assert.IsFalse(p.canNotAttack(pha, game));
               
            }
        }

        [TestMethod]
        public void CaoCaoAbtestnomock()
        {
            {
                Attack card = new Attack(CardSuit.Club, 1);
                IGame game = new TestGame(1);
                int health = 5;
                int harm = 1;
                Player p = new CaoCao(1);
                int old = p.handCards.Count;
                PhaseList ret = p.harm(new HarmPhase(p, null, harm, card), game);
                int newc = p.handCards.Count;


                Assert.IsTrue(old + 1 == newc);
            }
        }
        [TestMethod]
        public void CaoCaoAbtestnomockBVATest()
        {
            Attack card = new Attack(CardSuit.Club, 1);
            IGame game = new TestGame(1);
            int health = 5;
            int harm = 2;
            Player p = new Player(0, "Name", "Descript", health);
            int old = p.handCards.Count;
            PhaseList ret = p.harm(new HarmPhase(p, null, harm, card), game);
            int newc = p.handCards.Count;
            Assert.IsTrue(old == newc);
        }

        public void drawCardTest()
        {
            IGame game = mocks.DynamicMock<IGame>();

            Card x = new Attack(CardSuit.Club, 2);
            Card y = new Attack(CardSuit.Club, 3);
            Card z = new Attack(CardSuit.Club, 4);
            List<Card> cards = new List<Card>();
            cards.Add(x);
            cards.Add(y);
            cards.Add(z);

            using (mocks.Ordered())
            {
                Expect.Call(game.drawCard(3)).Return(cards);
            }

            mocks.ReplayAll();

            Player p = new Player(0);
            Assert.AreEqual(0, p.handCards.Count);

            p.drawCards(3, game);
            Assert.AreEqual(3, p.handCards.Count);
            Assert.IsTrue(p.handCards.Contains(x));
            Assert.IsTrue(p.handCards.Contains(y));
            Assert.IsTrue(p.handCards.Contains(z));

            mocks.VerifyAll();
        }

    }

    internal class TestGame : IGame
    {
        public int Num_Player { get; }
        public Player[] players { get; set; }
        public ICardSet cards { get; set; }
        public Phase curPhase { get; }
        public Player curRoundPlayer { get; }
        public TestGame(int n)
        {
            Num_Player = n;
        }

        public TestGame(int n, params Player[] players) : this(n)
        {
            this.players = players;
        }

        public bool tick()
        {
            throw new NotImplementedException();
        }

        public void nextStage(UserAction yesOrNoAction)
        {
            throw new NotImplementedException();
        }

        public List<Card> drawCard(int v)
        {
            throw new NotImplementedException();
        }

        public Player nextPlayer(int curPlayer, int count)
        {
            return players[(curPlayer + count) % Num_Player];
        }

        public void start()
        {
            throw new NotImplementedException();
        }
    }
}