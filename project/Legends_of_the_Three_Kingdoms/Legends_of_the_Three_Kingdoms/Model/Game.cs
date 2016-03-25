using System;
using System.Collections.Generic;
using LOTK.View;

namespace LOTK.Model
{
    /// <summary>
    /// This is created for the purpose of dependency injection
    /// </summary>
    public interface IGame
    {
        int Num_Player { get; }
    }

    /// <summary>
    /// The main model representing the game
    /// </summary>
    public class Game : IGame
    {

        public int Num_Player { get; }
        public Player[] players { get; }
        public CardSet cards { get; }


        private PhaseList stages;
        public Phase curPhase { get { return stages.top(); } }
        public Player curPhasePlayer { get { return players[curPhase.playerID]; } }

        public bool timerAutoAdvance;
        public bool timerVisit;

        private int curRoundPlayerID;
        public Player curRoundPlayer { get { return players[curRoundPlayerID]; } }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Num_player">The number of players</param>
        /// <param name="cardList">The list of cards</param>
        public Game(int Num_player, ICollection<Card> cardList)
        {
            Num_Player = Num_player;
            if (cardList != null)
                cards = new CardSet(cardList);

            players = new Player[Num_Player];
            for (int i = 0; i < Num_Player; i++)
            {
                players[i] = new Player(i, "Player Name", "Player Description");
            }
            stages = new PhaseList();
            stages.add(new Phase(0, PhaseType.PlayerTurn));
            nextStage();
        }

        /// <summary>
        /// Advance the next stage and skip the following stages that do not need user response
        /// </summary>
        public void nextStage()
        {
            timerAutoAdvance = false;
            advanceStage();
            while (!(stages.top().needResponse()))
            { // skipping all of those phases that do not need response
                advanceStage();
            }
            timerVisit = false;
            timerAutoAdvance = curPhasePlayer.autoPhase(curPhase);
        }

        private void advanceStage()
        {
            if (curPhase.type == PhaseType.PlayerTurn)
            { // when turn switches
                curRoundPlayerID = curPhase.playerID;
            }
            PhaseList followingPhases = curPhasePlayer.handlePhase(curPhase, this);
            stages.pop();
            stages.pushStageList(followingPhases);
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
                    nextStage();
                    return true;
                }
                timerVisit = true;
            }
            return false;
        }

        /// <summary>
        /// This is the entry for user response
        /// handle user action
        /// </summary>
        /// <param name="userAction"></param>
        public void userResponse(UserAction userAction)
        {
            if (players[curPhase.playerID].UserInputYesOrNo(curPhase, userAction))
                nextStage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="num">Number of cards</param>
        /// <returns>the card drown</returns>
        public List<Card> drawCard(int num)
        {
            List<Card> cards = new List<Card>();
            for (int i = 0; i < num; i++)
                cards.Add(this.cards.pop());
            return cards;
        }
    }
}