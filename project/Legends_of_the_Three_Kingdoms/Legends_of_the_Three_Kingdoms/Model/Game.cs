namespace LOTK.Model
{
    public class Game
    {
        private readonly int Num_Player;
        public StageList stages { get; set; }
        public Game(int Num_player)
        {
            Num_Player = Num_player;
            stages = new StageList();
            stages.add(new PlayerTurn(0));
        }
    }
}