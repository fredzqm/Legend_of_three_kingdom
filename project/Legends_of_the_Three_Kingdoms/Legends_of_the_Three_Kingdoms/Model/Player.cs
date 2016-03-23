using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOTK.View;

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
                new Phase((playerID + 1) % g.Num_Player, PhaseType.JudgePhase));
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
            throw new NotImplementedException();
        }

        internal List<Card> getHoldCards()
        {
            throw new NotImplementedException();
        }

        internal CardDisplay getWeapon()
        {
            throw new NotImplementedException();
        }

        internal CardDisplay getDefense()
        {
            throw new NotImplementedException();
        }

        internal string getAbilityDescription()
        {
            throw new NotImplementedException();
        }

        public PhaseList handlePhase(Phase curPhase, IGame IGame)
        {
            switch (curPhase.type)
            {
                case PhaseType.PlayerTurn:
                    return playerTurn(IGame);
                case PhaseType.JudgePhase:
                    return judgePhase(IGame);
                case PhaseType.DrawingPhase:
                    return drawingPhase(IGame);
                case PhaseType.ActionPhase:
                    return actionPhase(IGame);
                case PhaseType.DiscardPhase:
                    return discardPhase(IGame);
                default: throw new Exception("This type not defined");
            }
        }

        internal bool UserInput(UserAction userAction)
        {
            throw new NotImplementedException();
        }
    }
}
