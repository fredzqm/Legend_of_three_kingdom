﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LOTK.Controller;
using System.Globalization;
using System.Threading;

namespace LOTK.View
{   

    /// <summary>
    /// This is the function for clickButton
    /// </summary>
    /// <param name="playerID"></param>
    /// <param name="buttonID"></param> 
    public delegate void clickButton(int playerID, int buttonID);
    /// <summary>
    /// This is the function for click Card, Maybe changed based on the future design
    /// </summary>
    /// <param name="playerID"></param>
    /// <param name="cardID"></param>
    public delegate void clickCard(int playerID, int cardID); // maybe later changed to a card struct
    /// <summary>
    /// This is the function for click player
    /// </summary>
    /// <param name="playerID"></param>
    /// <param name="clickedPlayerID"></param>
    public delegate void clickPlayer(int playerID, int clickedPlayerID);

    public partial class GameView : Form
    {
        private viewController controller;
        private int position;

        private event clickButton clickbutton;
        private event clickCard clickcard; 
        private event clickPlayer clickplayer;
        private  Required_Data data;
        /// <summary>
        /// Initialize the game view
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="pos"></param>
        public GameView(viewController controller, int pos)
        {
            this.position = pos;
            this.controller = controller;
            clickbutton = controller.clickButton;
            clickcard = controller.clickCard;
            clickplayer = controller.clickPlayer;

            InitializeComponent();
            
        }


        delegate void updateDelegate();


        public void addimage()
        {
            // all character pics are from Romance of the Three Kingdoms 12, publisher: KOEI 
            // background pic is from rxsg.game2.cn
            addimagehelper(UpperLeft);
            addimagehelper(LowerLeft);
            addimagehelper(UpperRight);
            addimagehelper(LowerRight);
            addimagehelper(ThisPlayer);
        }

        private void addimagehelper(Button b)
        {
            if (b.Text.Contains(Legends_of_the_Three_Kingdoms.Properties.Resources.Liu_Bei))
            {
                b.BackgroundImage = global::Legends_of_the_Three_Kingdoms.Properties.Resources.LiuBei1;
            }
            else if (b.Text.Contains(Legends_of_the_Three_Kingdoms.Properties.Resources.Zhang_Fei))
            {
                b.BackgroundImage = global::Legends_of_the_Three_Kingdoms.Properties.Resources.ZhangFei1;
            }
            else if (b.Text.Contains(Legends_of_the_Three_Kingdoms.Properties.Resources.Cao_Cao))
            {
                b.BackgroundImage = global::Legends_of_the_Three_Kingdoms.Properties.Resources.CaoCao1;
            }
            else if (b.Text.Contains(Legends_of_the_Three_Kingdoms.Properties.Resources.Sun_Quan))
            {
                b.BackgroundImage = global::Legends_of_the_Three_Kingdoms.Properties.Resources.SunQuan1;
            }
            else if (b.Text.Contains(Legends_of_the_Three_Kingdoms.Properties.Resources.Lu_Meng))
            {
                b.BackgroundImage = global::Legends_of_the_Three_Kingdoms.Properties.Resources.LuMeng1;
            }
            else
            {
            }
        }

        private bool refresh;
        /// <summary>
        /// Update form after changing the required data package
        /// </summary>
        public void updateForm(bool refreshCards)
        {
            refresh = refreshCards;
            if (this.InvokeRequired)
                this.Invoke(new updateDelegate(this.updateFormDelegate));
            else
                this.updateFormDelegate();
        }

