using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOTK.View;

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
            return new PhaseList(new JudgePhase(this), new PlayerTurn(g.players[(this + 1)%g.Num_Player]));
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
            
        }


        public virtual PhaseList discardPhase(DiscardPhase curPhase, UserAction userAction, IGame g)
        {
            if (userAction == null)
                return null;
            return new PhaseList();
        }

        public virtual int numOfTargets(Card card, ActionPhase curPhase)
        {
            return card.numOfTargets();
        }

        public virtual PhaseList attack(Attack card, Player[] targets, ActionPhase actionPhase)
        {
            if (targets.Length > card.numOfTargets() || actionPhase.attackCount > 1 || targets[0] == this)
                return new PhaseList();
            int harm = 1;
            if (actionPhase.drunk)
                harm++;
            return new PhaseList(new ReponsePhase(targets[0], CardType.Miss, new HarmPhase(targets[0], this, harm)));
        }

        internal PhaseList attack(Player[] targets)
        {
            throw new NotImplementedException();
        }

        internal PhaseList useWine()
        {
            throw new NotImplementedException();
        }

        internal PhaseList useWine(Card card)
        {
            throw new NotImplementedException();
        }

        internal PhaseList usePeach(Card card)
        {
            throw new NotImplementedException();
        }


        internal PhaseList useAOE(Card card)
        {
            throw new NotImplementedException();
        }

        internal PhaseList useHailofArrow(Card card)
        {
            throw new NotImplementedException();
        }

        internal PhaseList usePeachGarden(Card card)
        {
            throw new NotImplementedException();
        }
    }
}
