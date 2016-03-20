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
        public string this_player_ab;
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
        public  string this_player_stage;
        public string tool_attack;
        public string tool_attack_ab;
        public string tool_defence;
        public string tool_defence_ab;
        public Required_Data()
        {
            tool_attack = "tool_attack";
            tool_attack_ab = "tool_attack_ab";
            tool_defence = "tool_defence";
            tool_defence_ab = "tool_defence";
            this_player_ab = "this_player_ab";
            this_player = "this_player";
            upleft_player = "upleft_player";
            upright_player = "upright_player";
            downleft_player = "downleft_player";
            downright_player = "downright_player";
            upleft_player_ab = "upleft_player_ab";
            upright_player_ab = "upright_player_ab";
            downleft_player_ab = "downleft_player_ab";
            downright_player_ab = "downright_player_ab";
            hold_cards = new string[3] { "hold_cards", "hold_cards2", "hold_cards3" };
            pool_cards = new string[3] { "pool_cards", "pool_cards2", "pool_cards3" };
            this_player_stage = "this_player_stage";
        }
    }
}
