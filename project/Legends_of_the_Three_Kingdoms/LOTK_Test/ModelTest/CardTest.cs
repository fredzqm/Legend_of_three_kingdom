using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Model;

namespace LOTK_Test.ModelTest
{
    [TestClass]
    class CardTest
    {
        [TestMethod]
        public void testConstructCard() {
            Card c = new Card(1, CardColor.Club, CardType.Attack);
            Card.defineCard(1, CardColor.Club, CardType.Attack);
            Assert.Equals(c, CardTest.find(1));
        }
    }
}
