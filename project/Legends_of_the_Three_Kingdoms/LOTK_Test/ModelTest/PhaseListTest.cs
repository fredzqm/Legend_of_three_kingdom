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
            ls.add(new Phase(new Player(0), PhaseType.PlayerTurn));
            ls.add(new Phase(new Player(1), PhaseType.PlayerTurn));
            ls.add(new Phase(new Player(2), PhaseType.PlayerTurn));
            ls.add(new Phase(new Player(3), PhaseType.PlayerTurn));
            ls.add(new Phase(new Player(4), PhaseType.PlayerTurn));
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
        public void TestPushStageList()
        {
            PhaseList ls = new PhaseList();
            ls.add(new Phase(new Player(0), PhaseType.PlayerTurn));
/*            ls.add(new Phase(new Player(1), PhaseType.PlayerTurn));
            ls.add(new Phase(new Player(2), PhaseType.PlayerTurn));
            PhaseList ls2 = new PhaseList();
            ls2.add(new Phase(new Player(3), PhaseType.PlayerTurn));
            ls2.add(new Phase(new Player(4), PhaseType.PlayerTurn));
            ls.pushStageList(ls2); 
            Assert.AreEqual(ls.pop().playerID, 3);
            Assert.AreEqual(ls.pop().playerID, 4); */
            Assert.AreEqual(ls.pop().playerID, 0);
/*            Assert.AreEqual(ls.pop().playerID, 1);
            Assert.AreEqual(ls.pop().playerID, 2); */
        }
    }

}
