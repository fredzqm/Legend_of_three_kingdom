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
        /// <summary>
        /// function for any button
        /// </summary>
        /// <param name="playerID"></param>
        /// <param name="buttonID"></param>
        void clickButton(int playerID, int buttonID);
        /// <summary>
        /// function for card choose
        /// </summary>
        /// <param name="playerID"></param>
        /// <param name="cardID"></param>
        void clickCard(int playerID, int cardID);
        /// <summary>
        /// function for player click
        /// </summary>
        /// <param name="playerID"></param>
        /// <param name="clickedPlayerID"></param>
        void clickPlayer(int playerID, int clickedPlayerID);
    }
    /// <summary>
    /// Set Different constant to different value
    /// </summary>
    public class ButtonID
    {
        public const int OK = 1;
        public const int Cancel = 0;
        public const int Ability = 10;
    }
}
