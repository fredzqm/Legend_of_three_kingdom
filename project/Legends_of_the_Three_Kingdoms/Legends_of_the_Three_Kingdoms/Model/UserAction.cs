using System;
using System.Collections.Generic;

namespace LOTK.Model
{
    /// <summary>
    /// The class used to specify the user action.
    /// Like click buttons
    /// </summary>
    public abstract class UserAction
    {
        /// <summary>
        /// This method parsed this userAction and ask userActionPhase to process it.
        /// </summary>
        /// <param name="userActionPhase"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        public abstract PhaseList processedBy(UserActionPhase userActionPhase, IGame game);
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
                return "No";
        }

        public override PhaseList processedBy(UserActionPhase userActionPhase, IGame game)
        {
            return userActionPhase.responseYesOrNo(yes, game);
        }

        public override bool Equals(object obj)
        {
            YesOrNoAction x = obj as YesOrNoAction;
            return x != null && this.yes == x.yes;
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
        public UseCardAction(IGame game, int CardID, int PlayerID) : this(game.cards[CardID], game.players[PlayerID]) { }

        public override PhaseList processedBy(UserActionPhase userActionPhase, IGame game)
        {
            return userActionPhase.responseUseCardAction(card, targets, game);
        }

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
        }
        /// <summary>
        /// create card action
        /// </summary>
        /// <param name="CardID"></param>
        /// <param name="game"></param>
        public CardAction(IGame game, int CardID) : this(game.cards[CardID]) { }

        public override PhaseList processedBy(UserActionPhase userActionPhase, IGame game)
        {
            return userActionPhase.responseCardAction(card, game);
        }

    }

    ///// <summary>
    ///// subclass of useraction
    ///// </summary>
    //public class UserActionPlayer : UserAction
    //{
    //    public Player player { get; }
    //    /// <summary>
    //    /// create useractionplayer action
    //    /// </summary>
    //    /// <param name="player"></param>
    //    public UserActionPlayer(Player player)
    //    {
    //        this.player = player;
    //    }


    //    /// <summary>
    //    /// create user action player action
    //    /// </summary>
    //    /// <param name="playerID"></param>
    //    /// <param name="game"></param>
    //    public UserActionPlayer(IGame game, int playerID) : this(game.players[playerID]) { }

    //}

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
        public AbilityActionSun(IGame game, int CardID) : this(game.cards[CardID]) { }

        public override PhaseList processedBy(UserActionPhase userActionPhase, IGame game)
        {
            return userActionPhase.responseAbilityActionSun(this, game);
        }
        public override bool Equals(object obj)
        {
            UseCardAction x = obj as UseCardAction;
            return x != null;
        }
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
        public AbilityAction(IGame game, int CardID, int PlayerID) : this(game.cards[CardID], game.players[PlayerID]) { }

        public override PhaseList processedBy(UserActionPhase userActionPhase, IGame game)
        {
            return userActionPhase.responseAbilityAction(this, game);
        }

    }

}