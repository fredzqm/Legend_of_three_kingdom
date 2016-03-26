using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{


    public class Attack : Card
    {

        public Attack(CardSuit s, byte n) : base(s, CardType.Attack, n) { }

        public override string getDescription()
        {
            return "Attack Description";
        }

        public override string ToString()
        {
            return "Attack";
        }
    }
    public class Miss : Card
    {

        public Miss(CardSuit s, byte n) : base(s, CardType.Miss, n) { }

        public override string getDescription()
        {
            return "Miss Description";
        }

        public override string ToString()
        {
            return "Miss";
        }
    }
    public class Wine : Card
    {

        public Wine(CardSuit s, byte n) : base(s, CardType.Wine, n) { }

        public override string getDescription()
        {
            return "Wine Description";
        }

        public override string ToString()
        {
            return "Wine";
        }
    }
    public class Peach : Card
    {

        public Peach(CardSuit s, byte n) : base(s, CardType.Peach, n) { }

        public override string getDescription()
        {
            return "Peach Description";
        }

        public override string ToString()
        {
            return "Peach";
        }
    }
    public class Negate : Card
    {

        public Negate(CardSuit s, byte n) : base(s, CardType.Negate, n) { }

        public override string getDescription()
        {
            return "Negate Description";
        }

        public override string ToString()
        {
            return "Negate";
        }
    }
    public class Barbarians : Card
    {

        public Barbarians(CardSuit s, byte n) : base(s, CardType.Barbarians, n) { }

        public override string getDescription()
        {
            return "Barbarians Description";
        }

        public override string ToString()
        {
            return "Barbarians";
        }
    }
    public class HailofArrow : Card
    {

        public HailofArrow(CardSuit s, byte n) : base(s, CardType.HailofArrow, n) { }

        public override string getDescription()
        {
            return "HailofArrow Description";
        }

        public override string ToString()
        {
            return "HailofArrow";
        }
    }
    public class PeachGarden : Card
    {

        public PeachGarden(CardSuit s, byte n) : base(s, CardType.PeachGarden, n) { }

        public override string getDescription()
        {
            return "PeachGarden Description";
        }

        public override string ToString()
        {
            return "PeachGarden";
        }
    }
    public class Wealth : Card
    {

        public Wealth(CardSuit s, byte n) : base(s, CardType.Wealth, n) { }

        public override string getDescription()
        {
            return "Wealth Description";
        }

        public override string ToString()
        {
            return "Wealth";
        }
    }
    public class Steal : Card
    {

        public Steal(CardSuit s, byte n) : base(s, CardType.Steal, n) { }

        public override string getDescription()
        {
            return "Steal Description";
        }

        public override string ToString()
        {
            return "Steal";
        }
    }
    public class Break : Card
    {

        public Break(CardSuit s, byte n) : base(s, CardType.Break, n) { }

        public override string getDescription()
        {
            return "Break Description";
        }

        public override string ToString()
        {
            return "Break";
        }
    }
    public class Capture : Card
    {

        public Capture(CardSuit s, byte n) : base(s, CardType.Capture, n) { }

        public override string getDescription()
        {
            return "Capture Description";
        }

        public override string ToString()
        {
            return "Capture";
        }
    }
    public class Starvation : Card
    {

        public Starvation(CardSuit s, byte n) : base(s, CardType.Starvation, n) { }

        public override string getDescription()
        {
            return "Starvation Description";
        }

        public override string ToString()
        {
            return "Starvation";
        }
    }
    public class Crossbow : Card
    {

        public Crossbow(CardSuit s, byte n) : base(s, CardType.Crossbow, n) { }

        public override string getDescription()
        {
            return "Crossbow Description";
        }

        public override string ToString()
        {
            return "Crossbow";
        }
    }
    public class IceSword : Card
    {

        public IceSword(CardSuit s, byte n) : base(s, CardType.IceSword, n) { }

        public override string getDescription()
        {
            return "IceSword Description";
        }

        public override string ToString()
        {
            return "IceSword";
        }
    }
    public class Scimitar : Card
    {

        public Scimitar(CardSuit s, byte n) : base(s, CardType.Scimitar, n) { }

        public override string getDescription()
        {
            return "Scimitar Description";
        }

        public override string ToString()
        {
            return "Scimitar";
        }
    }
    public class BlackShield : Card
    {

        public BlackShield(CardSuit s, byte n) : base(s, CardType.BlackShield, n) { }

        public override string getDescription()
        {
            return "BlackShield Description";
        }

        public override string ToString()
        {
            return "BlackShield";
        }
    }
    public class EightTrigrams : Card
    {

        public EightTrigrams(CardSuit s, byte n) : base(s, CardType.EightTrigrams, n) { }

        public override string getDescription()
        {
            return "EightTrigrams Description";
        }

        public override string ToString()
        {
            return "EightTrigrams";
        }
    }


}
