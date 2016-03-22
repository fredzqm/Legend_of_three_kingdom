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

        public PhaseList judgePhase(Game g)
        {
            return new PhaseList(new DrawingPhase(this), new ActionPhase(this));
        }
        public PhaseList drawingPhase(Game g)
        {
            return new PhaseList();
        }

        internal PhaseList actionPhase(Game g)
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

        internal PhaseList handlePhase(Phase currentPhase)
        {
            throw new NotImplementedException();
        }

        internal PhaseList discardPhase(Game g)
        {
            return new PhaseList();
        }
    }
}
