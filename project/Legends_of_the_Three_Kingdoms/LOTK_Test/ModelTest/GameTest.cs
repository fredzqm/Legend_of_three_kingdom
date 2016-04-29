using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Model;
using System.Collections.Generic;
using LOTK.Controller;
using System.Reflection;
using Rhino.Mocks;

namespace LOTK_Test.ModelTest
{


    [TestClass]
    public class GameUnitTest
    {
        private CardSet cardList;
        private Player[] players;
        private IGame game;

        private MockRepository mocks;

        [TestInitialize()]
        public void initialize()
        {
            mocks = new MockRepository();
            ICollection<Card> cards = new List<Card>();
            cards.Add(new Attack(CardSuit.Club, 2));
            cards.Add(new Attack(CardSuit.Club, 3));
            cards.Add(new Attack(CardSuit.Club, 4));
            cards.Add(new Attack(CardSuit.Club, 5));
            cards.Add(new Attack(CardSuit.Club, 6));
            cards.Add(new Attack(CardSuit.Club, 7));
            cards.Add(new Attack(CardSuit.Club, 8));
            cards.Add(new Attack(CardSuit.Club, 9));
            cards.Add(new Attack(CardSuit.Club, 2));
            cards.Add(new Attack(CardSuit.Club, 3));
            cards.Add(new Attack(CardSuit.Club, 4));
            cards.Add(new Attack(CardSuit.Club, 5));
            cards.Add(new Attack(CardSuit.Club, 6));
            cards.Add(new Attack(CardSuit.Club, 7));
            cards.Add(new Attack(CardSuit.Club, 8));
            cards.Add(new Attack(CardSuit.Club, 9));
            cards.Add(new Attack(CardSuit.Club, 2));
            cards.Add(new Attack(CardSuit.Club, 3));
            cards.Add(new Attack(CardSuit.Club, 4));
            cards.Add(new Attack(CardSuit.Club, 5));
            cards.Add(new Attack(CardSuit.Club, 6));
            cards.Add(new Attack(CardSuit.Club, 7));
            cards.Add(new Attack(CardSuit.Club, 8));
            cards.Add(new Attack(CardSuit.Club, 9));
            cards.Add(new Attack(CardSuit.Club, 2));
            cards.Add(new Attack(CardSuit.Club, 3));
            cards.Add(new Attack(CardSuit.Club, 4));
            cards.Add(new Attack(CardSuit.Club, 5));
            cards.Add(new Attack(CardSuit.Club, 6));
            cards.Add(new Attack(CardSuit.Club, 7));
            cards.Add(new Attack(CardSuit.Club, 8));
            cards.Add(new Attack(CardSuit.Club, 9));

            cardList = new CardSet(cards);

            players = new Player[5];
            for (int i = 0; i < 5; i++)
            {
                players[i] = new Player(i);
            }
            game = new Game(players, cardList);

        }

        [TestMethod]
        public void GameConstruct()
        {
            Type gameType = typeof(Game);
            FieldInfo stagesField = gameType.GetField("stages", BindingFlags.NonPublic | BindingFlags.Instance);
            PhaseList ls = stagesField.GetValue(game) as PhaseList;
            Assert.IsTrue(ls.isEmpty());
        }

        [TestMethod]
        public void GameEmptyException()
        {
            try
            {
                game.nextStage(null);
                Assert.Fail("EmptyException not thrown");
            }
            catch (EmptyException e) { }
        }


