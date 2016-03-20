using System;

namespace LOTK.Model
{
    public class Game
    {
        private readonly int Num_Player;
        public PhaseList stages { get; set; }
        public JudgePhase currentStage { get; set; }

        public Game(int Num_player)
        {
            Num_Player = Num_player;
            stages = new PhaseList();
            stages.add(new PlayerTurn(0));
        }

        public void nextStage()
        {
            throw new NotImplementedException();
        }
    }
}