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

namespace LOTK.View
{
    public partial class Form1 : Form
    {
        private viewController controller;
        private int position;
        public Form1(viewController controller, int pos)
        {
            this.position = pos;
            this.controller = controller;
            InitializeComponent();
        }

        public void updateForm()
        {
            Required_Data data = controller.getData(position);
            turn.Text = data.this_player_stage;
            thisPlayer.Text = data.players[0].name;
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



        private void Ability_Click(object sender, EventArgs e)
        {
            
        }

        private void Done_Click(object sender, EventArgs e)
        {

        }

        private void UpperLeft_Click(object sender, EventArgs e)
        {

        }
        private void LowerRight_Click(object sender, EventArgs e)
        {
           
        }

        private void LowerLeft_Click(object sender, EventArgs e)
        {
           
        }

        private void UpperRight_Click(object sender, EventArgs e)
        {
            
        }
    }
}