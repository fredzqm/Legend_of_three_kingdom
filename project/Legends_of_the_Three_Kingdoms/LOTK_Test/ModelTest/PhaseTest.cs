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
            Player p = new Player(0);
            Player p2 = new Player(0);
            Player p3 = new Player(0);
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
            Player p = new Player(0);
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

        //[TestMethod]
        //public void AttackMissedTest()
        //{
        //    Player p1 = new Player(0);
        //    Player p2 = new Player(1);
        //    Player p3 = new Player(2);
        //    IGame game = new TestGame(5, p1, p2, p3);
        //    Card attack = new Attack(CardSuit.Spade, 1);
        //    Miss miss = new Miss(CardSuit.Diamond, 2);

        //    Phase a = new ActionPhase(p1);
        //    PhaseList ret = a.advance(new UseCardAction(attack, p2), game);
        //    Phase b = ret.pop();
        //    Assert.IsInstanceOfType( b , typeof(AttackPhase));
        //    AttackPhase b2 = b as AttackPhase;
        //    Assert.AreEqual(attack , b2.attack);
        //    Assert.AreEqual(a, b2.actionPhase);
        //    Assert.AreEqual(p1, b2.player);
        //    Assert.AreEqual(p2, b2.targets[0]);
        //    Assert.AreEqual(a, ret.pop());
        //    Assert.IsTrue(ret.isEmpty());

        //    ret = b.advance(null, game);
        //    Phase c = ret.pop();
        //    Assert.IsInstanceOfType(c, typeof(responsePhase));
        //    responsePhase c2 = c as responsePhase;
        //    Assert.AreEqual(p2, c2.player);
        //    Assert.IsInstanceOfType(ret.pop(), typeof(AttackPhase));
        //    Assert.IsTrue(ret.isEmpty());

        //    ret = c.advance(new CardAction(miss), game);
        //    Assert.IsTrue(ret.isEmpty());
        //}

        [TestMethod]
        public void AttackHitTest()
        {
            Player p1 = new Player(0);
            Player p2 = new Player(1);
            Player p3 = new Player(2);
            IGame game = new TestGame(5, p1, p2, p3);
            Card attack = new Attack(CardSuit.Spade, 1);
            Miss miss = new Miss(CardSuit.Diamond, 2);

            Phase a = new ActionPhase(p1);
            PhaseList ret = a.advance(new UseCardAction(attack, p2), game);
            Phase b = ret.pop();
            Assert.IsInstanceOfType(b, typeof(AttackPhase));
            AttackPhase b2 = b as AttackPhase;
            Assert.AreEqual(attack, b2.attack);
            Assert.AreEqual(a, b2.actionPhase);
            Assert.AreEqual(p1, b2.player);
            Assert.AreEqual(p2, b2.targets[0]);
            Assert.AreEqual(a, ret.pop());
            Assert.IsTrue(ret.isEmpty());

            ret = b.advance(null, game);
            Phase c = ret.pop();
            Assert.IsInstanceOfType(c, typeof(responsePhase));
            responsePhase c_ = c as responsePhase;
            Assert.AreEqual(p2, c_.player);
            Phase c2 = ret.pop();
            Assert.IsInstanceOfType(c2, typeof(AttackPhase));
            AttackPhase c2_ = c2 as AttackPhase;
            Assert.AreEqual(b, c2);
            Assert.IsTrue(ret.isEmpty());

            ret = c.advance(new YesOrNoAction(false), game);
            Assert.IsTrue(ret.isEmpty());

            ret = c2.advance(null, game);
            Phase d = ret.pop();
            Assert.IsInstanceOfType(d, typeof(HarmPhase));
            HarmPhase d2 = d as HarmPhase;
            Assert.AreEqual(p2, d2.player);
            Assert.AreEqual(p1, d2.source);
            Assert.AreEqual(1, d2.harm);
            Assert.IsTrue(ret.isEmpty());

            ret = d.advance(null, game);
            Assert.IsTrue(ret.isEmpty());
        }
    }
}
