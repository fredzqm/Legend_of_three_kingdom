using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{
    public class Card
    {
        private CardType type;
        private CardSuit suit;
        private byte num;

        public Card(CardSuit s, CardType t, byte n)
        {
            this.suit = s;
            this.type = t;
            this.num = n;
        }
        
        public override bool Equals(object obj)
        {
            Card x = obj as Card;
            return (x != null) && (type == x.type) && (suit == x.suit);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return String.Format("Card {0}  {1}{3}",type,suit, num);
        }

        internal string getDescription()
        {
            throw new NotImplementedException();
        }

        internal string getName()
        {
            throw new NotImplementedException();
        }
    }

    public class CardSet
    {
        private Card[] ls;
        private LinkedList<Card> cardPile;

        public CardSet(int capacity)
        {
            ls = new Card[100];
        }
        public Card this[int i]
        {
            get
            {
                return ls[i];
            }
            set
            {
                if (ls[i] == null)
                    ls[i] = value;
                else
                    throw new Exception("Redefien Card");
            }
        }

        public Card pop()
        {
            Card ret = cardPile.First();
            cardPile.RemoveFirst();
            return ret;
        }

        public void shuffle()
        {
            cardPile = new LinkedList<Card>(ls);
            cardPile.OrderBy(a => Guid.NewGuid());
        }
    }


    public enum CardSuit
    {
        Heart,
        Spade,
        Diamond,
        Club,
    }
    public enum CardType
    {
        Attack,
        Miss,
        Wine
    }
}
