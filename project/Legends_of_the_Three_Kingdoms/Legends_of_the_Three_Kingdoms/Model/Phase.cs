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
        Player playerID { get; }
    }

    public interface ResponsivePhase : Phase
    {
        bool userInput(UserAction u);
    }
    
    public class PlayerTurn : Phase
    {
        public Player playerID { get; set; }

        public PlayerTurn(Player player)
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
        public Player playerID { get; set; }
        public JudgePhase(Player player)
        {
            playerID = player;
        }
        public PhaseList nextStage(Game g)
        {
            return new PhaseList(new DrawingPhase(playerID));
        }

        public bool userInput(UserAction u)
        {
            return false;
        }
    }
    public class DrawingPhase : ResponsivePhase
    {
        public Player playerID { get; set; }
        public DrawingPhase(Player player)
        {
            playerID = player;
        }
        public PhaseList nextStage(Game g)
        {
            return new PhaseList(new ActionPhase(playerID));
        }

        public bool userInput(UserAction u)
        {
            return false;
        }
    }
    public class ActionPhase : ResponsivePhase
    {
        public Player playerID { get; set; }
        public ActionPhase(Player player)
        {
            playerID = player;
        }
        public PhaseList nextStage(Game g)
        {
            return new PhaseList(new DiscardPhase(playerID));
        }

        public bool userInput(UserAction u)
        {
            switch (u.type)
            {
                case UserActionType.YES_OR_NO: return u.detail == UserAction.YES;
                default: return false;
            }
        }
    }
    public class DiscardPhase : ResponsivePhase
    {
        public Player playerID { get; set; }
        public DiscardPhase(Player player)
        {
            playerID = player;
        }
        public PhaseList nextStage(Game g)
        {
            return new PhaseList(new PlayerTurn(g.nextPlayer(playerID)));
        }

        public bool userInput(UserAction u)
        {
            switch (u.type)
            {
                case UserActionType.YES_OR_NO: return u.detail == UserAction.YES;
                default: return false;
            }
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
