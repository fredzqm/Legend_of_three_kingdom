﻿using System;
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
            Player p = new Player(0);

        }

        [TestMethod]
        public void PlayerFiveBasicPhaseTest()
        {
            Player p = new Player(0);
            IGame testgame = new TestGame(5);
            PhaseList ls;
            ls = p.handlePhase(new Phase(0, PhaseType.PlayerTurn), testgame);
            Assert.AreEqual(PhaseType.JudgePhase, ls.pop().type);
            Assert.AreEqual(PhaseType.PlayerTurn, ls.pop().type);
            Assert.IsTrue(ls.isEmpty());

            ls = p.handlePhase(new Phase(0, PhaseType.JudgePhase), testgame);
            Assert.AreEqual(PhaseType.DrawingPhase, ls.pop().type);
            Assert.AreEqual(PhaseType.ActionPhase, ls.pop().type);
            Assert.IsTrue(ls.isEmpty());

            ls = p.handlePhase(new Phase(0, PhaseType.DrawingPhase), testgame);
            Assert.IsTrue(ls.isEmpty());

            ls = p.handlePhase(new Phase(0, PhaseType.ActionPhase), testgame);
            Assert.AreEqual(ls.pop().type, PhaseType.DiscardPhase);
            Assert.IsTrue(ls.isEmpty());

            ls = p.handlePhase(new Phase(0, PhaseType.DiscardPhase), testgame);
            Assert.IsTrue(ls.isEmpty());

        }

        public void PlayerFiveAdvancePhaseTest()
        {
            Player p = new Player(0);
            IGame testgame = new TestGame(5);
            PhaseList ls;
            ls = p.handlePhase(new Phase(0, PhaseType.PlayerTurn), testgame);
            Assert.AreEqual(PhaseType.JudgePhase, ls.pop().type);
            Assert.AreEqual(PhaseType.PlayerTurn, ls.pop().type);
            Assert.IsTrue(ls.isEmpty());
            try { ls.pop(); }
            catch (EmptyException e)
            {
                Console.WriteLine("Empty Exception caught.", e);
            }

            ls = p.handlePhase(new Phase(0, PhaseType.JudgePhase), testgame);
            Assert.AreEqual(PhaseType.DrawingPhase, ls.pop().type);
            Assert.AreEqual(PhaseType.ActionPhase, ls.pop().type);
            Assert.IsTrue(ls.isEmpty());
            try { ls.pop(); }
            catch (EmptyException e)
            {
                Console.WriteLine("Empty Exception caught.", e);
            }

            ls = p.handlePhase(new Phase(0, PhaseType.DrawingPhase), testgame);
            Assert.IsTrue(ls.isEmpty());
            try { ls.pop(); }
            catch (EmptyException e)
            {
                Console.WriteLine("Empty Exception caught.", e);
            }

            ls = p.handlePhase(new Phase(0, PhaseType.ActionPhase), testgame);
            Assert.AreEqual(ls.pop().type, PhaseType.DiscardPhase);
            Assert.IsTrue(ls.isEmpty());
            try { ls.pop(); }
            catch (EmptyException e)
            {
                Console.WriteLine("Empty Exception caught.", e);
            }

            ls = p.handlePhase(new Phase(0, PhaseType.DiscardPhase), testgame);
            Assert.IsTrue(ls.isEmpty());
            try { ls.pop(); }
            catch (EmptyException e)
            {
                Console.WriteLine("Empty Exception caught.", e);
            }

        }

        internal class TestGame : IGame
        {
            public int Num_Player { get; }

            public TestGame(int n)
            {
                Num_Player = n;
            }
        }
    }
}