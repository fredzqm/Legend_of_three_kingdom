using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOTK.View;

namespace LOTK.Model
{
    /// <summary>
    /// The model that represents the default behaviour the player
    /// 
    /// </summary>
    public class Player
    {
        // basic and readonly properties, initialized in contructor
        public int playerID { get; }
        public string name { get; } 
        public string description { get; }

        public List<Card> handCards { get; }
        public int healthLimit { get; }
        public int health { get;  set; }

        public Card weapon { get; set; }
        public Card shield { get; set; }

        /// <summary>
        /// create player 
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="name"></param>
        /// <param name="descript"></param>
        /// <param name="healthLimit"></param>
        public Player(int pos, string name, string descript, int healthLimit)
        {
            playerID = pos;
            handCards = new List<Card>();
            weapon = null;
            shield = null;
            this.name = name;
            this.description = descript;
            this.health = healthLimit;
            this.healthLimit = healthLimit;
        }
        /// <summary>
        /// create player 
        /// </summary>
        /// <param name="pos"></param>
        public Player(int pos) : this(pos, "Player Name", "Player Description at" + pos, 4){}
        /// <summary>
        /// create player 
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="name"></param>
        /// <param name="descript"></param>
        public Player(int pos, string name, string descript) : this(pos, name, descript, 4) {}
        

        public static implicit operator int (Player p)
        {
            return p.playerID;
        }
        /// <summary>
        /// check if this player can attack
        /// </summary>
        /// <param name="curPhase"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        public virtual bool canNotAttack(AttackPhase curPhase, IGame game)
        {
            return (curPhase.targets.Length > curPhase.attack.numOfTargets() || curPhase.targets.Length == 0 || curPhase.actionPhase.attackCount >= 1 || curPhase.targets[0] == this);
        }

        // ----------------------------------------------------
        // The codes below specify are virtual methods of Players.
        // A new character can be created by overriden those method to customize player's behavior
        /// <summary>
        /// normal player get hurt behavior
        /// </summary>
        /// <param name="harmPhase"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        public virtual PhaseList harm(HarmPhase harmPhase, IGame game)
        {
            health -= harmPhase.harm;
            if (health < 0)
            {
                return new PhaseList(new AskForHelpPhase(this));
            }
            return new PhaseList();
        }
        /// <summary>
        /// normal player recover behavior
        /// </summary>
        /// <param name="recoverPhase"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        public virtual PhaseList recover(RecoverPhase recoverPhase, IGame game)
        {
            health += recoverPhase.recover;
            return new PhaseList();
        }
        /// <summary>
        /// normal player discard
        /// </summary>
        /// <param name="card"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        public virtual PhaseList discardCard(Card card, IGame game)
        {
            handCards.Remove(card);
            game.cards.discard(card);
            return new PhaseList();
        }
        /// <summary>
        /// normal player draw cards
        /// </summary>
        /// <param name="num"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        public virtual PhaseList drawCards(int num, IGame game)
        {
            handCards.AddRange(game.drawCard(num));
            return new PhaseList();
        }
        /// <summary>
        /// special for Lei bei
        /// </summary>
        /// <param name="abilityAction"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        public virtual PhaseList ability(AbilityAction abilityAction, IGame game)
        {
            return null;
        }
        /// <summary>
        /// special for Sun Quan
        /// </summary>
        /// <param name="abilityAction"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        public virtual PhaseList abilitySun(AbilityActionSun abilityAction, IGame game)
        {
            return null;
        }
        /// <summary>
        /// Special for Lu meng
        /// </summary>
        /// <returns></returns>
        public virtual int handcardCount()
        {
            return this.handCards.Count;
        }
    }
}
