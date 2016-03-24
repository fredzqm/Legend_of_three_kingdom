using LOTK.Controller;
using LOTK.View;
using System.Windows.Forms;
using System;

namespace LOTK.View
{
    public partial class Form1
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
            this.thisPlayer = new System.Windows.Forms.RichTextBox();
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
            this.SuspendLayout();
            // 
            // turn
            // 
            this.turn.AutoSize = true;
            this.turn.Location = new System.Drawing.Point(362, 33);
            this.turn.Name = "turn";
            this.turn.Size = new System.Drawing.Size(38, 17);
            this.turn.TabIndex = 9;
            this.turn.Text = "Turn";
            // 
            // thisPlayer
            // 
            this.thisPlayer.Location = new System.Drawing.Point(607, 409);
            this.thisPlayer.Name = "thisPlayer";
            this.thisPlayer.Size = new System.Drawing.Size(100, 96);
            this.thisPlayer.TabIndex = 10;
            this.thisPlayer.Text = "The player";
            // 
            // tool_attack
            // 
            this.tool_attack.Location = new System.Drawing.Point(12, 409);
            this.tool_attack.Name = "tool_attack";
            this.tool_attack.Size = new System.Drawing.Size(100, 96);
            this.tool_attack.TabIndex = 11;
            this.tool_attack.Text = "Weapon";
            // 
            // tool_defence
            // 
            this.tool_defence.Location = new System.Drawing.Point(118, 409);
            this.tool_defence.Name = "tool_defence";
            this.tool_defence.Size = new System.Drawing.Size(100, 96);
            this.tool_defence.TabIndex = 12;
            this.tool_defence.Text = "Shield";
            // 
            // hand_cards
            // 
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
            this.Pool.Location = new System.Drawing.Point(243, 87);
            this.Pool.Name = "Pool";
            this.Pool.Size = new System.Drawing.Size(320, 206);
            this.Pool.TabIndex = 14;
            this.Pool.Text = "Pool Cards";

            // 
            // Ability
            // 
            this.Ability.Location = new System.Drawing.Point(243, 342);
            this.Ability.Name = "Ability";
            this.Ability.Size = new System.Drawing.Size(75, 34);
            this.Ability.TabIndex = 15;
            this.Ability.Text = "Ability";
            this.Ability.UseVisualStyleBackColor = true;
            this.Ability.Click += new System.EventHandler(this.Ability_Click);
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(488, 511);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 30);
            this.OK.TabIndex = 16;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // UpperLeft
            // 
            this.UpperLeft.Location = new System.Drawing.Point(26, 12);
            this.UpperLeft.Name = "UpperLeft";
            this.UpperLeft.Size = new System.Drawing.Size(108, 153);
            this.UpperLeft.TabIndex = 17;
            this.UpperLeft.Text = "UpperLeft";
            this.UpperLeft.UseVisualStyleBackColor = true;
            this.UpperLeft.Click += new System.EventHandler(this.UpperLeft_Click);
            // 
            // UpperRight
            // 
            this.UpperRight.Location = new System.Drawing.Point(643, 12);
            this.UpperRight.Name = "UpperRight";
            this.UpperRight.Size = new System.Drawing.Size(108, 153);
            this.UpperRight.TabIndex = 18;
            this.UpperRight.Text = "UpperRight";
            this.UpperRight.UseVisualStyleBackColor = true;
            this.UpperRight.Click += new System.EventHandler(this.UpperRight_Click);
            // 
            // LowerLeft
            // 
            this.LowerLeft.Location = new System.Drawing.Point(26, 203);
            this.LowerLeft.Name = "LowerLeft";
            this.LowerLeft.Size = new System.Drawing.Size(108, 153);
            this.LowerLeft.TabIndex = 19;
            this.LowerLeft.Text = "LowerLeft";
            this.LowerLeft.UseVisualStyleBackColor = true;
            this.LowerLeft.Click += new System.EventHandler(this.LowerLeft_Click);
            // 
            // LowerRight
            // 
            this.LowerRight.Location = new System.Drawing.Point(643, 203);
            this.LowerRight.Name = "LowerRight";
            this.LowerRight.Size = new System.Drawing.Size(108, 153);
            this.LowerRight.TabIndex = 20;
            this.LowerRight.Text = "LowerRight";
            this.LowerRight.UseVisualStyleBackColor = true;
            this.LowerRight.Click += new System.EventHandler(this.LowerRight_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
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
            this.Controls.Add(this.thisPlayer);
            this.Controls.Add(this.turn);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

      

        #endregion
        private System.Windows.Forms.Label turn;
        private System.Windows.Forms.RichTextBox thisPlayer;
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
    }
}

