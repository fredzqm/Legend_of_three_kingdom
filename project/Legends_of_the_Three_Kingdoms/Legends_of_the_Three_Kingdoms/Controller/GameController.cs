using LOTK.Model;
using LOTK.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace LOTK.Controller
{
    public delegate void UpdateForm();


    public class GameController : viewController
    {
        const int NUM_OF_PLAYER = 5;
        const int DELAY_INTERVAL = 2000;

        public GameView view { get; }
        public Game game { get; }

        public event UpdateForm updateForm;

        private System.Timers.Timer aTimer;

        public GameController()
        {
            ICollection<Card> cardset = initialLizeCardSet();
            game = new Game(NUM_OF_PLAYER, cardset);
            view =  new GameView(this, 0);
            updateForm = view.updateForm;

            // set up timer
            aTimer = new System.Timers.Timer(DELAY_INTERVAL);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            //aTimer.Stop();
            //aTimer.Dispose();
        }

        /// <summary>
        /// Initialize the cardSet with default values
        /// </summary>
        /// <returns></returns>
        private ICollection<Card> initialLizeCardSet()
        {
            ICollection<Card> ls = new List<Card>();
            //ls.Add(new Card());
            return ls;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (game.tick())
                updateForm();
        }

        public Required_Data getData(int ownPlayer)
        {
            Required_Data rd = new Required_Data();
            for(int i = 0; i < NUM_OF_PLAYER; i++)
            {
                rd.players[i] = PlayerToPlayerDisplay(game.players[(i + ownPlayer) % NUM_OF_PLAYER]);
            }
            rd.pool_cards = new List<CardDisplay>(game.players[ownPlayer].handCards.Select(c => new CardDisplay(c.getName(), c.getDescription())));
            rd.hold_cards = new List<CardDisplay>(game.players[ownPlayer].handCards.Select(c => new CardDisplay(c.getName(), c.getDescription())));
            rd.this_player_stage = game.curPhase.ToString();
            rd.tool_attack = CardToCardDisplay( game.players[ownPlayer].weapon);
            rd.tool_defence = CardToCardDisplay( game.players[ownPlayer].shield);
            return rd;
        }



        public void clickButton(int playerID, int buttonID)
        {
            switch (buttonID)
            {
                case ButtonID.OK:
                    game.nextStage(new UserAction(UserActionType.YES_OR_NO, 1));
                    break;
                case ButtonID.Cancel:
                    game.nextStage(new UserAction(UserActionType.YES_OR_NO, 0));
                    break;
                default:
                    break;
            }
            updateForm();
            
        }

        public void clickCard(int playerID, int cardID)
        {
            throw new NotImplementedException();
        }

        public void clickPlayer(int playerID, int clickedPlayerID)
        {
            throw new NotImplementedException();
        }

        // -------------------------------------------------------------------------------------
        // helper methods to convert model objects to view objects.
        private PlayerDisplay PlayerToPlayerDisplay(Player player)
        {
            return new PlayerDisplay(player.name, player.description, player.playerID);
        }

        private CardDisplay CardToCardDisplay(Card card)
        {
            return new CardDisplay(card.getName(), card.getDescription(), game.cards[card] );
        }
    }
 
}
