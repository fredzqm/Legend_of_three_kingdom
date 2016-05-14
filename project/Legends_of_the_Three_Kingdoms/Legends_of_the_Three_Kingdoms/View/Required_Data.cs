using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.View
{
    public class Required_Data
    {
        const int NUM_OF_PLAYER = 5;
        public PlayerDisplay[] players = new PlayerDisplay[NUM_OF_PLAYER];

        public List<CardDisplay> hold_cards;
        public List<CardDisplay> pool_cards;
        public string this_player_stage;

        public CardDisplay tool_attack;
        public CardDisplay tool_defence;
        public int NumberOfCardsToClick;
        public string poolText;
        public int cardPileCount;
        public int discardPile;
        public int timeleft;

        /// <summary>
        /// Setting all the required data to default
        /// </summary>
        public Required_Data()
        {
            NumberOfCardsToClick = 0;
            tool_attack.name = Legends_of_the_Three_Kingdoms.Properties.Resources.tool_attack;
            tool_attack.ability = Legends_of_the_Three_Kingdoms.Properties.Resources.tool_attack_ab;
            tool_defence.name = Legends_of_the_Three_Kingdoms.Properties.Resources.tool_defence;
            tool_defence.ability = Legends_of_the_Three_Kingdoms.Properties.Resources.tool_defence;

            players[0].ability = Legends_of_the_Three_Kingdoms.Properties.Resources.this_player_ab;
            players[0].name = Legends_of_the_Three_Kingdoms.Properties.Resources.this_player;

            players[1].ability = Legends_of_the_Three_Kingdoms.Properties.Resources.downright_player_ab;
            players[1].name = Legends_of_the_Three_Kingdoms.Properties.Resources.downright_player;

            players[2].ability = Legends_of_the_Three_Kingdoms.Properties.Resources.upright_player_ab;
            players[2].name = Legends_of_the_Three_Kingdoms.Properties.Resources.upright_player;

            players[3].ability = Legends_of_the_Three_Kingdoms.Properties.Resources.upleft_player_ab;
            players[3].name = Legends_of_the_Three_Kingdoms.Properties.Resources.upleft_player;

            players[4].ability = Legends_of_the_Three_Kingdoms.Properties.Resources.downleft_player_ab;
            players[4].name = Legends_of_the_Three_Kingdoms.Properties.Resources.downleft_player;

            hold_cards = new List<CardDisplay>();
            hold_cards.Add(new CardDisplay(Legends_of_the_Three_Kingdoms.Properties.Resources.hold_cards, "", 1));
            hold_cards.Add(new CardDisplay(Legends_of_the_Three_Kingdoms.Properties.Resources.hold_cards, "", 2));
            hold_cards.Add(new CardDisplay(Legends_of_the_Three_Kingdoms.Properties.Resources.hold_cards, "", 3));
            hold_cards.Add(new CardDisplay(Legends_of_the_Three_Kingdoms.Properties.Resources.hold_cards, "", 4));

            pool_cards = new List<CardDisplay>();

            poolText = Legends_of_the_Three_Kingdoms.Properties.Resources.Messages;
            this_player_stage = Legends_of_the_Three_Kingdoms.Properties.Resources.this_player_stage;

            cardPileCount = 100;
            discardPile = 0;
        }
    }
    /// <summary>
    /// subclass for playerDisplay
    /// </summary>
    public struct PlayerDisplay
    {
        public string name;
        public string ability;
        private int name1;
        private object description;
        // TODO:
        public int id;
        internal int health;

        public PlayerDisplay(string name, string description, int id,int health) : this()
        {
            this.name = name;
            this.ability = description;
            this.id = id;
            this.health = health;
        }

        public PlayerDisplay(int name1, object description) : this()
        {
            this.name1 = name1;
            this.description = description;
        }
    }
    /// <summary>
    /// subclass for CardDisplay
    /// </summary>
    public struct CardDisplay
    {
        public string name;
        public string ability;
        // TODO:
        public int id;


        public CardDisplay(string n, string a, int id)
        {
            this.name = n;
            this.ability = a;
            this.id = id;
        }
    }
}
