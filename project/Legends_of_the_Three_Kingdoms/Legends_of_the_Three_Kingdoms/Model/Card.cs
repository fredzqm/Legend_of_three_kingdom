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

        private int id = -1;
        public int cardId
        {
            get
            {
                if (id == -1)
                    throw new Exception("CardID not specified (not placed into to a cardSet");
                return id;
            }
            set
            {
                if (id == -2)
                    throw new Exception("CardID cannot be redifined");
                id = value;
            }
        }

        public Card(CardSuit s, CardType t, byte n)
        {
            this.suit = s;
            this.type = t;
            this.num = n;
        }

        public override bool Equals(object obj)
        {
            Card x = obj as Card;
            return (x != null) && (type == x.type) && (suit == x.suit) && (num == x.num);
        }
        public override int GetHashCode()
        {
            return type.GetHashCode() + suit.GetHashCode() + num.GetHashCode();
        }
        public override string ToString()
        {
            return String.Format("Card {0}  {1}{3}", type, suit, num);
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
        private LinkedList<Card> discardPile;

        private Dictionary<Card, int> dict;

        public CardSet(int capacity)
        {
            ls = new Card[capacity];
            dict = new Dictionary<Card, int>();
        }

        public CardSet(List<Card> cls) : this(cls.Count)
        {
            for (int i = 0; i < cls.Count; i++)
            {
                this[i] = cls[i];
                this[i].cardId = i;
            }
            shuffle();
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
            if (cardPile.Count == 0)
            {
                reShuffle();
                if (cardPile.Count == 0)
                    throw new Exception("Run out of cards");
            }
            Card ret = cardPile.First();
            cardPile.RemoveFirst();
            return ret;
        }
        public void discard(Card c)
        {
            if (!this[c.cardId].Equals(c))
                throw new Exception("Cannot discard a unspecified Card");
            discardPile.AddFirst(c);
        }

        public void shuffle()
        {
            cardPile = new LinkedList<Card>(ls);
            discardPile = new LinkedList<Card>();
            cardPile.OrderBy(a => Guid.NewGuid());
        }

        public void reShuffle()
        {
            cardPile = discardPile;
            discardPile = new LinkedList<Card>();
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
