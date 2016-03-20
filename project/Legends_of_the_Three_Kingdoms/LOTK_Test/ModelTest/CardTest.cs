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

        [TestMethod]
        public void PopCardOneTest()
        {
            Card a = new Card(CardSuit.Club, CardType.Attack);
            CardSet s = new CardSet(10);
            s[0] = a;
            Assert.AreSame(s.pop(), a);
        }
    }
}
