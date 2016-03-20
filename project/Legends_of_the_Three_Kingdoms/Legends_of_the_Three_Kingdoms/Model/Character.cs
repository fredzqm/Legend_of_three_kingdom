using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
