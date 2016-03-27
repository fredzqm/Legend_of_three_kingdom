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
            ls.add(new PhaseSimple(0));
            ls.add(new PhaseSimple(1));
            ls.add(new PhaseSimple(2));
            ls.add(new PhaseSimple(3));
            ls.add(new PhaseSimple(4));
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
            ls.add(new PhaseSimple(0));
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
            ls.add(new PhaseSimple(0));
            ls.add(new PhaseSimple(1));
            ls.add(new PhaseSimple(2));
            PhaseList ls2 = new PhaseList();
            ls2.add(new PhaseSimple(3));
            ls2.add(new PhaseSimple(4));
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
            ls.add(new PhaseSimple(0));
            ls.add(new PhaseSimple(1));
            ls.add(new PhaseSimple(2));
            PhaseList ls2 = new PhaseList();
            ls2.add(new PhaseSimple(3));
            ls2.add(new PhaseSimple(4));
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

        internal class PhaseSimple : Phase
        {
    
            public PhaseSimple(int i) : base(new Player(i, null, null)) { }

            public override PhaseList advance(UserAction userAction, IGame game)
            {
                throw new NotImplementedException();
            }

            public override bool needResponse()
            {
                throw new NotImplementedException();
            }
        }
    }

}
