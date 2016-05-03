using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Model;
using System.Collections.Generic;

namespace BDD_Specflow
{
    public class FakeCardSet : ICardSet
    {
        public List<Card> cards { get; set; }

        public FakeCardSet(List<Card> cards)
        {
            this.cards = cards;
        }

        public int this[Card c]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Card this[int i]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int cardPileCount
        {
            get
            {
                return 0;
            }
        }

        public int discardPileCount
        {
            get
            {
                return 0;
            }
        }

        public void discardCards(IEnumerable<Card> cards)
        {
        }

        public void discardOne(Card card)
        {

        }

        public IEnumerable<Card> drawCard(int num)
        {
            List<Card> cards = new List<Card>();
            try
            {
                for (int i = 0; i < num; i++)
                    cards.Add(drawOne());
            }
            catch (NoCardException e)
            {
                throw new NoCardException("The card stack is empty", e);
            }
            return cards;
        }

        public Card drawOne()
        {
            Card c = cards[0];
            cards.RemoveAt(0);
            return c;
        }
    }
}
