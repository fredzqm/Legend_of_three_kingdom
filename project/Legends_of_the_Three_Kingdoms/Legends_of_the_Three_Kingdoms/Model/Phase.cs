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

        public abstract PhaseList handleResponse(UserAction userAction, IGame game);

    }

    public abstract class VisiblePhase : Phase
    {
        public VisiblePhase(Player player) : base(player) { }

        public sealed override bool needResponse()
        {
            return true;
        }
    }

    public abstract class HiddenPhase : Phase
    {
        public HiddenPhase(Player player) : base(player) { }

        public sealed override PhaseList handleResponse(UserAction userAction, IGame game)
        {
            if (userAction != null)
                throw new Exception();
            return process(game);
        }

        public abstract PhaseList process(IGame game);

        public sealed override bool needResponse()
        {
            return false;
        }
    }

    public class PlayerTurn : HiddenPhase
    {
        public PlayerTurn(Player player) : base(player) { }

        public override PhaseList process(IGame game)
        {
            return new PhaseList(new JudgePhase(player), new PlayerTurn(game.players[(playerID + 1) % game.Num_Player]));
        }

        public override string ToString()
        {
            return "Plyaer " + playerID + " at PlayerTurn";
        }
    }

    public class JudgePhase : VisiblePhase
    {
        public JudgePhase(Player player) : base(player) { }

        public override PhaseList handleResponse(UserAction userAction, IGame game)
        {
            return new PhaseList(new DrawingPhase(player), new ActionPhase(player));
        }

        public override string ToString()
        {
            return "Plyaer " + playerID + " at JudgePhase";
        }
    }

    public class DrawingPhase : VisiblePhase
    {
        public DrawingPhase(Player player) : base(player) { }

        public override PhaseList handleResponse(UserAction userAction, IGame game)
        {
            return new PhaseList();
        }

        public override string ToString()
        {
            return "Plyaer " + playerID + " at DrawingPhase";
        }
    }

    public class ActionPhase : VisiblePhase
    {
        public int attackCount { get; internal set; }
        public bool drunk { get; internal set; }

        public ActionPhase(Player player) : base(player) { }

        public override PhaseList handleResponse(UserAction userAction, IGame game)
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
                    return null;
            }
        }

        public override string ToString()
        {
            return "Plyaer " + playerID + " at ActionPhase";
        }
    }

    public class DiscardPhase : VisiblePhase
    {
        public DiscardPhase(Player player) : base(player) { }

        public override PhaseList handleResponse(UserAction userAction, IGame game)
        {
            if (userAction == null)
                return null;
            return new PhaseList();
        }

        public override string ToString()
        {
            return "Plyaer " + playerID + " at DiscardPhase";
        }
    }
       
}
