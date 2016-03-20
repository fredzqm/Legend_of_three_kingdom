using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.View
{

    public delegate void clickButton(int playerID, int buttonID);

    public delegate void clickCard(int playerID, int cardID); // maybe later changed to a card struct

    public delegate void clickPlayer(int playerID, int clickedPlayerID);

    class View
    {
        // Declare the delegate (if using non-generic pattern).        
        // Declare the event.
        public event clickButton SampleEvent;

        public View()
        {
            SampleEvent(1, 2);
        }
    }
}
