using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Model;
using System;
using System.Collections.Generic;

namespace LOTK_Test.ModelTest
{
    /// <summary>
    /// This test class only involves Card and CardSet
    /// Card here is essentially a stub.
    /// </summary>
    [TestClass]
    public class CarSetUnitTest
    {
        [TestMethod]
        public void CardSetConstructTest()
        {
            Card c = new Attack(CardSuit.Club, 0);
            ICollection<Card> ls = new List<Card>();
            ls.Add(c);
            ICardSet s = new CardSet(ls);
            Assert.AreEqual(c, s[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(NoCardException))]
        public void DiscardUndefinedCardExceptionTest()
        {
            ICollection<Card> ls = new List<Card>();
            ls.Add(new Attack(CardSuit.Club, 0));
            ls.Add(new Miss(CardSuit.Club, 1));
            ls.Add(new Miss(CardSuit.Diamond, 2));
            ls.Add(new Attack(CardSuit.Spade, 3));
            ls.Add(new Wine(CardSuit.Club, 4));
            ICardSet s = new CardSet(ls);
            s.discardOne(new Wine(CardSuit.Spade, 5));
        }

        [TestMethod]
        public void ShuffleOneCardTest()
        {
            Card a = new Attack(CardSuit.Club, 0);
            ICollection<Card> ls = new List<Card>();
            ls.Add(a);
            ICardSet s = new CardSet(ls);
            Assert.AreEqual(a, s.drawOne());
        }

        [TestMethod]
        public void ShuffleTwoCardTest()
        {
            Card a = new Attack(CardSuit.Club, 0);
            Card b = new Miss(CardSuit.Club, 1);
            ICollection<Card> ls = new List<Card>();
            ls.Add(a);
            ls.Add(b);
            ICardSet s = new CardSet(ls);
            Card x = s.drawOne();
            Card y = s.drawOne();
            Assert.IsTrue((a.Equals(x) && b.Equals(y))
                || (a.Equals(y) && b.Equals(x)));
        }

        [TestMethod]
        public void ShuffleManyCardTest()
        {
            List<Card> ls = new List<Card>();
            ls.Add(new Attack(CardSuit.Club, 0));
            ls.Add(new Miss(CardSuit.Club, 1));
            ls.Add(new Miss(CardSuit.Diamond, 2));
            ls.Add(new Attack(CardSuit.Spade, 3));
            ls.Add(new Wine(CardSuit.Club, 4));

            int size = ls.Count;
            ICardSet s = new CardSet(ls);
            for (int i = 0; i < size; i++)
            {
                Card c = s.drawOne();
                Assert.IsTrue(ls.Contains(c));
                ls.Remove(c);
            }
            Assert.AreEqual(0, ls.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(NoCardException))]
        public void CardPileRunoutExceptionTest()
        {
            List<Card> ls, lsbackup = new List<Card>();
            lsbackup.Add(new Attack(CardSuit.Club, 0));
            lsbackup.Add(new Miss(CardSuit.Club, 1));
            lsbackup.Add(new Miss(CardSuit.Diamond, 2));
            lsbackup.Add(new Attack(CardSuit.Spade, 3));
            lsbackup.Add(new Wine(CardSuit.Club, 4));

            int size = lsbackup.Count;
            ICardSet s = new CardSet(lsbackup);

            ls = new List<Card>(lsbackup);
            for (int i = 0; i < size; i++)
            {
                Card c = s.drawOne();
                ls.Remove(c);
            }
            s.drawOne();
        }

        [TestMethod]
        public void CardPileRunoutWithDiscardTest()
        {
            List<Card> ls, lsbackup = new List<Card>();
            lsbackup.Add(new Attack(CardSuit.Club, 0));
            lsbackup.Add(new Miss(CardSuit.Club, 1));
            lsbackup.Add(new Miss(CardSuit.Diamond, 2));
            lsbackup.Add(new Attack(CardSuit.Spade, 3));
            lsbackup.Add(new Wine(CardSuit.Club, 4));

            int size = lsbackup.Count;
            ICardSet s = new CardSet(lsbackup);

            for (int j = 0; j < 12; j++)
            {
                ls = new List<Card>(lsbackup);
                for (int i = 0; i < size; i++)
                {
                    Card c = s.drawOne();
                    Assert.IsTrue(ls.Contains(c));
                    ls.Remove(c);
                    s.discardOne(c);
                }
                Assert.AreEqual(0, ls.Count);
            }
        }

        [TestMethod]
        public void CardPileRandomeDiscardBackTest()
        {
            List<Card> ls, lsbackup = new List<Card>();
            lsbackup.Add(new Attack(CardSuit.Club, 0));
            lsbackup.Add(new Miss(CardSuit.Club, 1));
            lsbackup.Add(new Miss(CardSuit.Diamond, 2));
            lsbackup.Add(new Attack(CardSuit.Spade, 3));
            lsbackup.Add(new Wine(CardSuit.Club, 4));

            int size = lsbackup.Count;
            ICardSet s = new CardSet(lsbackup);

            Card c;
            for (int j = size; j > 0; j--)
            {
                ls = new List<Card>(lsbackup);
                for (int i = 0; i < j - 1; i++)
                {
                    c = s.drawOne();
                    Assert.IsTrue(ls.Contains(c));
                    ls.Remove(c);
                    s.discardOne(c);
                }
                c = s.drawOne();
                Assert.IsTrue(ls.Contains(c));
                ls.Remove(c);
                lsbackup.Remove(c);
                Assert.AreEqual(0, ls.Count);
            }
        }


        [TestMethod]
        public void getCardIDTest()
        {
            List<Card> ls = new List<Card>();
            Card a = new Attack(CardSuit.Club, 0);
            ls.Add(a);
            Card b = new Miss(CardSuit.Club, 1);
            ls.Add(b);
            Card c = new Miss(CardSuit.Diamond, 2);
            ls.Add(c);
            Card d = new Attack(CardSuit.Spade, 3);
            ls.Add(d);
            Card e = new Wine(CardSuit.Club, 4);
            ls.Add(e);
            ICardSet s = new CardSet(ls);

            Assert.AreEqual(a, s[s[a]]);
            Assert.AreEqual(b, s[s[b]]);
            Assert.AreEqual(c, s[s[c]]);
            Assert.AreEqual(d, s[s[d]]);
            Assert.AreEqual(e, s[s[e]]);
        }
    }
    
