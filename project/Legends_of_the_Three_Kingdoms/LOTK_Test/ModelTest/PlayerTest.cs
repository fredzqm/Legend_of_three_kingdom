using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Model;
using System.Collections.Generic;
using Legends_of_the_Three_Kingdoms.Model;

namespace LOTK_Test.ModelTest
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void PlayerConstructTest()
        {
            Player p = new Player(0, "Player Name", "Player Description");
        }

        [TestMethod]
        public void PlayerFiveBasicPhaseTest()
        {
            Player p = new Player(0, "Player Name", "Player Description");
            IGame testgame = new TestGame(5);
            PhaseList ls;
            ls = p.playerTurn(new PlayerTurn(0), testgame);
            Assert.AreEqual( typeof(JudgePhase) , ls.pop().GetType());
            Assert.AreEqual( typeof(PlayerTurn) , ls.pop().GetType());
            Assert.IsTrue(ls.isEmpty());

            ls = p.judgePhase(new JudgePhase(0), null, testgame);
            Assert.AreEqual( typeof(DrawingPhase) , ls.pop().GetType());
            Assert.AreEqual( typeof(ActionPhase) , ls.pop().GetType());
            Assert.IsTrue(ls.isEmpty());

            ls = p.drawingPhase(new DrawingPhase(0), null, testgame);
            Assert.IsTrue(ls.isEmpty());

            ls = p.actionPhase(new ActionPhase(0), null, testgame);
            Assert.AreEqual( typeof(DiscardPhase) , ls.pop().GetType());
            Assert.IsTrue(ls.isEmpty());

            ls = p.discardPhase(new DiscardPhase(0), null, testgame);
            Assert.IsTrue(ls.isEmpty());

        }

        [TestMethod]
        public void PlayerFiveAdvancePhaseTest()
        {
            Player p = new Player(0, "Player Name", "Player Description");
            IGame testgame = new TestGame(5);
            PhaseList ls;
            ls = p.playerTurn(new PlayerTurn(0), testgame);
            Assert.AreEqual( typeof(JudgePhase) , ls.pop().GetType());
            Assert.AreEqual( typeof(PlayerTurn) , ls.pop().GetType());
            Assert.IsTrue(ls.isEmpty());

            ls = p.judgePhase(new JudgePhase(0), null, testgame);
            Assert.AreEqual( typeof(DrawingPhase) , ls.pop().GetType());
            Assert.AreEqual( typeof(ActionPhase) , ls.pop().GetType());
            Assert.IsTrue(ls.isEmpty());

            ls = p.drawingPhase(new DrawingPhase(0), null, testgame);
            Assert.IsTrue(ls.isEmpty());

            ls = p.actionPhase(new ActionPhase(0), null, testgame);
            Assert.AreEqual(typeof(DiscardPhase), ls.pop().GetType());
            Assert.IsTrue(ls.isEmpty());

            ls = p.discardPhase(new DiscardPhase(0), null, testgame);
            Assert.IsTrue(ls.isEmpty());
            try { ls.pop(); }
            catch (EmptyException e)
            {
                Console.WriteLine("Empty Exception caught.", e);
            }

        }

        [TestMethod]
        public void UserInputYES_OR_NOTest()
        {
            Player g = new Player(0, "Player Name", "Player Description");
            Assert.IsTrue(g.judgePhase(new JudgePhase(0), 
                new UserAction(UserActionType.YES_OR_NO, 0), null) != null);
            Assert.IsTrue(g.judgePhase(new JudgePhase(0),
                new UserAction(UserActionType.YES_OR_NO, 1), null) != null);
            Assert.IsTrue(g.drawingPhase(new DrawingPhase(0),
                new UserAction(UserActionType.YES_OR_NO, 0), null) != null);
            Assert.IsTrue(g.drawingPhase(new DrawingPhase(0),
                new UserAction(UserActionType.YES_OR_NO, 1), null) != null);
            Assert.IsTrue(g.actionPhase(new ActionPhase(0),
                new UserAction(UserActionType.YES_OR_NO, 0), null) != null);
            Assert.IsFalse(g.actionPhase(new ActionPhase(0),
                new UserAction(UserActionType.YES_OR_NO, 1), null) != null);
            Assert.IsTrue(g.discardPhase(new DiscardPhase(0),
                new UserAction(UserActionType.YES_OR_NO, 0), null) != null);
            Assert.IsTrue(g.discardPhase(new DiscardPhase(0),
                new UserAction(UserActionType.YES_OR_NO, 1), null) != null);
        }

        [TestMethod]
        public void handlePhaseTest()
        {
            Player g = new Player(0, "Player Name", "Player Description");

        }


        internal class TestGame : IGame
        {
            public int Num_Player { get; }

            public Player[] players
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public CardSet cards
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public TestGame(int n)
            {
                Num_Player = n;
            }
        }
    }
}