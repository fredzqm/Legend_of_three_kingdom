using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Model;

namespace LOTK_Test
{
    [TestClass]
    public class StageListTest
    {
        [TestMethod]
        public void TestConstruct()
        {
            StageList ls = new StageList();
        }

        [TestMethod]
        public void TestAddPop()
        {
            StageList ls = new StageList();
            ls.add(new PlayerTurn(0));
            ls.add(new PlayerTurn(1));
            ls.add(new PlayerTurn(2));
            ls.add(new PlayerTurn(3));
            ls.add(new PlayerTurn(4));
            Assert.AreEqual(ls.pop().playerID, 0);
            Assert.AreEqual(ls.pop().playerID, 1);
            Assert.AreEqual(ls.pop().playerID, 2);
            Assert.AreEqual(ls.pop().playerID, 3);
            Assert.AreEqual(ls.pop().playerID, 4);
        }
    }
}
