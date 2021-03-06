﻿using System;
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
        public PlayerType playerType { get; }
        public string name { get; } 
        public string description { get; }

        public List<Card> handCards { get; set; }
        public int healthLimit { get; }
        public int health { get;  set; }

        public Card weapon { get; set; }
        public Card shield { get; set; }

        private bool dead;

        public bool isDead()
        {
            return dead;
        }


        public Player(int pos, string name, string descript, int healthLimit, PlayerType type)
        {
            if (pos < 0)
            {
                throw new ArgumentOutOfRangeException(Legends_of_the_Three_Kingdoms.Properties.Resources.pos_is_negative);
            }
            else if (name == null)
            {
                throw new EmptyException(Legends_of_the_Three_Kingdoms.Properties.Resources.name_is_null);
            }
            else if (descript == null)
            {
                throw new EmptyException(Legends_of_the_Three_Kingdoms.Properties.Resources.descripty_is_null);
            }
            else if (healthLimit < 0)
            {
                throw new ArgumentOutOfRangeException(Legends_of_the_Three_Kingdoms.Properties.Resources.healthlimit_is_negative);
            }
            playerID = pos;
            handCards = new List<Card>();
            weapon = null;
            shield = null;
            this.name = name;
            this.description = descript;
            this.health = healthLimit;
            this.healthLimit = healthLimit;
            dead = false;
            this.playerType = type;
        }
    

        /// <summary>
        /// create player 
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="name"></param>
        /// <param name="descript"></param>
        /// <param name="healthLimit"></param>
        public Player(int pos, string name, string descript, int healthLimit) : this(pos, name, descript, healthLimit, PlayerType.Undefined){}

        /// <summary>
        /// create player 
        /// </summary>
        /// <param name="pos"></param>
        public Player(int pos) : this(pos, "Player Name", Legends_of_the_Three_Kingdoms.Properties.Resources.Player_Description_at + pos, 4){}

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

        public PhaseList die(IGame game)
        {
            dead = true;
            return new PhaseList();
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
            if (health <= 0)
            {
                return new PhaseList(new AskForHelpPhase(this, harmPhase));
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
            if (this.healthLimit >= health + recoverPhase.recover)
            {
                health += recoverPhase.recover;
            }
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
            game.cards.discardOne(card);
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
            try
            {
                handCards.AddRange(game.cards.drawCard(num));
            }catch (NoCardException e)
            {
                throw new NoCardException(Legends_of_the_Three_Kingdoms.Properties.Resources.cannot_draw_card, e);
            }
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

    public enum PlayerType
    {
        King,
        Rebel,
        Loyal,
        Spy,
        Undefined
    }
}