    /// <summary>
    /// This test involves some basic category of differnet card
    /// They are all hard-coded attributes of each kind of cards
    /// </summary>
    [TestClass]
    public class CardUnitTest
    {

        [TestMethod]
        public void CardCategoryTest()
        {
            Assert.IsTrue( (new Attack(CardSuit.Club, 0)) is BasicCard );
            Assert.IsTrue( (new Miss(CardSuit.Club, 0)) is BasicCard );
            Assert.IsTrue( (new Wine(CardSuit.Club, 0)) is BasicCard );
            Assert.IsTrue( (new Peach(CardSuit.Club, 0)) is BasicCard );
            Assert.IsTrue( (new Negate(CardSuit.Club, 0)) is ToolCard );
            Assert.IsTrue( (new Barbarians(CardSuit.Club, 0)) is ToolCard );
            Assert.IsTrue( (new HailofArrow(CardSuit.Club, 0)) is ToolCard );
            Assert.IsTrue( (new PeachGarden(CardSuit.Club, 0)) is ToolCard );
            Assert.IsTrue( (new Wealth(CardSuit.Club, 0)) is ToolCard );
            Assert.IsTrue( (new Steal(CardSuit.Club, 0)) is ToolCard );
            Assert.IsTrue( (new Break(CardSuit.Club, 0)) is ToolCard );
            Assert.IsTrue( (new Capture(CardSuit.Club, 0)) is DelayToolCard );
            Assert.IsTrue( (new Starvation(CardSuit.Club, 0)) is DelayToolCard );
            Assert.IsTrue( (new Crossbow(CardSuit.Club, 0)) is Weapon );
            Assert.IsTrue( (new IceSword(CardSuit.Club, 0)) is Weapon );
            Assert.IsTrue( (new Scimitar(CardSuit.Club, 0)) is Weapon );
            Assert.IsTrue( (new BlackShield(CardSuit.Club, 0)) is Shield );
            Assert.IsTrue( (new EightTrigrams(CardSuit.Club, 0)) is Shield );
        }

        [TestMethod]
        public void UsageTest()
        {
            Card x;
            x = new Attack(CardSuit.Club, 0);
            Assert.AreEqual(1, x.numOfTargets());
            x = new Miss(CardSuit.Club, 0);
            Assert.AreEqual(-1, x.numOfTargets()); // this means not usable
            x = new Wine(CardSuit.Club, 0);
            Assert.AreEqual(0, x.numOfTargets());
            x = new Peach(CardSuit.Club, 0);
            Assert.AreEqual(0, x.numOfTargets());
            x = new Negate(CardSuit.Club, 0);
            Assert.AreEqual(-1, x.numOfTargets());
            x = new Barbarians(CardSuit.Club, 0);
            Assert.AreEqual(0, x.numOfTargets());
            x = new HailofArrow(CardSuit.Club, 0);
            Assert.AreEqual(0, x.numOfTargets());
            x = new PeachGarden(CardSuit.Club, 0);
            Assert.AreEqual(0, x.numOfTargets());
            x = new Wealth(CardSuit.Club, 0);
            Assert.AreEqual(0, x.numOfTargets());
            x = new Steal(CardSuit.Club, 0);
            Assert.AreEqual(1, x.numOfTargets());
            x = new Break(CardSuit.Club, 0);
            Assert.AreEqual(1, x.numOfTargets());
            x = new Capture(CardSuit.Club, 0);
            Assert.AreEqual(1, x.numOfTargets());
            x = new Starvation(CardSuit.Club, 0);
            Assert.AreEqual(1, x.numOfTargets());
            x = new Crossbow(CardSuit.Club, 0);
            Assert.AreEqual(0, x.numOfTargets());
            x = new IceSword(CardSuit.Club, 0);
            Assert.AreEqual(0, x.numOfTargets());
            x = new Scimitar(CardSuit.Club, 0);
            Assert.AreEqual(0, x.numOfTargets());
            x = new BlackShield(CardSuit.Club, 0);
            Assert.AreEqual(0, x.numOfTargets());
            x = new EightTrigrams(CardSuit.Club, 0);
            Assert.AreEqual(0, x.numOfTargets());
        }

    }
}
