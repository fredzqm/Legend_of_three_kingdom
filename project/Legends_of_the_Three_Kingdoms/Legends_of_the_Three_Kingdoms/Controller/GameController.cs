using LOTK.Model;
using LOTK.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
namespace LOTK.Controller
{/// <summary>
/// update to gui
/// </summary>
    public delegate void UpdateForm();
    /// <summary>
    /// this is the class to connect model and gui
    /// </summary>
    public class GameController : viewController
    {
        const int NUM_OF_PLAYER = 5;
        const int DELAY_INTERVAL = 1000;

        public GameView[] view { get; }
        public IGame game { get; }

        public int Num_Of_Player { get { return NUM_OF_PLAYER; } }
        public event UpdateForm updateViews;

        private System.Timers.Timer aTimer;
        public int ClickUser = -100;
        public int SelectCardId = -100;
        public int Ifabi = -10;

        /// <summary>
        /// create controller
        /// </summary>
        public GameController()
        {
            CardSet cardset = initialLizeCardSet();
            Player[] players = initializePlayers(Num_Of_Player);
            game = new Game(players, cardset);
            game.start();

            view = new GameView[NUM_OF_PLAYER];
            view[0] = new GameView(this, 0);
            updateViews = view[0].updateForm;
            
            for (int i = 1; i < NUM_OF_PLAYER; i++)
            {
                view[i] = new GameView(this, i);
                view[i].Show();
                updateViews += view[i].updateForm;
            }

            // set up timer
            aTimer = new System.Timers.Timer(DELAY_INTERVAL);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            //aTimer.Stop();
            //aTimer.Dispose();
        }

        /// <summary>
        /// check time and update 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (game.tick())
                updateViews();
        }

        /// <summary>
        /// get data from model
        /// </summary>
        /// <param name="ownPlayer"></param>
        /// <returns></returns>
        public Required_Data getData(int ownPlayer)
        {
            Required_Data rd = new Required_Data();
            for (int i = 0; i < NUM_OF_PLAYER; i++)
            {
                rd.players[i] = PlayerToPlayerDisplay(game.players[(i + ownPlayer) % NUM_OF_PLAYER]);
            }
            rd.pool_cards = new List<CardDisplay>(game.players[ownPlayer].handCards.Select(c => CardToCardDisplay(c)));
            rd.hold_cards = new List<CardDisplay>(game.players[ownPlayer].handCards.Select(c => CardToCardDisplay(c)));
            rd.this_player_stage = game.curPhase.ToString();
            rd.tool_attack = CardToCardDisplay(game.players[ownPlayer].weapon);
            rd.tool_defence = CardToCardDisplay(game.players[ownPlayer].shield);
            rd.poolText = game.logs;
            rd.cardPileCount = game.cards.cardPileCount;
            rd.discardPile = game.cards.discardPileCount;
            return rd;
        }


