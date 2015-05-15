﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Risk
{
    public partial class StatsScreen : Form
    {
        List<Player> playerList;
        bool keepPlaying = false;
        public StatsScreen(List<Player> players)
        {
            InitializeComponent();
            playerList = players;
            Control label = tableLayoutPanel1.GetControlFromPosition(0, 1);
            label.Text = "testtttttttttt";
        }

        public bool stillPlaying()
        {
            return keepPlaying;
        }

        private void newGame_Click(object sender, EventArgs e)
        {
            this.keepPlaying = true;
            this.Close();
        }

        private void quit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
