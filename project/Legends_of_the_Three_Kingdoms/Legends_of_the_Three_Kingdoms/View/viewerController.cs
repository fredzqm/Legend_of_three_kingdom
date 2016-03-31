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
        int Num_Of_Player { get; }
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
        public const int UpperLeft = 2111;
        public const int LoweRight = 1112;
        public const int LowerLeft = 1121;
        public const int UpperRight = 1211;
        public const int ThisPlayer = 2222;


    }
}
