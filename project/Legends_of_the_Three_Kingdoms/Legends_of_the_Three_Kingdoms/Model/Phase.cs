using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{
    /// <summary>
    /// Phase is an abstract class containing information of the phases of the game.
    /// Each phase contains information that the game needs to process.
    /// There are two kinds of Phase. 
    /// Phase contains all the information that the game needs.
    /// The game calls this function to ask the phase to process
    /// <seealso cref="Phase.advance(UserAction, IGame)"/>
    /// 
    /// </summary>
    public abstract class Phase
    {
        /// <summary>
        /// The ID of the player
        /// </summary>
        public int playerID { get { return player.playerID; } }
        /// <summary>
        /// The player that this Phase belongs to
        /// </summary>
        public Player player { get; }

        /// <summary>
        /// Construct a Phase of certain player
        /// </summary>
        /// <param name="player"></param>
        public Phase(Player player)
        {
            this.player = player;
        }

        /// <summary>
        /// This tells the game whether or not to process this Phase immediately, or wait for user's response.
        /// </summary>
        /// <returns>true if this phase require user response,
        /// fase if this phase should be invisible from outside</returns>
        public abstract bool needResponse();

        /// <summary>
        /// process this Phase given the current state of the game and possible userActions and return following phases.
        /// </summary>
        /// <param name="userAction">userAction feeded, null if there is no useraction</param>
        /// <param name="game">the game status</param>
        /// <returns>The following phases this phase leads</returns>
        public abstract PhaseList advance(UserAction userAction, IGame game);

        /// <summary>
        /// Get the time left for the user to make action
        /// </summary>
        /// <returns>time left</returns>
        public abstract int getTimeLeft();
    }

    /// <summary>
    /// This is abstract phase that is invisible to the user.
    /// It usually contains information that needs to be processed immediately.
    /// Many of them are very important in carrying on the game logic.
    /// </summary>
    public abstract class HiddenPhase : Phase
    {   /// <summary>
        /// create hidden phase
        /// </summary>
        /// <param name="player"></param>
        public HiddenPhase(Player player) : base(player) { }

        public sealed override PhaseList advance(UserAction userAction, IGame game)
        {
            return advance(game);
        }

        /// <summary>
        /// Since a hiddenPhase never takes any userAction, a simple interface is used to handle this phase.
        /// </summary>
        /// <param name="game">The game status</param>
        /// <returns>The following phases produced</returns>
        public abstract PhaseList advance(IGame game);

        /// <summary>
        /// A hidden phase never needs any response
        /// </summary>
        /// <returns>false</returns>
        public sealed override bool needResponse()
        {
            return false;
        }

        public override int getTimeLeft()
        {
            return 0;
        }
    }

    /// <summary>
    /// This Phase is visible to the user.
    /// It usually asks the user to provide some input, or just halt to display the game status.
    /// </summary>
    public abstract class VisiblePhase : Phase
    {
        /// <summary>
        /// If this game can be processed without userinput, this is the time that the game model should wait before processing it.
        /// </summary>
        public int timeOutTime { get; }
        public int timer { get; private set; }

        /// <summary>
        /// create visible phase
        /// </summary>
        /// <param name="player">whose phase</param>
        /// <param name="timeOutTime">The time for the game to wait for response</param>
        public VisiblePhase(Player player, int timeOutTime) : base(player)
        {
            if (timeOutTime < 0)
                throw new Exception(Legends_of_the_Three_Kingdoms.Properties.Resources.waitTime_should_not_be_negativ);
            this.timeOutTime = timeOutTime;
            this.timer = 0;
        }

        public sealed override PhaseList advance(UserAction userAction, IGame game)
        {
            if (userAction != null)
                return handleUserAction(userAction, game);
            timer++;
            if (timer >= timeOutTime)
            {
                PhaseList ls = timeOutAdvance(game);
                if (ls == null)
                    throw new InvalidOperationException(Legends_of_the_Three_Kingdoms.Properties.Resources.timeOutAdvance_cannot_return_n);
                return ls;
            }
            return autoAdvance(game);
        }

        /// <summary>
        /// Hanlde user action happened in this phase
        /// </summary>
        /// <param name="userAction"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        public abstract PhaseList handleUserAction(UserAction userAction, IGame game);

        /// <summary>
        /// This method is called when the time runs out for the player, the phase needed to handled.
        /// This method should return null
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public abstract PhaseList timeOutAdvance(IGame game);

        /// <summary>
        /// This method is called when the the clock tick but not yet timeout
        /// Sometime is might be helpful to check timer to see which clock tick it is
        /// By default, it should do nothing, but this method can be used to implemented auto response.
        /// 
        /// </summary>
        /// <param name="game">The game status</param>
        /// <returns>the following phases produced</returns>
        public virtual PhaseList autoAdvance(IGame game)
        {
            return null;
        }

        /// <summary>
        /// A visible phase already needs user response
        /// </summary>
        /// <returns>true</returns>
        public sealed override bool needResponse()
        {
            return true;
        }

        public override int getTimeLeft()
        {
            return timeOutTime - timer;
        }
    }


    /// <summary>
    /// This phase is visible to the user.
    /// It pauses the model to allow the view to display the effect.
    /// However, it usually does not need user input.
    /// </summary>
    public abstract class PausePhase : VisiblePhase
    {
        /// <summary>
        /// create pause phase
        /// </summary>
        /// <param name="player"></param>
        /// <param name="timeOutTime"></param>
        public PausePhase(Player player, int timeOutTime) : base(player, timeOutTime) { }

        public override sealed PhaseList handleUserAction(UserAction userAction, IGame game)
        {
            return timeOutAdvance(game);
        }

    }

    /// <summary>
    /// This phase is visible to the user.
    /// It is waiting for certain user action.
    /// </summary>
    public abstract class UserActionPhase : VisiblePhase
    {

        /// <summary>
        /// create a UserAction phase with different waitTime and TimeOut
        /// </summary>
        /// <param name="player"></param>
        /// <param name="timeOutTime"></param>
        /// <param name="autoProcessTime"></param>
        public UserActionPhase(Player player, int timeOutTime) : base(player, timeOutTime)
        {
        }

        /// <summary>
        /// It figures out the type of userAction and calls corresponding methods.
        /// Those methods will return null as default value, which means to do nothing.
        /// </summary>
        /// <param name="userAction">userAction to be handled, cannot be null</param>
        /// <param name="game"></param>
        /// <returns>the following phased produced</returns>
        public override sealed PhaseList handleUserAction(UserAction userAction, IGame game)
        {
            return userAction.processedBy(this, game);
        }

        /// <summary>
        /// this method is only for Liu Bei's ability
        /// </summary>
        /// <param name="abilityAction"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        public virtual PhaseList responseAbilityAction(AbilityAction abilityAction, IGame game)
        {
            game.log(Legends_of_the_Three_Kingdoms.Properties.Resources.Player + player.ToString() + Legends_of_the_Three_Kingdoms.Properties.Resources._Don_t_have_the_ability);
            return null;
        }

        /// <summary>
        /// this method is only for Sun Quan's ability
        /// </summary>
        /// <param name="abilityAction"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        public virtual PhaseList responseAbilityActionSun(AbilityActionSun abilityAction, IGame game)
        {
            game.log(Legends_of_the_Three_Kingdoms.Properties.Resources.Player + player.ToString() + Legends_of_the_Three_Kingdoms.Properties.Resources._Don_t_have_the_ability);
            return null;
        }

        /// <summary>
        /// When userAction is an YesOrNoAction, the player simply clicked OK or cancel.
        /// </summary>
        /// <param name="yes">yes or no</param>
        /// <param name="game">The game status</param>
        /// <returns>the following phased produced</returns>
        public virtual PhaseList responseYesOrNo(bool yes, IGame game)
        {
            if (yes)
                return autoAdvance(game);
            return timeOutAdvance(game);
        }

        /// <summary>
        /// When userAction is a UseCardAction, the player is trying to use a card.
        /// </summary>
        /// <param name="card"></param>
        /// <param name="targets">The target for the card</param>
        /// <param name="game">The game status</param>
        /// <returns>the following phased produced</returns>
        public virtual PhaseList responseUseCardAction(Card card, Player[] targets, IGame game)
        {
            game.log(Legends_of_the_Three_Kingdoms.Properties.Resources.Player + player.ToString() + Legends_of_the_Three_Kingdoms.Properties.Resources._try_to_use + card);
            return null;
        }

        /// <summary>
        /// When userAction is a UseCardAction, the player reponds some action with an card.
        /// </summary>
        /// <param name="card"></param>
        /// <param name="game">The game status</param>
        /// <returns>the following phased produced</returns>
        public virtual PhaseList responseCardAction(Card card, IGame game)
        {
                   game.log(Legends_of_the_Three_Kingdoms.Properties.Resources.Player + player.ToString() + Legends_of_the_Three_Kingdoms.Properties.Resources._response_card);
            return null;
        }

    }

    /// <summary>
    /// A very helpful kind of UserActionPhase, which only allows the user to respond with specific kind of card or cancel.
    /// After the player has responded, it reports the result to a NeedResponsePhase.
    /// </summary>
    public class ResponsePhase : UserActionPhase
    {
        private Func<Card, bool> allowed;
        private NeedResponsePhase responseTo;
        private string descript;
        /// <summary>
        /// Constructs a ResponsePhase of "player" and reports to "responseTo", a card that matches "allowd" is responded.
        /// </summary>
        /// <param name="player">who needs to provide a response</param>
        /// <param name="responseTo">The Phase to report the response result</param>
        /// <param name="allowed">A predicate that determines what kind of card is allowed</param>
        public ResponsePhase(Player player, NeedResponsePhase responseTo, Func<Card, bool> allowed, int timeOutTime) : base(player, timeOutTime)
        {
            this.allowed = allowed;
            this.responseTo = responseTo;
            this.descript = Legends_of_the_Three_Kingdoms.Properties.Resources.Response_Phase_of + playerID;
        }

        public ResponsePhase(Player player, NeedResponsePhase responseTo, Func<Card, bool> allowed, int timeOutTime, string descript): this(player, responseTo, allowed, timeOutTime)
        {
            this.descript = descript;
        }

        /// <summary>
        /// The player can respond with a card. This method would use "allowed" predicate to tell whether this card is a legal response. It reports if legal, otherwise reject the response.
        /// The player should be allowed to try multiple times until he or she hits a valid response.
        /// </summary>
        /// <param name="card"></param>
        /// <param name="game"></param>
        /// <returns>the following phased produced</returns>
        public override PhaseList responseCardAction(Card card, IGame game)
        {
            if (allowed(card))
            {
                responseTo.responseWith(card);
                return player.discardCard(card, game);
            }
            game.log(Legends_of_the_Three_Kingdoms.Properties.Resources.response_with_card + card.ToString());
            return null;
        }

        public override PhaseList timeOutAdvance(IGame game)
        {
            game.log(Legends_of_the_Three_Kingdoms.Properties.Resources.timeout);
            responseTo.responseWith(null);
            return new PhaseList();
        }

        public override PhaseList autoAdvance(IGame game)
        {
            if (player.handCards.Count == 0)
            {
                game.log(Legends_of_the_Three_Kingdoms.Properties.Resources.no_card_in_hands);
                return timeOutAdvance(game);
            }
            return null;
        }

        public override string ToString()
        {
            return descript;
        }

    }

    /// <summary>
    /// An abstract class for any Phase that will needs some reponses.
    /// It provides means for ResponsePhase to report the responded card.
    /// </summary>
    public abstract class NeedResponsePhase : HiddenPhase
    {
        private Card respondCard;
        private int count;
        private int handledCount;
        /// <summary>
        /// create need response phase 
        /// </summary>
        /// <param name="player"></param>
        public NeedResponsePhase(Player player) : base(player)
        {
            count = 0;
            handledCount = 0;
        }

        /// <summary>
        /// If no responses are issued, this method will call <seealso cref="askForResponse(int, IGame)"/>.
        /// If there are any unhandled responsed, this method will call <seealso cref="handleResponse(int, Card, IGame)"/>.
        /// A Need responsePhase should be processed at least twice. Once to create a respondPhase, once to handle the response. It can also create multiple responses
        /// </summary>
        /// <param name="game">The game status</param>
        /// <returns></returns> 
        public sealed override PhaseList advance(IGame game)
        {
            if (count == handledCount)
            {
                return askForResponse(count, game);
            }
            handledCount++;
            return handleResponse(handledCount, respondCard, game);
        }

        /// <summary>
        /// If no responses are issued, this method will call to create a respondPhase
        /// </summary>
        /// <param name="count">the number of responsed already made</param>
        /// <param name="game">The game status</param>
        /// <returns>the following phased produced</returns>
        public abstract PhaseList askForResponse(int count, IGame game);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="count">Which response it is hanlding</param>
        /// <param name="respondCard">the respond result</param>
        /// <param name="game">the game status</param>
        /// <returns>the following phased produced</returns>
        public abstract PhaseList handleResponse(int count, Card respondCard, IGame game);

        /// <summary>
        /// This method will be called by a ResponsePhase to report the result.
        /// It stores the information and wait the next time .advance() is called to process it.
        /// </summary>
        /// <param name="card">The respond result</param>
        public void responseWith(Card card)
        {
            respondCard = card;
            count++;
        }
    }

}
