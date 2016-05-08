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
        int cardPileCount { get; }
        int discardPileCount { get; }

        /// <summary>
        /// Known get the card instance with cardID
        /// This should always be true
        /// </summary>
        /// <param name="i">cardID</param>
        /// <returns>the corresponding Card instance</returns>
        Card this[int i] { get; }

        /// <summary>
        /// </summary>
        /// <param name="c">Card instance</param>
        /// <returns>CardID</returns>
        int this[Card c] { get; }

        /// <summary>
        /// pop the top card on the cardpile
        /// 
        /// </summary>
        /// <exception cref="NoCardException">the list has no more card</exception>
        /// <returns>The top card</returns>
        Card drawOne();

        /// <summary>
        /// discard one card to the discard card pile
        /// </summary>
        /// <exception cref="NoCardException">This exception is thrown when this card does not belong to this cardset</exception>
        /// <param name="card"></param>
        void discardOne(Card card);

        /// <summary>
        /// This method pops n cards from the card pile. If the card pile is empty, 
        /// it automatically shuffles the discard card pile, and draw the rest there
        /// 
        /// </summary>
        /// <exception cref="NoCardException"><seealso cref="PhaseList.pop"/></exception>
        /// <param name="num">Number of cards</param>
        /// <returns>the card drown</returns>
        IEnumerable<Card> drawCard(int num);

        /// <summary>
        /// This method discard cards into the discard card pile
        /// </summary>
        /// <param name="">the cards to be discarded</param>
        void discardCards(IEnumerable<Card> cards);
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

        public int cardPileCount {get{return cardPile.Count;}}
        public int discardPileCount { get { return discardPile.Count; } }

        /// <summary>
        /// create a cardset given an list of cards
        /// </summary>
        /// <param name="cardLs">A collection for all cards</param>
        public CardSet(ICollection<Card> cardLs)
        {
            if (cardLs == null)
                throw new NullReferenceException("Card collection for a carset cannot be null");
            this.cardLs = new Card[cardLs.Count];
            cardIDs = new Dictionary<Card, int>();
            IEnumerator<Card> itr = cardLs.GetEnumerator();
            for (int i = 0; i < cardLs.Count; i++)
            {
                itr.MoveNext();
                this.cardLs[i] = itr.Current;
                cardIDs[this.cardLs[i]] = i;
            }
            cardPile = new LinkedList<Card>(this.cardLs.OrderBy(a => Guid.NewGuid()));
            discardPile = new LinkedList<Card>();
        }

        public Card this[int i]
        {
            get
            {
                return cardLs[i];
            }
        }

        public int this[Card c]
        {
            get
            {
                return cardIDs[c];
            }
        }

       public Card drawOne()
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

        public void discardOne(Card c)
        {
            if (!cardIDs.ContainsKey(c))
                throw new NoCardException("Such Card Cannot be Found");
            discardPile.AddFirst(c);
        }


        public IEnumerable<Card> drawCard(int num)
        {
            List<Card> cards = new List<Card>();
            try
            {
                for (int i = 0; i < num; i++)
                    cards.Add(drawOne());
            }
            catch (NoCardException e)
            {
                throw new NoCardException("The card stack is empty", e);
            }
            return cards;
        }

        public void discardCards(IEnumerable<Card> cards)
        {
            foreach (Card c in cards)
                discardOne(c);
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
       public class Node
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
            public Node setNext(Node node)
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
