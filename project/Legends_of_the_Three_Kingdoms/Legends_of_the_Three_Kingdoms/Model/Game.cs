using System;
using System.Collections.Generic;

namespace LOTK.Model
{
    public class Game
    {
        public readonly int Num_Player;

        private CardSet cardpile;

        public PhaseList stages { get; set; }
        public ResponsivePhase currentStage {
            get {
                try {
                    return stages.top() as ResponsivePhase;
                } catch (InvalidCastException) {
                    throw new Exception("Still spinning");
                }
            }
        }
        private Player[] players;
        public Player this[int i] {get { return players[i]; } }
        private int curRoundPlayerID;
        public Player curRoundPlayer { get { return this[curRoundPlayerID]; } }

        public Game(int Num_player)
        {
            Num_Player = Num_player;
            players = new Player[Num_Player];
            for (int i = 0; i < Num_Player;i++)
            {
                players[i] = new Player(i);
            }
            stages = new PhaseList();
            stages.add(new PlayerTurn(players[0]));
            skipIrresponsivePhases();
        }

        public Game(int Num_player, List<Card> cardList) : this(Num_player)
        {
            cardpile = new CardSet(cardList);
        }

        public void nextStage()
        {
            stages.pushStageList(stages.pop().nextStage(this));
            skipIrresponsivePhases();
        }
        internal void setCurrentPlayerID(int curPlay)
        {
            curRoundPlayerID = curPlay;
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

        internal void initializeCardPile(List<Card> ls)
        {
           
        }

        public List<Card> drawCard(int v)
        {
            List<Card> cards = new List<Card>();
            for (int i = 0; i < v; i++)
                cards.Add(cardpile.pop());
            return cards;
        }
    }
}