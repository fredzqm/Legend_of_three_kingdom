using System;

namespace LOTK.Model
{
    public class HarmPhase : HiddenPhase
    {
        public int harm { get; }
        public Player source { get; }

        public HarmPhase(Player player, Player source, int harm) : base(player)
        {
            this.source = source;
            this.harm = harm;
        }

        public override PhaseList advance(IGame game)
        {
            return player.harm(this, game);
        }
    }

    public class askForHelpPhase : NeedResponsePhase
    {
        public askForHelpPhase(Player player) : base(player) {}

        public override PhaseList handleResponse(int count, Card respondCard, IGame game)
        {
            throw new NotImplementedException();
        }
    }

    public class RecoverPhase : HiddenPhase
    {
        public int recover { get;}
        public RecoverPhase(Player player, int recover): base(player)
        {
            this.recover = recover;
        }
        public override PhaseList advance(IGame game)
        {
            return player.recover(this, game);
        }
    }
}