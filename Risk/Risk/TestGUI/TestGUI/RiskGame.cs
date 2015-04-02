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

        public RiskGame()
        {
            InitializeComponent();
            Territory nAfrica = new Territory("Africa", "North Africa");
            Territory congo = new Territory("Africa", "Congo");
            Territory sAfrica = new Territory("Africa", "South Africa");

            territories.Add(nAfrica);
            territories.Add(congo);
            territories.Add(sAfrica);

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

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
            this.territories[0].addTroops(1);

        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.territories[1].addTroops(1);

        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.territories[2].addTroops(2);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
