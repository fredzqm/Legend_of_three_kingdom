using Legends_of_the_Three_Kingdoms.Model;
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

        /// <summary>
        /// create a cardset given an list of cards
        /// </summary>
        /// <param name="cls">A collection for all cards</param>
        public CardSet(ICollection<Card> cls)
        {
            cardLs = new Card[cls.Count];
            cardIDs = new Dictionary<Card, int>();
            IEnumerator<Card> itr = cls.GetEnumerator();
            for (int i = 0; i < cls.Count; i++)
            {
                itr.MoveNext();
                cardLs[i] = itr.Current;
                cardIDs[cardLs[i]] = i;
            }
            cardPile = new LinkedList<Card>(cardLs);
            discardPile = new LinkedList<Card>();
            cardPile.OrderBy(a => Guid.NewGuid());
        }

        /// <summary>
        /// Known get the card instance with cardID
        /// This should always be true
        /// <seealso cref="CardSet.getCardID(Card)">
        /// </summary>
        /// <param name="i">cardID</param>
        /// <returns>the corresponding Card instance</returns>
        public Card this[int i]
        {
            get
            {
                return cardLs[i];
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="c">Card instance</param>
        /// <returns>CardID</returns>
        public int this[Card c]
        {
            get
            {
                return cardIDs[c];
            }
        }

        public int getCardID(Card a)
        {
            return cardIDs[a];
        }

        /// <summary>
        /// pop the top card on the cardpile
        /// </summary>
        /// <returns>The top card</returns>
        public Card pop()
        {
            if (cardPile.Count == 0)
            {
                cardPile = discardPile;
                discardPile = new LinkedList<Card>();
                cardPile.OrderBy(a => Guid.NewGuid());
                if (cardPile.Count == 0)
                    throw new NoCardException("Run out of cards");
            }
            Card ret = cardPile.First();
            cardPile.RemoveFirst();
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c">The card Discarded</param>
        public void discard(Card c)
        {
            if (!cardIDs.ContainsKey(c))
                throw new NoCardException("Such Card Cannot be Found");
            discardPile.AddFirst(c);
        }


    }

    /// <summary>
    /// An instance 
    /// </summary>
    public abstract class Card
    {
        public CardType type { get; }
        public CardSuit suit { get; }
        public byte num { get; }

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

        public abstract int numOfTargets();

        public abstract string getDescription();

        /// <summary>
        /// Really for the purpose of testing.
        /// This is a uniform format for constructing a Card.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Card ConstructCard(CardSuit s, CardType t, byte v)
        {
            switch (t)
            {
                case CardType.Attack:
                    return new Attack(s, v);
                case CardType.Miss:
                    return new Miss(s, v);
                case CardType.Wine:
                    return new Wine(s, v);
                case CardType.Peach:
                    return new Peach(s, v);
                case CardType.Negate:
                    return new Negate(s, v);
                case CardType.Barbarians:
                    return new Barbarians(s, v);
                case CardType.HailofArrow:
                    return new HailofArrow(s, v);
                case CardType.PeachGarden:
                    return new PeachGarden(s, v);
                case CardType.Wealth:
                    return new Wealth(s, v);
                case CardType.Steal:
                    return new Steal(s, v);
                case CardType.Break:
                    return new Break(s, v);
                case CardType.Capture:
                    return new Capture(s, v);
                case CardType.Starvation:
                    return new Starvation(s, v);
                case CardType.Crossbow:
                    return new Crossbow(s, v);
                case CardType.IceSword:
                    return new IceSword(s, v);
                case CardType.Scimitar:
                    return new Scimitar(s, v);
                case CardType.BlackShield:
                    return new BlackShield(s, v);
                case CardType.EightTrigrams:
                    return new EightTrigrams(s, v);
                default:
                    throw new NotImplementedException();
            }
        }

    }

    public abstract class BasicCard : Card
    {
        public BasicCard(CardSuit s, CardType t, byte n) : base(s, t, n) { }
    }
    public abstract class ToolCard : Card
    {
        public ToolCard(CardSuit s, CardType t, byte n) : base(s, t, n) { }
    }
    public abstract class DelayToolCard : ToolCard
    {
        public DelayToolCard(CardSuit s, CardType t, byte n) : base(s, t, n) { }
    }
    public abstract class NonDelayToolCard : ToolCard
    {
        public NonDelayToolCard(CardSuit s, CardType t, byte n) : base(s, t, n) { }
    }

    public abstract class Equipment : Card
    {
        public Equipment(CardSuit s, CardType t, byte n) : base(s, t, n) { }
    }

    public abstract class Weapon : Equipment
    {
        public Weapon(CardSuit s, CardType t, byte n) : base(s, t, n) { }
    }
    public abstract class Shield : Equipment
    {
        public Shield(CardSuit s, CardType t, byte n) : base(s, t, n) { }
    }

    /// <summary>
    /// Incomplete now, 
    /// </summary>
    public enum CardType
    {
        Attack,
        Miss,
        Wine,
        Peach,
        Negate,
        Barbarians,
        HailofArrow,
        PeachGarden,
        Wealth,
        Steal,
        Break,
        Capture,
        Starvation,
        Crossbow,
        IceSword,
        Scimitar,
        BlackShield,
        EightTrigrams,
    }

    // Some helpful templete
    // switch(c.type) {
    //     case CardType.Attack: break;
    //     case CardType.Miss: break;
    //     case CardType.Wine: break;
    //     case CardType.Peach: break;
    //     case CardType.Negate: break;
    //     case CardType.Barbarians: break;
    //     case CardType.HailofArrow: break;
    //     case CardType.PeachGarden: break;
    //     case CardType.Wealth: break;
    //     case CardType.Steal: break;
    //     case CardType.Break: break;
    //     case CardType.Capture: break;
    //     case CardType.Starvation: break;
    //     case CardType.Crossbow: break;
    //     case CardType.IceSword: break;
    //     case CardType.Scimitar: break;
    //     case CardType.BlackShield: break;
    //     case CardType.EightTrigrams: break;
    //     default: break;
    // }

    /// <summary>
    /// Four kind of Suits
    /// </summary>
    public enum CardSuit
    {
        Heart,
        Spade,
        Diamond,
        Club,
    }

    public enum CardCategory
    {
        Basic,
        Tool,
        DelayTool,
        Weapon,
        Shield
    }

}