        private void updateFormDelegate()
        {
            data = controller.getData(position);
            if (refresh)
            {
                NumberOfCardsToClick = data.NumberOfCardsToClick;
                turn.Text = Legends_of_the_Three_Kingdoms.Properties.Resources.This_is_player + position + Legends_of_the_Three_Kingdoms.Properties.Resources.res + data.this_player_stage;
                ThisPlayer.Text = data.players[0].name + Legends_of_the_Three_Kingdoms.Properties.Resources._health + data.players[0].health;
                tool_attack.Text = data.tool_attack.name + Legends_of_the_Three_Kingdoms.Properties.Resources.res0 + data.tool_attack.ability;
                tool_defence.Text = data.tool_defence.name + Legends_of_the_Three_Kingdoms.Properties.Resources.res0 + data.tool_defence.ability;
                hand_cards.Items.Clear();
                for (int i = 0; i < data.hold_cards.Count; i++)
                {
                    hand_cards.Items.Insert(i, data.hold_cards[i].name + Legends_of_the_Three_Kingdoms.Properties.Resources.res0 + data.hold_cards[i].ability);
                }
                Pool.Text = data.poolText;
                UpperLeft.Text = data.players[3].name + Legends_of_the_Three_Kingdoms.Properties.Resources._health + data.players[3].health;
                UpperRight.Text = data.players[2].name + Legends_of_the_Three_Kingdoms.Properties.Resources._health + data.players[2].health;
                LowerLeft.Text = data.players[4].name + Legends_of_the_Three_Kingdoms.Properties.Resources._health + data.players[4].health;
                LowerRight.Text = data.players[1].name +  Legends_of_the_Three_Kingdoms.Properties.Resources._health + data.players[1].health;

                cardPileCount.Text = "" + data.cardPileCount;
                addimage();
            }
            this.label1.Text = "" + data.timeleft;
        }


        /// <summary>
        /// Listener for clicking ability
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ability_Click(object sender, EventArgs e)
        {
            clickbutton(position, ButtonID.Ability);
        }
        /// <summary>
        /// Listener for clicking OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OK_Click(object sender, EventArgs e)
        {
            hand_cards_SelectedIndexChanged();
            clickbutton(position, ButtonID.OK);
        }
        /// <summary>
        /// Listener for clicking Cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, EventArgs e)
        {
            hand_cards_SelectedIndexChanged();
            clickbutton(position, ButtonID.Cancel);
        }
        /// <summary>
        /// Listener for clicking UpperLeft player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpperLeft_Click(object sender, EventArgs e)
        {
            clickplayer(position, (position + 3) % controller.Num_Of_Player);
        }
        /// <summary>
        /// Listener for clicking LowerRight player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LowerRight_Click(object sender, EventArgs e)
        {
            clickplayer(position, (position + 1) % controller.Num_Of_Player);
        }
        /// <summary>
        /// Listener for clicking LowerLeft player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LowerLeft_Click(object sender, EventArgs e)
        {
            clickplayer(position, (position + 4) % controller.Num_Of_Player);
        }
        /// <summary>
        /// Listener for clicking UpperRight player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpperRight_Click(object sender, EventArgs e)
        {
            clickplayer(position, (position + 2) % controller.Num_Of_Player);
        }
        /// <summary>
        /// Listener for clicking this player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThisPlayer_Click(object sender, EventArgs e)
        {
            clickplayer(position, position);
        }

        public void hand_cards_SelectedIndexChanged()
        {
            int selectedcount = hand_cards.SelectedItems.Count;
            if (hand_cards.SelectedItems.Count == 0)
            {
                clickcard(position, -100);
            }
            else if (hand_cards.SelectedItems.Count != 1)
            {
                for (int i = 0; i < hand_cards.SelectedItems.Count; i++)
                {
                    hand_cards.SetItemChecked(i, false);
                }
            }
            else
            {
                int id = -1;
                for (int i = 0; i < data.hold_cards.Count; i++)
                {
                    if (hand_cards.CheckedItems.Count > 0 && hand_cards.CheckedItems[0].Equals(data.hold_cards.ElementAt(i).name + Legends_of_the_Three_Kingdoms.Properties.Resources.res0 + data.hold_cards.ElementAt(i).ability)){
                        id = data.hold_cards.ElementAt(i).id;
                    }
                }
                clickcard(position, id);
            }
        }
    
    }
}
