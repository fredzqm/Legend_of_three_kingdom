﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{

    public class PlayerTurn : HiddenPhase
    {
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

    public class JudgePhase : PausePhase
    {
        public JudgePhase(Player player) : base(player, 1) { }

        public override PhaseList advance(IGame game)
        {
            return new PhaseList(new DrawingPhase(player), new ActionPhase(player));
        }

        public override string ToString()
        {
            return "Plyaer " + playerID + " at JudgePhase";
        }
    }

    public class DrawingPhase : PausePhase
    {
        public DrawingPhase(Player player) : base(player, 1) { }

        public override PhaseList advance(IGame game)
        {
            player.drawCards(2, game);
            return new PhaseList();
        }

        public override string ToString()
        {
            return "Plyaer " + playerID + " at DrawingPhase";
        }
    }

    public class ActionPhase : UserActionPhase
    {
        public int attackCount { get; internal set; }
        public bool drunk { get; internal set; }

        public ActionPhase(Player player) : base(player, 20) { }

        public override PhaseList responseYesOrNo(bool yes, IGame game)
        {
            if (yes)
                return null;
            else
                return new PhaseList(new DiscardPhase(player));
        }

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

    public class DiscardPhase : UserActionPhase
    {
        public DiscardPhase(Player player) : base(player, 1) { }

        public override PhaseList autoAdvance(IGame game)
        {
            if (player.handcardCount() <= player.health)
            {
                return new PhaseList();
            }
            return new PhaseList(this);
        }
        public override PhaseList responseYesOrNo(bool yes, IGame game)
        {
            if (!yes)
                return new PhaseList();
            return autoAdvance(game);
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
