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
        public UseCardAction(int CardID, int PlayerID, IGame game) : this(game.cards[CardID]) { }
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

    public class AbilityAction : UserAction
    {
        public ICollection<Player> players;
        public ICollection<Card> cards;

        public AbilityAction(ICollection<Player> players, ICollection<Card> cards)
        {
            this.players = players;
            this.cards = cards;
        }
        public AbilityAction(IGame game, ICollection<int> playerIDs, ICollection<int> cardIDs)
        {
            this.players = new List<Player>();
            foreach (int i in playerIDs)
                players.Add(game.players[i]);
            this.cards = new List<Card>();
            foreach (int i in cardIDs)
                cards.Add(game.cards[i]);

        }
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