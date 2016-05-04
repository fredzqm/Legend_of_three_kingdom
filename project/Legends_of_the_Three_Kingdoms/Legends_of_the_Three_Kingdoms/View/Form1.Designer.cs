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
            this.turn.AutoSize = true;
            this.turn.BackColor = System.Drawing.Color.Transparent;
            this.turn.ForeColor = System.Drawing.Color.White;
            this.turn.Location = new System.Drawing.Point(325, 33);
            this.turn.Name = "turn";
            this.turn.Size = new System.Drawing.Size(38, 17);
            this.turn.TabIndex = 9;
            this.turn.Text = "Turn";
            // 
            // tool_attack
            // 
            this.tool_attack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.tool_attack.ForeColor = System.Drawing.Color.White;
            this.tool_attack.Location = new System.Drawing.Point(12, 409);
            this.tool_attack.Name = "tool_attack";
            this.tool_attack.Size = new System.Drawing.Size(100, 96);
            this.tool_attack.TabIndex = 11;
            this.tool_attack.Text = "Weapon";
            // 
            // tool_defence
            // 
            this.tool_defence.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.tool_defence.ForeColor = System.Drawing.Color.White;
            this.tool_defence.Location = new System.Drawing.Point(118, 409);
            this.tool_defence.Name = "tool_defence";
            this.tool_defence.Size = new System.Drawing.Size(100, 96);
            this.tool_defence.TabIndex = 12;
            this.tool_defence.Text = "Shield";
            // 
            // hand_cards
            // 
            this.hand_cards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.hand_cards.ForeColor = System.Drawing.Color.White;
            this.hand_cards.FormattingEnabled = true;
            this.hand_cards.Items.AddRange(new object[] {
            "Hand Card name\nHand card description",
            "Hand Card name2\nHand card description2"});
            this.hand_cards.Location = new System.Drawing.Point(243, 382);
            this.hand_cards.Name = "hand_cards";
            this.hand_cards.Size = new System.Drawing.Size(320, 123);
            this.hand_cards.TabIndex = 13;
            // 
            // Pool
            // 
            this.Pool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.Pool.ForeColor = System.Drawing.Color.White;
            this.Pool.Location = new System.Drawing.Point(243, 87);
            this.Pool.Name = "Pool";
            this.Pool.Size = new System.Drawing.Size(320, 206);
            this.Pool.TabIndex = 14;
            this.Pool.Text = "Pool Cards";
            // 
            // Ability
            // 
            this.Ability.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Ability.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Ability.Location = new System.Drawing.Point(243, 342);
            this.Ability.Name = "Ability";
            this.Ability.Size = new System.Drawing.Size(75, 34);
            this.Ability.TabIndex = 15;
            this.Ability.Text = "Ability";
            this.Ability.UseVisualStyleBackColor = false;
            this.Ability.Click += new System.EventHandler(this.Ability_Click);
            // 
            // OK
            // 
            this.OK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.OK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OK.Location = new System.Drawing.Point(382, 518);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 30);
            this.OK.TabIndex = 16;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = false;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // UpperLeft
            // 
            this.UpperLeft.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UpperLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UpperLeft.ForeColor = System.Drawing.Color.White;
            this.UpperLeft.Location = new System.Drawing.Point(26, 12);
            this.UpperLeft.Name = "UpperLeft";
            this.UpperLeft.Size = new System.Drawing.Size(108, 153);
            this.UpperLeft.TabIndex = 17;
            this.UpperLeft.Text = "UpperLeft";
            this.UpperLeft.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.UpperLeft.UseVisualStyleBackColor = true;
            this.UpperLeft.Click += new System.EventHandler(this.UpperLeft_Click);
            // 
            // UpperRight
            // 
            this.UpperRight.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UpperRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UpperRight.ForeColor = System.Drawing.Color.White;
            this.UpperRight.Location = new System.Drawing.Point(643, 12);
            this.UpperRight.Name = "UpperRight";
            this.UpperRight.Size = new System.Drawing.Size(108, 153);
            this.UpperRight.TabIndex = 18;
            this.UpperRight.Text = "UpperRight";
            this.UpperRight.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.UpperRight.UseVisualStyleBackColor = true;
            this.UpperRight.Click += new System.EventHandler(this.UpperRight_Click);
            // 
            // LowerLeft
            // 
            this.LowerLeft.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LowerLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LowerLeft.ForeColor = System.Drawing.Color.White;
            this.LowerLeft.Location = new System.Drawing.Point(26, 203);
            this.LowerLeft.Name = "LowerLeft";
            this.LowerLeft.Size = new System.Drawing.Size(108, 153);
            this.LowerLeft.TabIndex = 19;
            this.LowerLeft.Text = "LowerLeft";
            this.LowerLeft.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.LowerLeft.UseVisualStyleBackColor = true;
            this.LowerLeft.Click += new System.EventHandler(this.LowerLeft_Click);
            // 
            // LowerRight
            // 
            this.LowerRight.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LowerRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LowerRight.ForeColor = System.Drawing.Color.White;
            this.LowerRight.Location = new System.Drawing.Point(643, 203);
            this.LowerRight.Name = "LowerRight";
            this.LowerRight.Size = new System.Drawing.Size(108, 153);
            this.LowerRight.TabIndex = 20;
            this.LowerRight.Text = "LowerRight";
            this.LowerRight.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.LowerRight.UseVisualStyleBackColor = true;
            this.LowerRight.Click += new System.EventHandler(this.LowerRight_Click);
            // 
            // Cancel
            // 
            this.Cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Cancel.Location = new System.Drawing.Point(488, 518);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 30);
            this.Cancel.TabIndex = 21;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = false;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // ThisPlayer
            // 
            this.ThisPlayer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ThisPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ThisPlayer.ForeColor = System.Drawing.Color.White;
            this.ThisPlayer.Location = new System.Drawing.Point(633, 382);
            this.ThisPlayer.Name = "ThisPlayer";
            this.ThisPlayer.Size = new System.Drawing.Size(108, 146);
            this.ThisPlayer.TabIndex = 22;
            this.ThisPlayer.Text = "ThisPlayer";
            this.ThisPlayer.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ThisPlayer.UseVisualStyleBackColor = true;
            this.ThisPlayer.Click += new System.EventHandler(this.ThisPlayer_Click);
            // 
            // cardPileCount
            // 
            this.cardPileCount.AutoSize = true;
            this.cardPileCount.BackColor = System.Drawing.Color.Transparent;
            this.cardPileCount.ForeColor = System.Drawing.Color.White;
            this.cardPileCount.Location = new System.Drawing.Point(541, 33);
            this.cardPileCount.Name = "cardPileCount";
            this.cardPileCount.Size = new System.Drawing.Size(32, 17);
            this.cardPileCount.TabIndex = 23;
            this.cardPileCount.Text = "100";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(527, 351);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 24;
            this.label1.Text = "label1";
            // 
            // GameView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Legends_of_the_Three_Kingdoms.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(800, 600);
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

