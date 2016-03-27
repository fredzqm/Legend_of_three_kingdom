using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{
    /// <summary>
    /// Phase contains information that the game needs to process this phase
    /// </summary>
    public abstract class Phase
    {
        /// <summary>
        /// Whose phase
        /// </summary>
        public int playerID { get { return player.playerID; } }
        public Player player { get; }

        /// <summary>
        /// The type of the phase
        /// </summary>
        public Phase(Player player)
        {
            this.player = player;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>true if this phase require user response,
        /// fase if this phase should be invisible from outside</returns>
        public abstract bool needResponse();

        public abstract PhaseList handleResponse(UserAction userAction, Game game);

    }

    public abstract class FundamentalPhase : Phase
    {
        public FundamentalPhase(Player player) : base(player) { }

        public sealed override bool needResponse()
        {
            return true;
        }
    }

    public class PlayerTurn : Phase
    {
        public PlayerTurn(Player player) : base(player) { }

        public override PhaseList handleResponse(UserAction userAction, Game game)
        {
            return this.player.playerTurn(this, game);
        }

        public override string ToString()
        {
            return "Plyaer " + playerID + " at PlayerTurn";
        }
        public override sealed bool needResponse()
        {
            return false;
        }
    }

    public class JudgePhase : FundamentalPhase
    {
        public JudgePhase(Player player) : base(player) { }

        public override PhaseList handleResponse(UserAction userAction, Game game)
        {
            return this.player.judgePhase(this, userAction,  game);
        }

        public override string ToString()
        {
            return "Plyaer " + playerID + " at JudgePhase";
        }
    }

    public class DrawingPhase : FundamentalPhase
    {
        public DrawingPhase(Player player) : base(player) { }

        public override PhaseList handleResponse(UserAction userAction, Game game)
        {
            return this.player.drawingPhase(this, userAction, game);
        }

        public override string ToString()
        {
            return "Plyaer " + playerID + " at DrawingPhase";
        }
    }

    public class ActionPhase : FundamentalPhase
    {
        public int attackCount { get; internal set; }
        public bool drunk { get; internal set; }

        public ActionPhase(Player player) : base(player) { }

        public override PhaseList handleResponse(UserAction userAction, Game game)
        {
            if (userAction == null)
                return null;
            switch (userAction.type)
            {
                case UserActionType.YES_OR_NO:
                    if ((userAction as YesOrNoAction).no)
                        return new PhaseList(new DiscardPhase(player));
                    else
                        return null;
                case UserActionType.CARD:
                    CardAction cardAction = userAction as CardAction;
                    Card card = cardAction.card;
                    PhaseList ret = new PhaseList(this);
                    if (card is BasicCard)
                    {
                        Attack attack = card as Attack;
                        if (attack != null)
                        {
                            ret.add(new AttackPhase(player, attack, cardAction.targets, this));
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
                default:
                    return this.player.actionPhase(this, userAction, game);
            }
        }

        public override string ToString()
        {
            return "Plyaer " + playerID + " at ActionPhase";
        }
    }

    public class DiscardPhase : FundamentalPhase
    {
        public DiscardPhase(Player player) : base(player) { }

        public override PhaseList handleResponse(UserAction userAction, Game game)
        {
            if (userAction != null)
            {
                return null;
            }
            return this.player.discardPhase(this, userAction, game);
        }

        public override string ToString()
        {
            return "Plyaer " + playerID + " at DiscardPhase";
        }
    }
       
}
