using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Model;

namespace LOTK_Test.ModelTest
{
    [TestClass]
    public class CardLibTest
    {
        [TestMethod]
        public void AttackTest()
        {
            Attack c = new Attack(CardSuit.Club, (byte)1);
            Assert.AreEqual("Attack Description", c.getDescription());
        }

        [TestMethod]
        public void AttackTest2()
        {
            Attack c = new Attack(CardSuit.Club, (byte)1);
            Assert.AreEqual("Attack", c.ToString());
        }

        [TestMethod]
        public void MissTest()
        {
            Miss c = new Miss(CardSuit.Club, (byte)1);
            Assert.AreEqual("Miss Description", c.getDescription());
        }

        [TestMethod]
        public void Misstests()
        {
            Miss c = new Miss(CardSuit.Club, (byte)1);
            Assert.AreEqual("Miss", c.ToString());
        }

        [TestMethod]
        public void WineTest()
        {
            Wine c = new Wine(CardSuit.Club, (byte)1);
            Assert.AreEqual("Wine Description", c.getDescription());
        }

        [TestMethod]
        public void WineTest2()
        {
            Wine c = new Wine(CardSuit.Club, (byte)1);
            Assert.AreEqual("Wine", c.ToString());
        }

        [TestMethod]
        public void PeachTest()
        {
            Peach c = new Peach(CardSuit.Club, (byte)1);
            Assert.AreEqual("Peach Description", c.getDescription());
        }

        [TestMethod]
        public void PeachTest2()
        {
            Peach c = new Peach(CardSuit.Club, (byte)1);
            Assert.AreEqual("Peach", c.ToString());
        }

        [TestMethod]
        public void NegateTest()
        {
            Negate c = new Negate(CardSuit.Club, (byte)1);
            Assert.AreEqual("Negate Description", c.getDescription());
        }

        [TestMethod]
        public void NegateTest2()
        {
            Negate c = new Negate(CardSuit.Club, (byte)1);
            Assert.AreEqual("Negate", c.ToString());
        }

        [TestMethod]
        public void BarbarianTest()
        {
            Barbarians c = new Barbarians(CardSuit.Club, (byte)1);
            Assert.AreEqual("Barbarians Description", c.getDescription());
        }

        [TestMethod]
        public void BarbarianTest2()
        {
            Barbarians c = new Barbarians(CardSuit.Club, (byte)1);
            Assert.AreEqual("Barbarians", c.ToString());
        }

        [TestMethod]
        public void HailofArrowTest()
        {
            HailofArrow c = new HailofArrow(CardSuit.Club, (byte)1);
            Assert.AreEqual("HailofArrow Description", c.getDescription());
        }

        [TestMethod]
        public void HailofArrowTest2()
        {
            HailofArrow c = new HailofArrow(CardSuit.Club, (byte)1);
            Assert.AreEqual("HailofArrow", c.ToString());
        }

        [TestMethod]
        public void PeachGardenTest()
        {
            PeachGarden c = new PeachGarden(CardSuit.Club, (byte)1);
            Assert.AreEqual("PeachGarden Description", c.getDescription());
        }

        [TestMethod]
        public void PeachGardenTest2()
        {
            PeachGarden c = new PeachGarden(CardSuit.Club, (byte)1);
            Assert.AreEqual("PeachGarden", c.ToString());
        }

        [TestMethod]
        public void WealthTest()
        {
            Wealth c = new Wealth(CardSuit.Club, (byte)1);
            Assert.AreEqual("Wealth Description", c.getDescription());
        }

        [TestMethod]
        public void WealthTest2()
        {
            Wealth c = new Wealth(CardSuit.Club, (byte)1);
            Assert.AreEqual("Wealth", c.ToString());
        }

        [TestMethod]
        public void StealTest()
        {
            Steal c = new Steal(CardSuit.Club, (byte)1);
            Assert.AreEqual("Steal Description", c.getDescription());
        }

        [TestMethod]
        public void StealTest2()
        {
            Steal c = new Steal(CardSuit.Club, (byte)1);
            Assert.AreEqual("Steal", c.ToString());
        }

        [TestMethod]
        public void BreakTest()
        {
            Break c = new Break(CardSuit.Club, (byte)1);
            Assert.AreEqual("Break Description", c.getDescription());
        }

        [TestMethod]
        public void BreakTest2()
        {
            Break c = new Break(CardSuit.Club, (byte)1);
            Assert.AreEqual("Break", c.ToString());
        }

        [TestMethod]
        public void CaptureTest()
        {
            Capture c = new Capture(CardSuit.Club, (byte)1);
            Assert.AreEqual("Capture Description", c.getDescription());
        }

        [TestMethod]
        public void CaptureTest2()
        {
            Capture c = new Capture(CardSuit.Club, (byte)1);
            Assert.AreEqual("Capture", c.ToString());
        }


        [TestMethod]
        public void StarvationTest()
        {
            Starvation c = new Starvation(CardSuit.Club, (byte)1);
            Assert.AreEqual("Starvation Description", c.getDescription());
        }

        [TestMethod]
        public void StarvationTest2()
        {
            Starvation c = new Starvation(CardSuit.Club, (byte)1);
            Assert.AreEqual("Starvation", c.ToString());
        }

        [TestMethod]
        public void CrossbowTest()
        {
            Crossbow c = new Crossbow(CardSuit.Club, (byte)1);
            Assert.AreEqual("Crossbow Description", c.getDescription());
        }

        [TestMethod]
        public void CrossbowTest2()
        {
            Crossbow c = new Crossbow(CardSuit.Club, (byte)1);
            Assert.AreEqual("Crossbow", c.ToString());
        }

        [TestMethod]
        public void IceSwordTest()
        {
            IceSword c = new IceSword(CardSuit.Club, (byte)1);
            Assert.AreEqual("IceSword Description", c.getDescription());
        }

        [TestMethod]
        public void IceSwordTest2()
        {
            IceSword c = new IceSword(CardSuit.Club, (byte)1);
            Assert.AreEqual("IceSword", c.ToString());
        }

        [TestMethod]
        public void ScimitarTest()
        {
            Scimitar c = new Scimitar(CardSuit.Club, (byte)1);
            Assert.AreEqual("Scimitar Description", c.getDescription());
        }

        [TestMethod]
        public void ScimitarTest2()
        {
            Scimitar c = new Scimitar(CardSuit.Club, (byte)1);
            Assert.AreEqual("Scimitar", c.ToString());
        }

        [TestMethod]
        public void BlackShieldTest()
        {
            BlackShield c = new BlackShield(CardSuit.Club, (byte)1);
            Assert.AreEqual("BlackShield Description", c.getDescription());
        }

        [TestMethod]
        public void BlackShieldTest2()
        {
            BlackShield c = new BlackShield(CardSuit.Club, (byte)1);
            Assert.AreEqual("BlackShield", c.ToString());
        }

        [TestMethod]
        public void EightTrigramsTest()
        {
            EightTrigrams c = new EightTrigrams(CardSuit.Club, (byte)1);
            Assert.AreEqual("EightTrigrams Description", c.getDescription());
        }

        [TestMethod]
        public void EightTrigramsTest2()
        {
            EightTrigrams c = new EightTrigrams(CardSuit.Club, (byte)1);
            Assert.AreEqual("EightTrigrams", c.ToString());
        }


    }
}
