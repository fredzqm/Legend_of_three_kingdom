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
        /// <summary>
        /// the number of players in the game
        /// </summary>
        int Num_Player { get; }
        
        /// <summary>
        /// the player array of the game
        /// </summary>
        Player[] players { get; }

        /// <summary>
        /// a small helper method that help step through the player in game sequence
        /// </summary>
        /// <param name="curPlayer">the player to start</param>
        /// <param name="count">the number of steps to proceeds</param>
        /// <returns></returns>
        Player nextPlayer(int curPlayer, int count);

        /// <summary>
        /// the card set of the game
        /// </summary>
        ICardSet cards { get; }
        
        /// <summary>
        /// The current phase of the game
        /// </summary>
        Phase curPhase { get; }

        /// <summary>
        /// the player under whose turn the game is in
        /// </summary>
        Player curRoundPlayer { get; }

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
        bool tick();


        /// <summary>
        /// kick off the game
        /// </summary>
        void start();

        void nextStage(UserAction userAction);
        
    }

    /// <summary>
    /// The main model representing the game
    /// </summary>
    public class Game : IGame
    {
        public int Num_Player { get { return players.Length; }}

        public Player[] players { get; }

        public Player nextPlayer(int curPlayer, int count)
        {
            return players[(curPlayer + count) % Num_Player];
        }

        public ICardSet cards { get; }

        private PhaseList stages;

        public Phase curPhase {get {return stages.top();}}

        public Player curRoundPlayer { get; private set; }

        /// <summary>
        /// construct a game given player and cardlist
        /// It uses dependency injection, so players and carlist can be easily tested.
        /// </summary>
        /// <param name="players"></param>
        /// <param name="cardList"></param>
        public Game(Player[] players, ICardSet cardList)
        {
            if (cardList == null)
                throw new NotDefinedException("CardList is not defined");
            cards = cardList;

            if (players == null)
                throw new NotDefinedException("CardList is not defined");
            this.players = players;

            stages = new PhaseList();
        }

        public void start()
        {
            for (int i = 0; i < Num_Player; i++)
            {
                players[i].drawCards(4, this);
            }
            stages.add(new PlayerTurn(players[0]));
            nextStage(null);
        }

        /// <summary>
        /// process the user input
        /// </summary>
        /// <param name="fromPlayerID">the player who gives this input</param>
        /// <param name="userAction">the user action performed</param>
        public void processUserInput(int fromPlayerID, UserAction userAction)
        {
            if (fromPlayerID == curPhase.player)
                nextStage(userAction);
        }

        private bool timerAutoAdvance;
        private bool timerVisit;

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
                if (stages.isEmpty())
                {
                    throw new EmptyException("The stages stack is empty");
                }
                if (curPhase.needResponse())
                { // the next state need a user action for future decison
                  // but since it is supposed to be a responsive phase, pause a while before autoadvance
                    timerAutoAdvance = true;
                    return;
                }
            }
        }

        /// <summary>
        /// handle a use card action by the user
        /// </summary>
        /// <param name="playerID"></param>
        /// <param name="cardID"></param>
        /// <param name="targets"></param>
        internal void useCardAction(int playerID, int cardID, params int[] targets)
        {
            processUserInput(playerID, new UseCardAction(cards[cardID], players[targets[0]]));
        }

        /// <summary>
        /// handle a card action by the user
        /// </summary>
        /// <param name="playerID"></param>
        /// <param name="cardID"></param>
        internal void cardAction(int playerID, int cardID)
        {
            processUserInput(playerID, new CardAction(cards[cardID]));
        }

        /// <summary>
        /// handle a yes or no action by the player
        /// </summary>
        /// <param name="playerID"></param>
        /// <param name="v"></param>
        internal void yesOrNoAction(int playerID, bool v)
        {
            processUserInput(playerID, new YesOrNoAction(v));
        }


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

       
    }
}
