using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{
    /// <summary>
    /// It contains information that the game needs to process this phase
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

        /// <summary>{ get { return this; } }
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
        public override int GetHashCode()
        {
            return playerID;
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
                return responseCardAction(cardAction.card, game);
            UseCardAction useCardAction = userAction as UseCardAction;
            if (useCardAction != null)
                return responseUseCardAction(useCardAction.card, useCardAction.targets, game);
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
        public virtual PhaseList responseUseCardAction(Card card, Player[] targets, IGame game)
        {
            return null;
        }

        public virtual PhaseList responseCardAction(Card card, IGame game)
        {
            return null;
        }

    }

    public class ResponsePhase : UserActionPhase
    {
        private Func<Card, bool> allowed;
        private NeedResponsePhase responseTo;

        public ResponsePhase(Player player, NeedResponsePhase responseTo, Func<Card, bool> allowed) : base(player, 10)
        {
            this.allowed = allowed;
            this.responseTo = responseTo;
        }

        public override PhaseList responseYesOrNo(bool yes, IGame game)
        {
            if (!yes)
            {
                responseTo.responseWith(null);
                return new PhaseList();
            }
            return null;
        }

        public override PhaseList responseCardAction(Card card, IGame game)
        {
            if (allowed(card))
            {
                responseTo.responseWith(card);
                return player.discardCard(card, game);
            }
            return null;
        }
    }


    public abstract class NeedResponsePhase : HiddenPhase
    {
        private Card respondCard;
        private int count;
        private int handledCount;

        public NeedResponsePhase(Player player) : base(player) {
            count = 0;
            handledCount = 0;
        }

        public sealed override PhaseList advance(IGame game)
        {
            if (count == handledCount)
            {
                return askForResponse(count, game);
            }
            handledCount++;
            return handleResponse(count, respondCard, game);
        }
        public abstract PhaseList askForResponse(int count, IGame game);

        public abstract PhaseList handleResponse(int count, Card respondCard, IGame game);

        public void responseWith(Card card)
        {
            respondCard = card;
            count++;
        }
    }

}
