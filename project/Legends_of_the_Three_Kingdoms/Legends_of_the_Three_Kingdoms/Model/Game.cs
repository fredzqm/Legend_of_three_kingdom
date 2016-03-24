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

        private int curRoundPlayerID;
        public Player curRoundPlayer { get { return players[curRoundPlayerID]; } }

        private PhaseList stages { get; set; }
        public Phase currentStage { get { return stages.top(); } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Num_player">The number of players</param>
        /// <param name="cardList">The list of cards</param>
        public Game(int Num_player, ICollection<Card> cardList)
        {
            Num_Player = Num_player;
            players = new Player[Num_Player];
            if (cardList != null)
                cards = new CardSet(cardList);

            for (int i = 0; i < Num_Player; i++)
            {
                players[i] = new Player(i);
            }
            stages = new PhaseList();
            stages.add(new Phase(0, PhaseType.PlayerTurn));
            nextStage();
        }

        /// <summary>
        /// Advance the stage
        /// </summary>
        public void nextStage()
        {
            Phase curPhase = stages.pop();
            if (curPhase.type == PhaseType.PlayerTurn)
            { // when turn switches
                curRoundPlayerID = curPhase.playerID;
            }
            PhaseList followingPhases = players[curPhase.playerID].handlePhase(curPhase, this);
            stages.pushStageList(followingPhases);
            while (!(stages.top().needResponse()))
            { // skipping all of those phases that do need user response.
                nextStage();
            }
        }

        /// <summary>
        /// This is the entry for user response
        /// handle user action
        /// </summary>
        /// <param name="userAction"></param>
        public void userResponse(UserAction userAction)
        {
            if (players[currentStage.playerID].UserInput(currentStage, userAction))
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