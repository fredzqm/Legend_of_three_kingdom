using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{

    public class AttackPhase : HiddenPhase
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
        }

        public AttackPhase(Player player) : base(player) { }

        public override PhaseList advance(IGame game)
        {
            return player.attack(attack, targets, actionPhase);
        }
    }

    public class RequestNegatePhase : UserActionPhase
    {
        public ToolCard toolCard { get; }
        public Player target { get; }
        private UseToolPhase useToolPhase;

        public RequestNegatePhase(Player player, ToolCard toolCard, Player target, UseToolPhase useToolPhase) : base(player, 1)
        {
            this.toolCard = toolCard;
            this.target = target;
            this.useToolPhase = useToolPhase;
        }

        public override PhaseList timeOut(IGame game)
        {
            return new PhaseList();
        }

        public void negateTool()
        {
            useToolPhase.negate();
        }
    }

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

    public class ReponsePhase : UserActionPhase
    {
        private CardType needCard;
        private HarmPhase consequence;
        public ReponsePhase(Player player, CardType needCard, HarmPhase consequence) : base(player, 10)
        {
            this.needCard = needCard;
            this.consequence = consequence;
        }

        public override PhaseList responseYesOrNo(bool yes, IGame game)
        {
            if (yes)
                return null;
            else
                return new PhaseList(consequence);
        }

        public override PhaseList responseCardAction(Card card, Player[] targets)
        {
            if (card.type == needCard)
            {
                return new PhaseList();
            }
            return null;
        }
        
    }
    public class HarmPhase : HiddenPhase
    {
        public HarmPhase(Player player, Player source, int harm) : base(player)
        {
        }

        public override PhaseList advance(IGame game)
        {
            throw new NotImplementedException();
        }
    }
}
