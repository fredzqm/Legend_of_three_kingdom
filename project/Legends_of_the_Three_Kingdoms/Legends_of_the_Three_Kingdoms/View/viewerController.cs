using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LOTK.View
{

    public interface viewController
    {
        Required_Data getData(int ownPlayer);
        void clickButton(int playerID, int buttonID);
        void clickCard(int playerID, int cardID);
        void clickPlayer(int playerID, int clickedPlayerID);
    }

    public class ButtonID
    {
        public const int OK = 1;
        public const int Cancel = 0;
        public const int Ability = 10;
    }
}
