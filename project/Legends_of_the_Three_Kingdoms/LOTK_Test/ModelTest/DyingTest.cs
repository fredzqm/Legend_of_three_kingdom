using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Model;
using Rhino.Mocks;

namespace LOTK_Test.ModelTest
{
    [TestClass]
    public class DyingTest
    {
        private MockRepository mocks;

        [TestInitialize()]
        public void initialize()
        {
            mocks = new MockRepository();
        }

        [TestMethod]
        public void DyingAskForHelpAcceptTest1_2_3()
        {
            Player dying = new Player(0, "dying", "dying", 1);
            Player source = mocks.Stub<Player>(1);
            Player player2 = mocks.Stub<Player>(2);
            Player player3 = mocks.Stub<Player>(3);
            Player[] players = new Player[4];
            Phase p1, p2, p3, p4, p5;
            PhaseList ls;
            players[0] = dying;
            players[1] = source;
            players[2] = player2;
            players[3] = player3;

            Attack card = new Attack(CardSuit.Club, 1);
            Peach peach = new Peach(CardSuit.Heart, 2);
            Assert.AreEqual(1, dying.healthLimit);

            IGame game = MockRepository.GenerateStub<IGame>();
            game.Stub(x => x.players).Return(players);
            game.Stub(x => x.nextPlayer(source, 0)).Return(source);
            game.Stub(x => x.nextPlayer(source, 1)).Return(player2);
            game.Stub(x => x.nextPlayer(source, 2)).Return(player3);
            game.Stub(x => x.nextPlayer(source, 3)).Return(dying);
            game.Stub(x => x.curRoundPlayer).Return(source);
            game.Stub(s => s.Num_Player).Return(4);

            HarmPhase harm = new HarmPhase(dying, source, 1, card);
            ls = harm.advance(null, game);
            p1 = ls.pop();
            Assert.IsInstanceOfType(p1, typeof(AskForHelpPhase));
            Assert.IsTrue(ls.isEmpty());

            ls = p1.advance(null, game);
            p2 = ls.pop();
            p3 = ls.pop();
            Assert.IsTrue(ls.isEmpty());
            Assert.IsInstanceOfType(p2, typeof(ResponsePhase));
            Assert.AreEqual(source, p2.player);
            Assert.AreSame(p1, p3);

            ls = p2.advance(new CardAction(peach), game);
            Assert.IsTrue(ls.isEmpty());

            ls = p3.advance(null, game);
            p4 = ls.pop();
            p5 = ls.pop();
            Assert.IsTrue(ls.isEmpty());
            Assert.IsInstanceOfType(p4, typeof(RecoverPhase));
            Assert.AreEqual(1, ((RecoverPhase)p4).recover);
            Assert.AreSame(p3, p5);

            Assert.AreEqual(0, dying.health);
            ls = p4.advance(null, game);
            Assert.IsTrue(ls.isEmpty());
            Assert.AreEqual(1, dying.health);

            ls = p5.advance(null, game);
            Assert.IsTrue(ls.isEmpty());
            // ActionPhase produces attackPhase
        }

