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
            Assert.Equals(ls.pop().getPlayer(), 0);
            Assert.Equals(ls.pop().getPlayer(), 1);
            Assert.Equals(ls.pop().getPlayer(), 2);
            Assert.Equals(ls.pop().getPlayer(), 3);
            Assert.Equals(ls.pop().getPlayer(), 4);
        }
    }
}
