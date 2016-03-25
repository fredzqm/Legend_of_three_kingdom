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
    public class Phase
    {
        /// <summary>
        /// Whose phase
        /// </summary>
        public int playerID { get; }

        /// <summary>
        /// The type of the phase
        /// </summary>
        public PhaseType type { get;}

        private object[] extraInfor;
        private int v1;
        private double v2;
        private int v3;

        public Phase(int playerID, PhaseType playerTurn)
        {
            this.playerID = playerID;
            this.type = playerTurn;
        }

        public Phase(int playerID, PhaseType playerTurn, params object[] info) : this(playerID, playerTurn)
        {
            extraInfor = new object[info.Length];
            for (int i = 0; i < info.Length; i++)
            {
                extraInfor[i] = info[i];
            }
        }

        public object getInfor(int i)
        {
            return extraInfor[i];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>true if this phase require user response,
        /// fase if this phase should be invisible from outside</returns>
        public bool needResponse()
        {
            switch (type)
            {
                case PhaseType.PlayerTurn:
                    return false;
                case PhaseType.JudgePhase:
                case PhaseType.DrawingPhase:
                case PhaseType.ActionPhase:
                case PhaseType.DiscardPhase:
                    return true;
                default: throw new Exception("This type not defined");
            }
        }

        public override string ToString()
        {
            return "Plyaer "+playerID+ " at " + type.ToString();
        }


    }

    public enum PhaseType
    {
        PlayerTurn,
        JudgePhase,
        DrawingPhase,
        ActionPhase,
        DiscardPhase
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