        /// <summary>
        /// get the user behavior from gui
        /// </summary>
        /// <param name="playerID"></param>
        /// <param name="buttonID"></param>
        public void clickButton(int playerID, int buttonID)
        {
            switch (buttonID)
            {
                case ButtonID.OK:
                    if (Ifabi == 1 && ClickUser>=0)
                    {
                        game.processUserInput(playerID, new AbilityAction(game, SelectCardId, ClickUser));
                        Ifabi = -1;
                    }
                    else if (Ifabi == 1 && ClickUser < 0)
                    {
                        game.processUserInput(playerID, new AbilityActionSun(game, SelectCardId));
                        Ifabi = -1;
                    }
                    else if (SelectCardId < 0 && Ifabi < 0)
                    {
                        game.processUserInput(playerID, new YesOrNoAction(true));
                        ClickUser = -1;
                    }
                    else if (ClickUser < 0 && Ifabi < 0)
                    {
                        game.processUserInput(playerID, new CardAction(game, SelectCardId));
                        SelectCardId = -1;
                    }
                    else if (ClickUser >= 0 && SelectCardId >= 0 && Ifabi < 0)
                    {
                        game.processUserInput(playerID, new UseCardAction(game, SelectCardId, ClickUser));
                        SelectCardId = -1;
                        ClickUser = -1;
                    }
                    updateViews();
                    break;
                case ButtonID.Cancel:
                    SelectCardId = -1;
                    ClickUser = -1;
                    game.processUserInput(playerID, new YesOrNoAction(false));
                    updateViews();
                    break;
                case ButtonID.Ability:
                    Ifabi = 1;
                    break;
                default:
                    break;
            }

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

        // -------------------------------------------------------------------------------------
        // helper methods to convert model objects to view objects.
        /// <summary>
        /// convert player to display player 
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private PlayerDisplay PlayerToPlayerDisplay(Player player)
        {
            return new PlayerDisplay(player.name, player.description, player.playerID, player.health);
        }
        /// <summary>
        /// convert cards to display cards
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        private CardDisplay CardToCardDisplay(Card card)
        {
            if (card == null)
                return new CardDisplay("", "", -1);
            return new CardDisplay(card.ToString(), card.getDescription(), game.cards[card]);
        }
        /// <summary>
        /// initialize players 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static Player[] initializePlayers(int n)
        {
            Player[] players = new Player[n];

            players[0] = new LuMeng(0);
            players[1] = new SunQuan(1);
            players[2] = new LiuBei(2);
            players[3] = new ZhangFei(3);
            players[4] = new CaoCao(4);

            return players;
        }

        /// <summary>
        /// Initialize the cardSet with default values
        /// </summary>
        /// <returns></returns>
        public static CardSet initialLizeCardSet()
        {
            ICollection<Card> ls = new List<Card>();
            ls.Add(new Attack(CardSuit.Club, 2));
            ls.Add(new Attack(CardSuit.Club, 3));
            ls.Add(new Attack(CardSuit.Club, 4));
            ls.Add(new Attack(CardSuit.Club, 5));
            ls.Add(new Attack(CardSuit.Club, 6));
            ls.Add(new Attack(CardSuit.Club, 7));
            ls.Add(new Attack(CardSuit.Club, 8));
            ls.Add(new Attack(CardSuit.Club, 9));

            ls.Add(new Attack(CardSuit.Spade, 2));
            ls.Add(new Attack(CardSuit.Spade, 3));
            ls.Add(new Attack(CardSuit.Spade, 4));
            ls.Add(new Attack(CardSuit.Spade, 5));
            ls.Add(new Attack(CardSuit.Spade, 6));
            ls.Add(new Attack(CardSuit.Spade, 7));
            ls.Add(new Attack(CardSuit.Spade, 8));
            ls.Add(new Attack(CardSuit.Spade, 9));

            ls.Add(new Miss(CardSuit.Diamond, 2));
            ls.Add(new Miss(CardSuit.Diamond, 3));
            ls.Add(new Miss(CardSuit.Diamond, 4));
            ls.Add(new Miss(CardSuit.Diamond, 5));
            ls.Add(new Miss(CardSuit.Diamond, 6));
            ls.Add(new Miss(CardSuit.Diamond, 7));
            ls.Add(new Miss(CardSuit.Diamond, 8));
            ls.Add(new Miss(CardSuit.Diamond, 9));

            ls.Add(new Peach(CardSuit.Heart, 2));
            ls.Add(new Peach(CardSuit.Heart, 3));
            ls.Add(new Peach(CardSuit.Heart, 4));
            ls.Add(new Peach(CardSuit.Heart, 5));
            ls.Add(new Peach(CardSuit.Heart, 6));
            ls.Add(new Peach(CardSuit.Heart, 7));
            ls.Add(new Peach(CardSuit.Heart, 8));
            ls.Add(new Peach(CardSuit.Heart, 9));
            return new CardSet(ls);
        }

    }

}


