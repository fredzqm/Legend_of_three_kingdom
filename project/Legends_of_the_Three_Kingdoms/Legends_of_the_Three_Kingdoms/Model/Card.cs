using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{
    public struct Card
    {
        private CardType type;
        private CardSuit suit;


        public Card(CardSuit s, CardType t)
        {
            this.suit = s;
            this.type = t;
        }

    }

    public class CardSet
    {
        private Card[] ls;

        public CardSet(int capacity)
        {
            ls = new Card[100];
        }
        public Card this[int i]
        {
            get
            {
                return ls[i];
            }
            set
            {
                //if (ls[i] == null)
                ls[i] = value;
                //else
                //throw new Exception("Redefien Card");
            }
        }
    }


    public enum CardSuit
    {
        Heart,
        Spade,
        Diamond,
        Club,
    }
    public enum CardType
    {
        Attack,
        Miss,
        Wine
    }
}
