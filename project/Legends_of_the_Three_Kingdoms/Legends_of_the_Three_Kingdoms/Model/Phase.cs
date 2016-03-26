using Legends_of_the_Three_Kingdoms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{
    /// <summary>
    /// Phase contains information that the game needs to process this phase
    /// </summary>
    public abstract class Phase
    {
        /// <summary>
        /// Whose phase
        /// </summary>
        public int playerID { get { return player.playerID; } }
        public Player player { get; }

        /// <summary>
        /// The type of the phase
        /// </summary>
        public Phase(Player player)
        {
            this.player = player;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>true if this phase require user response,
        /// fase if this phase should be invisible from outside</returns>
        public abstract bool needResponse();

        public abstract PhaseList handleResponse(UserAction userAction, Game game);

    }

    public abstract class FundamentalPhase : Phase
    {
        public FundamentalPhase(Player player) : base(player) { }

        public sealed override bool needResponse()
        {
            return true;
        }
    }

    public class PlayerTurn : Phase
    {
        public PlayerTurn(Player player) : base(player) { }

        public override PhaseList handleResponse(UserAction userAction, Game game)
        {
            return this.player.playerTurn(this, game);
        }

        public override string ToString()
        {
            return "Plyaer " + playerID + " at PlayerTurn";
        }
        public override sealed bool needResponse()
        {
            return false;
        }
    }

    public class JudgePhase : FundamentalPhase
    {
        public JudgePhase(Player player) : base(player) { }

        public override PhaseList handleResponse(UserAction userAction, Game game)
        {
            return this.player.judgePhase(this, userAction,  game);
        }

        public override string ToString()
        {
            return "Plyaer " + playerID + " at JudgePhase";
        }
    }

    public class DrawingPhase : FundamentalPhase
    {
        public DrawingPhase(Player player) : base(player) { }

        public override PhaseList handleResponse(UserAction userAction, Game game)
        {
            return this.player.drawingPhase(this, userAction, game);
        }

        public override string ToString()
        {
            return "Plyaer " + playerID + " at DrawingPhase";
        }
    }

    public class ActionPhase : FundamentalPhase
    {
        public ActionPhase(Player player) : base(player) { }

        public override PhaseList handleResponse(UserAction userAction, Game game)
        {
            return this.player.actionPhase(this, userAction, game);
        }

        public override string ToString()
        {
            return "Plyaer " + playerID + " at ActionPhase";
        }
    }

    public class DiscardPhase : FundamentalPhase
    {
        public DiscardPhase(Player player) : base(player) { }

        public override PhaseList handleResponse(UserAction userAction, Game game)
        {
            return this.player.discardPhase(this, userAction, game);
        }

        public override string ToString()
        {
            return "Plyaer " + playerID + " at DiscardPhase";
        }
    }

    public class UsagePhase : Phase
    {
        Card card;
        Player[] targets;
        public UsagePhase(Player player, Card card, Player[] targets) : base(player) {
            this.card = card;
            this.targets = targets;
        }

        public override PhaseList handleResponse(UserAction userAction, Game game)
        {
            return null;
            //switch (t)
            //{
            //    case CardType.Attack:
            //        return new Attack(s, v);
            //    case CardType.Miss:
            //        return new Miss(s, v);
            //    case CardType.Wine:
            //        return new Wine(s, v);
            //    case CardType.Peach:
            //        return new Peach(s, v);
            //    case CardType.Negate:
            //        return new Negate(s, v);
            //    case CardType.Barbarians:
            //        return new Barbarians(s, v);
            //    case CardType.HailofArrow:
            //        return new HailofArrow(s, v);
            //    case CardType.PeachGarden:
            //        return new PeachGarden(s, v);
            //    case CardType.Wealth:
            //        return new Wealth(s, v);
            //    case CardType.Steal:
            //        return new Steal(s, v);
            //    case CardType.Break:
            //        return new Break(s, v);
            //    case CardType.Capture:
            //        return new Capture(s, v);
            //    case CardType.Starvation:
            //        return new Starvation(s, v);
            //    case CardType.Crossbow:
            //        return new Crossbow(s, v);
            //    case CardType.IceSword:
            //        return new IceSword(s, v);
            //    case CardType.Scimitar:
            //        return new Scimitar(s, v);
            //    case CardType.BlackShield:
            //        return new BlackShield(s, v);
            //    case CardType.EightTrigrams:
            //        return new EightTrigrams(s, v);
            //    default:
            //        throw new NotImplementedException();
            //}
        }

        public override bool needResponse()
        {
            return false;
        }
    }

    /// <summary>
    /// A simple data structure (linkedList) that used to store phases of game
    /// </summary>
    public class PhaseList
    {
        private Node head;
        private Node tail;

        public PhaseList()
        {
            head = null;
            tail = null;
        }

        public PhaseList(params Phase[] phases) : this()
        {
            foreach (Phase p in phases)
            {
                add(p);
            }
        }

        public void add(Phase s)
        {
            if (head == null)
            {
                head = new Node(s);
                tail = head;
            }
            else
            {
                tail.next = new Node(s);
                tail = tail.next;
            }
        }

        public bool isEmpty()
        {
            return head == null;
        }

        public Phase pop()
        {
            if (isEmpty())
            {
                throw new EmptyException("Empty PhaseList Exception");
            } 
            Phase retStage = head.stage;
            if (head == tail)
            { // empty
                head = null;
                tail = null;
            }
            else
            {
                head = head.next;
            }
            return retStage;
        }

        /// <summary>
        /// concatenate two phaseList together
        /// </summary>
        /// <param name="added"></param>
        public void pushStageList(PhaseList added)
        {
            if (added.head == null)
                return;
            added.tail.next = head;
            head = added.head;
        }

        public Phase top()
        {
            return head.stage;
        }

        class Node
        {
            internal Phase stage;
            internal Node next;

            public Node(Phase s)
            {
                this.stage = s;
                this.next = null;
            }

            internal Node setNext(Node node)
            {
                this.next = node;
                return node;
            }
        }
    }

}
