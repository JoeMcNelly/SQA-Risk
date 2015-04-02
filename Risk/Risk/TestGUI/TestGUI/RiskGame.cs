using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestGUI
{
    public partial class RiskGame : Form
    {

        List<Territory> territories;
        int allowedReinforcements = 15;
        List<Button> buttons;
        public RiskGame()
        {
            territories = new List<Territory>();


            buttons = new List<Button>();
            InitializeComponent();
            Territory nAfrica = new Territory("Africa", "North Africa");
            Territory congo = new Territory("Africa", "Congo");
            Territory sAfrica = new Territory("Africa", "South Africa");
            buttons.Add(button7);
            buttons.Add(button8);
            buttons.Add(button9);

            territories.Add(nAfrica);
            territories.Add(congo);
            territories.Add(sAfrica);

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Text = territories[i].getNumTroops().ToString();
            }
                label1.Text = allowedReinforcements.ToString();
        }

        private void clickTerritory(int index, Button button)
        {
            if (allowedReinforcements > 0)
            {
                this.territories[index].addTroops();
                allowedReinforcements--;
                button.Text = territories[index].tempTroops.ToString();
            }
            label1.Text = "" + allowedReinforcements;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (allowedReinforcements == 0)
            {
                String name = "";
                foreach (Territory t in territories)
                {
                    t.saveTroops();
                    name += t.terrName;
                    name += ": " + t.getNumTroops() + '\n';
                }
                Console.WriteLine(name);
            }
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Text = territories[i].getNumTroops().ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Territory t in territories)
            {
                allowedReinforcements += t.tempTroops;
                t.tempTroops = 0;
            }
            label1.Text = allowedReinforcements.ToString();
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Text = territories[i].getNumTroops().ToString();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            clickTerritory(0, button7);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            clickTerritory(1, button8);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            clickTerritory(2, button9);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
