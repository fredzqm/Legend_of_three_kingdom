using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.View
{
    public class Required_Data
    {   
        public string this_player;
        public string upleft_player;
        public string upleft_player_ab;
        public string upright_player;
        public string upright_player_ab;
        public string downleft_player;
        public string downleft_player_ab;
        public string downright_player;
        public string downright_player_ab;
        public string[] hold_cards;
        public string[] pool_cards;

        public Required_Data()
        {
            this_player = "this_player";
            upleft_player = "upleft_player";
            upright_player = "upright_player";
            downleft_player = "downleft_player";
            downright_player = "downright_player";
            upleft_player_ab = "upleft_player_ab";
            upright_player_ab = "upright_player_ab";
            downleft_player_ab = "downleft_player_ab";
            downright_player_ab = "downright_player_ab";
            hold_cards = new string[1] { "hold_cards" };
            pool_cards = new string[1] { "pool_cards" };
        }
    }
}
