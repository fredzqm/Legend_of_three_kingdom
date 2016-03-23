using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.View
{

    public interface viewController
    {
        Required_Data getData(int ownPlayer);
        void clickButton(int playerID, ButtonID buttonID);
        void clickCard(int playerID, int cardID);
        void clickPlayer(int playerID, int clickedPlayerID);
    }
    
    enum ButtonID
    {
        OK,
        Cancel,
        Ability1,
        Ability2
    }
}
