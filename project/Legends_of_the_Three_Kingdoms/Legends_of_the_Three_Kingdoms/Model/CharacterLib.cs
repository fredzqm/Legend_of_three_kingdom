using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{   /// <summary>
/// Player:ZhangFei
/// </summary>
   public  class ZhangFei : Player
    {/// <summary>
     /// create ZhangFei
     /// </summary>
     /// <param name="pos ">this is the position of the player in All player</param>
        public ZhangFei(int pos) :  base(pos, "Zhang Fei", "Zhang Fei has no restrictions on how many times he can attack during his turn", 4)
        {

        }
        /// <summary>
        /// override of Player.canNotAttack()
        /// </summary>
        /// <param name="curPhase  ">this is the current game phase</param>
        /// <param name="game"> this is the current game</param>
        /// <returns></returns>
        public override bool canNotAttack(AttackPhase curPhase, IGame game)
        {
            if (curPhase == null)
            {
                throw new EmptyException("curphase null");
            }
            else if (game == null)
            {
                throw new EmptyException("game null");
            }
            else {
                return (curPhase.targets.Length > curPhase.attack.numOfTargets() || curPhase.targets.Length == 0 || curPhase.targets[0] == this);
            }
        }

    }
    /// <summary>
    /// Player LiuBei
    /// </summary>
    public class LiuBei : Player
    {
        /// <summary>
        /// create LiuBei
        /// </summary>
        /// <param name="pos"> this is the position of the player in All player</param>
        public LiuBei(int pos) : base(pos, "Liu Bei", "Liu Bei's can give any number of his hand cards to any players. If he gives away more than one card, he recovers one unit of health", 4)
        {

        }
        /// <summary>
        /// override of Player.ability()
        /// </summary>
        /// <param name="abilityAction ">this is the the action for abilityaction</param>
        /// <param name="game"> this is the current game</param>
        /// <returns></returns>
        public override PhaseList ability(AbilityAction abilityAction, IGame game)
        {
            if (abilityAction == null)
            {
                throw new EmptyException("abilityaction null");
            }
            else if (game == null)
            {
                throw new EmptyException("game null");
            }
            this.handCards.Remove(abilityAction.card);
            abilityAction.targets[0].handCards.Add(abilityAction.card);
            return null;
        }

    }
    /// <summary>
    /// Player CaoCao
    /// </summary>
    public class CaoCao : Player
    {   /// <summary>
        /// create CaoCao
        /// </summary>
        /// <param name="pos"> this is the position of the player in All player</param>
        public CaoCao(int pos) : base(pos, "Cao Cao", "When Cao Cao is damaged by a card, he can immediately put it into his hand", 4)
        {
        }
        /// <summary>
        /// override of Player.harm()
        /// </summary>
        /// <param name="harmPhase"> this is the phase of harm</param>
        /// <param name="game  ">this is the game</param>
        /// <returns></returns>
        public override PhaseList harm(HarmPhase harmPhase, IGame game)
        {
            if (harmPhase == null)
            {
                throw new EmptyException("harmPhase null");
            }
            else if (game == null)
            {
                throw new EmptyException("game null");
            }
            this.health -= harmPhase.harm;
            this.handCards.Add(harmPhase.card);
            if (health < 0)
            {
                return new PhaseList(new AskForHelpPhase(this, harmPhase));
            }
            return new PhaseList();
        }
    }


    /// <summary>
    /// player SunQuan
    /// </summary>
    public class SunQuan : Player
    {/// <summary>
     /// create SunQuan
     /// </summary>
     /// <param name="pos ">is the position of the player in All player</param>
        public SunQuan(int pos) : base(pos, "Sun Quan", "Once during his turn, Sun Quan can discard any number of cards to draw the same number", 4)
        {

        }
        /// <summary>
        /// override Player.abilitySun()
        /// </summary>
        /// <param name="abilityAction "> this is the action of ability</param>
        /// <param name="game ">this is the current game</param>
        /// <returns>null nothing needs to return</returns>
        public override PhaseList abilitySun(AbilityActionSun abilityAction, IGame game)
        {

            if (abilityAction == null)
            {
                throw new EmptyException("abilityActon null");
            }
            else if (game == null)
            {
                throw new EmptyException("game null");
            }

            this.drawCards(1, game);
            this.handCards.Remove(abilityAction.card);
            return null;
        }

    }
    /// <summary>
    /// Player Lumeng
    /// </summary>
    public class LuMeng : Player
    {   /// <summary>
        /// Create Lumeng
        /// </summary>
        /// <param name="pos">this is the position of the player in All player</param>
        public LuMeng(int pos) : base(pos, "Lu Meng", "If Lu Meng does not use any Attack cards during his turn, he can skip his discard phase", 2)
        {

        }
        /// <summary>
        /// override player.handcardCount()
        /// </summary>
        /// <returns></returns>
        public override int handcardCount()
        {
            return 0;
        }

    }
}
