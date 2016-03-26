using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Model;
using System;
using System.Collections.Generic;
using Legends_of_the_Three_Kingdoms.Model;

namespace LOTK_Test.ModelTest
{

    [TestClass]
    public class CardSetTest
    {
        [TestMethod]
        public void CardSetConstructTest()
        {
            Card c = Card.ConstructCard(CardSuit.Club, CardType.Attack, 0);
            ICollection<Card> ls = new List<Card>();
            ls.Add(c);
            CardSet s = new CardSet(ls);
            Assert.AreEqual(c, s[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(NoCardException))]
        public void DiscardUndefinedCardExceptionTest()
        {
            ICollection<Card> ls = new List<Card>();
            ls.Add(Card.ConstructCard(CardSuit.Club, CardType.Attack, 0));
            ls.Add(Card.ConstructCard(CardSuit.Club, CardType.Miss, 1));
            ls.Add(Card.ConstructCard(CardSuit.Diamond, CardType.Miss, 2));
            ls.Add(Card.ConstructCard(CardSuit.Spade, CardType.Attack, 3));
            ls.Add(Card.ConstructCard(CardSuit.Club, CardType.Wine, 4));
            CardSet s = new CardSet(ls);
            s.discard(Card.ConstructCard(CardSuit.Spade, CardType.Wine, 5));
        }

        [TestMethod]
        public void ShuffleOneCardTest()
        {
            Card a = Card.ConstructCard(CardSuit.Club, CardType.Attack, 0);
            ICollection<Card> ls = new List<Card>();
            ls.Add(a);
            CardSet s = new CardSet(ls);
            Assert.AreEqual(a, s.pop());
        }

        [TestMethod]
        public void ShuffleTwoCardTest()
        {
            Card a = Card.ConstructCard(CardSuit.Club, CardType.Attack, 0);
            Card b = Card.ConstructCard(CardSuit.Club, CardType.Miss, 1);
            ICollection<Card> ls = new List<Card>();
            ls.Add(a);
            ls.Add(b);
            CardSet s = new CardSet(ls);
            Card x = s.pop();
            Card y = s.pop();
            Assert.IsTrue((a.Equals(x) && b.Equals(y))
                || (a.Equals(y) && b.Equals(x)));
        }

        [TestMethod]
        public void ShuffleManyCardTest()
        {
            List<Card> ls = new List<Card>();
            ls.Add(Card.ConstructCard(CardSuit.Club, CardType.Attack, 0));
            ls.Add(Card.ConstructCard(CardSuit.Club, CardType.Miss, 1));
            ls.Add(Card.ConstructCard(CardSuit.Diamond, CardType.Miss, 2));
            ls.Add(Card.ConstructCard(CardSuit.Spade, CardType.Attack, 3));
            ls.Add(Card.ConstructCard(CardSuit.Club, CardType.Wine, 4));

            int size = ls.Count;
            CardSet s = new CardSet(ls);
            for (int i = 0; i < size; i++)
            {
                Card c = s.pop();
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
            lsbackup.Add(Card.ConstructCard(CardSuit.Club, CardType.Attack, 0));
            lsbackup.Add(Card.ConstructCard(CardSuit.Club, CardType.Miss, 1));
            lsbackup.Add(Card.ConstructCard(CardSuit.Diamond, CardType.Miss, 2));
            lsbackup.Add(Card.ConstructCard(CardSuit.Spade, CardType.Attack, 3));
            lsbackup.Add(Card.ConstructCard(CardSuit.Club, CardType.Wine, 4));

            int size = lsbackup.Count;
            CardSet s = new CardSet(lsbackup);

            ls = new List<Card>(lsbackup);
            for (int i = 0; i < size; i++)
            {
                Card c = s.pop();
                ls.Remove(c);
            }
            s.pop();
        }

        [TestMethod]
        public void CardPileRunoutWithDiscardTest()
        {
            List<Card> ls, lsbackup = new List<Card>();
            lsbackup.Add(Card.ConstructCard(CardSuit.Club, CardType.Attack, 0));
            lsbackup.Add(Card.ConstructCard(CardSuit.Club, CardType.Miss, 1));
            lsbackup.Add(Card.ConstructCard(CardSuit.Diamond, CardType.Miss, 2));
            lsbackup.Add(Card.ConstructCard(CardSuit.Spade, CardType.Attack, 3));
            lsbackup.Add(Card.ConstructCard(CardSuit.Club, CardType.Wine, 4));

            int size = lsbackup.Count;
            CardSet s = new CardSet(lsbackup);

            for (int j = 0; j < 12; j++)
            {
                ls = new List<Card>(lsbackup);
                for (int i = 0; i < size; i++)
                {
                    Card c = s.pop();
                    Assert.IsTrue(ls.Contains(c));
                    ls.Remove(c);
                    s.discard(c);
                }
                Assert.AreEqual(0, ls.Count);
            }
        }

        [TestMethod]
        public void CardPileRandomeDiscardBackTest()
        {
            List<Card> ls, lsbackup = new List<Card>();
            lsbackup.Add(Card.ConstructCard(CardSuit.Club, CardType.Attack, 0));
            lsbackup.Add(Card.ConstructCard(CardSuit.Club, CardType.Miss, 1));
            lsbackup.Add(Card.ConstructCard(CardSuit.Diamond, CardType.Miss, 2));
            lsbackup.Add(Card.ConstructCard(CardSuit.Spade, CardType.Attack, 3));
            lsbackup.Add(Card.ConstructCard(CardSuit.Club, CardType.Wine, 4));

            int size = lsbackup.Count;
            CardSet s = new CardSet(lsbackup);

            Card c;
            for (int j = size; j > 0; j--)
            {
                ls = new List<Card>(lsbackup);
                for (int i = 0; i < j - 1; i++)
                {
                    c = s.pop();
                    Assert.IsTrue(ls.Contains(c));
                    ls.Remove(c);
                    s.discard(c);
                }
                c = s.pop();
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
            Card a = Card.ConstructCard(CardSuit.Club, CardType.Attack, 0);
            ls.Add(a);
            Card b = Card.ConstructCard(CardSuit.Club, CardType.Miss, 1);
            ls.Add(b);
            Card c = Card.ConstructCard(CardSuit.Diamond, CardType.Miss, 2);
            ls.Add(c);
            Card d = Card.ConstructCard(CardSuit.Spade, CardType.Attack, 3);
            ls.Add(d);
            Card e = Card.ConstructCard(CardSuit.Club, CardType.Wine, 4);
            ls.Add(e);
            CardSet s = new CardSet(ls);

            Assert.AreEqual(a, s[s[a]]);
            Assert.AreEqual(b, s[s[b]]);
            Assert.AreEqual(c, s[s[c]]);
            Assert.AreEqual(d, s[s[d]]);
            Assert.AreEqual(e, s[s[e]]);
        }
    }
    
    [TestClass]
    public class CardTest
    {

        [TestMethod]
        public void CardCategoryTest()
        {
            Assert.IsTrue( (Card.ConstructCard(CardSuit.Club, CardType.Attack, 0)) is BasicCard );
            Assert.IsTrue( (Card.ConstructCard(CardSuit.Club, CardType.Miss, 0)) is BasicCard );
            Assert.IsTrue( (Card.ConstructCard(CardSuit.Club, CardType.Wine, 0)) is BasicCard );
            Assert.IsTrue( (Card.ConstructCard(CardSuit.Club, CardType.Peach, 0)) is BasicCard );
            Assert.IsTrue( (Card.ConstructCard(CardSuit.Club, CardType.Negate, 0)) is ToolCard );
            Assert.IsTrue( (Card.ConstructCard(CardSuit.Club, CardType.Barbarians, 0)) is ToolCard );
            Assert.IsTrue( (Card.ConstructCard(CardSuit.Club, CardType.HailofArrow, 0)) is ToolCard );
            Assert.IsTrue( (Card.ConstructCard(CardSuit.Club, CardType.PeachGarden, 0)) is ToolCard );
            Assert.IsTrue( (Card.ConstructCard(CardSuit.Club, CardType.Wealth, 0)) is ToolCard );
            Assert.IsTrue( (Card.ConstructCard(CardSuit.Club, CardType.Steal, 0)) is ToolCard );
            Assert.IsTrue( (Card.ConstructCard(CardSuit.Club, CardType.Break, 0)) is ToolCard );
            Assert.IsTrue( (Card.ConstructCard(CardSuit.Club, CardType.Capture, 0)) is DelayToolCard );
            Assert.IsTrue( (Card.ConstructCard(CardSuit.Club, CardType.Starvation, 0)) is DelayToolCard );
            Assert.IsTrue( (Card.ConstructCard(CardSuit.Club, CardType.Crossbow, 0)) is Weapon );
            Assert.IsTrue( (Card.ConstructCard(CardSuit.Club, CardType.IceSword, 0)) is Weapon );
            Assert.IsTrue( (Card.ConstructCard(CardSuit.Club, CardType.Scimitar, 0)) is Weapon );
            Assert.IsTrue( (Card.ConstructCard(CardSuit.Club, CardType.BlackShield, 0)) is Shield );
            Assert.IsTrue( (Card.ConstructCard(CardSuit.Club, CardType.EightTrigrams, 0)) is Shield );
        }

        [TestMethod]
        public void UsageTest()
        {
            Card x;
            x = Card.ConstructCard(CardSuit.Club, CardType.Attack, 0);
            Assert.AreEqual(1, x.numOfTargets());
            x = Card.ConstructCard(CardSuit.Club, CardType.Miss, 0);
            Assert.AreEqual(-1, x.numOfTargets()); // this means not usable
            x = Card.ConstructCard(CardSuit.Club, CardType.Wine, 0);
            Assert.AreEqual(0, x.numOfTargets());
            x = Card.ConstructCard(CardSuit.Club, CardType.Peach, 0);
            Assert.AreEqual(0, x.numOfTargets());
            x = Card.ConstructCard(CardSuit.Club, CardType.Negate, 0);
            Assert.AreEqual(-1, x.numOfTargets());
            x = Card.ConstructCard(CardSuit.Club, CardType.Barbarians, 0);
            Assert.AreEqual(0, x.numOfTargets());
            x = Card.ConstructCard(CardSuit.Club, CardType.HailofArrow, 0);
            Assert.AreEqual(0, x.numOfTargets());
            x = Card.ConstructCard(CardSuit.Club, CardType.PeachGarden, 0);
            Assert.AreEqual(0, x.numOfTargets());
            x = Card.ConstructCard(CardSuit.Club, CardType.Wealth, 0);
            Assert.AreEqual(0, x.numOfTargets());
            x = Card.ConstructCard(CardSuit.Club, CardType.Steal, 0);
            Assert.AreEqual(1, x.numOfTargets());
            x = Card.ConstructCard(CardSuit.Club, CardType.Break, 0);
            Assert.AreEqual(1, x.numOfTargets());
            x = Card.ConstructCard(CardSuit.Club, CardType.Capture, 0);
            Assert.AreEqual(1, x.numOfTargets());
            x = Card.ConstructCard(CardSuit.Club, CardType.Starvation, 0);
            Assert.AreEqual(1, x.numOfTargets());
            x = Card.ConstructCard(CardSuit.Club, CardType.Crossbow, 0);
            Assert.AreEqual(0, x.numOfTargets());
            x = Card.ConstructCard(CardSuit.Club, CardType.IceSword, 0);
            Assert.AreEqual(0, x.numOfTargets());
            x = Card.ConstructCard(CardSuit.Club, CardType.Scimitar, 0);
            Assert.AreEqual(0, x.numOfTargets());
            x = Card.ConstructCard(CardSuit.Club, CardType.BlackShield, 0);
            Assert.AreEqual(0, x.numOfTargets());
            x = Card.ConstructCard(CardSuit.Club, CardType.EightTrigrams, 0);
            Assert.AreEqual(0, x.numOfTargets());
        }

    }
}
