using LOTK.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOTK.Model;

namespace LOTK.Controller
{
    public class UserActionHandler
    {

        public int ClickUser = -100;
        public int SelectCardId = -100;
        public int Ifabi = -10;

        private IGame game;

        public UserActionHandler(IGame game)
        {
            this.game = game;
        }

        /// <summary>
        /// Called after user click a card
        /// </summary>
        /// <param name="playerID"></param>
        /// <param name="cardID"></param>
        public void clickCard(int playerID, int cardID)
        {
            SelectCardId = cardID;
        }
        /// <summary>
        /// called after user click a player 
        /// </summary>
        /// <param name="playerID"></param>
        /// <param name="clickedPlayerID"></param>
        public void clickPlayer(int playerID, int clickedPlayerID)
        {
            ClickUser = clickedPlayerID;
        }

        public UserAction clickCancel(int playerID)
        {
            SelectCardId = -1;
            ClickUser = -1;
            return new YesOrNoAction(false);
        }

        public UserAction clickOK(int playerID)
        {
            if (Ifabi == 1 && ClickUser >= 0)
            {
                if (SelectCardId < 0)
                    throw new InvalidOperationException("Ability of Liu is given no card");
                return new AbilityAction(game, SelectCardId, ClickUser);
            }
            else if (Ifabi == 1 && ClickUser < 0)
            {
                if (SelectCardId < 0)
                    throw new InvalidOperationException("Ability of Sun is given no card");
                return new AbilityActionSun(game, SelectCardId);
            }
            else if (SelectCardId < 0 && ClickUser < 0)
            {
                return new YesOrNoAction(true);
            }
            else if (ClickUser < 0 && Ifabi < 0)
            {
                return new CardAction(game, SelectCardId);
            }
            else if (ClickUser >= 0 && SelectCardId >= 0)
            {
                return new UseCardAction(game, SelectCardId, ClickUser);
            }else
            {
                throw new InvalidOperationException("Invalid operation");
            }
        }

        public void clickAbility(int buttonID)
        {
            Ifabi = 1;
        }
    }
}
