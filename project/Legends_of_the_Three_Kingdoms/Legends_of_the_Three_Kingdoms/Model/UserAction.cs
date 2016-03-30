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

    public class YesOrNoAction : UserAction
    {
        public bool yes { get; }
        public YesOrNoAction(bool yesOrNo)
        {
            this.yes = yesOrNo;
        }
    }

    public class UseCardAction : UserAction
    {
        public Card card { get; }
        public Player[] targets { get; }

        public UseCardAction(Card card, params Player[] targets)
        {
            this.card = card;
            this.targets = targets;
        }
        public UseCardAction(int CardID, IGame game) : this(game.cards[CardID]) { }
    }

    public class CardAction : UserAction
    {
        public Card card { get; }

        public CardAction(Card card)
        {
            this.card = card;
        }
        public CardAction(int CardID, IGame game) : this(game.cards[CardID]) { }
    }
    
    public class UserActionPlayer : UserAction
    {
        public Player player{ get; }
        public UserActionPlayer(Player player)
        {
            this.player = player;
        }
        public UserActionPlayer(int playerID, IGame game) : this(game.players[playerID]) { }
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