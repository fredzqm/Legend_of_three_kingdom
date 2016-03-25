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
            handCards.Add(new Card(CardSuit.Club, CardType.Attack, 0));
            weapon = new Card(CardSuit.Club, CardType.Attack, 0);
            shield = new Card(CardSuit.Club, CardType.Attack, 0);
        }

        public static implicit operator int (Player p)
        {
            return p.playerID;
        }

        // ----------------------------------------------------
        // The codes below specify the default behaviour of the player
        // Many methods can be overriden by a character class.

        /// <summary>
        /// handle this phase.
        /// It checks the type of the phases and call corresponding method, 
        /// which can be overriden
        /// </summary>
        /// <param name="curPhase"></param>
        /// <param name="game"></param>
        /// <returns>The phases following the phase,
        /// they will pushed into the phase stack in game</returns>
        public PhaseList handlePhase(Phase curPhase, IGame game)
        {
            return handlePhase(curPhase, null, game);
        }

        public PhaseList handlePhase(Phase curPhase, UserAction userAction, IGame game)
        {
            switch (curPhase.type)
            {
                case PhaseType.PlayerTurn:
                    return playerTurn(curPhase, game);
                case PhaseType.JudgePhase:
                    return judgePhase(curPhase, userAction, game);
                case PhaseType.DrawingPhase:
                    return drawingPhase(curPhase, userAction, game);
                case PhaseType.ActionPhase:
                    return actionPhase(curPhase, userAction, game);
                case PhaseType.DiscardPhase:
                    return discardPhase(curPhase, userAction, game);
                default:
                    return handleSpecialPhase(curPhase, game);
            }
        }

        public virtual PhaseList handleSpecialPhase(Phase curPhase, IGame game)
        {
            throw new NotDefinedException("This type not defined");
        }

        public bool autoPhase(Phase curPhase)
        {
            switch (curPhase.type)
            {
                case PhaseType.JudgePhase:
                case PhaseType.DrawingPhase:
                    return true;
                case PhaseType.ActionPhase:
                    return false;
                case PhaseType.DiscardPhase:
                    return false;
                default:
                    return autoSpecialPhase(curPhase);
            }
        }

        public virtual bool autoSpecialPhase(Phase curPhase)
        {
            throw new NotDefinedException("This type not defined");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="curPhase"></param>
        /// <param name="userAction"></param>
        /// <returns>true if this user action can cause the game to proceed to the next phase, otherwise it should remain in the same phase</returns>
        public bool UserInput(Phase curPhase, UserAction userAction)
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


        public virtual PhaseList playerTurn(Phase curPhase, IGame g)
        {
            return new PhaseList(new Phase(this, PhaseType.JudgePhase),
                new Phase((playerID + 1) % g.Num_Player, PhaseType.PlayerTurn));
        }

        public virtual PhaseList judgePhase(Phase curPhase, UserAction userAction, IGame g)
        {
            return new PhaseList(new Phase(this, PhaseType.DrawingPhase), new Phase(this, PhaseType.ActionPhase));
        }
        public PhaseList drawingPhase(Phase curPhase, UserAction userAction, IGame g)
        {
            return new PhaseList();
        }

        internal virtual PhaseList actionPhase(Phase curPhase, UserAction userAction, IGame g)
        {
            return new PhaseList(new Phase(this, PhaseType.DiscardPhase));
        }
        internal virtual PhaseList discardPhase(Phase curPhase, UserAction userAction, IGame g)
        {
            return new PhaseList();
        }

    }
}
