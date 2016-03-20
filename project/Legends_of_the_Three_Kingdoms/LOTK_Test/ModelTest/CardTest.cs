using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Model;

namespace LOTK_Test.ModelTest
{
    [TestClass]
    public class CardTest
    {
        [TestMethod]
        public void testConstructCardSet() {
            Card c = new Card(CardSuit.Club, CardType.Attack);
            CardSet s = new CardSet(10);
            s[0] = c;
            Assert.AreSame(c, s[0] );
        }
    }
}
