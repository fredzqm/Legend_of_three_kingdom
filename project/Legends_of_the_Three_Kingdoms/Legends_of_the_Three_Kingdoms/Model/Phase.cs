using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{
    /// <summary>
    /// Phase contains information that the game needs to process this phase
    /// There are two kinds of Phase. 
    /// Phase contains all the information that the game needs.
    /// The game calls this function to ask the phase to process
    /// <seealso cref="Phase.advance(UserAction, IGame)"/>
    /// VisiblePhase is usually waiting for userAction, but HiddenPhase should not
    /// get any userAction.
    /// 
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

        public abstract PhaseList advance(UserAction userAction, IGame game);

        public override bool Equals(object obj)
        {
            Phase b = obj as Phase;
            return (b != null && player == b.player) ;
        }

    }

    /// <summary>
    /// This phase is invisible to the user.
    /// However, many of them are very important in carrying on the game logic.
    /// </summary>
    public abstract class HiddenPhase : Phase
    {
        public HiddenPhase(Player player) : base(player) { }

        public sealed override PhaseList advance(UserAction userAction, IGame game)
        {
            return advance(game);
        }

        public abstract PhaseList advance(IGame game);

        public sealed override bool needResponse()
        {
            return false;
        }
    }

    /// <summary>
    /// This Phase is visible to the user.
    /// It also has a property specifying how many clock cycle the game should wait.
    /// </summary>
    public abstract class VisiblePhase : Phase
    {
        public int waitTime {get;}

        public VisiblePhase(Player player, int waitTime) : base(player)
        {
            this.waitTime = waitTime;
        }
        public sealed override bool needResponse()
        {
            return true;
        }
    }


    /// <summary>
    /// This phase is visible to the user.
    /// It pauses the model to allow the view to display the effect.
    /// However, it usually does not need user input.
    /// </summary>
    public abstract class PausePhase : VisiblePhase
    {
        public PausePhase(Player player, int waitTime) : base(player, waitTime) { }

        public sealed override PhaseList advance(UserAction userAction, IGame game)
        {
            return advance(game);
        }

        public abstract PhaseList advance(IGame game);

    }

    /// <summary>
    /// This phase is visible to the user.
    /// It is waiting for certain user action
    /// </summary>
    public abstract class UserActionPhase : VisiblePhase
    {
        public int timeOutTime { get; }
        public UserActionPhase(Player player, int waitTime, int timeOut) : base(player, waitTime) {
            this.timeOutTime = timeOut;
        }
        public UserActionPhase(Player player, int waitTime) : this(player, waitTime, waitTime) { }

        public sealed override PhaseList advance(UserAction userAction, IGame game)
        {
            if (userAction == null)
                return autoAdvance(game);
            YesOrNoAction yesOrNoAction = userAction as YesOrNoAction;
            if (yesOrNoAction != null)
                return responseYesOrNo(yesOrNoAction.yes, game);
            CardAction cardAction = userAction as CardAction;
            if (cardAction != null)
                return responseCardAction(cardAction.card, cardAction.targets);
            throw new NotDefinedException("This kind of useraction is not yet defined");
        }

        public virtual PhaseList timeOut(IGame game)
        {
            throw new NotImplementedException();
        }

        public virtual PhaseList autoAdvance(IGame game)
        {
            return null;
        }

        public virtual PhaseList responseYesOrNo(bool yes, IGame game)
        {
            return null;
        }

        public virtual PhaseList responseCardAction(Card card, Player[] targets)
        {
            return null;
        }

    }


    public class PlayerTurn : HiddenPhase
    {
        public PlayerTurn(Player player) : base(player) { }

        public override PhaseList advance(IGame game)
        {
            return new PhaseList(new JudgePhase(player), new PlayerTurn(game.players[(playerID + 1) % game.Num_Player]));
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

        public override PhaseList responseCardAction(Card card, Player[] targets)
        {
            PhaseList ret = new PhaseList(this);
            if (card is BasicCard)
            {
                Attack attack = card as Attack;
                if (attack != null)
                {
                    ret.add(new AttackPhase(player, attack, targets, this));
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
            return base.autoAdvance(game);
        }
        public override PhaseList responseYesOrNo(bool yes, IGame game)
        {
            return new PhaseList();
        }

        public override string ToString()
        {
            return "Plyaer " + playerID + " at DiscardPhase";
        }
    }
       
}
