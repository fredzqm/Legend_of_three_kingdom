using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{

    /// <summary>
    /// Claas of different kinds cards
    /// </summary>
    public abstract class Card
    {/// <summary>
    /// type of card
    /// </summary>
        public CardType type { get; }
        /// <summary>
        /// suit of card
        /// </summary>
        public CardSuit suit { get; }
        /// <summary>
        /// card's number
        /// </summary>
        public byte num { get; }
        /// <summary>
        /// create card
        /// </summary>
        /// <param name="suit"></param>
        /// <param name="type"></param>
        /// <param name="num"></param>
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
        /// <summary>
        /// get number of target for this card
        /// </summary>
        /// <returns></returns>
        public abstract int numOfTargets();
        /// <summary>
        ///  get a string as card description
        /// </summary>
        /// <returns></returns>
        public abstract string getDescription();

        public override string ToString()
        {
            return type.ToString();
        }
    }
    /// <summary>
    /// subclass of card 
    /// </summary>
    public abstract class BasicCard : Card
    {
        /// <summary>
        /// create basic card
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <param name="n"></param>
        public BasicCard(CardSuit s, CardType t, byte n) : base(s, t, n) { }
    }
    /// <summary>
    /// subclass of card
    /// </summary>
    public abstract class ToolCard : Card
    {
        /// <summary>
        /// create tool card
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <param name="n"></param>
        public ToolCard(CardSuit s, CardType t, byte n) : base(s, t, n) { }
    }
    /// <summary>
    /// subclass of delaytoolcard
    /// </summary>
    public abstract class DelayToolCard : ToolCard
    {
        /// <summary>
        /// create delaytool card
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <param name="n"></param>
        public DelayToolCard(CardSuit s, CardType t, byte n) : base(s, t, n) { }

        public override int numOfTargets()
        {
            return 1;
        }

    }
    /// <summary>
    /// subclass of card
    /// </summary>
    public abstract class NonDelayToolCard : ToolCard
    {
        /// <summary>
        /// create non delay tool card
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <param name="n"></param>
        public NonDelayToolCard(CardSuit s, CardType t, byte n) : base(s, t, n) { }
    }
    /// <summary>
    /// subclass of card
    /// </summary>
    public abstract class Equipment : Card
    {
        /// <summary>
        /// create euipment card
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <param name="n"></param>
        public Equipment(CardSuit s, CardType t, byte n) : base(s, t, n) { }
        public override int numOfTargets()
        {
            return 0;
        }
    }
    /// <summary>
    /// subclass of Euipment card
    /// </summary>
    public abstract class Weapon : Equipment
    {
        /// <summary>
        /// create weapon
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <param name="n"></param>
        public Weapon(CardSuit s, CardType t, byte n) : base(s, t, n) { }
    }
    /// <summary>
    /// subclass of Equipment
    /// </summary>
    public abstract class Shield : Equipment
    {
        /// <summary>
        /// create shield
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <param name="n"></param>
        public Shield(CardSuit s, CardType t, byte n) : base(s, t, n) { }
    }

    /// <summary>
    /// Incomplete now, Type pf cards
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

    /// <summary>
    /// kinds of cards
    /// </summary>
    public enum CardCategory
    {
        Basic,
        Tool,
        DelayTool,
        Weapon,
        Shield
    }

}
