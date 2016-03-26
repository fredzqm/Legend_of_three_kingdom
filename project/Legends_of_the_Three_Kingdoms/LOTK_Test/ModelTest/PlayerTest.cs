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
        public void PlayerFiveAdvancePhaseTest()
        {
            Player p = new Player(0, "Player Name", "Player Description");
            IGame testgame = new TestGame(5);
            PhaseList ls;
            ls = p.playerTurn(new PlayerTurn(p), testgame);
            Assert.AreEqual( typeof(JudgePhase) , ls.pop().GetType());
            Assert.AreEqual( typeof(PlayerTurn) , ls.pop().GetType());
            Assert.IsTrue(ls.isEmpty());

            ls = p.judgePhase(new JudgePhase(p), null, testgame);
            Assert.AreEqual( typeof(DrawingPhase) , ls.pop().GetType());
            Assert.AreEqual( typeof(ActionPhase) , ls.pop().GetType());
            Assert.IsTrue(ls.isEmpty());
            ls = p.judgePhase(new JudgePhase(p), new YesOrNoAction(true), testgame);
            Assert.AreEqual(typeof(DrawingPhase), ls.pop().GetType());
            Assert.AreEqual(typeof(ActionPhase), ls.pop().GetType());
            Assert.IsTrue(ls.isEmpty());
            ls = p.judgePhase(new JudgePhase(p), new YesOrNoAction(false), testgame);
            Assert.AreEqual(typeof(DrawingPhase), ls.pop().GetType());
            Assert.AreEqual(typeof(ActionPhase), ls.pop().GetType());
            Assert.IsTrue(ls.isEmpty());

            ls = p.drawingPhase(new DrawingPhase(p), null, testgame);
            Assert.IsTrue(ls.isEmpty());
            ls = p.drawingPhase(new DrawingPhase(p), new YesOrNoAction(true), testgame);
            Assert.IsTrue(ls.isEmpty());
            ls = p.drawingPhase(new DrawingPhase(p), new YesOrNoAction(false), testgame);
            Assert.IsTrue(ls.isEmpty());

            ls = p.actionPhase(new ActionPhase(p), null, testgame);
            Assert.IsNull(ls);
            ls = p.actionPhase(new ActionPhase(p), new YesOrNoAction(true), testgame);
            Assert.IsNull(ls);
            ls = p.actionPhase(new ActionPhase(p), new YesOrNoAction(false), testgame);
            Assert.AreEqual(typeof(DiscardPhase), ls.pop().GetType());
            Assert.IsTrue(ls.isEmpty());

            ls = p.discardPhase(new DiscardPhase(p), null, testgame);
            Assert.IsNull(ls); // in the future this will be changed to true
            ls = p.discardPhase(new DiscardPhase(p), new YesOrNoAction(true), testgame);
            Assert.IsTrue(ls.isEmpty());
            ls = p.discardPhase(new DiscardPhase(p), new YesOrNoAction(false), testgame);
            Assert.IsTrue(ls.isEmpty());
        }

        [TestMethod]
        public void UserInputYES_OR_NOTest()
        {
            Player p = new Player(0, "Player Name", "Player Description");
            Assert.IsTrue(p.judgePhase(new JudgePhase(p), 
                new YesOrNoAction(false), null) != null);
            Assert.IsTrue(p.judgePhase(new JudgePhase(p),
                new YesOrNoAction(true), null) != null);
            Assert.IsTrue(p.drawingPhase(new DrawingPhase(p),
                new YesOrNoAction(false), null) != null);
            Assert.IsTrue(p.drawingPhase(new DrawingPhase(p),
                new YesOrNoAction(true), null) != null);
            Assert.IsTrue(p.actionPhase(new ActionPhase(p),
                new YesOrNoAction(false), null) != null);
            Assert.IsFalse(p.actionPhase(new ActionPhase(p),
                new YesOrNoAction(true), null) != null);
            Assert.IsTrue(p.discardPhase(new DiscardPhase(p),
                new YesOrNoAction(false), null) != null);
            Assert.IsTrue(p.discardPhase(new DiscardPhase(p),
                new YesOrNoAction(true), null) != null);
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