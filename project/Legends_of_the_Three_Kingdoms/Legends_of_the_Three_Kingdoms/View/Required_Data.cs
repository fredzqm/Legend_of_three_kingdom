using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.View
{
    public class Required_Data
    {
        public PlayerDisplay[] players = new PlayerDisplay[5];

        public List<CardDisplay> hold_cards;
        public List<CardDisplay> pool_cards;
        public string this_player_stage;

        public CardDisplay tool_attack;
        public CardDisplay tool_defence;
        /// <summary>
        /// Setting all the required data to default
        /// </summary>
        public Required_Data()
        {
            tool_attack.name = "tool_attack";
            tool_attack.ability = "tool_attack_ab";
            tool_defence.name = "tool_defence";
            tool_defence.ability = "tool_defence";

            players[0].ability = "this_player_ab";
            players[0].name = "this_player";

            players[1].ability = "downright_player_ab";
            players[1].name = "downright_player";

            players[2].ability = "upright_player_ab";
            players[2].name = "upright_player";

            players[3].ability = "upleft_player_ab";
            players[3].name = "upleft_player";

            players[4].ability = "downleft_player_ab";
            players[4].name = "downleft_player";

            hold_cards = new List<CardDisplay>();
            hold_cards.Add(new CardDisplay("hold_cards", ""));
            hold_cards.Add(new CardDisplay("hold_cards", ""));
            hold_cards.Add(new CardDisplay("hold_cards", ""));
            hold_cards.Add(new CardDisplay("hold_cards", ""));

            pool_cards = new List<CardDisplay>();

            this_player_stage = "this_player_stage";
        }
    }
    /// <summary>
    /// subclass for playerDisplay
    /// </summary>
        public struct PlayerDisplay{
            public string name;
            public string ability;
        }
    /// <summary>
    /// subclass for CardDisplay
    /// </summary>
        public struct CardDisplay
        {
            public string name;
            public string ability;

            public CardDisplay(string n, string a)
            {
                this.name = n;
                this.ability = a;
            }
        }
}
