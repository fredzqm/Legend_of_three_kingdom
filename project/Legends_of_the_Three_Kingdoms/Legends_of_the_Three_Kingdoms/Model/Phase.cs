using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{
    public interface Phase
    {
        PhaseList process(Game g);
        int playerID { get; }
    }
    
    public class PlayerTurn : Phase
    {
        public int playerID { get; set; }

        public PlayerTurn(int player)
        {
            playerID = player;
        }

        public PhaseList process(Game g)
        {
            return new PhaseList(new JudgePhase(playerID));
        }
    }

    public class JudgePhase : Phase
    {
        public int playerID { get; set; }
        public JudgePhase(int player)
        {
            playerID = player;
        }
        public PhaseList process(Game g)
        {
            return new PhaseList(new DrawingPhase(playerID));
        }
    }
    public class DrawingPhase : Phase
    {
        public int playerID { get; set; }
        public DrawingPhase(int player)
        {
            playerID = player;
        }
        public PhaseList process(Game g)
        {
            return new PhaseList(new ActionPhase(playerID));
        }
    }
    public class ActionPhase : Phase
    {
        public int playerID { get; set; }
        public ActionPhase(int player)
        {
            playerID = player;
        }
        public PhaseList process(Game g)
        {
            return new PhaseList(new DiscardPhase(playerID));
        }
    }
    public class DiscardPhase : Phase
    {
        public int playerID { get; set; }
        public DiscardPhase(int player)
        {
            playerID = player;
        }
        public PhaseList process(Game g)
        {
            return new PhaseList(new PlayerTurn((playerID+1)% g.Num_Player));
        }
    }

    public class PhaseList
    {
        private Node head;
        private Node tail;
        private JudgePhase judgePhase;

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

        public Phase pop()
        {
            if (head == null)
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

        public Phase peek()
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
