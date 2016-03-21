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
            CardSet s = new CardSet(10);
            s[0] = c;
            Assert.AreSame(c, s[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RedefineCardTest()
        {
            Card c = new Card(CardSuit.Club, CardType.Attack, 0);
            Card b = new Card(CardSuit.Club, CardType.Miss, 1);
            CardSet s = new CardSet(10);
            s[0] = c;
            s[0] = b;
        }

        [TestMethod]
        public void ShuffleOneCardTest()
        {
            Card a = new Card(CardSuit.Club, CardType.Attack, 0);
            CardSet s = new CardSet(1);
            s[0] = a;
            s.shuffle();
            Assert.AreEqual(a, s.pop());
        }

        [TestMethod]
        public void ShuffleTwoCardTest()
        {
            Card a = new Card(CardSuit.Club, CardType.Attack, 0);
            Card b = new Card(CardSuit.Club, CardType.Miss, 1);
            CardSet s = new CardSet(2);
            s[0] = a;
            s[1] = b;
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
        public void CardPileRunoutTest()
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
            Assert.AreEqual(0, ls.Count);
            // Second round, it should automatic shuffle
            for (int i = 0; i < size; i++)
            {
                Card c = s.pop();
                Assert.IsTrue(ls.Contains(c));
                ls.Remove(c);
            }
            Assert.AreEqual(0, ls.Count);
        }
    }
}
