using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Model;
using System;
using System.Collections.Generic;

namespace LOTK_Test.ModelTest
{

    [TestClass]
    public class PhaseTest
    {
        [TestMethod]
        public void PlayerFiveAdvancePhaseTest()
        {
            Player p = new Player(0, "Player Name", "Player Description");
            Player p2 = new Player(0, "Player Name1", "Player Description1");
            Player p3 = new Player(0, "Player Name2", "Player Description2");
            IGame testgame = new TestGame(5, p, p2, p3);
            PhaseList ls;
            ls = (new PlayerTurn(p)).advance( null, testgame);
            Assert.AreEqual(typeof(JudgePhase), ls.pop().GetType());
            Assert.AreEqual(typeof(PlayerTurn), ls.pop().GetType());
            Assert.IsTrue(ls.isEmpty());

            ls = (new JudgePhase(p)).advance( null, testgame);
            Assert.AreEqual(typeof(DrawingPhase), ls.pop().GetType());
            Assert.AreEqual(typeof(ActionPhase), ls.pop().GetType());
            Assert.IsTrue(ls.isEmpty());
            ls = (new JudgePhase(p)).advance( new YesOrNoAction(true), testgame);
            Assert.AreEqual(typeof(DrawingPhase), ls.pop().GetType());
            Assert.AreEqual(typeof(ActionPhase), ls.pop().GetType());
            Assert.IsTrue(ls.isEmpty());
            ls = (new JudgePhase(p)).advance( new YesOrNoAction(false), testgame);
            Assert.AreEqual(typeof(DrawingPhase), ls.pop().GetType());
            Assert.AreEqual(typeof(ActionPhase), ls.pop().GetType());
            Assert.IsTrue(ls.isEmpty());

            ls = (new DrawingPhase(p)).advance( null, testgame);
            Assert.IsTrue(ls.isEmpty());
            ls = (new DrawingPhase(p)).advance( new YesOrNoAction(true), testgame);
            Assert.IsTrue(ls.isEmpty());
            ls = (new DrawingPhase(p)).advance( new YesOrNoAction(false), testgame);
            Assert.IsTrue(ls.isEmpty());

            ls = (new ActionPhase(p)).advance( null, testgame);
            Assert.IsNull(ls);
            ls = (new ActionPhase(p)).advance( new YesOrNoAction(true), testgame);
            Assert.IsNull(ls);
            ls = (new ActionPhase(p)).advance( new YesOrNoAction(false), testgame);
            Assert.AreEqual(typeof(DiscardPhase), ls.pop().GetType());
            Assert.IsTrue(ls.isEmpty());

            ls = (new DiscardPhase(p)).advance( null, testgame);
            Assert.IsNull(ls); // in the future this will be changed to true
            ls = (new DiscardPhase(p)).advance( new YesOrNoAction(true), testgame);
            Assert.IsTrue(ls.isEmpty());
            ls = (new DiscardPhase(p)).advance( new YesOrNoAction(false), testgame);
            Assert.IsTrue(ls.isEmpty());
        }

        [TestMethod]
        public void UserInputYES_OR_NOTest()
        {
            Player p = new Player(0, "Player Name", "Player Description");
            Assert.IsTrue((new JudgePhase(p)).advance(
                new YesOrNoAction(false), null) != null);
            Assert.IsTrue((new JudgePhase(p)).advance(
                new YesOrNoAction(true), null) != null);
            Assert.IsTrue((new DrawingPhase(p)).advance(
                new YesOrNoAction(false), null) != null);
            Assert.IsTrue((new DrawingPhase(p)).advance(
                new YesOrNoAction(true), null) != null);
            Assert.IsTrue((new ActionPhase(p)).advance(
                new YesOrNoAction(false), null) != null);
            Assert.IsFalse((new ActionPhase(p)).advance(
                new YesOrNoAction(true), null) != null);
            Assert.IsTrue((new DiscardPhase(p)).advance(
                new YesOrNoAction(false), null) != null);
            Assert.IsTrue((new DiscardPhase(p)).advance(
                new YesOrNoAction(true), null) != null);
        }

        [TestMethod]
        public void AttackTest()
        {
            Player p1 = new Player(0, "Player Name", "Player Description");
            Player p2 = new Player(1, "Player Name1", "Player Description1");
            Player p3 = new Player(2, "Player Name2", "Player Description2");
            IGame game = new TestGame(5, p1, p2, p3);

            Card attack = new Attack(CardSuit.Spade, 1);
            Phase a = new ActionPhase(p1);
            PhaseList ret = a.advance(new CardAction(attack, p2), game);
            Phase b = ret.pop();
            Assert.IsInstanceOfType( b , typeof(AttackPhase));
            AttackPhase b2 = b as AttackPhase;
            Assert.AreEqual(attack , b2.attack);
            Assert.AreEqual(a, b2.actionPhase);
            Assert.AreEqual(p1, b2.player);
            Assert.AreEqual(p2, b2.targets[1]);

        }
    }
}
