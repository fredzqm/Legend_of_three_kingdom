using LOTK.Model;
using LOTK.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
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
        public int ClickUser = -100;
        public int SelectCardId = -100;

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
            //ls.Add(Card.ConstructCard());
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
            rd.pool_cards = new List<CardDisplay>(game.players[ownPlayer].handCards.Select(c => CardToCardDisplay(c)));
            rd.hold_cards = new List<CardDisplay>(game.players[ownPlayer].handCards.Select(c => CardToCardDisplay(c)));
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
                    //   game.nextStage(new UserActionYesOrNo(true));
                    if (SelectCardId < 0)
                    {
                        YesOrNoAction e = new YesOrNoAction(true);
                        game.nextStage(e);

                    }
                    else if (ClickUser < 0)
                    {
                        CardAction e = new CardAction(SelectCardId, game);
                        game.nextStage(e);
                    }
                    else if (ClickUser > 0 && SelectCardId > 0)
                    {
                        // UserActionPlayer e =new UserActionPlayer
                        UseCardAction e = new UseCardAction(SelectCardId, ClickUser,game);
                        game.nextStage(e);
                    }
                    break;
                case ButtonID.Cancel:
                   // game.nextStage(new UserActionYesOrNo(false));
                    break;
                case ButtonID.Ability:
                    //do nothing right
                    break;
                case ButtonID.LoweRight:
                    ClickUser = (playerID+1)%game.Num_Player;
                    break;
                case ButtonID.LowerLeft:
                    ClickUser = (playerID + 4) % game.Num_Player;
                    break;
                case ButtonID.UpperLeft:
                    ClickUser = (playerID + 3) % game.Num_Player;
                    break;
                case ButtonID.UpperRight:
                    ClickUser = (playerID + 2) % game.Num_Player;
                    break;
                case ButtonID.ThisPlayer:
                    ClickUser = playerID;
                    break;
                
                default:
                    break;
            }
            updateForm();
            
        }

        public void clickCard(int playerID, int cardID)
        {
            SelectCardId = cardID;
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
            return new CardDisplay(card.ToString(), card.getDescription(), game.cards[card] );
        }
    }
 
}


