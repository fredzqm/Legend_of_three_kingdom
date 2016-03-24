using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOTK.View;
using Legends_of_the_Three_Kingdoms.Model;

namespace LOTK.Model
{
    public class Player
    {
        public int playerID { get; set; }
        public Player(int pos)
        {
            playerID = pos;
        }

        public static implicit operator int (Player p)
        {
            return p.playerID;
        }

        public PhaseList playerTurn(IGame g)
        {
            return new PhaseList(new Phase(this, PhaseType.JudgePhase),
                new Phase((playerID + 1) % g.Num_Player, PhaseType.PlayerTurn));
        }

        public PhaseList judgePhase(IGame g)
        {
            return new PhaseList(new Phase(this, PhaseType.DrawingPhase), new Phase(this, PhaseType.ActionPhase));
        }
        public PhaseList drawingPhase(IGame g)
        {
            return new PhaseList();
        }

        internal PhaseList actionPhase(IGame g)
        {
            return new PhaseList(new Phase(this, PhaseType.DiscardPhase));
        }
        internal PhaseList discardPhase(IGame g)
        {
            return new PhaseList();
        }

        internal string getName()
        {
            return "Player Name";
        }

        internal List<Card> getHoldCards()
        {
            List<Card> ls = new List<Card>();
            ls.Add(new Card(CardSuit.Club, CardType.Attack, 0));
            return ls;
        }

        internal Card getWeapon()
        {
            return new Card(CardSuit.Club, CardType.Attack, 0);
        }

        internal Card getDefense()
        {
            return new Card(CardSuit.Club, CardType.Attack, 0);
        }

        internal string getAbilityDescription()
        {
            return "Ability Description";
        }

        public PhaseList handlePhase(Phase curPhase, IGame game)
        {
            switch (curPhase.type)
            {
                case PhaseType.PlayerTurn:
                    return playerTurn(game);
                case PhaseType.JudgePhase:
                    return judgePhase(game);
                case PhaseType.DrawingPhase:
                    return drawingPhase(game);
                case PhaseType.ActionPhase:
                    return actionPhase(game);
                case PhaseType.DiscardPhase:
                    return discardPhase(game);
                default: throw new NotDefinedException("This type not defined");
            }
        }

        internal bool UserInput(Phase curPhase, UserAction userAction)
        {
            if (!curPhase.needResponse())
                return true;
            switch (curPhase.type)
            {
                case PhaseType.JudgePhase:
                case PhaseType.DrawingPhase:
                    return true;
                case PhaseType.ActionPhase:
                    if (userAction.type == UserActionType.YES_OR_NO)
                    {
                        return (userAction.detail == 0);
                    }
                    return false;
                case PhaseType.DiscardPhase:
                    if (userAction.type == UserActionType.YES_OR_NO)
                    {
                        return true;
                    }
                    return false;
                default:
                    return false;
            }
        }
    }
}