        [TestMethod]
        public void nextStageTest()
        {
            Type gameType = typeof(Game);
            FieldInfo stagesField = gameType.GetField("stages", BindingFlags.NonPublic | BindingFlags.Instance);

            Player player = mocks.Stub<Player>(0);
            Phase p1 = mocks.DynamicMock<Phase>(player);
            Phase p2 = mocks.DynamicMock<Phase>(player);
            Phase p3 = mocks.DynamicMock<Phase>(player);
            Phase p4 = mocks.DynamicMock<Phase>(player);

            stagesField.SetValue(game, new PhaseList(p1));

            using (mocks.Ordered())
            {
                Expect.Call(p1.advance(null, game)).Return(new PhaseList(p2));
                Expect.Call(p2.advance(null, game)).Return(new PhaseList(p3));
                Expect.Call(p3.advance(null, game)).Return(new PhaseList(p4));
            }

            mocks.ReplayAll();

            game.nextStage(null);
            game.nextStage(null);
            game.nextStage(null);

            Phase left = ((PhaseList)stagesField.GetValue(game)).top();
            Assert.AreEqual(p4, left);

            mocks.VerifyAll();
        }

        [TestMethod]
        public void nextStageUtillEmptyTest()
        {
            Type gameType = typeof(Game);
            FieldInfo stagesField = gameType.GetField("stages", BindingFlags.NonPublic | BindingFlags.Instance);

            Player player = mocks.Stub<Player>(0);
            Phase p1 = mocks.DynamicMock<Phase>(player);
            Phase p2 = mocks.DynamicMock<Phase>(player);
            Phase p3 = mocks.DynamicMock<Phase>(player);
            Phase p4 = mocks.DynamicMock<Phase>(player);
            UserAction x = mocks.Stub<UserAction>();

            stagesField.SetValue(game, new PhaseList(p1));

            using (mocks.Ordered())
            {
                Expect.Call(p1.advance(null, game)).Return(new PhaseList(p2, p1));
                Expect.Call(p2.advance(null, game)).Return(new PhaseList(p3, p4));
                Expect.Call(p3.advance(null, game)).Return(new PhaseList());
                Expect.Call(p4.advance(null, game)).Return(new PhaseList());
                Expect.Call(p1.advance(x, game)).Return(new PhaseList());
            }

            mocks.ReplayAll();

            game.nextStage(null);
            game.nextStage(null);
            game.nextStage(null);
            game.nextStage(null);
            try
            {
                game.nextStage(x);
                Assert.Fail("No exception thrown");
            }
            catch (EmptyException e)
            {

            }
            PhaseList left = (PhaseList)stagesField.GetValue(game);
            Assert.IsTrue(left.isEmpty());

            mocks.VerifyAll();
        }


    }


    [TestClass]
    public class GameEndGameTest
    {

        private CardSet cardList;
        private Player[] players;
        private IGame game;

        private MockRepository mocks;

        [TestInitialize()]
        public void initialize()
        {
            mocks = new MockRepository();
            ICollection<Card> cards = new List<Card>();
            cards.Add(new Attack(CardSuit.Club, 2));
            cards.Add(new Attack(CardSuit.Club, 3));
            cards.Add(new Attack(CardSuit.Club, 4));
            cards.Add(new Attack(CardSuit.Club, 5));
            cards.Add(new Attack(CardSuit.Club, 6));
            cards.Add(new Attack(CardSuit.Club, 7));
            cards.Add(new Attack(CardSuit.Club, 8));
            cards.Add(new Attack(CardSuit.Club, 9));
            cards.Add(new Attack(CardSuit.Club, 2));
            cards.Add(new Attack(CardSuit.Club, 3));
            cards.Add(new Attack(CardSuit.Club, 4));
            cards.Add(new Attack(CardSuit.Club, 5));
            cards.Add(new Attack(CardSuit.Club, 6));
            cards.Add(new Attack(CardSuit.Club, 7));
            cards.Add(new Attack(CardSuit.Club, 8));
            cards.Add(new Attack(CardSuit.Club, 9));
            cards.Add(new Attack(CardSuit.Club, 2));
            cards.Add(new Attack(CardSuit.Club, 3));
            cards.Add(new Attack(CardSuit.Club, 4));
            cards.Add(new Attack(CardSuit.Club, 5));
            cards.Add(new Attack(CardSuit.Club, 6));
            cards.Add(new Attack(CardSuit.Club, 7));
            cards.Add(new Attack(CardSuit.Club, 8));
            cards.Add(new Attack(CardSuit.Club, 9));
            cards.Add(new Attack(CardSuit.Club, 2));
            cards.Add(new Attack(CardSuit.Club, 3));
            cards.Add(new Attack(CardSuit.Club, 4));
            cards.Add(new Attack(CardSuit.Club, 5));
            cards.Add(new Attack(CardSuit.Club, 6));
            cards.Add(new Attack(CardSuit.Club, 7));
            cards.Add(new Attack(CardSuit.Club, 8));
            cards.Add(new Attack(CardSuit.Club, 9));

            cardList = new CardSet(cards);
            players = new Player[5];    
            players[0] = new Player(0, "Player 0", "Player 0", 1, PlayerType.King);
            players[1] = new Player(1, "Player 1", "Player 1", 1, PlayerType.Rebel);
            players[2] = new Player(2, "Player 2", "Player 2", 1, PlayerType.Loyal);
            players[3] = new Player(3, "Player 3", "Player 3", 1, PlayerType.Rebel);
            players[4] = new Player(4, "Player 4", "Player 4", 1, PlayerType.Spy);

            game = new Game(players, cardList);
        }

