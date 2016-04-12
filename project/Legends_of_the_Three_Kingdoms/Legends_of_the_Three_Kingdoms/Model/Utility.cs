using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{

    public interface ICardSet
    {
        Card this[int i] { get; }
        int this[Card c] { get; }

        Card pop();
        void discard(Card card);
    }

    /// <summary>
    /// CardSet is literally what it means. It represents the cardpile for this game
    /// The user can pop() the top of the cardStack, discard() card back to the cardpile.
    /// When the cardpile is empty, it will automatically shuffle the discarded card.
    /// 
    /// Each Card is associated with an ID, which can be get with get 
    /// </summary>
    public class CardSet : ICardSet
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
            cardPile = new LinkedList<Card>(cardLs.OrderBy(a => Guid.NewGuid()));
            discardPile = new LinkedList<Card>();
        }

        /// <summary>
        /// Known get the card instance with cardID
        /// This should always be true
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

        /// <summary>
        /// pop the top card on the cardpile
        /// 
        /// </summary>
        /// <exception cref="NoCardException">the list has no more card</exception>
        /// <returns>The top card</returns>
        public Card pop()
        {
            if (cardPile.Count == 0)
            {
                cardPile = new LinkedList<Card>(discardPile.OrderBy(a => Guid.NewGuid()));
                discardPile = new LinkedList<Card>();
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
        /// <summary>
        /// create phase list
        /// </summary>
        public PhaseList()
        {
            head = null;
            tail = null;

        }/// <summary>
        /// check if the list is empty
        /// </summary>
        /// <returns></returns>
        public bool isEmpty()
        {
            return head == null;
        }
        /// <summary>
        /// add phase 
        /// </summary>
        /// <param name="phases"></param>
        public PhaseList(params Phase[] phases) : this()
        {
            foreach (Phase p in phases)
            {
                add(p);
            }
        }
        /// <summary>
        /// add phase to end 
        /// </summary>
        /// <param name="s"></param>
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
        /// <summary>
        /// push phase to head
        /// </summary>
        /// <param name="s"></param>
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
        /// <summary>
        /// pop phase 
        /// </summary>
        /// <returns></returns>
        public Phase pop()

        {
            if (isEmpty())
            {
                throw new EmptyException("Empty PhaseList Exception");
            }
            Phase retStage = head.data;
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
            if (isEmpty())
            {
                head = added.head;
                tail = added.tail;
            }
            else if (added.isEmpty()) { }
            else
            {
                added.tail.next = head;
                head = added.head;
            }
        }
        /// <summary>
        /// top of the phase list
        /// </summary>
        /// <returns></returns>
        public Phase top()
        {
            if (isEmpty())
                throw new EmptyException("Phaselist is empty");
            return head.data;
        }
        /// <summary>
        /// get enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Phase> GetEnumerator()
        {
            return new PhaseListEnumerator(this);
        }
        /// <summary>
        /// Ienumerator 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new PhaseListEnumerator(this);
        }
        /// <summary>
        /// enumerable for phase list
        /// </summary>
        class PhaseListEnumerator : IEnumerator<Phase>
        {
            private PhaseList phaseList;
            private Node curNode;
            public PhaseListEnumerator(PhaseList phaseList)
            {
                this.phaseList = phaseList;
                Reset();
            }
            /// <summary>
            /// get current
            /// </summary>
            public Phase Current
            {
                get
                {
                    if (curNode == null)
                        return null;
                    return curNode.data;
                }
            }
            /// <summary>
            /// get current
            /// </summary>
            object IEnumerator.Current { get { return Current; } }
            /// <summary>
            /// dispose 
            /// </summary>
            public void Dispose()
            {
            }
            /// <summary>
            /// move to next
            /// </summary>
            /// <returns></returns>
            public bool MoveNext()
            {
                if (curNode == null)
                    curNode = phaseList.head;
                else
                    curNode = curNode.next;
                return (curNode != null);
            }
            /// <summary>
            /// reset
            /// </summary>
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

        /// <summary>
        /// class of node 
        /// </summary>
        class Node
        {
            internal Phase data;
            internal Node next;
            /// <summary>
            /// create node 
            /// </summary>
            /// <param name="s"></param>
            public Node(Phase s)
            {
                this.data = s;
                this.next = null;
            }
            /// <summary>
            /// set next to this node 
            /// </summary>
            /// <param name="node"></param>
            /// <returns></returns>
            internal Node setNext(Node node)
            {
                this.next = node;
                return node;
            }

            public override string ToString()
            {
                return data.ToString();
            }
        }
    }

}
