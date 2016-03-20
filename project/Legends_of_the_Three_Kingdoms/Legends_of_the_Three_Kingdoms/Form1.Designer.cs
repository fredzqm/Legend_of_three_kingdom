using LOTK.View;

namespace LOTK
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Required_Data data = new Required_Data();

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.SuspendLayout();
            // 
            // Upperright
            // 
            this.Upperright.Location = new System.Drawing.Point(647, 30);
            this.Upperright.Name = "Upperright";
            this.Upperright.Size = new System.Drawing.Size(100, 96);
            this.Upperright.TabIndex = 5;
            this.Upperright.Text = data.upright_player+"\n"+data.upright_player_ab;
            // 
            // DownLeft
            // 
            this.DownLeft.Location = new System.Drawing.Point(52, 253);
            this.DownLeft.Name = "DownLeft";
            this.DownLeft.Size = new System.Drawing.Size(100, 96);
            this.DownLeft.TabIndex = 6;
            this.DownLeft.Text = data.downleft_player + "\n" + data.downleft_player_ab;
            // 
            // UpperLeft
            // 
            this.UpperLeft.Location = new System.Drawing.Point(52, 30);
            this.UpperLeft.Name = "UpperLeft";
            this.UpperLeft.Size = new System.Drawing.Size(100, 96);
            this.UpperLeft.TabIndex = 7;
            this.UpperLeft.Text = data.upleft_player+"\n"+ data.upleft_player_ab;
            // 
            // DownRight
            // 
            this.DownRight.Location = new System.Drawing.Point(647, 253);
            this.DownRight.Name = "DownRight";
            this.DownRight.Size = new System.Drawing.Size(100, 96);
            this.DownRight.TabIndex = 8;
            this.DownRight.Text = data.downright_player + "\n" + data.downright_player_ab; 
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.DownRight);
            this.Controls.Add(this.UpperLeft);
            this.Controls.Add(this.DownLeft);
            this.Controls.Add(this.Upperright);
            this.Name = data.this_player;
            this.Text = data.this_player;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox Upperright;
        private System.Windows.Forms.RichTextBox DownLeft;
        private System.Windows.Forms.RichTextBox UpperLeft;
        private System.Windows.Forms.RichTextBox DownRight;
    }
}