        [TestMethod]
        public void testInit()
        {
            Assert.IsFalse(players[0].isDead());
            Assert.IsFalse(players[1].isDead());
            Assert.IsFalse(players[2].isDead());
            Assert.IsFalse(players[3].isDead());
            Assert.IsFalse(players[4].isDead());
        }


    }

    /// <summary>
    /// This is an integrated test for the model
    /// It involves cards, 
    /// </summary>
    [TestClass]
    public class GameIntegratedTest
    {

        private ICardSet cardList;
        private Player[] players;
        private Player[] players8;
        private IGame game;
        private IGame game8;


        [TestInitialize()]
        public void initialize()
        {
            ICollection<Card> cards = new List<Card>();
            cards.Add(new Attack(CardSuit.Club, 2));
            cards.Add(new Attack(CardSuit.Club, 3));
            cards.Add(new Attack(CardSuit.Club, 4));
            cards.Add(new Attack(CardSuit.Club, 5));
            cards.Add(new Attack(CardSuit.Club, 6));
            cards.Add(new Attack(CardSuit.Club, 7));
            cards.Add(new Attack(CardSuit.Club, 8));
            cards.Add(new Attack(CardSuit.Club, 9));
            cards.Add(new Attack(CardSuit.Club, 10));
            cards.Add(new Attack(CardSuit.Club, 11));
            cards.Add(new Attack(CardSuit.Club, 12));

            cards.Add(new Attack(CardSuit.Spade, 2));
            cards.Add(new Attack(CardSuit.Spade, 3));
            cards.Add(new Attack(CardSuit.Spade, 4));
            cards.Add(new Attack(CardSuit.Spade, 5));
            cards.Add(new Attack(CardSuit.Spade, 6));
            cards.Add(new Attack(CardSuit.Spade, 7));
            cards.Add(new Attack(CardSuit.Spade, 8));
            cards.Add(new Attack(CardSuit.Spade, 9));
            cards.Add(new Attack(CardSuit.Spade, 10));
            cards.Add(new Attack(CardSuit.Spade, 11));
            cards.Add(new Attack(CardSuit.Spade, 12));

            cards.Add(new Miss(CardSuit.Diamond, 2));
            cards.Add(new Miss(CardSuit.Diamond, 3));
            cards.Add(new Miss(CardSuit.Diamond, 4));
            cards.Add(new Miss(CardSuit.Diamond, 5));
            cards.Add(new Miss(CardSuit.Diamond, 6));
            cards.Add(new Miss(CardSuit.Diamond, 7));
            cards.Add(new Miss(CardSuit.Diamond, 8));
            cards.Add(new Miss(CardSuit.Diamond, 9));
            cards.Add(new Miss(CardSuit.Diamond, 10));
            cards.Add(new Miss(CardSuit.Diamond, 11));
            cards.Add(new Miss(CardSuit.Diamond, 12));

            cards.Add(new Peach(CardSuit.Heart, 2));
            cards.Add(new Peach(CardSuit.Heart, 3));
            cards.Add(new Peach(CardSuit.Heart, 4));
            cards.Add(new Peach(CardSuit.Heart, 5));
            cards.Add(new Peach(CardSuit.Heart, 6));
            cards.Add(new Peach(CardSuit.Heart, 7));
            cards.Add(new Peach(CardSuit.Heart, 8));
            cards.Add(new Peach(CardSuit.Heart, 9));

            cardList = new CardSet(cards);
            players = new Player[5];
            for (int i = 0; i < 5; i++)
            {
                players[i] = new Player(i);
            }
            game = new Game(players, cardList);
            game.start();

            cardList = new CardSet(cards);
            players8 = new Player[8];
            for (int i = 0; i < 8; i++)
            {
                players8[i] = new Player(i);
            }
            game8 = new Game(players8, cardList);
            game8.start();
        }


