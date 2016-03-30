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

    public class AskForHelpPhase : NeedResponsePhase
    {
        public AskForHelpPhase(Player player) : base(player) {}

        public override PhaseList askForResponse(int count, IGame g)
        {
            if (player == g.curRoundPlayer)
                return new PhaseList(new ResponsePhase(g.nextPlayer(g.curRoundPlayer,count), this, c => (c is Peach) || (c is Wine)), this);
            else
                return new PhaseList(new ResponsePhase(g.nextPlayer(g.curRoundPlayer,count), this, c => c is Peach), this);
        }

        public override PhaseList handleResponse(int count, Card respondCard, IGame g)
        {
            if(respondCard != null)
            {
                return new PhaseList(new RecoverPhase(player, 1), this);
            }
            return askForResponse(count, g);
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