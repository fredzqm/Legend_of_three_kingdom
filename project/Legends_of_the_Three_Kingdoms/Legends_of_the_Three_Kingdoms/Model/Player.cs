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

        public static implicit operator int(Player p)
        {
            return p.playerID;
        }

        public PhaseList playerTurn(IGame g)
        {
            return new PhaseList(new JudgePhase(playerID),
                new PlayerTurn((playerID + 1) % g.Num_Player));
        }

        public PhaseList judgePhase(IGame g)
        {
            return new PhaseList(new DrawingPhase(this), new ActionPhase(this));
        }
        public PhaseList drawingPhase(IGame g)
        {
            return new PhaseList();
        }

        internal PhaseList actionPhase(IGame g)
        {
            return new PhaseList(new DiscardPhase(this));
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
            if (curPhase is PlayerTurn)
            {
                return playerTurn(IGame);
            }else if (curPhase is JudgePhase)
            {
                return judgePhase(IGame);
            }else if (curPhase is DrawingPhase)
            {
                return drawingPhase(IGame);
            }
            else if (curPhase is ActionPhase)
            {
                return actionPhase(IGame);
            }
            else if (curPhase is DiscardPhase)
            {
                return discardPhase(IGame);
            }
            throw new Exception("No such phase defined!");
        }

        internal PhaseList discardPhase(IGame g)
        {
            return new PhaseList();
        }
    }
}
