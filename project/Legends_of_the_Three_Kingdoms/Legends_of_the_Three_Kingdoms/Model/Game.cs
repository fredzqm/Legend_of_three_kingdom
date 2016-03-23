using System;
using System.Collections.Generic;

namespace LOTK.Model
{

    public interface IGame
    {
        int Num_Player { get; }
    }

    public class Game : IGame
    {
        public int Num_Player { get; }
        public readonly Player[] players;
        public readonly CardSet cards;
        private int curRoundPlayerID;
        public Player curRoundPlayer { get { return players[curRoundPlayerID]; } }
        private PhaseList stages { get; set; }
        public Phase currentStage { get { return stages.top(); } }

        public Game(int Num_player, List<Card> cardList)
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
            skipIrresponsivePhases();
        }

        public void nextStage()
        {
            Phase curPhase = stages.pop();
            if (curPhase.type == PhaseType.PlayerTurn)
            { // when turn switches
                curRoundPlayerID = players[curPhase.playerID];
            }
            PhaseList followingPhases = players[curPhase.playerID].handlePhase(curPhase, this);
            stages.pushStageList(followingPhases);
            skipIrresponsivePhases();
        }

        private void skipIrresponsivePhases()
        {
            while (!(stages.top().needResponse()))
            {
                nextStage();
            }
        }

        public bool userResponse(UserAction userAction)
        {
            return players[currentStage.playerID].UserInput(currentStage, userAction);
        }

        public List<Card> drawCard(int v)
        {
            List<Card> cards = new List<Card>();
            for (int i = 0; i < v; i++)
                cards.Add(this.cards.pop());
            return cards;
        }
    }
}