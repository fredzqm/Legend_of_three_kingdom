using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Model;
using Legends_of_the_Three_Kingdoms.Model;

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
            try
            {
                ls.pop();
            }
            catch (EmptyException e)
            {
                Console.WriteLine("Pop Empty List Exception caught.", e);
            }
        }

        [TestMethod]
        public void TestPushOneStageList()
        {
            PhaseList ls = new PhaseList();
            ls.add(new PlayerTurn(0));
            Assert.AreEqual(ls.pop().playerID, 0);
            try
            {
                ls.pop();
            }
            catch (EmptyException e)
            {
                Console.WriteLine("Empty Exception caught.", e);
            }
        }

        [TestMethod]
        public void TestPushStageList()
        {
            PhaseList ls = new PhaseList();
            ls.add(new PlayerTurn(0));
            ls.add(new PlayerTurn(1));
            ls.add(new PlayerTurn(2));
            PhaseList ls2 = new PhaseList();
            ls2.add(new PlayerTurn(3));
            ls2.add(new PlayerTurn(4));
            ls.pushStageList(ls2); 
            Assert.AreEqual(ls.pop().playerID, 3);
            Assert.AreEqual(ls.pop().playerID, 4); 
            Assert.AreEqual(ls.pop().playerID, 0);
            Assert.AreEqual(ls.pop().playerID, 1);
            Assert.AreEqual(ls.pop().playerID, 2); 
        }

        [TestMethod]
        public void TestPushStageListTillEmpty()
        {
            PhaseList ls = new PhaseList();
            ls.add(new PlayerTurn(0));
            ls.add(new PlayerTurn(1));
            ls.add(new PlayerTurn(2));
            PhaseList ls2 = new PhaseList();
            ls2.add(new PlayerTurn(3));
            ls2.add(new PlayerTurn(4));
            ls.pushStageList(ls2);
            Assert.AreEqual(ls.pop().playerID, 3);
            Assert.AreEqual(ls.pop().playerID, 4);
            Assert.AreEqual(ls.pop().playerID, 0);
            Assert.AreEqual(ls.pop().playerID, 1);
            Assert.AreEqual(ls.pop().playerID, 2);
            try
            {
                ls.pop();
            } catch (EmptyException e)
            {
                Console.WriteLine("Pop Empty List Exception caught.", e);
            }
        }
    }

}
