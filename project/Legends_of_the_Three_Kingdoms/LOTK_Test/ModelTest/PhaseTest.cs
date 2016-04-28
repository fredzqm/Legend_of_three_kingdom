using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Model;
using System;
using System.Collections.Generic;
using Rhino.Mocks;

namespace LOTK_Test.ModelTest
{

    [TestClass]
    public class PhaseTest
    {

        private MockRepository mocks;

        [TestInitialize()]
        public void initialize()
        {
            mocks = new MockRepository();
        }

        [TestMethod]
        public void PlayerFiveAdvancePhaseTest()
        {
            Player p = mocks.Stub<Player>(0);
            Player p2 = mocks.Stub<Player>(0);
            Player p3 = mocks.Stub<Player>(0);
            IGame testgame = mocks.Stub<IGame>();
            PhaseList ls;
            ls = (new PlayerTurn(p)).advance(null, testgame);
            Assert.AreEqual(typeof(JudgePhase), ls.pop().GetType());
            Assert.AreEqual(typeof(PlayerTurn), ls.pop().GetType());
            Assert.IsTrue(ls.isEmpty());

            ls = (new JudgePhase(p)).advance(null, testgame);
            Assert.AreEqual(typeof(DrawingPhase), ls.pop().GetType());
            Assert.AreEqual(typeof(ActionPhase), ls.pop().GetType());
            Assert.IsTrue(ls.isEmpty());
            ls = (new JudgePhase(p)).advance(new YesOrNoAction(true), testgame);
            Assert.AreEqual(typeof(DrawingPhase), ls.pop().GetType());
            Assert.AreEqual(typeof(ActionPhase), ls.pop().GetType());
            Assert.IsTrue(ls.isEmpty());
            ls = (new JudgePhase(p)).advance(new YesOrNoAction(false), testgame);
            Assert.AreEqual(typeof(DrawingPhase), ls.pop().GetType());
            Assert.AreEqual(typeof(ActionPhase), ls.pop().GetType());
            Assert.IsTrue(ls.isEmpty());

            ls = (new DrawingPhase(p)).advance(null, testgame);
            Assert.IsTrue(ls.isEmpty());
            ls = (new DrawingPhase(p)).advance(new YesOrNoAction(true), testgame);
            Assert.IsTrue(ls.isEmpty());
            ls = (new DrawingPhase(p)).advance(new YesOrNoAction(false), testgame);
            Assert.IsTrue(ls.isEmpty());

            ls = (new ActionPhase(p)).advance(null, testgame);
            Assert.IsNull(ls);
            ls = (new ActionPhase(p)).advance(new YesOrNoAction(true), testgame);
            Assert.IsNull(ls);
            ls = (new ActionPhase(p)).advance(new YesOrNoAction(false), testgame);
            Assert.AreEqual(typeof(DiscardPhase), ls.pop().GetType());
            Assert.IsTrue(ls.isEmpty());

            ls = (new DiscardPhase(p)).advance(null, testgame);
            Assert.IsNull(ls); // in the future this will be changed to true
            ls = (new DiscardPhase(p)).advance(new YesOrNoAction(true), testgame);
            Assert.IsTrue(ls.isEmpty());
            ls = (new DiscardPhase(p)).advance(new YesOrNoAction(false), testgame);
            Assert.IsTrue(ls.isEmpty());
        }

