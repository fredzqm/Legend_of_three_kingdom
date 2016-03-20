using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Model;
using System;

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

    }
}
