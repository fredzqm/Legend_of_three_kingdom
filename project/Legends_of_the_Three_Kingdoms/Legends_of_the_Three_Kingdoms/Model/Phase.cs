using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{
    public class Phase
    {
        public int playerID { get; }
        public PhaseType type { get;}

        public Phase(int playerID, PhaseType playerTurn)
        {
            this.playerID = playerID;
            this.type = playerTurn;
        }

        public bool needResponse()
        {
            switch (type)
            {
                case PhaseType.PlayerTurn:
                case PhaseType.JudgePhase:
                case PhaseType.DrawingPhase:
                    return false;
                case PhaseType.ActionPhase:
                case PhaseType.DiscardPhase:
                    return true;
                default: throw new Exception("This type not defined");
            }
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
                throw new Exception("Empty PhaseList Exception");
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

        public Phase bottom()
        {
            return tail.stage;
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
