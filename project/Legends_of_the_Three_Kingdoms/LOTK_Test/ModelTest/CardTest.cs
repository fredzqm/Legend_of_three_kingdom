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
            Card c = new Card(CardSuit.Club, CardType.Attack);
            CardSet s = new CardSet(10);
            s[0] = c;
            Assert.AreSame(c, s[0] );
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RedefineCardTest()
        {
            Card c = new Card(CardSuit.Club, CardType.Attack);
            Card b = new Card(CardSuit.Club, CardType.Miss);
            CardSet s = new CardSet(10);
            s[0] = c;
            s[0] = b;
        }

        public void ShuffleOneCardTest()
        {
            Card a = new Card(CardSuit.Club, CardType.Attack);
            CardSet s = new CardSet(1);
            s[0] = a;
            s.shuffle();
            Assert.AreSame(s.pop(), a);
        }

        [TestMethod]
        public void ShuffleTwoCardTest()
        {
            Card a = new Card(CardSuit.Club, CardType.Attack);
            Card b = new Card(CardSuit.Club, CardType.Miss);
            CardSet s = new CardSet(2);
            s[0] = a;
            s[1] = b;
            s.shuffle();
            Card x = s.pop();
            Card y = s.pop();
            Assert.IsTrue(((a==x)&&(b==y)) || (a == y) && (b == x));
        }

    }
}
