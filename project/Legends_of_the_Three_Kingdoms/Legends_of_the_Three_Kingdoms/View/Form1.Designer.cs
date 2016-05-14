using LOTK.Controller;
using LOTK.View;
using System.Windows.Forms;
using System;
using System.Drawing;

namespace LOTK.View
{
    public partial class GameView
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameView));
            this.turn = new System.Windows.Forms.Label();
            this.tool_attack = new System.Windows.Forms.RichTextBox();
            this.tool_defence = new System.Windows.Forms.RichTextBox();
            this.hand_cards = new System.Windows.Forms.CheckedListBox();
            this.Pool = new System.Windows.Forms.RichTextBox();
            this.Ability = new System.Windows.Forms.Button();
            this.OK = new System.Windows.Forms.Button();
            this.UpperLeft = new System.Windows.Forms.Button();
            this.UpperRight = new System.Windows.Forms.Button();
            this.LowerLeft = new System.Windows.Forms.Button();
            this.LowerRight = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.ThisPlayer = new System.Windows.Forms.Button();
            this.cardPileCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // turn
            // 
            resources.ApplyResources(this.turn, "turn");
            this.turn.BackColor = System.Drawing.Color.Transparent;
            this.turn.ForeColor = System.Drawing.Color.White;
            this.turn.Name = "turn";
            // 
            // tool_attack
            // 
            this.tool_attack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.tool_attack.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.tool_attack, "tool_attack");
            this.tool_attack.Name = "tool_attack";
            // 
            // tool_defence
            // 
            this.tool_defence.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.tool_defence.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.tool_defence, "tool_defence");
            this.tool_defence.Name = "tool_defence";
            // 
            // hand_cards
            // 
            this.hand_cards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.hand_cards.ForeColor = System.Drawing.Color.White;
            this.hand_cards.FormattingEnabled = true;
            this.hand_cards.Items.AddRange(new object[] {
            resources.GetString("hand_cards.Items"),
            resources.GetString("hand_cards.Items1")});
            resources.ApplyResources(this.hand_cards, "hand_cards");
            this.hand_cards.Name = "hand_cards";
            // 
            // Pool
            // 
            this.Pool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.Pool.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.Pool, "Pool");
            this.Pool.Name = "Pool";
            // 
            // Ability
            // 
            this.Ability.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Ability.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.Ability, "Ability");
            this.Ability.Name = "Ability";
            this.Ability.UseVisualStyleBackColor = false;
            this.Ability.Click += new System.EventHandler(this.Ability_Click);
            // 
            // OK
            // 
            this.OK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.OK.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.OK, "OK");
            this.OK.Name = "OK";
            this.OK.UseVisualStyleBackColor = false;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // UpperLeft
            // 
            this.UpperLeft.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.UpperLeft, "UpperLeft");
            this.UpperLeft.ForeColor = System.Drawing.Color.White;
            this.UpperLeft.Name = "UpperLeft";
            this.UpperLeft.UseVisualStyleBackColor = true;
            this.UpperLeft.Click += new System.EventHandler(this.UpperLeft_Click);
            // 
            // UpperRight
            // 
            this.UpperRight.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.UpperRight, "UpperRight");
            this.UpperRight.ForeColor = System.Drawing.Color.White;
            this.UpperRight.Name = "UpperRight";
            this.UpperRight.UseVisualStyleBackColor = true;
            this.UpperRight.Click += new System.EventHandler(this.UpperRight_Click);
            // 
            // LowerLeft
            // 
            this.LowerLeft.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.LowerLeft, "LowerLeft");
            this.LowerLeft.ForeColor = System.Drawing.Color.White;
            this.LowerLeft.Name = "LowerLeft";
            this.LowerLeft.UseVisualStyleBackColor = true;
            this.LowerLeft.Click += new System.EventHandler(this.LowerLeft_Click);
            // 
            // LowerRight
            // 
            this.LowerRight.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.LowerRight, "LowerRight");
            this.LowerRight.ForeColor = System.Drawing.Color.White;
            this.LowerRight.Name = "LowerRight";
            this.LowerRight.UseVisualStyleBackColor = true;
            this.LowerRight.Click += new System.EventHandler(this.LowerRight_Click);
            // 
            // Cancel
            // 
            this.Cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.Cancel, "Cancel");
            this.Cancel.Name = "Cancel";
            this.Cancel.UseVisualStyleBackColor = false;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // ThisPlayer
            // 
            this.ThisPlayer.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.ThisPlayer, "ThisPlayer");
            this.ThisPlayer.ForeColor = System.Drawing.Color.White;
            this.ThisPlayer.Name = "ThisPlayer";
            this.ThisPlayer.UseVisualStyleBackColor = true;
            this.ThisPlayer.Click += new System.EventHandler(this.ThisPlayer_Click);
            // 
            // cardPileCount
            // 
            resources.ApplyResources(this.cardPileCount, "cardPileCount");
            this.cardPileCount.BackColor = System.Drawing.Color.Transparent;
            this.cardPileCount.ForeColor = System.Drawing.Color.White;
            this.cardPileCount.Name = "cardPileCount";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // GameView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Legends_of_the_Three_Kingdoms.Properties.Resources.background;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cardPileCount);
            this.Controls.Add(this.ThisPlayer);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.UpperLeft);
            this.Controls.Add(this.UpperRight);
            this.Controls.Add(this.LowerLeft);
            this.Controls.Add(this.LowerRight);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.Ability);
            this.Controls.Add(this.Pool);
            this.Controls.Add(this.hand_cards);
            this.Controls.Add(this.tool_defence);
            this.Controls.Add(this.tool_attack);
            this.Controls.Add(this.turn);
            this.Name = "GameView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

      

        #endregion
        private System.Windows.Forms.Label turn;
        private System.Windows.Forms.RichTextBox tool_attack;
        private System.Windows.Forms.RichTextBox tool_defence;
        private System.Windows.Forms.CheckedListBox hand_cards;
        private System.Windows.Forms.RichTextBox Pool;
        private System.Windows.Forms.Button Ability;
        private System.Windows.Forms.Button OK;
        private Button UpperLeft;
        private Button UpperRight;
        private Button LowerLeft;
        private Button LowerRight;
        private Button Cancel;
        private Button ThisPlayer;
        public int NumberOfCardsToClick;
        private Label cardPileCount;
        private Label label1;
    }
}

