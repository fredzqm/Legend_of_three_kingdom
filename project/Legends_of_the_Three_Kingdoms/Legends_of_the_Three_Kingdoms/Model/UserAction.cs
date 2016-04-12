using System.Collections.Generic;

namespace LOTK.Model
{
    /// <summary>
    /// The class used to specify the user action.
    /// Like click buttons
    /// </summary>
    public class UserAction
    {

    }
    /// <summary>
    /// subclass of useraction
    /// </summary>
    public class YesOrNoAction : UserAction
    {
        public bool yes { get; }
        /// <summary>
        /// set yes to input
        /// </summary>
        /// <param name="yesOrNo"></param>
        public YesOrNoAction(bool yesOrNo)
        {
            this.yes = yesOrNo;
        }
        /// <summary>
        /// toString yes
        /// </summary>
        /// <returns></returns>
        public string toString()
        {
            if (yes)
                return "Yes";
            else
                return "Yes";
        }
    }
    /// <summary>
    /// subclass of useraction
    /// </summary>
    public class UseCardAction : UserAction
    {
        public Card card { get; }
        public Player[] targets { get; }
        /// <summary>
        /// create usecard action
        /// </summary>
        /// <param name="card"></param>
        /// <param name="targets"></param>
        public UseCardAction(Card card, params Player[] targets)
        {
            this.card = card;
            this.targets = targets;
        }
        /// <summary>
        /// create usecard action
        /// </summary>
        /// <param name="CardID"></param>
        /// <param name="PlayerID"></param>
        /// <param name="game"></param>
        public UseCardAction(int CardID, int PlayerID, IGame game) : this(game.cards[CardID]) { }
    }
    /// <summary>
    /// subclass of useraction
    /// </summary>
    public class CardAction : UserAction
    {
        public Card card { get; }
        /// <summary>
        /// create cardaction
        /// </summary>
        /// <param name="card"></param>
        public CardAction(Card card)
        {
            this.card = card;
        }/// <summary>
        /// create card action
        /// </summary>
        /// <param name="CardID"></param>
        /// <param name="game"></param>
        public CardAction(int CardID, IGame game) : this(game.cards[CardID]) { }
    }
    /// <summary>
    /// subclass of useraction
    /// </summary>
    public class UserActionPlayer : UserAction
    {
        public Player player{ get; }
        /// <summary>
        /// create useractionplayer action
        /// </summary>
        /// <param name="player"></param>
        public UserActionPlayer(Player player)
        {
            this.player = player;
        }
        /// <summary>
        /// create user action player action
        /// </summary>
        /// <param name="playerID"></param>
        /// <param name="game"></param>
        public UserActionPlayer(int playerID, IGame game) : this(game.players[playerID]) { }
    }
    /// <summary>
    /// subclass of useraction,special for Sun Quan
    /// </summary>
    public class AbilityActionSun : UserAction
    {
        public Card card { get; }
        /// <summary>
        /// create action
        /// </summary>
        /// <param name="card"></param>
        public AbilityActionSun(Card card)
        {
            this.card = card;
        }
        /// <summary>
        /// create cation
        /// </summary>
        /// <param name="CardID"></param>
        /// <param name="game"></param>
        public AbilityActionSun(int CardID, IGame game) : this(game.cards[CardID]) { }
    }

    /// <summary>
    /// subclass of useraction,special for Lei Bei
    /// </summary>
    public class AbilityAction : UserAction
    {
        public Card card { get; }
        public Player[] targets { get; }
        /// <summary>
        /// create action
        /// </summary>
        /// <param name="card"></param>
        /// <param name="targets"></param>
        public AbilityAction(Card card, params Player[] targets)
        {
            this.card = card;
            this.targets = targets;
        }
        /// <summary>
        /// create action
        /// </summary>
        /// <param name="CardID"></param>
        /// <param name="PlayerID"></param>
        /// <param name="game"></param>
        public AbilityAction(int CardID, int PlayerID, IGame game) : this(game.cards[CardID], game.players[PlayerID]) { }
    }

    //public enum UserActionType
    //{
    //    YES_OR_NO, // 1 or 0
    //    CARD, // CardID
    //    CARDS,
    //    PLAYER, // PlayerID
    //    USE_CARD
    //}
}