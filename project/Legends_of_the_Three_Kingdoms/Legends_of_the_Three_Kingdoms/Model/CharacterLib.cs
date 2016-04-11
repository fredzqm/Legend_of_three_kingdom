using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{   /// <summary>
/// Player:ZhangFei
/// </summary>
    class ZhangFei : Player
    {/// <summary>
    /// create ZhangFei
    /// </summary>
    /// <param name="pos"></param>
        public ZhangFei(int pos) :  base(pos, "Zhang Fei", "Zhang Fei has no restrictions on how many times he can attack during his turn", 4)
        {

        }

        public override bool canAttack(AttackPhase curPhase, IGame game)
        {
            return (curPhase.targets.Length > curPhase.attack.numOfTargets() || curPhase.targets.Length == 0 || curPhase.targets[0] == this);
        }

    }
    /// <summary>
    /// Player LiuBei
    /// </summary>
    class LiuBei : Player
    {
        /// <summary>
        /// create LiuBei
        /// </summary>
        /// <param name="pos"></param>
        public LiuBei(int pos) : base(pos, "Liu Bei", "Liu Bei's can give any number of his hand cards to any players. If he gives away more than one card, he recovers one unit of health", 4)
        {

        }

    }
    /// <summary>
    /// Player CaoCao
    /// </summary>
    class CaoCao : Player
    {   /// <summary>
    /// create CaoCao
    /// </summary>
    /// <param name="pos"></param>
        public CaoCao(int pos) : base(pos, "Cao Cao", "When Cao Cao is damaged by a card, he can immediately put it into his hand",4)
        { 

        }

    }
    /// <summary>
    /// player SunQuan
    /// </summary>
    class SunQuan : Player
    {/// <summary>
    /// create SunQuan
    /// </summary>
    /// <param name="pos"></param>
        public SunQuan(int pos) : base(pos, "Sun Quan", "Once during his turn, Sun Quan can discard any number of cards to draw the same number", 4)
        {

        }

    }
    /// <summary>
    /// Player Lumeng
    /// </summary>
    class LuMeng : Player
    {   /// <summary>
    /// Create Lumeng
    /// </summary>
    /// <param name="pos"></param>
        public LuMeng(int pos) : base(pos, "Lu Meng", "If Lu Meng does not use any Attack cards during his turn, he can skip his discard phase", 4)
        {

        }

    }
}
