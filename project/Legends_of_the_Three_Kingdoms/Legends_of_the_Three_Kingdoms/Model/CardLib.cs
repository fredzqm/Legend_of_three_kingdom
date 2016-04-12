using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{

    /// <summary>
    /// subclass of basic card
    /// </summary>
    public class Attack : BasicCard
    {
        /// <summary>
        /// create attack card
        /// </summary>
        /// <param name="s"></param>
        /// <param name="n"></param>
        public Attack(CardSuit s, byte n) : base(s, CardType.Attack, n) { }

        public override string getDescription()
        {
            return "Attack Description";
        }

        public override int numOfTargets()
        {
            return 1;
        }

        public override string ToString()
        {
            return "Attack";
        }
    }
    /// <summary>
    /// subclass of basic card
    /// </summary>
    public class Miss : BasicCard
    {
        /// <summary>
        /// create miss card
        /// </summary>
        /// <param name="s"></param>
        /// <param name="n"></param>
        public Miss(CardSuit s, byte n) : base(s, CardType.Miss, n) { }

        public override string getDescription()
        {
            return "Miss Description";
        }
        public override int numOfTargets()
        {
            return -1;
        }

        public override string ToString()
        {
            return "Miss";
        }
    }
    /// <summary>
    /// subclass of basic card
    /// </summary>
    public class Wine : BasicCard
    {

        /// <summary>
        /// create wine
        /// </summary>
        /// <param name="s"></param>
        /// <param name="n"></param>
        public Wine(CardSuit s, byte n) : base(s, CardType.Wine, n) { }

        public override string getDescription()
        {
            return "Wine Description";
        }
        public override int numOfTargets()
        {
            return 0;
        }

        public override string ToString()
        {
            return "Wine";
        }
    }
    /// <summary>
    /// subclass of basic card
    /// </summary>
    public class Peach : BasicCard
    {
        /// <summary>
        /// create peach
        /// </summary>
        /// <param name="s"></param>
        /// <param name="n"></param>
        public Peach(CardSuit s, byte n) : base(s, CardType.Peach, n) { }

        public override string getDescription()
        {
            return "Peach Description";
        }
        public override int numOfTargets()
        {
            return 0;
        }

        public override string ToString()
        {
            return "Peach";
        }
    }
    /// <summary>
    /// subclas of non delay tool card
    /// </summary>
    public class Negate : NonDelayToolCard
    {

        /// <summary>
        /// create negate card
        /// </summary>
        /// <param name="s"></param>
        /// <param name="n"></param>
        public Negate(CardSuit s, byte n) : base(s, CardType.Negate, n) { }

        public override string getDescription()
        {
            return "Negate Description";
        }
        public override int numOfTargets()
        {
            return -1;
        }

        public override string ToString()
        {
            return "Negate";
        }
    }
    /// <summary>
    /// subclass of non delay tool card
    /// </summary>
    public class Barbarians : NonDelayToolCard
    {
        /// <summary>
        /// create barbarians
        /// </summary>
        /// <param name="s"></param>
        /// <param name="n"></param>
        public Barbarians(CardSuit s, byte n) : base(s, CardType.Barbarians, n) { }

        public override string getDescription()
        {
            return "Barbarians Description";
        }
        public override int numOfTargets()
        {
            return 0;
        }

        public override string ToString()
        {
            return "Barbarians";
        }
    }
    /// <summary>
    /// subclass of nondelaytool card
    /// </summary>
    public class HailofArrow : NonDelayToolCard
    {
/// <summary>
/// create hail of arrow
/// </summary>
/// <param name="s"></param>
/// <param name="n"></param>
        public HailofArrow(CardSuit s, byte n) : base(s, CardType.HailofArrow, n) { }

        public override string getDescription()
        {
            return "HailofArrow Description";
        }
        public override int numOfTargets()
        {
            return 0;
        }

        public override string ToString()
        {
            return "HailofArrow";
        }
    }
    /// <summary>
    /// subclass of nondelaytool card
    /// </summary>
    public class PeachGarden : NonDelayToolCard
    {
        /// <summary>
        /// create peachgarden
        /// </summary>
        /// <param name="s"></param>
        /// <param name="n"></param>
        public PeachGarden(CardSuit s, byte n) : base(s, CardType.PeachGarden, n) { }

        public override string getDescription()
        {
            return "PeachGarden Description";
        }
        public override int numOfTargets()
        {
            return 0;
        }

        public override string ToString()
        {
            return "PeachGarden";
        }
    }
    /// <summary>
    /// subclass of nondelaytool card
    /// </summary>
    public class Wealth : NonDelayToolCard
    {

        public Wealth(CardSuit s, byte n) : base(s, CardType.Wealth, n) { }
        /// <summary>
        /// create wealth
        /// </summary>
        /// <returns></returns>
        public override string getDescription()
        {
            return "Wealth Description";
        }
        public override int numOfTargets()
        {
            return 0;
        }

        public override string ToString()
        {
            return "Wealth";
        }
    }
    /// <summary>
    /// subclass of nondelaytool card
    /// </summary>
    public class Steal : NonDelayToolCard
    {
        /// <summary>
        /// create steal
        /// </summary>
        /// <param name="s"></param>
        /// <param name="n"></param>
        public Steal(CardSuit s, byte n) : base(s, CardType.Steal, n) { }

        public override string getDescription()
        {
            return "Steal Description";
        }
        public override int numOfTargets()
        {
            return 1;
        }

        public override string ToString()
        {
            return "Steal";
        }
    }
    /// <summary>
    /// subclass of nondelaytool card
    /// </summary>
    public class Break : NonDelayToolCard
    {
        /// <summary>
        /// create break
        /// </summary>
        /// <param name="s"></param>
        /// <param name="n"></param>
        public Break(CardSuit s, byte n) : base(s, CardType.Break, n) { }

        public override string getDescription()
        {
            return "Break Description";
        }
        public override int numOfTargets()
        {
            return 1;
        }

        public override string ToString()
        {
            return "Break";
        }
    }
    /// <summary>
    /// subclass of nondelaytool card
    /// </summary>
    public class Capture : DelayToolCard
    {
        /// <summary>
        /// create capture
        /// </summary>
        /// <param name="s"></param>
        /// <param name="n"></param>
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
    /// <summary>
    /// subclass of delaytool card
    /// </summary>
    public class Starvation : DelayToolCard
    {
        /// <summary>
        /// create starvation
        /// </summary>
        /// <param name="s"></param>
        /// <param name="n"></param>
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
    /// <summary>
    /// subclass of weapon
    /// </summary>
    public class Crossbow : Weapon
    {
        /// <summary>
        /// create crosbow
        /// </summary>
        /// <param name="s"></param>
        /// <param name="n"></param>
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
    /// <summary>
    /// subclass of weapon
    /// </summary>
    public class IceSword : Weapon
    {
        /// <summary>
        /// create icesword
        /// </summary>
        /// <param name="s"></param>
        /// <param name="n"></param>
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
    /// <summary>
    /// subclass of weapon
    /// </summary>
    public class Scimitar : Weapon
    {
        /// <summary>
        /// create scimitar
        /// </summary>
        /// <param name="s"></param>
        /// <param name="n"></param>
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
    /// <summary>
    /// subclass of shield
    /// </summary>
    public class BlackShield : Shield
    {
        /// <summary>
        /// create black shield
        /// </summary>
        /// <param name="s"></param>
        /// <param name="n"></param>
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
    /// <summary>
    /// subclass of shield
    /// </summary>
    public class EightTrigrams : Shield
    {
        /// <summary>
        /// create eight trigrams
        /// </summary>
        /// <param name="s"></param>
        /// <param name="n"></param>
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
