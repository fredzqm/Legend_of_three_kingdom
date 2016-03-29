using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LOTK.Controller;

namespace LOTK.View
{   /// <summary>
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

        /// <summary>
        /// Update form after changing the required data package
        /// </summary>
        public void updateForm()
        {
            if (this.InvokeRequired)
                this.Invoke(new updateDelegate(this.updateFormDelegate));
            else
                this.updateFormDelegate();
        }

        private void updateFormDelegate()
        {
            Required_Data data = controller.getData(position);
            turn.Text = data.this_player_stage;
            ThisPlayer.Text = data.players[0].name + ": "+ data.players[0].ability;
            tool_attack.Text = data.tool_attack.name + ": " + data.tool_attack.ability;
            tool_defence.Text = data.tool_defence.name + ": " + data.tool_defence.ability;
            for (int i = 0; i < data.hold_cards.Count; i++)
            {

                hand_cards.Items.Insert(i, data.hold_cards[i].name+": "+data.hold_cards[i].ability);
            }
            string temp = "";
            for (int i = 0; i < data.pool_cards.Count; i++)
            {
            temp = temp + "\n" + data.pool_cards[i].name; 
               
            }
            Pool.Text = temp;
            UpperLeft.Text = data.players[1].name + ": " + data.players[1].ability;
            UpperRight.Text = data.players[2].name + ": " + data.players[2].ability;
            LowerLeft.Text = data.players[3].name + ": " + data.players[3].ability;
            LowerRight.Text = data.players[4].name + ": " + data.players[4].ability;

        }


        /// <summary>
        /// Listener for clicking ability
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ability_Click(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// Listener for clicking OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OK_Click(object sender, EventArgs e)
        {
            clickbutton(position, ButtonID.OK);
        }
        /// <summary>
        /// Listener for clicking Cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, EventArgs e)
        {
            clickbutton(position, ButtonID.Cancel);
        }
        /// <summary>
        /// Listener for clicking UpperLeft player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpperLeft_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Listener for clicking LowerRight player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LowerRight_Click(object sender, EventArgs e)
        {
           
        }
        /// <summary>
        /// Listener for clicking LowerLeft player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LowerLeft_Click(object sender, EventArgs e)
        {
           
        }
        /// <summary>
        /// Listener for clicking UpperRight player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpperRight_Click(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// Listener for clicking this player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThisPlayer_Click(object sender, EventArgs e)
        {

        }

        private void hand_cards_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.Write("triggerd");
        }
    }
}
