using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{
    /// <summary>
    /// subclass of hiddenphase 
    /// </summary>
    public class PlayerTurn : HiddenPhase
    {/// <summary>
    /// create playerturn phase
    /// </summary>
    /// <param name="player"></param>
        public PlayerTurn(Player player) : base(player) { }

        public override PhaseList advance(IGame game)
        {
            return new PhaseList(new JudgePhase(player), new PlayerTurn(game.nextPlayer(player, 1)));
        }

        public override string ToString()
        {
            return "Plyaer " + playerID + " at PlayerTurn";
        }
    }

    /// <summary>
    /// subclass of pause phase
    /// </summary>
    public class JudgePhase : PausePhase
    {/// <summary>
    /// create judge phase
    /// </summary>
    /// <param name="player"></param>
        public JudgePhase(Player player) : base(player, 2) { }

        public override PhaseList timeOutAdvance(IGame game)
        {
            return new PhaseList(new DrawingPhase(player), new ActionPhase(player));
        }

        public override string ToString()
        {
            return "Plyaer " + playerID + " at JudgePhase";
        }
    }

    /// <summary>
    /// subclass of pause phase
    /// </summary>
    public class DrawingPhase : PausePhase
    {/// <summary>
    /// create draw phase
    /// </summary>
    /// <param name="player"></param>
        public DrawingPhase(Player player) : base(player, 2) { }

        public override PhaseList timeOutAdvance(IGame game)
        {
            player.drawCards(2, game);
            return new PhaseList();
        }

        public override string ToString()
        {
            return "Plyaer " + playerID + " at DrawingPhase";
        }
    }

    /// <summary>
    /// subclass of useractionphase
    /// </summary>
    public class ActionPhase : UserActionPhase
    {
        public int attackCount { get;  set; }
        public bool drunk { get;  set; }
        /// <summary>
        /// create action phase
        /// </summary>
        /// <param name="player"></param>
        public ActionPhase(Player player) : base(player, 5) { }

        public override PhaseList responseUseCardAction(Card card, Player[] targets, IGame game)
        {
            PhaseList ret = new PhaseList(this);
            if (card is BasicCard)
            {
                Attack attack = card as Attack;
                if (attack != null)
                {
                    ret.push(new AttackPhase(player, attack, targets, this));
                    return ret;
                }
                Wine wine = card as Wine;
                if (wine != null)
                {
                    if (drunk)
                        return null;
                    player.discardCard(wine, game);
                    drunk = true;
                    return ret;
                }
                Peach peach = card as Peach;
                if (peach != null)
                {
                    if (player.health == player.healthLimit)
                        return null;
                    player.discardCard(peach, game);
                    ret.push(new RecoverPhase(player, 1));
                    return ret;
                }
            }
            else if (card is ToolCard)
            {
                throw new NotImplementedException();
            }
            else if (card is Equipment)
            {
                throw new NotImplementedException();
                //return new PhaseList(new UseEquipmentPhase(player, card as Equipment), this);
            }
            throw new Exception();
        }

        public override PhaseList timeOutAdvance(IGame game)
        {
            return new PhaseList(new DiscardPhase(player));
        }

        public override PhaseList responseAbilityAction(AbilityAction abilityAction, IGame game)
        {
            player.ability(abilityAction, game);
            return new PhaseList(this);
        }

        public override PhaseList responseAbilityActionSun(AbilityActionSun abilityAction, IGame game)
        {
            player.abilitySun(abilityAction, game);
            return new PhaseList(this);
        }

        public override string ToString()
        {
            return "Plyaer " + playerID + " at ActionPhase";
        }

    }
    /// <summary>
    /// subclass of useractionphase
    /// </summary>
    public class DiscardPhase : UserActionPhase
    {/// <summary>
    /// create discard phase
    /// </summary>
    /// <param name="player"></param>
        public DiscardPhase(Player player) : base(player, 5) { }

        public override PhaseList autoAdvance(IGame game)
        {
            if (player.handcardCount() <= player.health)
            {
                return new PhaseList();
            }
            return new PhaseList(this);
        }

        public override PhaseList timeOutAdvance(IGame game)
        {
            while (player.handcardCount() > player.health)
            {
                Card discard = player.handCards[0];
                player.handCards.Remove(discard);
                game.cards.discardOne(discard);
            }
            return new PhaseList();
        }

        public override PhaseList responseCardAction(Card card, IGame game)
        {
            player.discardCard(card, game);
            return autoAdvance(game);
        }

        public override string ToString()
        { 
            return "Plyaer " + playerID + " at DiscardPhase";
        }
    }
}
