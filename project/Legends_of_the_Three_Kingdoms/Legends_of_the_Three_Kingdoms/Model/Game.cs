using System;

namespace LOTK.Model
{
    public class Game
    {
        public readonly int Num_Player;
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
        public int currentPlayerID { get { return stages.bottom().playerID; } }
        public Game(int Num_player)
        {
            Num_Player = Num_player;
            stages = new PhaseList();
            stages.add(new PlayerTurn(0));
            skipIrresponsivePhases();
        }

        public void nextStage()
        {
            stages.pushStageList(stages.pop().nextStage(this));
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
    }
}