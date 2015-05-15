using System;
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
            setLabels();
        }

        public void setLabels()
        {
            int numPlayers = playerList.Count;
            var table = tableLayoutPanel1;
            Player winner = new Player("fake",-1);
            int count = 0;
            foreach(Player p in playerList)
            {
                if (!p.isEliminated())
                {
                    count++;
                    winner = p;
                }   
            }
            if (count != 1)
            {
                label36.Text = "No one wins!";
            }
            else
            {
                label36.Text = winner.playerName + " wins!";
                label36.ForeColor = winner.getColor();
            }
            for(int i = 1 ; i <= 6; i++)
            {
                if(i<=numPlayers)
                {
                    table.GetControlFromPosition(i, 0).Text = playerList[i-1].playerName;
                    table.GetControlFromPosition(i, 0).ForeColor = playerList[i-1].getColor();
                    table.GetControlFromPosition(i, 1).Text = playerList[i-1].getTroopsKilled().ToString();
                    table.GetControlFromPosition(i, 2).Text = playerList[i-1].getLostTroops().ToString();
                    table.GetControlFromPosition(i, 3).Text = playerList[i-1].getTerritoriesConquered().ToString();
                    table.GetControlFromPosition(i, 4).Text = playerList[i-1].getTerritoriesLost().ToString();
                }
                else
                {
                    table.GetControlFromPosition(i, 0).Text = "";
                    table.GetControlFromPosition(i, 1).Text = "";
                    table.GetControlFromPosition(i, 2).Text = "";
                    table.GetControlFromPosition(i, 3).Text = "";
                    table.GetControlFromPosition(i, 4).Text = "";
                }
            }
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