        [TestMethod]
        public void DyingAskForHelpRejectTest()
        {
            Player dying = new Player(0, "dying", "dying", 1);
            Player source = mocks.Stub<Player>(1);
            Player player2 = mocks.Stub<Player>(2);
            Player player3 = mocks.Stub<Player>(3);
            Player[] players = new Player[4];
            Phase p1, p2, p3, p4, p5;
            PhaseList ls;
            players[0] = dying;
            players[1] = source;
            players[2] = player2;
            players[3] = player3;

            Attack card = new Attack(CardSuit.Club, 1);
            Wine wine = new Wine(CardSuit.Heart, 2);
            Assert.AreEqual(1, dying.healthLimit);
            ICardSet cardStack = MockRepository.GenerateStub<ICardSet>();

            IGame game = MockRepository.GenerateStub<IGame>();
            game.Stub(x => x.players).Return(players);
            game.Stub(x => x.nextPlayer(source, 0)).Return(source);
            game.Stub(x => x.nextPlayer(source, 1)).Return(player2);
            game.Stub(x => x.nextPlayer(source, 2)).Return(player3);
            game.Stub(x => x.nextPlayer(source, 3)).Return(dying);
            game.Stub(x => x.curRoundPlayer).Return(source);
            game.Stub(s => s.cards).Return(cardStack);
            game.Stub(s => s.Num_Player).Return(4);

            HarmPhase harm = new HarmPhase(dying, source, 1, card);
            ls = harm.advance(null, game);
            p1 = ls.pop();
            Assert.IsInstanceOfType(p1, typeof(AskForHelpPhase));
            Assert.IsTrue(ls.isEmpty());

            ls = p1.advance(null, game);
            p2 = ls.pop();
            p3 = ls.pop();
            Assert.IsTrue(ls.isEmpty());
            Assert.IsInstanceOfType(p2, typeof(ResponsePhase));
            Assert.AreEqual(source, p2.player);
            Assert.AreSame(p1, p3);

            ls = p2.advance(new YesOrNoAction(false), game);
            Assert.IsTrue(ls.isEmpty());

            ls = p3.advance(null, game);
            p4 = ls.pop();
            p5 = ls.pop();
            Assert.IsTrue(ls.isEmpty());
            Assert.IsInstanceOfType(p4, typeof(ResponsePhase));
            Assert.AreEqual(player2, p4.player);
            Assert.AreSame(p1, p5);

            ls = p4.advance(new YesOrNoAction(false), game);
            Assert.IsTrue(ls.isEmpty());

            ls = p5.advance(null, game);
            p4 = ls.pop();
            p5 = ls.pop();
            Assert.IsTrue(ls.isEmpty());
            Assert.IsInstanceOfType(p4, typeof(ResponsePhase));
            Assert.AreEqual(player3, p4.player);
            Assert.AreSame(p1, p5);

            ls = p4.advance(new YesOrNoAction(false), game);
            Assert.IsTrue(ls.isEmpty());

            ls = p5.advance(null, game);
            p4 = ls.pop();
            p5 = ls.pop();
            Assert.IsTrue(ls.isEmpty());
            Assert.IsInstanceOfType(p4, typeof(ResponsePhase));
            Assert.AreEqual(dying, p4.player);
            Assert.AreSame(p1, p5);

            ls = p4.advance(new CardAction(wine), game);
            Assert.IsTrue(ls.isEmpty());

            ls = p5.advance(null, game);
            p4 = ls.pop();
            p5 = ls.pop();
            Assert.IsTrue(ls.isEmpty());
            Assert.IsInstanceOfType(p4, typeof(RecoverPhase));
            Assert.AreEqual(dying, p4.player);
            Assert.AreSame(p1, p5);

            Assert.AreEqual(0, dying.health);
            ls = p4.advance(null, game);
            Assert.IsTrue(ls.isEmpty());
            Assert.AreEqual(1, dying.health);

            ls = p5.advance(null, game);
            Assert.IsTrue(ls.isEmpty());
            // ActionPhase produces attackPhase
        }

        [TestMethod]
        public void DyingAskForHelpRejectTest2()
        {
            Player dying = new Player(0, "dying", "dying", 1);
            Player source = mocks.Stub<Player>(1);
            Player player2 = mocks.Stub<Player>(2);
            Player player3 = mocks.Stub<Player>(3);
            Player[] players = new Player[4];
            Phase p1, p2, p3, p4, p5;
            PhaseList ls;
            players[0] = dying;
            players[1] = source;
            players[2] = player2;
            players[3] = player3;

            Attack card = new Attack(CardSuit.Club, 1);
            Peach peach = new Peach(CardSuit.Heart, 2);
            Assert.AreEqual(1, dying.healthLimit);
            ICardSet cardStack = MockRepository.GenerateStub<ICardSet>();

            IGame game = MockRepository.GenerateStub<IGame>();
            game.Stub(x => x.players).Return(players);
            game.Stub(x => x.nextPlayer(source, 0)).Return(source);
            game.Stub(x => x.nextPlayer(source, 1)).Return(player2);
            game.Stub(x => x.nextPlayer(source, 2)).Return(player3);
            game.Stub(x => x.nextPlayer(source, 3)).Return(dying);
            game.Stub(x => x.curRoundPlayer).Return(source);
            game.Stub(s => s.cards).Return(cardStack);
            game.Stub(s => s.Num_Player).Return(4);
            game.Stub(s => s.Num_Player).Return(4);

            HarmPhase harm = new HarmPhase(dying, source, 1, card);
            ls = harm.advance(null, game);
            p1 = ls.pop();
            Assert.IsInstanceOfType(p1, typeof(AskForHelpPhase));
            Assert.IsTrue(ls.isEmpty());

            ls = p1.advance(null, game);
            p2 = ls.pop();
            p3 = ls.pop();
            Assert.IsTrue(ls.isEmpty());
            Assert.IsInstanceOfType(p2, typeof(ResponsePhase));
            Assert.AreEqual(source, p2.player);
            Assert.AreSame(p1, p3);

            ls = p2.advance(new YesOrNoAction(false), game);
            Assert.IsTrue(ls.isEmpty());

            ls = p3.advance(null, game);
            p4 = ls.pop();
            p5 = ls.pop();
            Assert.IsTrue(ls.isEmpty());
            Assert.IsInstanceOfType(p4, typeof(ResponsePhase));
            Assert.AreEqual(player2, p4.player);
            Assert.AreSame(p1, p5);

            ls = p4.advance(new YesOrNoAction(false), game);
            Assert.IsTrue(ls.isEmpty());

            ls = p5.advance(null, game);
            p4 = ls.pop();
            p5 = ls.pop();
            Assert.IsTrue(ls.isEmpty());
            Assert.IsInstanceOfType(p4, typeof(ResponsePhase));
            Assert.AreEqual(player3, p4.player);
            Assert.AreSame(p1, p5);

            ls = p4.advance(new YesOrNoAction(false), game);
            Assert.IsTrue(ls.isEmpty());

            ls = p5.advance(null, game);
            p4 = ls.pop();
            p5 = ls.pop();
            Assert.IsTrue(ls.isEmpty());
            Assert.IsInstanceOfType(p4, typeof(ResponsePhase));
            Assert.AreEqual(dying, p4.player);
            Assert.AreSame(p1, p5);

            ls = p4.advance(new CardAction(peach), game);
            Assert.IsTrue(ls.isEmpty());

            ls = p5.advance(null, game);
            p4 = ls.pop();
            p5 = ls.pop();
            Assert.IsTrue(ls.isEmpty());
            Assert.IsInstanceOfType(p4, typeof(RecoverPhase));
            Assert.AreEqual(dying, p4.player);
            Assert.AreSame(p1, p5);

            Assert.AreEqual(0, dying.health);
            ls = p4.advance(null, game);
            Assert.IsTrue(ls.isEmpty());
            Assert.AreEqual(1, dying.health);

            ls = p5.advance(null, game);
            Assert.IsTrue(ls.isEmpty());
            // ActionPhase produces attackPhase
        }

