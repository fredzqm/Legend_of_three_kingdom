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
        ICardSet cards { get; }
        Phase curPhase { get; }
        Player curRoundPlayer { get; }
        bool tick();
        void nextStage(UserAction userAction);
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
        /// <summary>
        /// return the next player should act
        /// </summary>
        /// <param name="curPlayer"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public Player nextPlayer(int curPlayer, int count)
        {
            return players[(curPlayer + count) % Num_Player];
        }

        public ICardSet cards { get; }


        private PhaseList stages;
        /// <summary>
        /// get current phase
        /// </summary>
        public Phase curPhase {get {return stages.top();}}
        /// <summary>
        /// get the act player on the current stage 
        /// </summary>
        public Player curPhasePlayer { get { return curPhase.player; } }

        public bool timerAutoAdvance;
        public bool timerVisit;

        public Player curRoundPlayer { get; private set; }
        /// <summary>
        /// create game
        /// </summary>
        /// <param name="players"></param>
        /// <param name="cardList"></param>
        public Game(Player[] players, ICardSet cardList)
        {
            Num_Player = players.Length;
            if (cardList == null)
                throw new NotDefinedException("CardList is not defined");
            cards = cardList;

            if (players == null)
                throw new NotDefinedException("CardList is not defined");
            this.players = players;

            stages = new PhaseList();
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
            nextStage(null);
        }
        /// <summary>
        /// judge the user behavior and react
        /// </summary>
        /// <param name="fromPlayerID"></param>
        /// <param name="userAction"></param>
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
        /// user click card and player
        /// </summary>
        /// <param name="playerID"></param>
        /// <param name="cardID"></param>
        /// <param name="targets"></param>
        internal void useCardAction(int playerID, int cardID, params int[] targets)
        {
            processUserInput(playerID, new UseCardAction(cards[cardID], players[targets[0]]));
        }
        /// <summary>
        ///  use click card but no player
        /// </summary>
        /// <param name="playerID"></param>
        /// <param name="cardID"></param>
        internal void cardAction(int playerID, int cardID)
        {
            processUserInput(playerID, new CardAction(cards[cardID]));
        }
        /// <summary>
        /// user only click ok or cancel
        /// </summary>
        /// <param name="playerID"></param>
        /// <param name="v"></param>
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
        /// <exception cref="NoCardException"><seealso cref="PhaseList.pop"/></exception>
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
                throw new NoCardException("The card stack is empty", e);
            }
            return cards;
        }
    }
}
