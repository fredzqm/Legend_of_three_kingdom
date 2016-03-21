using LOTK.Model;
using LOTK.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Controller
{
    class GameController : viewController
    {
        const int NUM_OF_PLAYER = 5;

        public Game game { get; set; }
        public GameController()
        {
            game = new Game(NUM_OF_PLAYER);
        }

        public Required_Data getData(int ownPlayer)
        {
            Required_Data rd = new Required_Data();
            for(int i = 0; i < NUM_OF_PLAYER; i++)
            {
                rd.players[i].ability = game[(i + ownPlayer) % NUM_OF_PLAYER].getAbilityDescription();
                rd.players[i].name = game[(i + ownPlayer) % NUM_OF_PLAYER].getName();
            }
            rd.pool_cards = null;
            rd.hold_cards = new List<CardDisplay>(game[ownPlayer].getHoldCards().Select(c => new CardDisplay(c.getName(), c.getDescription())));
            rd.this_player_stage = game.currentStage.ToString();
            rd.tool_attack = game[ownPlayer].getWeapon();
            rd.tool_defence = game[ownPlayer].getDefense();
            return rd;
        }

        //public void applyUserResponse(UserAction userAction)
        //{
        //    if (game.userResponse(userAction))
        //    {
        //        game.nextStage();
        //    }
        //}
    }

    public interface viewController
    {
        Required_Data getData(int ownPlayer);

    }
}
