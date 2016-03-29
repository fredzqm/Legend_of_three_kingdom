using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{

    public class AttackPhase : NeedResponsePhase
    {
        public Attack attack { get; }
        public ActionPhase actionPhase { get; }
        public Player[] targets { get; }
        public int harm { get; set; }
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

        public AttackPhase(Player player) : base(player) { }

        public override PhaseList handleResponse(int count, Card respondCard, IGame game)
        {
            if (count == 0)
            {
                if (targets.Length > attack.numOfTargets() || actionPhase.attackCount > 1 || targets[0] == player)
                    return new PhaseList();
                return new PhaseList(new responsePhase(targets[0], this, c => c is Miss), this);
            }
            if (count > 1)
                throw new Exception("Only should response once");
            if (respondCard == null)
            {
                return new PhaseList(new HarmPhase(targets[0], player, harm));
            }
            if (!(respondCard is Miss))
                throw new Exception("Only miss should occur");
            return new PhaseList();
        }
    }

    //public class PeachPhase : HiddenPhase
    //{
    //    public Attack attack { get; }
    //    public ActionPhase actionPhase { get; }
    //    public Player[] targets { get; }
    //    public int harm { get; set; }
    //    public PeachPhase(Player player, Attack card, Player[] targets, ActionPhase actionPhase) : base(player)
    //    {
    //        this.attack = card;
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

   
    public class UseToolPhase : HiddenPhase
    {
        bool negated;
        ToolCard card;
        Player[] targets;
        public UseToolPhase(Player player, ToolCard card, Player[] targets) : base(player)
        {
            negated = false;
            this.card = card;
            this.targets = targets;
        }

        public override PhaseList advance(IGame game)
        {
            if (negated)
                return new PhaseList();
            throw new NotImplementedException();
        }

        public void negate()
        {
            negated = true;
        }

    }

    public class UseEquipmentPhase : HiddenPhase
    {
        Equipment card;
        public UseEquipmentPhase(Player player, Equipment card) : base(player)
        {
            this.card = card;
        }
        public override PhaseList advance(IGame game)
        {
            throw new NotImplementedException();
        }
    }


    public class UseCardPhase : HiddenPhase
    {
        public Card card { get; }
        public UseCardPhase(Player player, Card card) : base(player)
        {
            this.card = card;
        }
        public override PhaseList advance(IGame game)
        {
            if (player.handCards.Contains(card))
            {
                throw new NotImplementedException();
            }
            throw new Exception("Cannot Use card not in hand");
        }
    }

 

}