        [TestMethod]
        public void DyingAskForHelpDeadTest1()
        {
            Player dying = new Player(0, "dying", "dying", 1);
            Player source = mocks.Stub<Player>(1);
            Player player2 = mocks.Stub<Player>(2);
            Player player3 = mocks.Stub<Player>(3);
            Player[] players = new Player[4];
            Phase p1, p2, p3, p4, p5;
            PhaseList ls;
            players[0] = dying;
            players[1] = source;
            players[2] = player2;
            players[3] = player3;

            Attack card = new Attack(CardSuit.Club, 1);
            Peach peach = new Peach(CardSuit.Heart, 2);
            Assert.AreEqual(1, dying.healthLimit);
            ICardSet cardStack = MockRepository.GenerateStub<ICardSet>();

            IGame game = MockRepository.GenerateStub<IGame>();
            game.Stub(x => x.players).Return(players);
            game.Stub(x => x.nextPlayer(source, 0)).Return(source);
            game.Stub(x => x.nextPlayer(source, 1)).Return(player2);
            game.Stub(x => x.nextPlayer(source, 2)).Return(player3);
            game.Stub(x => x.nextPlayer(source, 3)).Return(dying);
            game.Stub(x => x.curRoundPlayer).Return(source);
            game.Stub(s => s.cards).Return(cardStack);
            game.Stub(s => s.Num_Player).Return(4);

            HarmPhase harm = new HarmPhase(dying, source, 1, card);
            ls = harm.advance(null, game);
            p1 = ls.pop();
            Assert.IsInstanceOfType(p1, typeof(AskForHelpPhase));
            Assert.IsTrue(ls.isEmpty());

            ls = p1.advance(null, game);
            p2 = ls.pop();
            p3 = ls.pop();
            Assert.IsTrue(ls.isEmpty());
            Assert.IsInstanceOfType(p2, typeof(ResponsePhase));
            Assert.AreEqual(source, p2.player);
            Assert.AreSame(p1, p3);

            ls = p2.advance(new YesOrNoAction(false), game);
            Assert.IsTrue(ls.isEmpty());

            ls = p3.advance(null, game);
            p4 = ls.pop();
            p5 = ls.pop();
            Assert.IsTrue(ls.isEmpty());
            Assert.IsInstanceOfType(p4, typeof(ResponsePhase));
            Assert.AreEqual(player2, p4.player);
            Assert.AreSame(p1, p5);

            ls = p4.advance(new YesOrNoAction(false), game);
            Assert.IsTrue(ls.isEmpty());

            ls = p5.advance(null, game);
            p4 = ls.pop();
            p5 = ls.pop();
            Assert.IsTrue(ls.isEmpty());
            Assert.IsInstanceOfType(p4, typeof(ResponsePhase));
            Assert.AreEqual(player3, p4.player);
            Assert.AreSame(p1, p5);

            ls = p4.advance(new YesOrNoAction(false), game);
            Assert.IsTrue(ls.isEmpty());

            ls = p5.advance(null, game);
            p4 = ls.pop();
            p5 = ls.pop();
            Assert.IsTrue(ls.isEmpty());
            Assert.IsInstanceOfType(p4, typeof(ResponsePhase));
            Assert.AreEqual(dying, p4.player);
            Assert.AreSame(p1, p5);

            ls = p4.advance(new YesOrNoAction(false), game);
            Assert.IsTrue(ls.isEmpty());

            ls = p5.advance(null, game);
            p4 = ls.pop();
            Assert.IsTrue(ls.isEmpty());
            Assert.IsInstanceOfType(p4, typeof(DeadPhase));
            Assert.AreEqual(dying, p4.player);

            ls = p4.advance(null, game);
            Assert.IsTrue(ls.isEmpty());
            Assert.IsTrue(dying.isDead());
        }
    }
}
