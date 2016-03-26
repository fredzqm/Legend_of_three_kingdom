using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOTK.View;
using Legends_of_the_Three_Kingdoms.Model;

namespace LOTK.Model
{
    /// <summary>
    /// The model that represents the default behaviour the player
    /// 
    /// </summary>
    public class Player
    {
        // basic and readonly properties, initialized in contructor
        public int playerID { get; }
        public string name { get; }
        public string description { get; }

        public List<Card> handCards { get; }
        public Card weapon { get; set; }
        public Card shield { get; set; }

        public Player(int pos, string name, string descript)
        {
            playerID = pos;
            handCards = new List<Card>();
            // just for testing purpose
            this.name = name;
            this.description = descript;
            handCards.Add(Card.ConstructCard(CardSuit.Club, CardType.Attack, 0));
            weapon = Card.ConstructCard(CardSuit.Club, CardType.Attack, 0);
            shield = Card.ConstructCard(CardSuit.Club, CardType.Attack, 0);
        }

        public static implicit operator int (Player p)
        {
            return p.playerID;
        }

        // ----------------------------------------------------
        // The codes below specify the default behaviour of the player
        // Many methods can be overriden by a character class.

        public virtual PhaseList playerTurn(PlayerTurn curPhase, IGame g)
        {
            return new PhaseList(new JudgePhase(this), new PlayerTurn((this + 1)%g.Num_Player));
        }

        public virtual PhaseList judgePhase(JudgePhase curPhase, UserAction userAction, IGame g)
        {
            return new PhaseList(new DrawingPhase(this), new ActionPhase(this));
        }

        public PhaseList drawingPhase(DrawingPhase curPhase, UserAction userAction, IGame g)
        {
            return new PhaseList();
        }

        public virtual PhaseList actionPhase(ActionPhase curPhase, UserAction userAction, IGame g)
        {
            if (userAction == null)
                return null;
            switch (userAction.type)
            {
                case UserActionType.YES_OR_NO:
                    if ((userAction as YesOrNoAction).no)
                        return new PhaseList(new DiscardPhase(this));
                    else
                        return null;
                case UserActionType.CARD:
                    UseCardAction a = userAction as UseCardAction;
                    Card c = a.card;
                    if (c.numOfTargets() != a.targetCount)
                        return null;
                    return new PhaseList(new UsagePhase(this, c, a.targets), curPhase);
                default:
                    return null;
            }
        }
        public virtual PhaseList discardPhase(DiscardPhase curPhase, UserAction userAction, IGame g)
        {
            if (userAction == null)
                return null;
            return new PhaseList();
        }

    }
}
