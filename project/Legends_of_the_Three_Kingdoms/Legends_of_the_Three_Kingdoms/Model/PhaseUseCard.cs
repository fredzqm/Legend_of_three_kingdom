using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{
    /// <summary>
    /// subclass of need response phase 
    /// </summary>
    public class AttackPhase : NeedResponsePhase
    {
        public Attack attack { get; }
        public ActionPhase actionPhase { get; }
        public Player[] targets { get; }

        public int harm { get; set; }
        /// <summary>
        /// create attack phase 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="card"></param>
        /// <param name="targets"></param>
        /// <param name="actionPhase"></param>
        public AttackPhase(Player player, Attack card, Player[] targets, ActionPhase actionPhase) : base(player)
        {
            this.attack = card;
            this.targets = targets;
            this.actionPhase = actionPhase;
            harm = 1;
            if (actionPhase.drunk)
            {
                actionPhase.drunk = false;
                harm++;
            }
        }

        public override PhaseList handleResponse(int count, Card respondCard, IGame game)
        {
            if (respondCard == null)
                return new PhaseList(new HarmPhase(targets[0], player, harm,attack));
            return new PhaseList();
        }

        public override PhaseList askForResponse(int count, IGame game)
        {
            if (player.canNotAttack(this, game))
                return new PhaseList();
            actionPhase.attackCount++;
            player.handCards.Remove(attack);
            game.cards.discardOne(attack);
            return new PhaseList(new ResponsePhase(targets[0], this, c => c is Miss), this);
        }
    }

    //public class PeachPhase : HiddenPhase
    //{
    //    public Attack attack { get; }
    //    public ActionPhase actionPhase { get; }
    //    public Player[] targets { get; }
    //    public int harm { get; set; }
    //    public PeachPhase(Player player, Player target, ActionPhase actionPhase) : base(player)
    //    {
    //        this.targets = targets;
    //        this.actionPhase = actionPhase;
    //        this.harm = 1;
    //        if (actionPhase.drunk)
    //        {
    //            actionPhase.drunk = false;
    //            harm++;
    //        }
    //    }

    //    public PeachPhase(Player player) : base(player) { }

    //    public override PhaseList advance(IGame game)
    //    {
    //        return player.attack(this, game);
    //    }
    //}

        /// <summary>
        /// subclass of hiddenphase 
        /// </summary>
    //public class UseToolPhase : HiddenPhase
    //{
    //    bool negated;
    //    ToolCard card;
    //    Player[] targets;
    //    /// <summary>
    //    /// create hidden phase 
    //    /// </summary>
    //    /// <param name="player"></param>
    //    /// <param name="card"></param>
    //    /// <param name="targets"></param>
    //    public UseToolPhase(Player player, ToolCard card, Player[] targets) : base(player)
    //    {
    //        negated = false;
    //        this.card = card;
    //        this.targets = targets;
    //    }

    //    public override PhaseList advance(IGame game)
    //    {
    //        if (negated)
    //            return new PhaseList();
    //        throw new NotImplementedException();
    //    }
    //    /// <summary>
    //    /// set negated to true
    //    /// </summary>
    //    public void negate()
    //    {
    //        negated = true;
    //    }

    //}
    /// <summary>
    /// subclass of hiddenphase 
    /// </summary>
    //public class UseEquipmentPhase : HiddenPhase
    //{
    //    Equipment card;
    //    /// <summary>
    //    /// create use equipment phase 
    //    /// </summary>
    //    /// <param name="player"></param>
    //    /// <param name="card"></param>
    //    public UseEquipmentPhase(Player player, Equipment card) : base(player)
    //    {
    //        this.card = card;
    //    }
    //    public override PhaseList advance(IGame game)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    ///// <summary>
    ///// subclass of hidden phase 
    ///// </summary>
    //public class UseCardPhase : HiddenPhase
    //{
    //    public Card card { get; }
    //    /// <summary>
    //    /// create use card phase 
    //    /// </summary>
    //    /// <param name="player"></param>
    //    /// <param name="card"></param>
    //    public UseCardPhase(Player player, Card card) : base(player)
    //    {
    //        this.card = card;
    //    }
    //    public override PhaseList advance(IGame game)
    //    {
    //        if (player.handCards.Contains(card))
    //        {
    //            throw new NotImplementedException();
    //        }
    //        throw new Exception("Cannot Use card not in hand");
    //    }
    //}

 

}
