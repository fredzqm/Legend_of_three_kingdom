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
        public ZhangFei(int pos, PlayerType type) :  base(pos, Legends_of_the_Three_Kingdoms.Properties.Resources.Zhang_Fei, Legends_of_the_Three_Kingdoms.Properties.Resources.Zhang_Fei_has_no_restrictions_, 4, type) {}
        public ZhangFei(int pos) :  this(pos, PlayerType.Undefined){}
        public override string ToString()
        {
            return Legends_of_the_Three_Kingdoms.Properties.Resources.ZhangFei;
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
                throw new EmptyException(Legends_of_the_Three_Kingdoms.Properties.Resources.curphase_null);
            }
            else if (game == null)
            {
                throw new EmptyException(Legends_of_the_Three_Kingdoms.Properties.Resources.game_null);
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
        public LiuBei(int pos, PlayerType type) : base(pos, Legends_of_the_Three_Kingdoms.Properties.Resources.Liu_Bei, Legends_of_the_Three_Kingdoms.Properties.Resources.Liu_Bei_s_can_give_any_number_, 4, type) {}
        public override string ToString()
        {
            return Legends_of_the_Three_Kingdoms.Properties.Resources.LiuBei;
        }
        public LiuBei(int pos) :  this(pos, PlayerType.Undefined){ }

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
                throw new EmptyException(Legends_of_the_Three_Kingdoms.Properties.Resources.abilityaction_null);
            }
            else if (game == null)
            {
                throw new EmptyException(Legends_of_the_Three_Kingdoms.Properties.Resources.game_null);
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
        public CaoCao(int pos, PlayerType type) : base(pos, Legends_of_the_Three_Kingdoms.Properties.Resources.Cao_Cao, Legends_of_the_Three_Kingdoms.Properties.Resources.When_Cao_Cao_is_damaged_by_a_c, 4, type) {}
        public CaoCao(int pos) :  this(pos, PlayerType.Undefined){ }
        public override string ToString()
        {
            return Legends_of_the_Three_Kingdoms.Properties.Resources.CaoCao;
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
                throw new EmptyException(Legends_of_the_Three_Kingdoms.Properties.Resources.harmPhase_null);
            }
            else if (game == null)
            {
                throw new EmptyException(Legends_of_the_Three_Kingdoms.Properties.Resources.game_null);
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
        public SunQuan(int pos, PlayerType type) : base(pos, Legends_of_the_Three_Kingdoms.Properties.Resources.Sun_Quan, Legends_of_the_Three_Kingdoms.Properties.Resources.Once_during_his_turn_Sun_Quan_, 4, type) { }
        public SunQuan(int pos) :  this(pos, PlayerType.Undefined){}
        public override string ToString()
        {
            return Legends_of_the_Three_Kingdoms.Properties.Resources.SunQuan;
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
                throw new EmptyException(Legends_of_the_Three_Kingdoms.Properties.Resources.abilityActon_null);
            }
            else if (game == null)
            {
                throw new EmptyException(Legends_of_the_Three_Kingdoms.Properties.Resources.game_null);
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
        public LuMeng(int pos, PlayerType type) : base(pos, Legends_of_the_Three_Kingdoms.Properties.Resources.Lu_Meng, Legends_of_the_Three_Kingdoms.Properties.Resources.If_Lu_Meng_does_not_use_any_At, 4, type) {}
        public LuMeng(int pos) :  this(pos, PlayerType.Undefined){ }

        public override string ToString()
        {
            return Legends_of_the_Three_Kingdoms.Properties.Resources.LuMeng;
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
