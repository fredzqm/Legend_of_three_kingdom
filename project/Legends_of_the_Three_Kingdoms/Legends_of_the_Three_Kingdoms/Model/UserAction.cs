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
        public string toString()
        {
            if (yes)
                return "Yes";
            else
                return "Yes";
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
        public UseCardAction(IGame game, int CardID, int PlayerID) : this(game.cards[CardID]) { }
    }

    public class CardAction : UserAction
    {
        public Card card { get; }

        public CardAction(Card card)
        {
            this.card = card;
        }
        public CardAction(IGame game, int CardID) : this(game.cards[CardID]) { }
    }
    
    public class UserActionPlayer : UserAction
    {
        public Player player{ get; }
        public UserActionPlayer(Player player)
        {
            this.player = player;
        }
        public UserActionPlayer(IGame game, int playerID) : this(game.players[playerID]) { }
    }

    public class AbilityActionSun : UserAction
    {
        public Card card { get; }

        public AbilityActionSun(Card card)
        {
            this.card = card;
        }
        public AbilityActionSun(IGame game, int CardID) : this(game.cards[CardID]) { }
    }


    public class AbilityAction : UserAction
    {
        public Card card { get; }
        public Player[] targets { get; }

        public AbilityAction(Card card, params Player[] targets)
        {
            this.card = card;
            this.targets = targets;
        }
        public AbilityAction(IGame game, int CardID, int PlayerID) : this(game.cards[CardID], game.players[PlayerID]) { }
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