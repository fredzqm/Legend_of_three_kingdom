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
        public ResponsivePhase currentStage
        {
            get
            {
               ResponsivePhase ret = stages.top() as ResponsivePhase;
                if (ret == null)
                    throw new Exception("Still running");
                return ret;
            }
        }

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
            stages.add(new PlayerTurn(players[0]));
            skipIrresponsivePhases();
        }

        public void nextStage()
        {
            Phase curPhase = stages.pop();
            if (curPhase is PlayerTurn)
            { // when turn switches
                curRoundPlayerID = players[curPhase.playerID];
            }
            PhaseList followingPhases = players[curPhase.playerID].handlePhase(curPhase, this);
            stages.pushStageList(followingPhases);
            skipIrresponsivePhases();
        }

        private void skipIrresponsivePhases()
        {
            while(! (stages.top() is ResponsivePhase))
            {
                nextStage();
            }
        }

        public bool userResponse(UserAction userAction)
        {
            return currentStage.userInput(userAction);
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