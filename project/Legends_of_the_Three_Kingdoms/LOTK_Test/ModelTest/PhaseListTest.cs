using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Model;

namespace LOTK_Test.ModelTest
{
    [TestClass]
    public class PhaseListTest
    {
        [TestMethod]
        public void TestConstruct()
        {
            PhaseList ls = new PhaseList();
        }

        [TestMethod]
        public void TestAddPop()
        {
            PhaseList ls = new PhaseList();
            ls.add(new PlayerTurn(new Player(0)));
            ls.add(new PlayerTurn(new Player(1)));
            ls.add(new PlayerTurn(new Player(2)));
            ls.add(new PlayerTurn(new Player(3)));
            ls.add(new PlayerTurn(new Player(4)));
            Assert.AreEqual(ls.pop().playerID, 0);
            Assert.AreEqual(ls.pop().playerID, 1);
            Assert.AreEqual(ls.pop().playerID, 2);
            Assert.AreEqual(ls.pop().playerID, 3);
            Assert.AreEqual(ls.pop().playerID, 4);
        }

        [TestMethod]
        public void TestPushStageList()
        {
            PhaseList ls = new PhaseList();
            ls.add(new PlayerTurn(new Player(0)));
            ls.add(new PlayerTurn(new Player(1)));
            ls.add(new PlayerTurn(new Player(2)));
            PhaseList ls2 = new PhaseList();
            ls2.add(new PlayerTurn(new Player(3)));
            ls2.add(new PlayerTurn(new Player(4)));
            ls.pushStageList(ls2);
            Assert.AreEqual(ls.pop().playerID, 3);
            Assert.AreEqual(ls.pop().playerID, 4);
            Assert.AreEqual(ls.pop().playerID, 0);
            Assert.AreEqual(ls.pop().playerID, 1);
            Assert.AreEqual(ls.pop().playerID, 2);
        }
    }

}
