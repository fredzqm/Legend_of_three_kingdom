﻿using LOTK.Model;
using LOTK.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
namespace LOTK.Controller
{/// <summary>
/// update to gui
/// </summary>
    public delegate void UpdateForm(bool refreshCards);
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

        private UserActionHandler handler;

        private string logs;

        /// <summary>
        /// create controller
        /// </summary>
        public GameController()
        {
            CardSet cardset = initialLizeCardSet();
            Player[] players = initializePlayers(Num_Of_Player);
            game = new Game(players, cardset);
            game.start(4);

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

            handler = new UserActionHandler(game);

            logs = "";
            game.logEvent += addLog;
        }

        private void addLog(string str)
        {
            logs += str + Legends_of_the_Three_Kingdoms.Properties.Resources.res;
        }

        /// <summary>
        /// check time and update 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.Write(Legends_of_the_Three_Kingdoms.Properties.Resources.Tick);
            updateViews(game.tick());
        }

        /// <summary>
        /// get data from model
        /// </summary>
        /// <param name="ownPlayer"></param>
        /// <returns></returns>
        public Required_Data getData(int ownPlayer)
        {
            if (game.hasEnd())
            {
                game.log(Legends_of_the_Three_Kingdoms.Properties.Resources.The_game_has_ended + game.status.ToString());
            }
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
            rd.poolText = logs;
            rd.cardPileCount = game.cards.cardPileCount;
            rd.discardPile = game.cards.discardPileCount;
            rd.timeleft = game.curPhase.getTimeLeft();
            return rd;
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
            String displayedStr;
            if (player.isDead())
                displayedStr = player.name + player.playerType.ToString() + Legends_of_the_Three_Kingdoms.Properties.Resources._Dead;
            else if (player.playerType == PlayerType.King)
            {
                displayedStr = player.name +  Legends_of_the_Three_Kingdoms.Properties.Resources._King;
            } else
            {
                displayedStr = player.name;
            }
            return new PlayerDisplay(displayedStr, player.description, player.playerID, player.health);
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

            players[0] = new LuMeng(0, PlayerType.King);
            players[1] = new SunQuan(1, PlayerType.Loyal);
            players[2] = new LiuBei(2, PlayerType.Rebel);
            players[3] = new ZhangFei(3, PlayerType.Rebel);
            players[4] = new CaoCao(4, PlayerType.Spy);

            return players;
        }

        /// <summary>
        /// Initialize the cardSet with default values
        /// </summary>
        /// <returns></returns>
        public static CardSet initialLizeCardSet()
        {
            ICollection<Card> ls = new List<Card>();
            ls.Add(new PeachGarden(CardSuit.Club, 2));
            ls.Add(new PeachGarden(CardSuit.Club, 3));
            ls.Add(new PeachGarden(CardSuit.Club, 4));
            ls.Add(new Wine(CardSuit.Club, 5));
            ls.Add(new PeachGarden(CardSuit.Club, 6));
            ls.Add(new PeachGarden(CardSuit.Club, 7));
            ls.Add(new PeachGarden(CardSuit.Club, 8));
            ls.Add(new Wealth(CardSuit.Club, 9));

            ls.Add(new PeachGarden(CardSuit.Spade, 2));
            ls.Add(new PeachGarden(CardSuit.Spade, 3));
            ls.Add(new PeachGarden(CardSuit.Spade, 4));
            ls.Add(new Wealth(CardSuit.Spade, 5));
            ls.Add(new Attack(CardSuit.Spade, 6));
            ls.Add(new Attack(CardSuit.Spade, 7));
            ls.Add(new Attack(CardSuit.Spade, 8));
            ls.Add(new Attack(CardSuit.Spade, 9));

            ls.Add(new PeachGarden(CardSuit.Diamond, 2));
            ls.Add(new Wealth(CardSuit.Diamond, 3));
            ls.Add(new Wealth(CardSuit.Diamond, 4));
            ls.Add(new PeachGarden(CardSuit.Diamond, 5));
            ls.Add(new PeachGarden(CardSuit.Diamond, 6));
            ls.Add(new PeachGarden(CardSuit.Diamond, 7));
            ls.Add(new PeachGarden(CardSuit.Diamond, 8));
            ls.Add(new Miss(CardSuit.Diamond, 9));

            ls.Add(new Wealth(CardSuit.Heart, 2));
            ls.Add(new Wealth(CardSuit.Heart, 3));
            ls.Add(new Wealth(CardSuit.Heart, 4));
            ls.Add(new Wealth(CardSuit.Heart, 5));
            ls.Add(new Wealth(CardSuit.Heart, 6));
            ls.Add(new Wealth(CardSuit.Heart, 7));
            ls.Add(new Peach(CardSuit.Heart, 8));
            ls.Add(new Peach(CardSuit.Heart, 9));
            return new CardSet(ls);
        }

        public void clickButton(int playerID, int buttonID)
        {
            UserAction useraction = null;
            try
            {
                switch (buttonID)
                {
                    case ButtonID.OK:
                        useraction = handler.clickOK(playerID);
                        if (useraction != null)
                            game.processUserInput(playerID, useraction);
                        handler.init();
                        updateViews(true);
                        break;
                    case ButtonID.Cancel:
                        useraction = handler.clickCancel(playerID);
                        if (useraction != null)
                            game.processUserInput(playerID, useraction);
                        game.processUserInput(playerID, new YesOrNoAction(false));
                        handler.init();
                        updateViews(true);
                        break;
                    case ButtonID.Ability:
                        handler.clickAbility(buttonID);
                        break;
                    default:
                        throw new InvalidOperationException(Legends_of_the_Three_Kingdoms.Properties.Resources.Button_type_not_found);
                }
            }catch (InvalidOperationException e)
            {
                game.log(e.Message);
            }
        }

        public void clickCard(int playerID, int cardID)
        {
            handler.clickCard(playerID, cardID);
        }

        public void clickPlayer(int playerID, int clickedPlayerID)
        {
            handler.clickPlayer(playerID, clickedPlayerID);
        }
    }

}