        [TestMethod]
        public void FourStageTest()
        {
            Assert.AreEqual(typeof(JudgePhase), game.curPhase.GetType());
            Assert.AreEqual(0, game.curPhase.playerID);
            game.nextStage(null);
            Assert.AreEqual(typeof(DrawingPhase), game.curPhase.GetType());
            Assert.AreEqual(0, game.curPhase.playerID);
            game.nextStage(null);
            Assert.AreEqual(typeof(ActionPhase), game.curPhase.GetType());
            Assert.AreEqual(0, game.curPhase.playerID);
            game.nextStage(new YesOrNoAction(false));
            Assert.AreEqual(typeof(DiscardPhase), game.curPhase.GetType());
            Assert.AreEqual(0, game.curPhase.playerID);
            game.nextStage(new YesOrNoAction(false));
            Assert.AreEqual(typeof(JudgePhase), game.curPhase.GetType());
            Assert.AreEqual(1, game.curPhase.playerID);
        }

        [TestMethod]
        public void EightPeopleGameCycleTest()
        {
            for (int j = 0; j < 1; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    Assert.AreEqual(typeof(JudgePhase), game8.curPhase.GetType());
                    Assert.AreEqual(game8.curPhase.playerID, i);
                    game8.nextStage(null);
                    Assert.AreEqual(typeof(DrawingPhase), game8.curPhase.GetType());
                    Assert.AreEqual(game8.curPhase.playerID, i);
                    game8.nextStage(null);
                    Assert.AreEqual(typeof(ActionPhase), game8.curPhase.GetType());
                    Assert.AreEqual(game8.curPhase.playerID, i);
                    game8.nextStage(new YesOrNoAction(false));
                    Assert.AreEqual(typeof(DiscardPhase), game8.curPhase.GetType());
                    Assert.AreEqual(game8.curPhase.playerID, i);
                    game8.nextStage(new YesOrNoAction(false));
                }
            }
        }

