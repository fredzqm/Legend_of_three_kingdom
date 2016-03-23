using LOTK.Model;
using LOTK.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Controller
{
    public interface viewController
    {
        Required_Data getData(int ownPlayer);

    }


    class GameController : viewController
    {
        const int NUM_OF_PLAYER = 5;

        public Game game { get; set; }
        public GameController()
        {
            ICollection<Card> cardset = initialLizeCardSet();
            game = new Game(NUM_OF_PLAYER, cardset);
        }

        private ICollection<Card> initialLizeCardSet()
        {
            ICollection<Card> ls = new List<Card>();
            //ls.Add(new Card());
            return ls;
        }

        public Required_Data getData(int ownPlayer)
        {
            Required_Data rd = new Required_Data();
            for(int i = 0; i < NUM_OF_PLAYER; i++)
            {
                rd.players[i].ability = game.players[(i + ownPlayer) % NUM_OF_PLAYER].getAbilityDescription();
                rd.players[i].name = game.players[(i + ownPlayer) % NUM_OF_PLAYER].getName();
            }
            rd.pool_cards = null;
            rd.hold_cards = new List<CardDisplay>(game.players[ownPlayer].getHoldCards().Select(c => new CardDisplay(c.getName(), c.getDescription())));
            rd.this_player_stage = game.currentStage.ToString();
            rd.tool_attack = game.players[ownPlayer].getWeapon();
            rd.tool_defence = game.players[ownPlayer].getDefense();
            return rd;
        }

        public void applyUserResponse(UserAction userAction)
        {
            if (game.userResponse(userAction))
            {
                game.nextStage();
            }
        }
    }

 
}
