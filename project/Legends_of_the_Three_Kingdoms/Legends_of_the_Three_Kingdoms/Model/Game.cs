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
        /// log a message
        /// </summary>
        /// <param name="message"></param>
        void log(string message);

        /// <summary>
        /// the player under whose turn the game is in
        /// </summary>
        Player curRoundPlayer { get; }

        /// <summary>
        /// the logs of the game, should be printed out or displayed
        /// </summary>
        string logs { get; }

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
        
        /// <summary>
        /// process the userAction
        /// </summary>
        /// <param name="playerID"></param>
        /// <param name="userAction"></param>
        void processUserInput(int playerID, UserAction userAction);
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

        public Player curPhasePlayer { get { return curPhase.player; } }

        public Player curRoundPlayer { get; private set; }

        public string logs {get; private set;}

        /// <summary>
        /// construct a game given player and cardlist
        /// It uses dependency injection, so players and carlist can be easily tested.
        public Game(Player[] players, ICardSet cardList)
        {
            if (cardList == null)
                throw new NotDefinedException("CardList is not defined");
            cards = cardList;

            if (players == null)
                throw new NotDefinedException("CardList is not defined");
            this.players = players;

            stages = new PhaseList();
            logs = "";
        }
        
        /// <summary>
        /// start of game 
        /// </summary>
        public void start()
        {
            for (int i = 0; i < Num_Player; i++)
            {
                players[i].drawCards(4, this);
            }
            stages.add(new PlayerTurn(players[0]));
            log("Start the game");
            nextStage(null);
        }

        public void log(String message)
        {
            logs = logs + message + "\n";
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
                    log("The round of " + curRoundPlayer +" start");
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
                    log(curPhase.ToString());
                    timerAutoAdvance = true;
                    return;
                }
            }
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


    }
}
