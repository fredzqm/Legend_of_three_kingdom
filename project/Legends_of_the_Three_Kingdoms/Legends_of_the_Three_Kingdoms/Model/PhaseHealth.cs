using System;

namespace LOTK.Model
{/// <summary>
/// subclass of hiddenphase
/// </summary>
    public class HarmPhase : HiddenPhase
    {
        public int harm { get; }
        public Player source { get; }
        public Attack card { get; }
        /// <summary>
        /// create harmphase
        /// </summary>
        /// <param name="player"></param>
        /// <param name="source"></param>
        /// <param name="harm"></param>
        /// <param name="card"></param>
        public HarmPhase(Player player, Player source, int harm, Attack card) : base(player)
        {
            this.source = source;
            this.harm = harm;
            this.card = card;
        }

        public override PhaseList advance(IGame game)
        {
            return player.harm(this, game);
        }
    }
    /// <summary>
    /// subclass of need preponse phase
    /// </summary>
    public class AskForHelpPhase : NeedResponsePhase
    {
        public HarmPhase harmSource { get; }

        /// <summary>
        /// create ask for help phase
        /// </summary>
        /// <param name="player"></param>
        public AskForHelpPhase(Player player, HarmPhase harm) : base(player)
        {
            harmSource = harm;
        }

        public override PhaseList askForResponse(int count, IGame g)
        {
            if (player.health > 0)
                return new PhaseList();
            if (count == g.Num_Player)
            {
                return new PhaseList(new DeadPhase(player, harmSource));
            }
            Player helpProvider = g.nextPlayer(g.curRoundPlayer, count);
            if (player == helpProvider)
                return new PhaseList(new ResponsePhase(helpProvider, this, c => (c is Peach) || (c is Wine)), this);
            else
                return new PhaseList(new ResponsePhase(helpProvider, this, c => c is Peach), this);
        }

        public override PhaseList handleResponse(int count, Card respondCard, IGame g)
        {
            if (respondCard != null)
            {
                return new PhaseList(new RecoverPhase(player, 1), this);
            }
            return askForResponse(count, g);
        }
    }
    /// <summary>
    /// subclass of hidden phase 
    /// </summary>
    public class RecoverPhase : HiddenPhase
    {
        public int recover { get; }
        /// <summary>
        /// create recover phase
        /// </summary>
        /// <param name="player"></param>
        /// <param name="recover"></param>
        public RecoverPhase(Player player, int recover) : base(player)
        {
            this.recover = recover;
        }
        public override PhaseList advance(IGame game)
        {
            return player.recover(this, game);
        }
    }

    public class DeadPhase : HiddenPhase
    {
        public HarmPhase harmSource { get; }

        public DeadPhase(Player player, HarmPhase harmSource) : base(player)
        {
            this.harmSource = harmSource;
        }

        public override PhaseList advance(IGame game)
        {
            return player.die(game);
        }
    }
}