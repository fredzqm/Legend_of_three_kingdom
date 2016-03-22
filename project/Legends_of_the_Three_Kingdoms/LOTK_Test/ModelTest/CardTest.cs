using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Model;
using System;
using System.Collections.Generic;

namespace LOTK_Test.ModelTest
{
    
    [TestClass]
    public class CardSetTest
    {
        [TestMethod]
        public void CardSetConstructTest() {
            Card c = new Card(CardSuit.Club, CardType.Attack, 0);
            ICollection<Card> ls = new List<Card>();
            ls.Add(c);
            CardSet s = new CardSet(ls);
            Assert.AreEqual(c, s[0]);
        }

        //[TestMethod]
        //[ExpectedException(typeof(Exception))]
        //public void RedefineCardTest()
        //{
        //    Card c = new Card(CardSuit.Club, CardType.Attack, 0);
        //    Card b = new Card(CardSuit.Club, CardType.Miss, 1);
        //    CardSet s = new CardSet(10);
        //    s.addCard();
        //    s[0] = c;
        //    s[0] = b;
        //}

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void DiscardUndefinedCardExceptionTest()
        {
            ICollection<Card> ls = new List<Card>();
            ls.Add(new Card(CardSuit.Club, CardType.Attack, 0));
            ls.Add(new Card(CardSuit.Club, CardType.Miss, 1));
            ls.Add(new Card(CardSuit.Diamond, CardType.Miss, 2));
            ls.Add(new Card(CardSuit.Spade, CardType.Attack, 3));
            ls.Add(new Card(CardSuit.Club, CardType.Wine, 4));
            CardSet s = new CardSet(ls);
            s.discard(new Card(CardSuit.Spade, CardType.Wine, 5));
        }

        [TestMethod]
        public void ShuffleOneCardTest()
        {
            Card a = new Card(CardSuit.Club, CardType.Attack, 0);
            ICollection<Card> ls = new List<Card>();
            ls.Add(a);
            CardSet s = new CardSet(ls);
            s.shuffle();
            Assert.AreEqual(a, s.pop());
        }

        [TestMethod]
        public void ShuffleTwoCardTest()
        {
            Card a = new Card(CardSuit.Club, CardType.Attack, 0);
            Card b = new Card(CardSuit.Club, CardType.Miss, 1);
            ICollection<Card> ls = new List<Card>();
            ls.Add(a);
            ls.Add(b);
            CardSet s = new CardSet(ls);
            s.shuffle();
            Card x = s.pop();
            Card y = s.pop();
            Assert.IsTrue((a.Equals(x) && b.Equals(y)) 
                || (a.Equals(y) && b.Equals(x)));
        }

        [TestMethod]
        public void ShuffleManyCardTest()
        {
            List<Card> ls = new List<Card>();
            ls.Add(new Card(CardSuit.Club, CardType.Attack, 0));
            ls.Add(new Card(CardSuit.Club, CardType.Miss, 1));
            ls.Add(new Card(CardSuit.Diamond, CardType.Miss, 2));
            ls.Add(new Card(CardSuit.Spade, CardType.Attack, 3));
            ls.Add(new Card(CardSuit.Club, CardType.Wine, 4));

            int size = ls.Count;
            CardSet s = new CardSet(size);
            for (int i = 0; i < size; i++)
            {
                s[i] = ls[i];
            }
            s.shuffle();

            for (int i = 0; i < size; i++)
            {
                Card c = s.pop();
                Assert.IsTrue(ls.Contains(c));
                ls.Remove(c);
            }
            Assert.AreEqual(0,ls.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CardPileRunoutExceptionTest()
        {
            List<Card> ls, lsbackup = new List<Card>();
            lsbackup.Add(new Card(CardSuit.Club, CardType.Attack, 0));
            lsbackup.Add(new Card(CardSuit.Club, CardType.Miss, 1));
            lsbackup.Add(new Card(CardSuit.Diamond, CardType.Miss, 2));
            lsbackup.Add(new Card(CardSuit.Spade, CardType.Attack, 3));
            lsbackup.Add(new Card(CardSuit.Club, CardType.Wine, 4));

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
            lsbackup.Add(new Card(CardSuit.Club, CardType.Attack, 0));
            lsbackup.Add(new Card(CardSuit.Club, CardType.Miss, 1));
            lsbackup.Add(new Card(CardSuit.Diamond, CardType.Miss, 2));
            lsbackup.Add(new Card(CardSuit.Spade, CardType.Attack, 3));
            lsbackup.Add(new Card(CardSuit.Club, CardType.Wine, 4));

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

        //[TestMethod]
        //public void CardPileRepeatTest()
        //{
        //    List<Card> ls = new List<Card>();
        //    ls.Add(new Card(CardSuit.Club, CardType.Attack, 0));
        //    ls.Add(new Card(CardSuit.Club, CardType.Miss, 1));
        //    ls.Add(new Card(CardSuit.Diamond, CardType.Miss, 2));
        //    ls.Add(new Card(CardSuit.Spade, CardType.Attack, 3));
        //    ls.Add(new Card(CardSuit.Club, CardType.Wine, 4));
        //    List<Card> ls2 = new List<Card>(ls);

        //    int size = ls.Count;
        //    CardSet s = new CardSet(size + 10);
        //    for (int i = 0; i < size; i++)
        //    {
        //        s[i] = ls[i];
        //    }
        //    s.shuffle();

        //    for (int i = 0; i < size; i++)
        //    {
        //        Card c = s.pop();
        //        Assert.IsTrue(ls.Contains(c));
        //        ls.Remove(c);
        //    }
        //    Assert.AreEqual(0, ls.Count);
        //}

    }
}
