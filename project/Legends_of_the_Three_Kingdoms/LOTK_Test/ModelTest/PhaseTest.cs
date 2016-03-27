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
            ls = (new PlayerTurn(p)).handleResponse( null, testgame);
            Assert.AreEqual(typeof(JudgePhase), ls.pop().GetType());
            Assert.AreEqual(typeof(PlayerTurn), ls.pop().GetType());
            Assert.IsTrue(ls.isEmpty());

            ls = (new JudgePhase(p)).handleResponse( null, testgame);
            Assert.AreEqual(typeof(DrawingPhase), ls.pop().GetType());
            Assert.AreEqual(typeof(ActionPhase), ls.pop().GetType());
            Assert.IsTrue(ls.isEmpty());
            ls = (new JudgePhase(p)).handleResponse( new YesOrNoAction(true), testgame);
            Assert.AreEqual(typeof(DrawingPhase), ls.pop().GetType());
            Assert.AreEqual(typeof(ActionPhase), ls.pop().GetType());
            Assert.IsTrue(ls.isEmpty());
            ls = (new JudgePhase(p)).handleResponse( new YesOrNoAction(false), testgame);
            Assert.AreEqual(typeof(DrawingPhase), ls.pop().GetType());
            Assert.AreEqual(typeof(ActionPhase), ls.pop().GetType());
            Assert.IsTrue(ls.isEmpty());

            ls = (new DrawingPhase(p)).handleResponse( null, testgame);
            Assert.IsTrue(ls.isEmpty());
            ls = (new DrawingPhase(p)).handleResponse( new YesOrNoAction(true), testgame);
            Assert.IsTrue(ls.isEmpty());
            ls = (new DrawingPhase(p)).handleResponse( new YesOrNoAction(false), testgame);
            Assert.IsTrue(ls.isEmpty());

            ls = (new ActionPhase(p)).handleResponse( null, testgame);
            Assert.IsNull(ls);
            ls = (new ActionPhase(p)).handleResponse( new YesOrNoAction(true), testgame);
            Assert.IsNull(ls);
            ls = (new ActionPhase(p)).handleResponse( new YesOrNoAction(false), testgame);
            Assert.AreEqual(typeof(DiscardPhase), ls.pop().GetType());
            Assert.IsTrue(ls.isEmpty());

            ls = (new DiscardPhase(p)).handleResponse( null, testgame);
            Assert.IsNull(ls); // in the future this will be changed to true
            ls = (new DiscardPhase(p)).handleResponse( new YesOrNoAction(true), testgame);
            Assert.IsTrue(ls.isEmpty());
            ls = (new DiscardPhase(p)).handleResponse( new YesOrNoAction(false), testgame);
            Assert.IsTrue(ls.isEmpty());
        }

        [TestMethod]
        public void UserInputYES_OR_NOTest()
        {
            Player p = new Player(0, "Player Name", "Player Description");
            Assert.IsTrue((new JudgePhase(p)).handleResponse(
                new YesOrNoAction(false), null) != null);
            Assert.IsTrue((new JudgePhase(p)).handleResponse(
                new YesOrNoAction(true), null) != null);
            Assert.IsTrue((new DrawingPhase(p)).handleResponse(
                new YesOrNoAction(false), null) != null);
            Assert.IsTrue((new DrawingPhase(p)).handleResponse(
                new YesOrNoAction(true), null) != null);
            Assert.IsTrue((new ActionPhase(p)).handleResponse(
                new YesOrNoAction(false), null) != null);
            Assert.IsFalse((new ActionPhase(p)).handleResponse(
                new YesOrNoAction(true), null) != null);
            Assert.IsTrue((new DiscardPhase(p)).handleResponse(
                new YesOrNoAction(false), null) != null);
            Assert.IsTrue((new DiscardPhase(p)).handleResponse(
                new YesOrNoAction(true), null) != null);
        }
    }
}