        [TestMethod]
        public void UserInputYES_OR_NOTest()
        {
            Player p = mocks.Stub<Player>(0);
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
        public void AttackHitTest()
        {
            Player p1 = mocks.Stub<Player>(0);
            Player p2 = mocks.Stub<Player>(1);
            IGame game = mocks.Stub<IGame>();

            Card attack = new Attack(CardSuit.Spade, 1);

            // ActionPhase produces attackPhase
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
            // AttackPhase produces responsePhase
            ret = b.advance(null, game);
            Phase c = ret.pop();
            Assert.IsInstanceOfType(c, typeof(ResponsePhase));
            ResponsePhase c_ = c as ResponsePhase;
            Assert.AreEqual(p2, c_.player);
            Phase c2 = ret.pop();
            Assert.IsInstanceOfType(c2, typeof(AttackPhase));
            Assert.AreEqual(b, c2);
            Assert.IsTrue(ret.isEmpty());

            // response with cancel
            ret = c.advance(new YesOrNoAction(false), game);
            Assert.IsTrue(ret.isEmpty());

            // attackPhase produces harmPhase
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

        [TestMethod]
        public void AttackMissTest()
        {
            Player p1 = mocks.Stub<Player>(0);
            Player p2 = mocks.Stub<Player>(1);

            IGame game = mocks.Stub<IGame>();

            Card attack = new Attack(CardSuit.Spade, 1);
            Miss miss = new Miss(CardSuit.Diamond, 2);
            // ActionPhase produces attackPhase
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
            // AttackPhase produces responsePhase
            ret = b.advance(null, game);
            Phase c = ret.pop();
            Assert.IsInstanceOfType(c, typeof(ResponsePhase));
            ResponsePhase c_ = c as ResponsePhase;
            Assert.AreEqual(p2, c_.player);
            Phase c2 = ret.pop();
            Assert.IsInstanceOfType(c2, typeof(AttackPhase));
            Assert.AreEqual(b, c2);
            Assert.IsTrue(ret.isEmpty());

            // response with cancel
            ret = c.advance(new CardAction(miss), game);
            Assert.IsTrue(ret.isEmpty());

            // attackPhase produces nothing
            ret = c2.advance(null, game);
            Assert.IsTrue(ret.isEmpty());

        }


        [TestMethod]
        public void DyingAskForHelpTest()
        {
            Player dying = new Player(0, "dying", "dying", 1);
            Player source = mocks.Stub<Player>(1);
            Player player2 = mocks.Stub<Player>(2);
            Player player3 = mocks.Stub<Player>(3);
            Player[] players = new Player[4];
            Phase p1, p2, p3;
            PhaseList ls;
            players[0] = dying;
            players[1] = source;
            players[2] = player2;
            players[3] = player3;

            Attack card = new Attack(CardSuit.Club, 1);
            Assert.AreEqual(2, dying.healthLimit);

            IGame game = MockRepository.GenerateStub<IGame>();
            game.Stub(x => x.players).Return(players);
            game.Stub(x => x.curRoundPlayer).Return(source);

            HarmPhase harm = new HarmPhase(dying, source, 1, card);
            ls = harm.advance(null, game);
            p1 = ls.pop();
            Assert.IsInstanceOfType(p1, typeof(AskForHelpPhase));
            
            ls = p1.advance(null, game);
            p2 = ls.pop();
            Assert.IsInstanceOfType(p2, typeof(ResponsePhase));
            Assert.AreEqual(source, p2.player);

            // ActionPhase produces attackPhase
        }


        [TestMethod]
        public void responseCardActionTest()
        {
            Player p1 = mocks.Stub<Player>(0);

            IGame game = mocks.Stub<IGame>();

            Card attack = new Attack(CardSuit.Spade, 1);
            Miss miss = new Miss(CardSuit.Diamond, 2);
            // ActionPhase produces attackPhase
            UserActionPhase a = new DiscardPhase(p1);

            Assert.IsInstanceOfType(a.responseCardAction(attack, game), typeof(PhaseList));

        }


        [TestMethod]
        public void autoAdvanceTest()
        {
            ZhangFei p1 = new ZhangFei(0);
            Card attack = new Attack(CardSuit.Spade, 1);
            Miss miss = new Miss(CardSuit.Diamond, 2);
            p1.handCards.Add(attack);
            p1.handCards.Add(miss);
            p1.health = 1;

            IGame game = mocks.Stub<IGame>();


            // ActionPhase produces attackPhase
            UserActionPhase a = new DiscardPhase(p1);

            Assert.IsInstanceOfType(a.autoAdvance(game), typeof(PhaseList));

        }

        [TestMethod]
        public void autoAdvanceTest2()
        {
            ZhangFei p1 = new ZhangFei(0);
            Card attack = new Attack(CardSuit.Spade, 1);
            Miss miss = new Miss(CardSuit.Diamond, 2);
            p1.handCards.Add(attack);
            p1.handCards.Add(miss);
            p1.health = 1;

            IGame game = mocks.Stub<IGame>();


            // ActionPhase produces attackPhase
            UserActionPhase a = new ActionPhase(p1);

            Assert.AreEqual(a.autoAdvance(game), null);

        }

        [TestMethod]
        public void resAdvanceTest3()
        {
            ZhangFei p1 = new ZhangFei(0);
            Card attack = new Attack(CardSuit.Spade, 1);
            Miss miss = new Miss(CardSuit.Diamond, 2);
            p1.handCards.Add(attack);
            p1.handCards.Add(miss);
            p1.health = 1;

            IGame game = mocks.Stub<IGame>();


            // ActionPhase produces attackPhase
            UserActionPhase a = new ActionPhase(p1);

            Assert.AreEqual(a.timeOutAdvance(game), null);

        }

        [TestMethod]
        public void resAdvanceTest4()
        {
            Player p1 = new Player(0);
            Card attack = new Attack(CardSuit.Spade, 1);
            Miss miss = new Miss(CardSuit.Diamond, 2);
            p1.handCards.Add(attack);
            p1.handCards.Add(miss);
            p1.health = 1;

            IGame game = mocks.Stub<IGame>();


            // ActionPhase produces attackPhase
            UserActionPhase a = new Testuserphase(p1,1);

            Assert.AreEqual(a.responseYesOrNo(true,game), null);

        }

        [TestMethod]
        public void resAdvanceTest5()
        {
            Player p1 = new Player(0);
            Card attack = new Attack(CardSuit.Spade, 1);
            Miss miss = new Miss(CardSuit.Diamond, 2);
            p1.handCards.Add(attack);
            p1.handCards.Add(miss);
            p1.health = 1;

            IGame game = mocks.Stub<IGame>();


            // ActionPhase produces attackPhase
            UserActionPhase a = new DiscardPhase(p1);
            Player[] p = new Player[1];
            p[0] = p1;
            Assert.AreEqual(a.responseUseCardAction(miss,p,game), null);

        }
        [TestMethod]
        public void resAdvanceTest6()
        {
            Player p1 = new Player(0);
            Card attack = new Attack(CardSuit.Spade, 1);
            Miss miss = new Miss(CardSuit.Diamond, 2);
            p1.handCards.Add(attack);
            p1.handCards.Add(miss);
            p1.health = 1;

            IGame game = mocks.Stub<IGame>();


            // ActionPhase produces attackPhase
            UserActionPhase a = new ActionPhase(p1);
            Player[] p = new Player[1];
            p[0] = p1;
            Assert.AreEqual(a.responseCardAction(miss, game), null);

        }
        [TestMethod]
        public void VisiblePhasetest()
        {

            Player p1 = new Player(0);
            Card attack = new Attack(CardSuit.Spade, 1);
            Miss miss = new Miss(CardSuit.Diamond, 2);
            p1.handCards.Add(attack);
            p1.handCards.Add(miss);
            p1.health = 1;

            IGame game = mocks.Stub<IGame>();
            Player[] p = new Player[1];
            p[0] = p1;

            // ActionPhase produces attackPhase
            UserActionPhase a = new DiscardPhase(p1);
            Assert.AreEqual(a.responseAbilityAction(new AbilityAction(new Attack(CardSuit.Spade,(byte) 1) ,p), game),null);

        }

  

        [TestMethod]
        public void VisiblePhaseSuntest()
        {

            Player p1 = new Player(0);
            Card attack = new Attack(CardSuit.Spade, 1);
            Miss miss = new Miss(CardSuit.Diamond, 2);
            p1.handCards.Add(attack);
            p1.handCards.Add(miss);
            p1.health = 1;

            IGame game = mocks.Stub<IGame>();
            Player[] p = new Player[1];
            p[0] = p1;

            // ActionPhase produces attackPhase
            UserActionPhase a = new DiscardPhase(p1);
            Assert.AreEqual(a.responseAbilityActionSun(new AbilityActionSun(new Attack(CardSuit.Spade, (byte)1)), game), null);

        }


        [TestMethod]
        public void ResPhasetest()
        {
            Player p1 = mocks.Stub<Player>(0);
            Player p2 = mocks.Stub<Player>(1);
            IGame game = mocks.Stub<IGame>();

            Card attack = new Attack(CardSuit.Spade, 1);
            
            Phase a = new ActionPhase(p1);
            PhaseList ret = a.advance(new UseCardAction(attack, p2), game);
            Phase b = ret.pop();
            AttackPhase b2 = b as AttackPhase;
            Miss miss = new Miss(CardSuit.Diamond, 2);
            p1.handCards.Add(attack);
            p1.handCards.Add(miss);
            p1.health = 1;

            Player[] p = new Player[1];
            p[0] = p1;
            ret = b.advance(null, game);
            Phase c = ret.pop();
            ResponsePhase c_ = c as ResponsePhase;

            Assert.AreEqual(c_.responseYesOrNo(true, game), null);

        }

        [TestMethod]
        public void Res2Phasetest()
        {
            Player p1 = mocks.Stub<Player>(0);
            Player p2 = mocks.Stub<Player>(1);
            IGame game = mocks.Stub<IGame>();

            Card attack = new Attack(CardSuit.Spade, 1);

            // ActionPhase produces attackPhase
            Phase a = new ActionPhase(p1);
            PhaseList ret = a.advance(new UseCardAction(attack, p2), game);
            Phase b = ret.pop();
            AttackPhase b2 = b as AttackPhase;
            Miss miss = new Miss(CardSuit.Diamond, 2);
            p1.handCards.Add(attack);
            p1.handCards.Add(miss);
            p1.health = 1;

            Player[] p = new Player[1];
            p[0] = p1;
            // AttackPhase produces responsePhase
            ret = b.advance(null, game);
            Phase c = ret.pop();
            ResponsePhase c_ = c as ResponsePhase;

            
            Assert.AreEqual(c_.responseCardAction(new Attack(CardSuit.Club,(byte)1), game), null);

        }

        [TestMethod]
        public void Res4Phasetest()
        {
            LiuBei p1 = new LiuBei(0);
            SunQuan p2 = new SunQuan(1);
            IGame game = mocks.Stub<IGame>();

            Card attack = new Attack(CardSuit.Spade, 1);

            // ActionPhase produces attackPhase
           ActionPhase a = new ActionPhase(p1);
            PhaseList ret = a.advance(new UseCardAction(attack, p2), game);
            Phase b = ret.pop();
            AttackPhase b2 = b as AttackPhase;
            Miss miss = new Miss(CardSuit.Diamond, 2);
            p1.handCards.Add(attack);
            p1.handCards.Add(miss);
            p1.health = 1;

            Player[] p = new LiuBei[1];
            p[0] = p1;
            // AttackPhase produces responsePhase
            ret = b.advance(null, game);
            Phase c = ret.pop();
            ResponsePhase c_ = c as ResponsePhase;


            Assert.IsInstanceOfType(a.responseAbilityAction(new AbilityAction(attack,p), game), typeof(PhaseList));

        }

        [TestMethod]
        public void Res5Phasetest()
        {
            SunQuan p1 = new SunQuan(0);
            LiuBei p2 = new LiuBei(1);
            List<Card> x = new List<Card>();

            Player[] p = new SunQuan[1];
            p[0] = p1;
            Miss miss = new Miss(CardSuit.Diamond, 2);
            Card attack = new Attack(CardSuit.Spade, 1);
            x.Add(miss);
            x.Add(attack);
            Game game = new Game(p, new CardSet(x));

        

            // ActionPhase produces attackPhase
            ActionPhase a = new ActionPhase(p1);
            PhaseList ret = a.advance(new UseCardAction(attack, p2), game);
            Phase b = ret.pop();
            AttackPhase b2 = b as AttackPhase;
           
            p1.handCards.Add(attack);
            p1.handCards.Add(miss);
            p1.health = 1;

            // AttackPhase produces responsePhase
            ret = b.advance(null, game);
            Phase c = ret.pop();
            ResponsePhase c_ = c as ResponsePhase;


            Assert.IsInstanceOfType(a.responseAbilityActionSun(new AbilityActionSun(attack), game), typeof(PhaseList));

        }

        [TestMethod]
        public void Res3Phasetest()
        {

        
      

            // ActionPhase produces attackPhase

            Player p1 = mocks.Stub<Player>(0);
            Player p2 = mocks.Stub<Player>(1);
            IGame game = mocks.Stub<IGame>();

            Card attack = new Attack(CardSuit.Spade, 1);

            // ActionPhase produces attackPhase
            ActionPhase a = new ActionPhase(p1);
            PhaseList ret = a.advance(new UseCardAction(attack, p2), game);
            Phase b = ret.pop();
            AttackPhase b2 = b as AttackPhase;
            Miss miss = new Miss(CardSuit.Diamond, 2);
            p1.handCards.Add(attack);
            p1.handCards.Add(miss);
            p1.health = 1;

            Player[] p = new Player[1];
            p[0] = p1;
            // AttackPhase produces responsePhase
            ret = b.advance(null, game);
            Phase c = ret.pop();
            ResponsePhase c_ = c as ResponsePhase;
            
            Assert.AreEqual(c_.ToString(), "Response Phase of 1");

        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void resPhasetest()
        {

            VisiblePhase p = new Testvisphase(new Player(1), -1);

        }

        [TestMethod]
        public void playerturntest()
        {


            Player p = mocks.Stub<Player>(0);
            Player p2 = mocks.Stub<Player>(0);
            Player p3 = mocks.Stub<Player>(0);
            IGame testgame = mocks.Stub<IGame>();
            PhaseList ls;
            ls = (new PlayerTurn(p)).advance(null, testgame);
            Assert.AreEqual(new PlayerTurn(p).ToString(), "Plyaer " + 0 + " at PlayerTurn");
        }



        public class Testvisphase : PausePhase
        {
            public Testvisphase(Player p, int i) : base(p, i) { }

            public override PhaseList advance(IGame game)
            {
                throw new NotImplementedException();
            }
        }

        public class Testuserphase : UserActionPhase
        {
            public Testuserphase(Player p, int i) : base(p, i) { }

          
        }

    }
}