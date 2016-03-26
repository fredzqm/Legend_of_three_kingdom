using System.Collections.Generic;

namespace LOTK.Model
{
    /// <summary>
    /// The class used to specify the user action.
    /// Like click buttons
    /// </summary>
    public class UserAction
    {
        public const int YES = 1;
        public const int NO = 0;


        public UserActionType type { get; set; }

        public UserAction(UserActionType t)
        {
            type = t;
        }
    }

    public class YesOrNoAction : UserAction
    {
        public bool yes { get; }
        public bool no { get { return !yes; } }
        public YesOrNoAction(bool yesOrNo) : base(UserActionType.YES_OR_NO)
        {
            this.yes = yesOrNo;
        }
    }

    public class UseCardAction : UserAction
    {
        public Card card { get; }

        public UseCardAction(Card card) : base(UserActionType.CARD)
        {
            this.card = card;
        }
        public UseCardAction(Card card, params Player[] targets) : this(card)
        {

        }
        public UseCardAction(int CardID, Game game) : this(game.cards[CardID]) { }
    }

    public class UserActionCards : UserAction
    {
        public ICollection<Card> cards { get; }
        public int cout { get { return cards.Count; } }
        public UserActionCards(ICollection<Card> cards) : base(UserActionType.CARDS)
        {
            this.cards = cards;
        }
        public UserActionCards(ICollection<int> cardIDs, Game game) : base(UserActionType.CARDS)
        {
            cards = new List<Card>();
            foreach(int i in cardIDs)
                cards.Add(game.cards[i]);
        }
    }

    public class UserActionPlayer : UserAction
    {
        public Player player{ get; }
        public UserActionPlayer(Player player) : base(UserActionType.PLAYER)
        {
            this.player = player;
        }
        public UserActionPlayer(int playerID, Game game) : this(game.players[playerID]) { }
    }
    public enum UserActionType
    {
        YES_OR_NO, // 1 or 0
        CARD, // CardID
        CARDS,
        PLAYER // PlayerID
    }
}