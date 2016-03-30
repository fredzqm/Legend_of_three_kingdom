using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{

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

        public override int numOfTargets()
        {
            return 1;
        }

    }
    public abstract class NonDelayToolCard : ToolCard
    {
        public NonDelayToolCard(CardSuit s, CardType t, byte n) : base(s, t, n) { }
    }

    public abstract class Equipment : Card
    {
        public Equipment(CardSuit s, CardType t, byte n) : base(s, t, n) { }
        public override int numOfTargets()
        {
            return 0;
        }
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
