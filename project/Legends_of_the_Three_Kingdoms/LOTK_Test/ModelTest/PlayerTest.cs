using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Model;
using System.Collections.Generic;
using Rhino.Mocks;

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
        public void initialize()
        {
            mocks = new MockRepository();
        }


        [TestMethod]
        public void HarmTest()
        {
            IGame game = new TestGame(1);
            int health = 5;
            int harm = 2;
            Player p = new Player(0, "Name", "Descript", health);
            PhaseList ret = p.harm(new HarmPhase(p, null, harm), game);
            Assert.IsTrue(ret.isEmpty());

            Assert.AreEqual(health - harm , p.health);
        }

        [TestMethod]
        public void HarmDyingTest()
        {
            IGame game = new TestGame(1);
            int health = 5;
            int harm = 10;
            Player p = new Player(0, "Name", "Descript", health);
            PhaseList ret = p.harm(new HarmPhase(p, null, harm), game);

            Phase x = ret.pop();
            Assert.IsInstanceOfType(x, typeof(AskForHelpPhase));
            Assert.IsTrue(ret.isEmpty());

            Assert.AreEqual(health - harm, p.health);
        }

        [TestMethod]
        public void drawCardTest()
        {
            IGame game = mocks.DynamicMock<IGame>();

            Card x = mocks.Stub<Card>();
            Card y = mocks.Stub<Card>();
            Card z = mocks.Stub<Card>();
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
        public CardSet cards { get; set; }
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