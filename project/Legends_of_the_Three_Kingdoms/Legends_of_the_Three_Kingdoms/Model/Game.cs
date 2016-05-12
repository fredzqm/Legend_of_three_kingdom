using System;
using System.Collections.Generic;
using LOTK.View;
using LOTK.Controller;

namespace LOTK.Model
{
    public delegate void PrintString(string str);

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
        //void logEvent(string message);
        event PrintString logEvent;

        /// <summary>
        /// broadcast the log information to all listeners
        /// </summary>
        /// <param name="v"></param>
        void log(string v);

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
        void start(int initCardNum);

        bool nextStage(UserAction userAction);

        /// <summary>
        /// process the userAction
        /// </summary>
        /// <param name="playerID"></param>
        /// <param name="userAction"></param>
        void processUserInput(int playerID, UserAction userAction);

        /// <summary>
        /// The status of the game.
        /// It indicates whether the game has end or not and who wins the game.
        /// </summary>
        GameStatus status { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>true if the game has ended</returns>
        bool hasEnd();

        /// <summary>
        /// for test only
        /// </summary>
        /// <param name="p"></param>
        void addstage(Phase p);
        
    }



    /// <summary>
    /// The main model representing the game
    /// </summary>
    public class Game : IGame
    {
        public int Num_Player { get { return players.Length; } }

        public Player[] players { get; }

        public Player nextPlayer(int curPlayer, int count)
        {
            while (count > 0)
            {
                curPlayer = (curPlayer + 1) % Num_Player;
                if (!players[curPlayer].isDead())
                    count--;
            }
            return players[curPlayer];
        }

        public ICardSet cards { get; }

        private PhaseList stages;

        public Phase curPhase { get { return stages.top(); } }

        public Player curPhasePlayer { get { return curPhase.player; } }

        public Player curRoundPlayer { get; private set; }

        public event PrintString logEvent;

        public GameStatus status { get; private set; }

        public void log(string v)
        {
            logEvent?.Invoke(v);
        }

        public void addstage(Phase p)
        {
            stages.add(p);
        }

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

            status = GameStatus.NotFinish;
            stages = new PhaseList();
        }

        /// <summary>
        /// start of game 
        /// </summary>
        public void start(int initCardNum)
        {
            if (initCardNum > 0)
            {
                for (int i = 0; i < Num_Player; i++)
                {
                    players[i].drawCards(initCardNum, this);
                }
            }
            stages.add(new PlayerTurn(players[0]));
            log("Start the game");
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

        /// <summary>
        /// Advance the next stage and skip the following stages that do not need user response
        /// </summary>
        public bool nextStage(UserAction userAction)
        {
            while (true)
            {
                PhaseList followingPhases;
                if (curPhase is PlayerTurn)
                { // when turn switches
                    curRoundPlayer = curPhase.player;
                    log("The round of " + curRoundPlayer.ToString() + " start");
                }
                followingPhases = curPhase.advance(userAction, this);
                if (followingPhases == null)
                { // the next state need a user action for future decison
                    if (curPhase is HiddenPhase)
                        throw new InvalidOperationException("Invisible Phase should not give null " + curPhase);
                    else
                        return false;
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
                    return true;
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
            return nextStage(null);
            //if (timerAutoAdvance)
            //{
            //    if (timerVisit)
            //    {
            //        nextStage(null);
            //        return true;
            //    }
            //    timerVisit = true;
            //}
            //return false;
        }

        public bool hasEnd()
        {
            if (status != GameStatus.NotFinish)
                return true;
            int king = 0, loyal = 0, rebel = 0, spy = 0;
            foreach (Player p in players)
            {
                if (!p.isDead())
                {

                    switch (p.playerType)
                    {
                        case PlayerType.King:
                            king++;
                            break;
                        case PlayerType.Loyal:
                            loyal++;
                            break;
                        case PlayerType.Rebel:
                            rebel++;
                            break;
                        case PlayerType.Spy:
                            spy++;
                            break;
                        default:
                            throw new NotImplementedException("There should not be this kind of player type " + p.playerType);
                    }
                }
            }
            if (king == 0)
            {
                if (rebel == 0 && loyal == 0 && spy == 1)
                    status = GameStatus.SpyWin;
                else
                    status = GameStatus.RebelWin;
                return true;
            }
            else if (rebel == 0 && spy == 0)
            {
                status = GameStatus.KingWin;
                return true;
            }
            return false;
        }
    }

    public enum GameStatus
    {
        NotFinish,
        RebelWin,
        KingWin,
        SpyWin
    }
}
