using System;

namespace LOTK.Model
{
    public class Game
    {
        public readonly int Num_Player;
        public PhaseList stages { get; set; }
        public Phase currentStage { get { return stages.top(); } }
        public int currentPlayerID { get { return stages.bottom().playerID; } }
        public Game(int Num_player)
        {
            Num_Player = Num_player;
            stages = new PhaseList();
            stages.add(new PlayerTurn(0));
        }

        public void nextStage()
        {
            stages.pushStageList(stages.pop().nextStage(this));
        }

        public bool userResponse(UserAction userAction)
        {
            throw new NotImplementedException();
        }
    }
}