using System;
using System.Collections.Generic;
using LOTK.View;
using LOTK.Controller;

namespace LOTK.Model
{
    /// <summary>
    /// This is created for the purpose of dependency injection
    /// </summary>
    public interface IGame
    {
        int Num_Player { get; }
        Player[] players { get; }
        CardSet cards { get; }
        Phase curPhase { get; }
        Player curRoundPlayer { get; }
        bool tick();
        void nextStage(UserAction yesOrNoAction);
        Player nextPlayer(int curPlayer, int count);
        List<Card> drawCard(int v);
        void start();
    }

    /// <summary>
    /// The main model representing the game
    /// </summary>
    public class Game : IGame
    {
        public int Num_Player { get; }
        public Player[] players { get; }
        public Player nextPlayer(int curPlayer, int count)
        {
            return players[(curPlayer + count) % Num_Player];
        }

        public CardSet cards { get; }


        private PhaseList stages;
        public Phase curPhase { get { return stages.top(); } }
        public Player curPhasePlayer { get { return curPhase.player; } }

        public bool timerAutoAdvance;
        public bool timerVisit;

        public Player curRoundPlayer { get; private set; }

        public Game(Player[] players, ICollection<Card> cardList)
        {
            Num_Player = players.Length;
            if (cardList == null)
                throw new NotDefinedException("CardList is not defined");
            cards = new CardSet(cardList);

            if (players == null)
                throw new NotDefinedException("CardList is not defined");
            this.players = players;

            stages = new PhaseList();
            stages.add(new PlayerTurn(players[0]));
        }

        public void start()
        {
            for (int i = 0; i < Num_Player; i++)
            {
                players[i].drawCards(4, this);
            }
            nextStage(null);
        }

        public void processUserInput(int fromPlayerID, UserAction userAction)
        {
            if (fromPlayerID == curPhasePlayer)
                nextStage(userAction);
        }

        /// <summary>
        /// Advance the next stage and skip the following stages that do not need user response
        /// </summary>
        public void nextStage(UserAction userAction)
        {
            timerAutoAdvance = false;
            timerVisit = false;
            while (true)
            {
                if (curPhase is PlayerTurn)
                { // when turn switches
                    curRoundPlayer = curPhase.player;
                }
                PhaseList followingPhases = curPhase.advance(userAction, this);
                if (followingPhases == null)
                { // the next state need a user action for future decison
                    return;
                }
                stages.pop();
                stages.pushList(followingPhases);
                if (curPhase.needResponse())
                { // the next state need a user action for future decison
                  // but since it is supposed to be a responsive phase, pause a while before autoadvance
                    timerAutoAdvance = true;
                    return;
                }
            }
        }

        internal void useCardAction(int playerID, int cardID, params int[] targets)
        {
            processUserInput(playerID, new UseCardAction(cards[cardID], players[targets[0]]));
        }

        internal void cardAction(int playerID, int cardID)
        {
            processUserInput(playerID, new UseCardAction(cards[cardID]));
        }

        internal void yesOrNoAction(int playerID, bool v)
        {
            processUserInput(playerID, new YesOrNoAction(v));
        }


        /// <summary>
        /// Under certain circumstances, the player might have only one option.
        /// In those cases, we want the game to pause for a small interval
        /// and then automactially advance to the next stage.
        /// This can save the player from unnesessary clicks.
        /// 
        /// This method will be called from the controller at specific interval.
        /// This interval is customizable by controller not the game itself.
        /// </summary>
        /// <returns>True if the game is auto advanced and the GUI should update correspondedly</returns>
        public bool tick()
        {
            if (timerAutoAdvance)
            {
                if (timerVisit)
                {
                    nextStage(null);
                    return true;
                }
                timerVisit = true;
            }
            return false;
        }

        /// <summary>
        /// This method pops n cards from the card pile. If the card pile is empty, 
        /// it automatically shuffles the discard card pile, and draw the rest there
        /// 
        /// </summary>
        /// <param name="num">Number of cards</param>
        /// <returns>the card drown</returns>
        public List<Card> drawCard(int num)
        {
            List<Card> cards = new List<Card>();
            try{
                for (int i = 0; i < num; i++)
                    cards.Add(this.cards.pop());
            }catch(NoCardException e)
            {
                throw new NoCardException("The card stack is empty");
            }
            return cards;
        }
    }
}
