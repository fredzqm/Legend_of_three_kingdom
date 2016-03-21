using LOTK.Controller;
using LOTK.View;
using System.Windows.Forms;

namespace LOTK
{
    public class Form1 : Form
    {
        private viewController controller;
        private int position;
        public Form1(viewController controller, int pos)
        {
            this.position = pos;
            this.controller = controller;
            InitializeComponent();
        }


        private System.ComponentModel.IContainer components = null;
        public Required_Data data {get { return controller.getData(position); }}
        
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
            this.Upperright = new System.Windows.Forms.RichTextBox();
            this.DownLeft = new System.Windows.Forms.RichTextBox();
            this.UpperLeft = new System.Windows.Forms.RichTextBox();
            this.DownRight = new System.Windows.Forms.RichTextBox();
            this.turn = new System.Windows.Forms.Label();
            this.thisPlayer = new System.Windows.Forms.RichTextBox();
            this.tool_attack = new System.Windows.Forms.RichTextBox();
            this.tool_defence = new System.Windows.Forms.RichTextBox();
            this.hand_cards = new System.Windows.Forms.CheckedListBox();
            this.Pool = new System.Windows.Forms.RichTextBox();
            this.ability = new System.Windows.Forms.Button();
            this.done = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Upperright
            // 
            this.Upperright.Location = new System.Drawing.Point(647, 30);
            this.Upperright.Name = "Upperright";
            this.Upperright.Size = new System.Drawing.Size(100, 96);
            this.Upperright.TabIndex = 5;
            this.Upperright.Text = data.players[2].name + "\n" + data.players[2].ability;
            // 
            // DownLeft
            // 
            this.DownLeft.Location = new System.Drawing.Point(52, 253);
            this.DownLeft.Name = "DownLeft";
            this.DownLeft.Size = new System.Drawing.Size(100, 96);
            this.DownLeft.TabIndex = 6;
            this.DownLeft.Text = data.players[4].name + "\n" + data.players[4].ability;
            // 
            // UpperLeft
            // 
            this.UpperLeft.Location = new System.Drawing.Point(52, 30);
            this.UpperLeft.Name = "UpperLeft";
            this.UpperLeft.Size = new System.Drawing.Size(100, 96);
            this.UpperLeft.TabIndex = 7;
            this.UpperLeft.Text = data.players[3].name + "\n" + data.players[3].ability;
            // 
            // DownRight
            // 
            this.DownRight.Location = new System.Drawing.Point(647, 253);
            this.DownRight.Name = "DownRight";
            this.DownRight.Size = new System.Drawing.Size(100, 96);
            this.DownRight.TabIndex = 8;
            this.DownRight.Text = data.players[1].name + "\n" + data.players[1].ability;
            // 
            // turn
            // 
            this.turn.AutoSize = true;
            this.turn.Location = new System.Drawing.Point(362, 33);
            this.turn.Name = "turn";
            this.turn.Size = new System.Drawing.Size(0, 17);
            this.turn.TabIndex = 9;
            this.turn.Text = data.players[0].name + "\n" + data.this_player_stage;
            // 
            // thisPlayer
            // 
            this.thisPlayer.Location = new System.Drawing.Point(607, 409);
            this.thisPlayer.Name = "thisPlayer";
            this.thisPlayer.Size = new System.Drawing.Size(100, 96);
            this.thisPlayer.TabIndex = 10;
            this.thisPlayer.Text = data.players[0].name + "\n" + data.players[0].ability;
            // 
            // tool_attack
            // 
            this.tool_attack.Location = new System.Drawing.Point(12, 409);
            this.tool_attack.Name = "tool_attack";
            this.tool_attack.Size = new System.Drawing.Size(100, 96);
            this.tool_attack.TabIndex = 11;
            this.tool_attack.Text = data.tool_attack.name +": "+ data.tool_attack.ability;
            // 
            // tool_defence
            // 
            this.tool_defence.Location = new System.Drawing.Point(118, 409);
            this.tool_defence.Name = "tool_defence";
            this.tool_defence.Size = new System.Drawing.Size(100, 96);
            this.tool_defence.TabIndex = 12;
            this.tool_defence.Text = data.tool_defence.name + ": " + data.tool_defence.ability;
            // 
            // hand_cards
            // 
            this.hand_cards.FormattingEnabled = true;
            this.hand_cards.Location = new System.Drawing.Point(243, 382);
            this.hand_cards.Name = "hand_cards";
            this.hand_cards.Size = new System.Drawing.Size(320, 123);
            this.hand_cards.TabIndex = 13;
            for (int i = 0; i < data.hold_cards.Count; i++)
            {

                this.hand_cards.Items.Insert(i, data.hold_cards[i].name+"\n"+data.hold_cards[i].ability);
            }
            // 
            // Pool
            // 
            this.Pool.Location = new System.Drawing.Point(243, 87);
            this.Pool.Name = "Pool";
            this.Pool.Size = new System.Drawing.Size(320, 206);
            this.Pool.TabIndex = 14;
            string temp2 = "";
            for (int i = 0; i < data.pool_cards.Count; i++)
            {
                temp2 = temp2 + data.pool_cards[i].name + "\n";
            }
            this.Pool.Text = temp2;
            // 
            // ability
            // 
            this.ability.Location = new System.Drawing.Point(243, 342);
            this.ability.Name = "ability";
            this.ability.Size = new System.Drawing.Size(75, 34);
            this.ability.TabIndex = 15;
            this.ability.Text = "Ability";
            this.ability.UseVisualStyleBackColor = true;
            // 
            // done
            // 
            this.done.Location = new System.Drawing.Point(488, 511);
            this.done.Name = "done";
            this.done.Size = new System.Drawing.Size(75, 30);
            this.done.TabIndex = 16;
            this.done.Text = "Done";
            this.done.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.done);
            this.Controls.Add(this.ability);
            this.Controls.Add(this.Pool);
            this.Controls.Add(this.hand_cards);
            this.Controls.Add(this.tool_defence);
            this.Controls.Add(this.tool_attack);
            this.Controls.Add(this.thisPlayer);
            this.Controls.Add(this.turn);
            this.Controls.Add(this.DownRight);
            this.Controls.Add(this.UpperLeft);
            this.Controls.Add(this.DownLeft);
            this.Controls.Add(this.Upperright);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

            //this.Refresh();
            //this.Update();
        }

        #endregion

        private System.Windows.Forms.RichTextBox Upperright;
        private System.Windows.Forms.RichTextBox DownLeft;
        private System.Windows.Forms.RichTextBox UpperLeft;
        private System.Windows.Forms.RichTextBox DownRight;
        private System.Windows.Forms.Label turn;
        private System.Windows.Forms.RichTextBox thisPlayer;
        private System.Windows.Forms.RichTextBox tool_attack;
        private System.Windows.Forms.RichTextBox tool_defence;
        private System.Windows.Forms.CheckedListBox hand_cards;
        private System.Windows.Forms.RichTextBox Pool;
        private System.Windows.Forms.Button ability;
        private System.Windows.Forms.Button done;
    }
}

