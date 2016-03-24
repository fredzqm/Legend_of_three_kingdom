using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{
    /// <summary>
    /// CardSet is literally what it means. It represents the cardpile for this game
    /// The user can pop() the top of the cardStack, discard() card back to the cardpile.
    /// When the cardpile is empty, it will automatically shuffle the discarded card.
    /// 
    /// Each Card is associated with an ID, which can be get with get 
    /// </summary>
    public class CardSet
    {
        private readonly Card[] cardLs;
        private Dictionary<Card, int> cardIDs;
        private LinkedList<Card> cardPile;
        private LinkedList<Card> discardPile;

        private Dictionary<Card, int> dict;

        public CardSet(ICollection<Card> cls)
        {
            cardLs = new Card[cls.Count];
            dict = new Dictionary<Card, int>();
            cardIDs = new Dictionary<Card, int>();
            IEnumerator<Card> itr =  cls.GetEnumerator();
            for (int i = 0; i < cls.Count; i++)
            {
                itr.MoveNext();
                cardLs[i] = itr.Current;
                cardIDs[cardLs[i]]= i;
            }
            cardPile = new LinkedList<Card>(cardLs);
            discardPile = new LinkedList<Card>();
            cardPile.OrderBy(a => Guid.NewGuid());
        }

        public Card this[int i]
        {
            get
            {
                return cardLs[i];
            }
        }

        public Card pop()
        {
            if (cardPile.Count == 0)
            {
                cardPile = discardPile;
                discardPile = new LinkedList<Card>();
                cardPile.OrderBy(a => Guid.NewGuid());
                if (cardPile.Count == 0)
                    throw new Exception("Run out of cards");
            }
            Card ret = cardPile.First();
            cardPile.RemoveFirst();
            return ret;
        }
        public void discard(Card c)
        {
            if (!cardIDs.ContainsKey(c))
                throw new Exception();
            discardPile.AddFirst(c);
        }

    }

    public class Card
    {
        private CardType type;
        private CardSuit suit;
        private byte num;

        private int id = -1;
        public int CardID { get { return id; } }

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
            return "Card Description";
        }

        internal string getName()
        {
            return "Card Name";
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
