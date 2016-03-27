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

        public override PhaseList process(Game game)
        {
            return player.attack(attack, targets, actionPhase);
        }
    }

    public class RequestNegatePhase : VisiblePhase
    {
        public ToolCard toolCard { get; }
        public Player target { get; }
        private UseToolPhase useToolPhase;

        public RequestNegatePhase(Player player, ToolCard toolCard, Player target, UseToolPhase useToolPhase) : base(player)
        {
            this.toolCard = toolCard;
            this.target = target;
            this.useToolPhase = useToolPhase;
        }

        public RequestNegatePhase(Player player) : base(player)
        {
            throw new NotImplementedException();
        }

        public override PhaseList handleResponse(UserAction userAction, Game game)
        {
            throw new NotImplementedException();
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

        public override PhaseList process(Game game)
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
        public override PhaseList process(Game game)
        {
            throw new NotImplementedException();
        }
    }

    public class ReponsePhase : VisiblePhase
    {
        private CardType needCard;
        private HarmPhase consequence;
        public ReponsePhase(Player player, CardType needCard, HarmPhase consequence) : base(player)
        {
            this.needCard = needCard;
            this.consequence = consequence;
        }

        public override PhaseList handleResponse(UserAction userAction, Game game)
        {
            if (userAction == null)
                return null;
            if (userAction == null)
                return null;
            switch (userAction.type)
            {
                case UserActionType.YES_OR_NO:
                    if ((userAction as YesOrNoAction).no)
                        return new PhaseList(consequence);
                    else
                        return null;
                case UserActionType.CARD:
                    CardAction cardAction = userAction as CardAction;
                    Card card = cardAction.card;
                    if (card.type == needCard)
                    {

                        return new PhaseList();
                    }
                    return null;
                default:
                    return null;
            }
        }
        
    }
    public class HarmPhase : HiddenPhase
    {
        public HarmPhase(Player player, Player source, int harm) : base(player)
        {
        }

        public override PhaseList process(Game game)
        {
            throw new NotImplementedException();
        }
    }
}
