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
        public int playerID { get; }

        /// <summary>
        /// The type of the phase
        /// </summary>
        public Phase(int playerID)
        {
            this.playerID = playerID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>true if this phase require user response,
        /// fase if this phase should be invisible from outside</returns>
        public abstract bool needResponse();

        public abstract PhaseList handleResponse(UserAction userAction, Game game);

        public override string ToString()
        {
            return "Plyaer "+playerID+ " at ";
        }
    }

    public abstract class HiddenPhase : Phase
    {
        public HiddenPhase(int playerID) : base(playerID)
        {

        }

        public override sealed bool needResponse()
        {
            return false;
        }
    }

    public abstract class ResponsivePhase : Phase
    {
        public ResponsivePhase(int playerID) : base(playerID)
        {

        }

        public sealed override bool needResponse()
        {
            return true;
        }
    }

    public class PlayerTurn : ResponsivePhase
    {
        public PlayerTurn(int playerID) : base(playerID)
        {

        }

        public override PhaseList handleResponse(UserAction userAction, Game game)
        {
            return game.players[playerID].playerTurn(this, game);
        }

    }

    public class JudgePhase : ResponsivePhase
    {
        public JudgePhase(int playerID) : base(playerID)
        {

        }

        public override PhaseList handleResponse(UserAction userAction, Game game)
        {
            return game.players[playerID].judgePhase(this, userAction,  game);
        }

    }

    public class DrawingPhase : ResponsivePhase
    {
        public DrawingPhase(int playerID) : base(playerID)
        {

        }

        public override PhaseList handleResponse(UserAction userAction, Game game)
        {
            return game.players[playerID].drawingPhase(this, userAction, game);
        }

    }

    public class ActionPhase : ResponsivePhase
    {
        public ActionPhase(int playerID) : base(playerID)
        {

        }

        public override PhaseList handleResponse(UserAction userAction, Game game)
        {
            return game.players[playerID].actionPhase(this, userAction, game);
        }

    }

    public class DiscardPhase : ResponsivePhase
    {
        public DiscardPhase(int playerID) : base(playerID)
        {

        }

        public override PhaseList handleResponse(UserAction userAction, Game game)
        {
            return game.players[playerID].discardPhase(this, userAction, game);
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