        [TestMethod]
        public void CurrentPlayerTest()
        {
            for (int j = 0; j < 1; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    Assert.AreEqual(typeof(JudgePhase), game8.curPhase.GetType());
                    Assert.AreEqual(game8.curRoundPlayer, i);
                    game8.nextStage(null);
                    Assert.AreEqual(typeof(DrawingPhase), game8.curPhase.GetType());
                    Assert.AreEqual(game8.curRoundPlayer, i);
                    game8.nextStage(null);
                    Assert.AreEqual(typeof(ActionPhase), game8.curPhase.GetType());
                    Assert.AreEqual(game8.curRoundPlayer, i);
                    game8.nextStage(new YesOrNoAction(false));
                    Assert.AreEqual(typeof(DiscardPhase), game8.curPhase.GetType());
                    Assert.AreEqual(game8.curRoundPlayer, i);
                    game8.nextStage(new YesOrNoAction(false));
                }
            }
        }

        [TestMethod]
        public void UserResponseYES_OR_NOTest()
        {
            Assert.AreEqual(typeof(JudgePhase), game.curPhase.GetType());
            // advance
            game.nextStage(new YesOrNoAction(false));
            Assert.AreEqual(typeof(DrawingPhase), game.curPhase.GetType());
            // advance
            game.nextStage(new YesOrNoAction(false));
            Assert.AreEqual(typeof(ActionPhase), game.curPhase.GetType());
            // not advance
            game.nextStage(new YesOrNoAction(true));
            Assert.AreEqual(typeof(ActionPhase), game.curPhase.GetType());
            // advance
            game.nextStage(new YesOrNoAction(false));
            Assert.AreEqual(typeof(DiscardPhase), game.curPhase.GetType());
            // advance
            game.nextStage(new YesOrNoAction(false));
            Assert.AreEqual(typeof(JudgePhase), game.curPhase.GetType());
        }

        [TestMethod]
        public void AutoAdvancedTest()
        {
            Assert.AreEqual(typeof(JudgePhase), game.curPhase.GetType());
            game.tick();
            Assert.AreEqual(typeof(JudgePhase), game.curPhase.GetType());
            game.tick();
            Assert.AreEqual(typeof(DrawingPhase), game.curPhase.GetType());
            game.tick();
            Assert.AreEqual(typeof(DrawingPhase), game.curPhase.GetType());
            game.tick();
            Assert.AreEqual(typeof(ActionPhase), game.curPhase.GetType());
            game.tick();
            Assert.AreEqual(typeof(ActionPhase), game.curPhase.GetType());
            game.tick();
            Assert.AreEqual(typeof(ActionPhase), game.curPhase.GetType());
            game.nextStage(new YesOrNoAction(false));
            Assert.AreEqual(typeof(DiscardPhase), game.curPhase.GetType());
            game.tick();
            Assert.AreEqual(typeof(DiscardPhase), game.curPhase.GetType());
            game.tick();
            Assert.AreEqual(typeof(DiscardPhase), game.curPhase.GetType());
        }

        [TestMethod]
        public void GameIniTest()
        {
            try
            {
                new Game(players, null);
            }
            catch (NotDefinedException e)
            {
                Assert.IsNotNull(e);
            }
        }
        [TestMethod]
        public void GameIniTest2()
        {
            try
            {
                new Game(null, cardList);
            }
            catch (NotDefinedException e)
            {
                Assert.IsNotNull(e);
            }
        }

        [TestMethod]
        public void processUserInputTest()
        {
            MockRepository mocks = new MockRepository();
            mocks.Stub<UserAction>();
            Game g = new Game(players, cardList);
            Type stage = typeof(Game);
            FieldInfo stinfo = stage.GetField("stages",
            BindingFlags.NonPublic | BindingFlags.Instance);

            PhaseList p = new PhaseList();
            p.add(new DiscardPhase(players[0]));
            p.add(new DiscardPhase(players[0]));
            stinfo.SetValue(g, p);
            g.processUserInput(0, mocks.Stub<UserAction>());

        }

        [TestMethod]
        public void curPhassPlayertest()
        {
            MockRepository mocks = new MockRepository();
            List<Card> c = new List<Card>();
            c.Add(new Attack(CardSuit.Club, (byte)1));
            Player p = new Player(1);
            Player[] l = new Player[1];
            l[0] = p;
            Type gameType = typeof(Game);
            FieldInfo stagesField = gameType.GetField("stages", BindingFlags.NonPublic | BindingFlags.Instance);


            Phase p1 = mocks.DynamicMock<Phase>(p);
            Phase p2 = mocks.DynamicMock<Phase>(p);
            Phase p3 = mocks.DynamicMock<Phase>(p);
            Phase p4 = mocks.DynamicMock<Phase>(p);
            Game g = new Game(l, new CardSet(c));
            stagesField.SetValue(g, new PhaseList(p1));


            Assert.AreEqual(p, g.curPhasePlayer);

        }


    }
}
