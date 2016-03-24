using LOTK.Model;
using LOTK.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LOTK.Controller
{
    class GameController : viewController
    {
        const int NUM_OF_PLAYER = 5;
        public GameView view { get; }
        public Game game { get; }

        public GameController()
        {
            ICollection<Card> cardset = initialLizeCardSet();
            game = new Game(NUM_OF_PLAYER, cardset);
            view =  new GameView(this, 0);
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
            rd.pool_cards = new List<CardDisplay>(game.players[ownPlayer].getHoldCards().Select(c => new CardDisplay(c.getName(), c.getDescription())));
            rd.hold_cards = new List<CardDisplay>(game.players[ownPlayer].getHoldCards().Select(c => new CardDisplay(c.getName(), c.getDescription())));
            rd.this_player_stage = game.currentStage.ToString();
            rd.tool_attack = CardToCardDisplay( game.players[ownPlayer].getWeapon());
            rd.tool_defence = CardToCardDisplay( game.players[ownPlayer].getDefense());
            return rd;
        }

        private CardDisplay CardToCardDisplay(Card card)
        {
            return new CardDisplay(card.getName(), card.getDescription());
        }

        public void clickButton(int playerID, int buttonID)
        {
            switch (buttonID)
            {
                case ButtonID.OK:
                    game.userResponse(new UserAction(UserActionType.YES_OR_NO, 1));
                    break;
                case ButtonID.Cancel:
                    game.userResponse(new UserAction(UserActionType.YES_OR_NO, 0));
                    break;
                default:
                    break;
            }
            view.updateForm();
        }

        public void clickCard(int playerID, int cardID)
        {
            throw new NotImplementedException();
        }

        public void clickPlayer(int playerID, int clickedPlayerID)
        {
            throw new NotImplementedException();
        }
    }

 
}
