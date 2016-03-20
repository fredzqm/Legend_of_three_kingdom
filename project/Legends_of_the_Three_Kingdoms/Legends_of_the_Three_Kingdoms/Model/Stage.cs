using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{
    public interface Stage
    {
        StageList process(Game g);
        int playerID { get; }
    }

    public class StageList
    {
        private Node head;
        private Node tail;

        public StageList()
        {
            head = null;
            tail = null;
        }

        public void add(Stage s)
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

        public Stage pop()
        {
            if (head == null)
            {
                throw new Exception("Empty StageList Exception");
            }
            Stage retStage = head.stage;
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
        public void pushStageList(StageList added)
        {
            if (added.head == null)
                return;
            added.tail.next = head;
            head = added.head;
        }
        class Node
        {
            internal Stage stage;
            internal Node next;

            public Node(Stage s)
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

    public class PlayerTurn : Stage
    {
        public int playerID { get; set; }

        public PlayerTurn(int player)
        {
            playerID = player;
        }

        public StageList process(Game g)
        {
            throw new NotImplementedException();
        }
    }

}
