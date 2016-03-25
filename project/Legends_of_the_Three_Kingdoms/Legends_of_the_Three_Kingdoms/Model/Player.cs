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
        private int i;

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

        /// <summary>
        /// a default constructor solely for the purpose of testing
        /// </summary>
        /// <param name="i"></param>
        public Player(int i):this(i, "Player Name", "Player Description")
        {
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
                    return autoPhase(curPhase);
            }
        }

        public virtual bool autoSpecialPhase(Phase curPhase, IGame game)
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


        public virtual PhaseList playerTurn(IGame g)
        {
            return new PhaseList(new Phase(this, PhaseType.JudgePhase),
                new Phase((playerID + 1) % g.Num_Player, PhaseType.PlayerTurn));
        }

        public virtual PhaseList judgePhase(IGame g)
        {
            return new PhaseList(new Phase(this, PhaseType.DrawingPhase), new Phase(this, PhaseType.ActionPhase));
        }
        public PhaseList drawingPhase(IGame g)
        {
            return new PhaseList();
        }

        internal virtual PhaseList actionPhase(IGame g)
        {
            return new PhaseList(new Phase(this, PhaseType.DiscardPhase));
        }
        internal virtual PhaseList discardPhase(IGame g)
        {
            return new PhaseList();
        }

    }
}
