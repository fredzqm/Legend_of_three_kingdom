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
            ls.add(new Phase(0, PhaseType.PlayerTurn));
            ls.add(new Phase(1, PhaseType.PlayerTurn));
            ls.add(new Phase(2, PhaseType.PlayerTurn));
            ls.add(new Phase(3, PhaseType.PlayerTurn));
            ls.add(new Phase(4, PhaseType.PlayerTurn));
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
            ls.add(new Phase(0, PhaseType.PlayerTurn));
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
            ls.add(new Phase( 0, PhaseType.PlayerTurn));
            ls.add(new Phase( 1, PhaseType.PlayerTurn));
            ls.add(new Phase( 2, PhaseType.PlayerTurn));
            PhaseList ls2 = new PhaseList();
            ls2.add(new Phase( 3, PhaseType.PlayerTurn));
            ls2.add(new Phase( 4, PhaseType.PlayerTurn));
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
            ls.add(new Phase( 0, PhaseType.PlayerTurn));
            ls.add(new Phase( 1, PhaseType.PlayerTurn));
            ls.add(new Phase( 2, PhaseType.PlayerTurn));
            PhaseList ls2 = new PhaseList();
            ls2.add(new Phase( 3, PhaseType.PlayerTurn));
            ls2.add(new Phase( 4, PhaseType.PlayerTurn));
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

    [TestClass]
    public class PhaseTest
    {
        [TestMethod]
        public void PhaseExtraInfoListTest()
        {
            Phase x = new Phase(0, PhaseType.ActionPhase, 1, 2.7, 3);
            Assert.AreEqual(1, x.getInfor(0));
            Assert.AreEqual(2.7, x.getInfor(1));
            Assert.AreEqual(3, x.getInfor(2));

            Card c = new Card(CardSuit.Club, CardType.Attack, 3);
            Phase y = new Phase(0, PhaseType.ActionPhase, PhaseType.ActionPhase, c , 2);
            Assert.AreEqual(PhaseType.ActionPhase, y.getInfor(0));
            Assert.AreEqual(c, y.getInfor(1));
            Assert.AreEqual(2, y.getInfor(2));
        }
    }

}
