﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Model;
using System.Collections.Generic;

namespace LOTK_Test.ModelTest
{
    /// <summary>
    /// This tests PhaseList, a basic datastructure for Phases.
    /// It uses <seealso cref="PhaseSimple"/> as a stub.
    /// </summary>
    [TestClass]
    public class PhaseListUnitTest
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
            Assert.IsTrue(ls.isEmpty());

        }

        [TestMethod]
        public void TestPushOneStageList()
        {
            PhaseList ls = new PhaseList();
            ls.add(new PhaseSimple(0));
            Assert.AreEqual(ls.pop().playerID, 0);
            Assert.IsTrue(ls.isEmpty());
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
            ls.pushList(ls2); 
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
            ls.pushList(ls2);
            Assert.AreEqual(ls.pop().playerID, 3);
            Assert.AreEqual(ls.pop().playerID, 4);
            Assert.AreEqual(ls.pop().playerID, 0);
            Assert.AreEqual(ls.pop().playerID, 1);
            Assert.AreEqual(ls.pop().playerID, 2);
            Assert.IsTrue(ls.isEmpty());
        }

        [TestMethod]
        public void TestEnumerator()
        {
            List<Phase> list = new List<Phase>();
            list.Add(new PhaseSimple(0));
            list.Add(new PhaseSimple(1));
            list.Add(new PhaseSimple(2));
            list.Add(new PhaseSimple(3));
            list.Add(new PhaseSimple(4));
            PhaseList ls = new PhaseList();
            ls.add(list[0]);
            ls.add(list[1]);
            ls.add(list[2]);
            ls.add(list[3]);
            ls.add(list[4]);

            int i = 0;
            foreach (Phase p in ls)
            {
                Assert.AreEqual(list[i], p);
                i++;
            }
        }

        internal class PhaseSimple : Phase
        {
            public PhaseSimple(int i) : base(new Player(i)) { }

            public override PhaseList advance(UserAction userAction, IGame game)
            {
                throw new NotImplementedException();
            }

            public override int getTimeLeft()
            {
                throw new NotImplementedException();
            }

            public override bool needResponse()
            {
                throw new NotImplementedException();
            }
            public override string ToString()
            {
                return "Index " + playerID;
            }
        }
    }

}
