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
        public string name { get; } 
        public string description { get; }

        public List<Card> handCards { get; }
        public int healthLimit { get; }
        public int health { get;  set; }

        public Card weapon { get; set; }
        public Card shield { get; set; }


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

        public Player(int pos) : this(pos, "Player Name", "Player Description at" + pos, 4){}
        public Player(int pos, string name, string descript) : this(pos, name, descript, 4) {}
        

        public static implicit operator int (Player p)
        {
            return p.playerID;
        }

        public virtual bool canNotAttack(AttackPhase curPhase, IGame game)
        {
            return (curPhase.targets.Length > curPhase.attack.numOfTargets() || curPhase.targets.Length == 0 || curPhase.actionPhase.attackCount >= 1 || curPhase.targets[0] == this);
        }

        // ----------------------------------------------------
        // The codes below specify are virtual methods of Players.
        // A new character can be created by overriden those method to customize player's behavior

        public virtual PhaseList harm(HarmPhase harmPhase, IGame game)
        {
            health -= harmPhase.harm;
            if (health < 0)
            {
                return new PhaseList(new AskForHelpPhase(this));
            }
            return new PhaseList();
        }

        public virtual PhaseList recover(RecoverPhase recoverPhase, IGame game)
        {
            health += recoverPhase.recover;
            return new PhaseList();
        }

        public virtual PhaseList discardCard(Card card, IGame game)
        {
            handCards.Remove(card);
            game.cards.discardOne(card);
            return new PhaseList();
        }

        public virtual PhaseList drawCards(int num, IGame game)
        {
            try
            {
                handCards.AddRange(game.cards.drawCard(num));
            }catch (NoCardException e)
            {
                throw new NoCardException("cannot draw card", e);
            }
            return new PhaseList();
        }

        public virtual PhaseList ability(AbilityAction abilityAction, IGame game)
        {
            return null;
        }

        public virtual PhaseList abilitySun(AbilityActionSun abilityAction, IGame game)
        {
            return null;
        }

        public virtual int handcardCount()
        {
            return this.handCards.Count;
        }
    }
}
