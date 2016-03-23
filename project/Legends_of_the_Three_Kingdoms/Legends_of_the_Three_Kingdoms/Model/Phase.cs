﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{
    public class Phase
    {
        public virtual PhaseList nextStage(Game g)
        {
            return new PhaseList();
        }
        public Phase(int playerId)
        {
            playerID = playerId;
        }
        public int playerID { get; }
    }

  
    public class ResponsivePhase : Phase
    {
        public ResponsivePhase(int playerID) : base(playerID)
        {

        }

        public virtual bool userInput(UserAction u)
        {
            return true;
        }
    }
    
    public class PlayerTurn : Phase
    {
        public PlayerTurn(int playerID) : base(playerID)
        {

        }

        //public PhaseList nextStage(Game g)
        //{
        //    return new PhaseList(new JudgePhase(player), 
        //        new PlayerTurn(g.players[(player + 1) % g.Num_Player]));
        //}

    }

    public class JudgePhase : ResponsivePhase
    {
        public JudgePhase(int playerID) : base(playerID)
        {

        }

        //public PhaseList nextStage(Game g)
        //{
        //    return player.judgePhase(g);
        //}

        public bool userInput(UserAction u)
        {
            return false;
        }
    }
    public class DrawingPhase : ResponsivePhase
    {
        public DrawingPhase(int playerID) : base(playerID)
        {

        }
        //public PhaseList nextStage(Game g)
        //{
        //    return player.drawingPhase(g);
        //}

        //public bool userInput(UserAction u)
        //{
        //    return false;
        //}
    }
    public class ActionPhase : ResponsivePhase
    {
        public ActionPhase(int playerID) : base(playerID)
        {

        }
        //public PhaseList nextStage(Game g)
        //{
        //    return player.actionPhase(g);
        //}

        //public bool userInput(UserAction u)
        //{
        //    switch (u.type)
        //    {
        //        case UserActionType.YES_OR_NO: return u.detail == UserAction.YES;
        //        default: return false;
        //    }
        //}
    }
    public class DiscardPhase : ResponsivePhase
    {
        public DiscardPhase(int playerID) : base(playerID)
        {

        }

        //public PhaseList nextStage(Game g)
        //{
        //    return player.discardPhase(g);
        //}

        //public bool userInput(UserAction u)
        //{  
        //    switch (u.type)
        //    {
        //        case UserActionType.YES_OR_NO: return u.detail == UserAction.YES;
        //        default: return false;
        //    }
        //}
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
