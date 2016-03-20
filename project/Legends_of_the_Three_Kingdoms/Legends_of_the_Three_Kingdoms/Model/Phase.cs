using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{
    public interface Phase
    {
        PhaseList nextStage(Game g);
        int playerID { get; }
    }

    public interface ResponsivePhase : Phase
    {
        bool userInput(UserAction u);
    }
    
    public class PlayerTurn : Phase
    {
        public int playerID { get; set; }

        public PlayerTurn(int player)
        {
            playerID = player;
        }

        public PhaseList nextStage(Game g)
        {
            return new PhaseList(new JudgePhase(playerID));
        }

    }

    public class JudgePhase : ResponsivePhase
    {
        public int playerID { get; set; }
        public JudgePhase(int player)
        {
            playerID = player;
        }
        public PhaseList nextStage(Game g)
        {
            return new PhaseList(new DrawingPhase(playerID));
        }

        public bool userInput(UserAction u)
        {
            throw new NotImplementedException();
        }
    }
    public class DrawingPhase : ResponsivePhase
    {
        public int playerID { get; set; }
        public DrawingPhase(int player)
        {
            playerID = player;
        }
        public PhaseList nextStage(Game g)
        {
            return new PhaseList(new ActionPhase(playerID));
        }

        public bool userInput(UserAction u)
        {
            throw new NotImplementedException();
        }
    }
    public class ActionPhase : ResponsivePhase
    {
        public int playerID { get; set; }
        public ActionPhase(int player)
        {
            playerID = player;
        }
        public PhaseList nextStage(Game g)
        {
            return new PhaseList(new DiscardPhase(playerID));
        }

        public bool userInput(UserAction u)
        {
            throw new NotImplementedException();
        }
    }
    public class DiscardPhase : ResponsivePhase
    {
        public int playerID { get; set; }
        public DiscardPhase(int player)
        {
            playerID = player;
        }
        public PhaseList nextStage(Game g)
        {
            return new PhaseList(new PlayerTurn((playerID+1)% g.Num_Player));
        }

        public bool userInput(UserAction u)
        {
            throw new NotImplementedException();
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
