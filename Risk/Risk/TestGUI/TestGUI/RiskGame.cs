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
        int currentPlayer = 0;
        int numberOfPlayers = 6;
        Game game;
        public int phase = 0; //0 is reinforce, 1 is attack, 2 is fortify

        public RiskGame()
        {
            InitializeComponent();
            territories = new List<Territory>();
            this.game = new Game(2); // Hard coding in 2 players for now



            save.Enabled = false;
            reset.Enabled = false;
            attack.Enabled = false;
            endAttack.Enabled = false;
            fortify.Enabled = false;
            resetFortify.Enabled = false;
            initReinforcePhase();

            #region territories
            buttons = new List<Button>();

            territories.Add(new Territory("Africa", "North Africa", 0));
            territories.Add(new Territory("Africa", "Congo", 0));
            territories.Add(new Territory("Africa", "South Africa", 0));
            territories.Add(new Territory("Africa", "Madagascar", 0));
            territories.Add(new Territory("Africa", "East Africa", 0));
            territories.Add(new Territory("Africa", "Egypt", 0));
            //6
            buttons.Add(button7);
            buttons.Add(button8);
            buttons.Add(button9);
            buttons.Add(button1);
            buttons.Add(button10);
            buttons.Add(button11);

            territories.Add(new Territory("South America", "Brazil", 1));
            territories.Add(new Territory("South America", "Argentina", 1));
            territories.Add(new Territory("South America", "Peru", 1));
            territories.Add(new Territory("South America", "Venezuela", 1));
            //4
            buttons.Add(button14);
            buttons.Add(button12);
            buttons.Add(button13);
            buttons.Add(button15);

            territories.Add(new Territory("North America", "Central America", 2));
            territories.Add(new Territory("North America", "Eastern US", 2));
            territories.Add(new Territory("North America", "Western US", 2));
            territories.Add(new Territory("North America", "Alberta", 2));
            territories.Add(new Territory("North America", "Alaska", 2));
            territories.Add(new Territory("North America", "Greenland", 2));
            territories.Add(new Territory("North America", "Northwest Territory", 2));
            territories.Add(new Territory("North America", "Quebec", 2));
            territories.Add(new Territory("North America", "Ontario", 2));
            //9
            buttons.Add(button16);
            buttons.Add(button18);
            buttons.Add(button17);
            buttons.Add(button19);
            buttons.Add(button20);
            buttons.Add(button24);
            buttons.Add(button21);
            buttons.Add(button23);
            buttons.Add(button22);

            territories.Add(new Territory("Europe", "Great Britan", 3));
            territories.Add(new Territory("Europe", "Iceland", 3));
            territories.Add(new Territory("Europe", "North Europe", 3));
            territories.Add(new Territory("Europe", "South Europe", 3));
            territories.Add(new Territory("Europe", "West Europe", 3));
            territories.Add(new Territory("Europe", "Scandinavia", 3));
            territories.Add(new Territory("Europe", "Ukraine", 3));
            //7
            buttons.Add(button26);
            buttons.Add(button25);
            buttons.Add(button29);
            buttons.Add(button30);
            buttons.Add(button27);
            buttons.Add(button28);
            buttons.Add(button31);

            territories.Add(new Territory("Asia", "China", 4));
            territories.Add(new Territory("Asia", "Irkutsk", 4));
            territories.Add(new Territory("Asia", "Kamchatka", 4));
            territories.Add(new Territory("Asia", "Mongolia", 4));
            territories.Add(new Territory("Asia", "Siberia", 4));
            territories.Add(new Territory("Asia", "Yakutsk", 4));
            territories.Add(new Territory("Asia", "Afghanistan", 4));
            territories.Add(new Territory("Asia", "India", 4));
            territories.Add(new Territory("Asia", "Japan", 4));
            territories.Add(new Territory("Asia", "Middle-East", 4));
            territories.Add(new Territory("Asia", "Siam", 4));
            territories.Add(new Territory("Asia", "Ural", 4));
            //12
            buttons.Add(button36);
            buttons.Add(button40);
            buttons.Add(button39);
            buttons.Add(button41);
            buttons.Add(button37);
            buttons.Add(button38);
            buttons.Add(button34);
            buttons.Add(button35);
            buttons.Add(button47);
            buttons.Add(button33);
            buttons.Add(button42);
            buttons.Add(button32);

            territories.Add(new Territory("Australia", "East Australia", 5));
            territories.Add(new Territory("Australia", "West Australia", 5));
            territories.Add(new Territory("Australia", "Indonesia", 5));
            territories.Add(new Territory("Australia", "New-Guinea", 5));
            //4
            buttons.Add(button46);
            buttons.Add(button45);
            buttons.Add(button43);
            buttons.Add(button44);
            #endregion

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Text = territories[i].getNumTroops().ToString();
            }
            label1.Text = "reinforcements left: " + allowedReinforcements.ToString();
        }

        public void nextPlayer()
        {
            currentPlayer = (currentPlayer + 1) % numberOfPlayers;
            label2.Text = "Player " + (currentPlayer + 1);
        }

        private void clickTerritory(int index, Button button)
        {

            Territory current = this.territories[index];
            switch (this.phase)
            {
                case 0:
                    if (allowedReinforcements > 0 && current.getOwner() == currentPlayer)
                    {
                        current.addTroops();
                        allowedReinforcements--;
                        button.Text = (current.getTemporaryReinforcements() + current.getNumTroops()) + "";
                        label1.Text = "reinforcements left: " + allowedReinforcements.ToString();
                    }
                    break;
                case 1:
                    //do attacking things
                    break;
                case 2:
                    //do fortify things
                    break;
            }




        }

        private void initReinforcePhase()
        {
            allowedReinforcements = 15; ////magic number here
            label1.Text = "" + allowedReinforcements;
            save.Enabled = true;
            reset.Enabled = true;
            fortify.Enabled = false;
            resetFortify.Enabled = false;
            this.phase = 0;
        }

        private void initAttackPhase()
        {
            save.Enabled = false;
            reset.Enabled = false;
            attack.Enabled = true;
            endAttack.Enabled = true;
            this.phase = 1;
        }

        private void initFortifyPhase()
        {
            attack.Enabled = false;
            endAttack.Enabled = false;
            fortify.Enabled = true;
            resetFortify.Enabled = true;
            this.phase = 2;
        }

        public void save_Click(object sender, EventArgs e)
        {
            if (allowedReinforcements == 0)
            {
                String name = "";
                foreach (Territory t in territories)
                {
                    t.saveTroops();
                    name += t.getName();
                    name += ": " + t.getNumTroops() + '\n';
                }
                Console.WriteLine(name);

                for (int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].Text = territories[i].getNumTroops().ToString();
                }
                initAttackPhase();
                label1.Text = "";



            }

        }


        private void reset_Click(object sender, EventArgs e)
        {
            foreach (Territory t in territories)
            {
                allowedReinforcements += t.getTemporaryReinforcements();
                t.resetReinforcements();
            }
            label1.Text = allowedReinforcements.ToString();
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Text = territories[i].getNumTroops().ToString();
            }
        }

        private void attack_click(object sender, EventArgs e)
        {

        }

        private void endAttack_click(object sender, EventArgs e)
        {
            initFortifyPhase();
        }

        private void fortify_click(object sender, EventArgs e)
        {
            initReinforcePhase();
            nextPlayer();
        }

        private void resetFortify_click(object sender, EventArgs e)
        {

        }



        private void endTurn_Click(object sender, EventArgs e)
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

        #region Territory Buttons
        private void button1_Click(object sender, EventArgs e)
        {
            clickTerritory(3, button1);
        }

        

        public void button7_Click(object sender, EventArgs e)
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
        private void button10_Click(object sender, EventArgs e)
        {
            clickTerritory(4, button10);

        }
        private void button11_Click(object sender, EventArgs e)
        {
            clickTerritory(5, button11);

        }
        private void button12_Click(object sender, EventArgs e)
        {
            clickTerritory(7, button12);

        }
        private void button13_Click(object sender, EventArgs e)
        {
            clickTerritory(8, button13);

        }
        private void button14_Click(object sender, EventArgs e)
        {
            clickTerritory(6, button14);

        }
        private void button15_Click(object sender, EventArgs e)
        {
            clickTerritory(9, button15);

        }
        private void button16_Click(object sender, EventArgs e)
        {
            clickTerritory(10, button16);

        }
        private void button17_Click(object sender, EventArgs e)
        {
            clickTerritory(12, button17);

        }
        private void button18_Click(object sender, EventArgs e)
        {
            clickTerritory(11, button18);

        }
        private void button19_Click(object sender, EventArgs e)
        {
            clickTerritory(13, button19);

        }
        private void button20_Click(object sender, EventArgs e)
        {
            clickTerritory(14, button20);

        }
        private void button21_Click(object sender, EventArgs e)
        {
            clickTerritory(16, button21);

        }
        private void button22_Click(object sender, EventArgs e)
        {
            clickTerritory(18, button22);

        }
        private void button23_Click(object sender, EventArgs e)
        {
            clickTerritory(17, button23);

        }
        private void button24_Click(object sender, EventArgs e)
        {
            clickTerritory(15, button24);

        }
        private void button25_Click(object sender, EventArgs e)
        {
            clickTerritory(20, button25);

        }
        private void button26_Click(object sender, EventArgs e)
        {
            clickTerritory(19, button26);

        }
        private void button27_Click(object sender, EventArgs e)
        {
            clickTerritory(23, button27);

        }
        private void button28_Click(object sender, EventArgs e)
        {
            clickTerritory(24, button28);

        }
        private void button29_Click(object sender, EventArgs e)
        {
            clickTerritory(21, button29);

        }
        private void button30_Click(object sender, EventArgs e)
        {
            clickTerritory(22, button30);

        }
        private void button31_Click(object sender, EventArgs e)
        {
            clickTerritory(25, button31);

        }
        private void button32_Click(object sender, EventArgs e)
        {
            clickTerritory(37, button32);

        }
        private void button33_Click(object sender, EventArgs e)
        {
            clickTerritory(35, button33);

        }
        private void button34_Click(object sender, EventArgs e)
        {
            clickTerritory(32, button34);

        }
        private void button35_Click(object sender, EventArgs e)
        {
            clickTerritory(33, button35);

        }
        private void button36_Click(object sender, EventArgs e)
        {
            clickTerritory(26, button36);

        }
        private void button37_Click(object sender, EventArgs e)
        {
            clickTerritory(30, button37);

        }
        private void button38_Click(object sender, EventArgs e)
        {
            clickTerritory(31, button38);

        }
        private void button39_Click(object sender, EventArgs e)
        {
            clickTerritory(28, button39);

        }
        private void button40_Click(object sender, EventArgs e)
        {
            clickTerritory(27, button40);

        }
        private void button41_Click(object sender, EventArgs e)
        {
            clickTerritory(29, button41);

        }
        private void button42_Click(object sender, EventArgs e)
        {
            clickTerritory(36, button42);

        }
        private void button43_Click(object sender, EventArgs e)
        {
            clickTerritory(40, button43);

        }
        private void button44_Click(object sender, EventArgs e)
        {
            clickTerritory(41, button44);

        }
        private void button45_Click(object sender, EventArgs e)
        {
            clickTerritory(39, button45);

        }
        private void button46_Click(object sender, EventArgs e)
        {
            clickTerritory(38, button46);

        }
        private void button47_Click(object sender, EventArgs e)
        {
            clickTerritory(34, button47);

        }

        #endregion
        private void label1_Click(object sender, EventArgs e)
        {

        }







    }
}
