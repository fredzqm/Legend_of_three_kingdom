using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{
    /// <summary>
    /// CardSet is literally what it means. It represents the cardpile for this game
    /// The user can pop() the top of the cardStack, discard() card back to the cardpile.
    /// When the cardpile is empty, it will automatically shuffle the discarded card.
    /// 
    /// Each Card is associated with an ID, which can be get with get 
    /// </summary>
    public class CardSet
    {
        private readonly Card[] cardLs;
        private Dictionary<Card, int> cardIDs;
        private LinkedList<Card> cardPile;
        private LinkedList<Card> discardPile;

        /// <summary>
        /// create a cardset given an list of cards
        /// </summary>
        /// <param name="cls">A collection for all cards</param>
        public CardSet(ICollection<Card> cls)
        {
            cardLs = new Card[cls.Count];
            cardIDs = new Dictionary<Card, int>();
            IEnumerator<Card> itr = cls.GetEnumerator();
            for (int i = 0; i < cls.Count; i++)
            {
                itr.MoveNext();
                cardLs[i] = itr.Current;
                cardIDs[cardLs[i]] = i;
            }
            cardPile = new LinkedList<Card>(cardLs);
            discardPile = new LinkedList<Card>();
            cardPile.OrderBy(a => Guid.NewGuid());
        }

        /// <summary>
        /// Known get the card instance with cardID
        /// This should always be true
        /// <seealso cref="CardSet.getCardID(Card)">
        /// </summary>
        /// <param name="i">cardID</param>
        /// <returns>the corresponding Card instance</returns>
        public Card this[int i]
        {
            get
            {
                return cardLs[i];
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="c">Card instance</param>
        /// <returns>CardID</returns>
        public int this[Card c]
        {
            get
            {
                return cardIDs[c];
            }
        }

        public int getCardID(Card a)
        {
            return cardIDs[a];
        }

        /// <summary>
        /// pop the top card on the cardpile
        /// </summary>
        /// <returns>The top card</returns>
        public Card pop()
        {
            if (cardPile.Count == 0)
            {
                cardPile = discardPile;
                discardPile = new LinkedList<Card>();
                cardPile.OrderBy(a => Guid.NewGuid());
                if (cardPile.Count == 0)
                    throw new NoCardException("Run out of cards");
            }
            Card ret = cardPile.First();
            cardPile.RemoveFirst();
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c">The card Discarded</param>
        public void discard(Card c)
        {
            if (!cardIDs.ContainsKey(c))
                throw new NoCardException("Such Card Cannot be Found");
            discardPile.AddFirst(c);
        }


    }


    /// <summary>
    /// A simple data structure (linkedList) that used to store phases of game
    /// </summary>
    public class PhaseList : IEnumerable<Phase>
    {
        private Node head;
        private Node tail;

        public PhaseList()
        {
            head = null;
            tail = null;

        }
        public bool isEmpty()
        {
            return head == null;
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

        public void push(Phase s)
        {
            if (head == null)
            {
                head = new Node(s);
                tail = head;
            }
            else
            {
                Node added = new Node(s);
                added.next = head;
                head = added;
            }
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
        public void pushList(PhaseList added)
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

        public IEnumerator<Phase> GetEnumerator()
        {
            return new PhaseListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new PhaseListEnumerator(this);
        }

        class PhaseListEnumerator : IEnumerator<Phase>
        {
            private PhaseList phaseList;
            private Node curNode;
            public PhaseListEnumerator(PhaseList phaseList)
            {
                this.phaseList = phaseList;
                Reset();
            }

            public Phase Current
            {
                get
                {
                    if (curNode == null)
                        return null;
                    return curNode.stage;
                }
            }

            object IEnumerator.Current { get { return Current; } }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (curNode == null)
                    curNode = phaseList.head;
                else
                    curNode = curNode.next;
                return (curNode != null);
            }

            public void Reset()
            {
                curNode = null;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (Phase p in this)
            {
                sb.Append(p.ToString());
                sb.Append(", ");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return sb.ToString();
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

            public override string ToString()
            {
                return stage.ToString();
            }
        }
    }

}
