﻿using System;
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
            this.game = new Game(); // Hard coding in 2 players for now



            save.Enabled = false;
            reset.Enabled = false;
            attack.Enabled = false;
            endAttack.Enabled = false;
            fortify.Enabled = false;
            resetFortify.Enabled = false;
            initReinforcePhase();

            #region buttons
            buttons = new List<Button>();

            //6
            buttons.Add(NorthAfricaButton);
            buttons.Add(CongoButton);
            buttons.Add(SouthAfricaButton);
            buttons.Add(MadagascarButton);
            buttons.Add(EastAfricaButton);
            buttons.Add(EgyptButton);

            //4
            buttons.Add(BrazilButton);
            buttons.Add(ArgentinaButton);
            buttons.Add(PeruButton);
            buttons.Add(VenezuelaButton);

            //9
            buttons.Add(CentralAmericaButton);
            buttons.Add(EasternUSButton);
            buttons.Add(WesternUSButton);
            buttons.Add(AlbertaButton);
            buttons.Add(AlaskaButton);
            buttons.Add(GreenlandButton);
            buttons.Add(NorthwestTerritoryButton);
            buttons.Add(QuebecButton);
            buttons.Add(OntarioButton);

            //7
            buttons.Add(GreatBritainButton);
            buttons.Add(IcelandButton);
            buttons.Add(NorthEuropeButton);
            buttons.Add(SouthEuropeButton);
            buttons.Add(WestEuropeButton);
            buttons.Add(ScandinaviaButton);
            buttons.Add(UkraineButton);

            //12
            buttons.Add(ChinaButton);
            buttons.Add(IrkutskButton);
            buttons.Add(KamchatkaButton);
            buttons.Add(MongoliaButton);
            buttons.Add(SiberiaButton);
            buttons.Add(YakutskButton);
            buttons.Add(AfghanistanButton);
            buttons.Add(IndiaButton);
            buttons.Add(JapanButton);
            buttons.Add(MiddleEastButton);
            buttons.Add(SiamButton);
            buttons.Add(UralButton);

            //4
            buttons.Add(EastAustraliaButton);
            buttons.Add(WestAustraliaButton);
            buttons.Add(IndonesiaButton);
            buttons.Add(NewGuineaButton);
            #endregion

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Text = game.getTerritories()[i].getNumTroops().ToString();
            }
            label1.Text = "Reinforcements left: " + allowedReinforcements.ToString();
        }

        public void nextPlayer()
        {
            currentPlayer = (currentPlayer + 1) % numberOfPlayers;
            //label2.Text = "Player " + (currentPlayer + 1);
            setPlayerPhaseLabel();
        }

        private void clickTerritory(int index, Button button)
        {
            this.game.clickTerritory(index, button, label1);

        }

        private void setPlayerPhaseLabel()
        {
            var stringPhase = "";
            switch (this.phase)
            {
                case 0:
                    stringPhase = " Reinforce";
                    break;
                case 1:
                    stringPhase = " Attack";
                    break;
                case 2:
                    stringPhase = " Fortify";
                    break;
            }
            label2.Text = "Player " + (currentPlayer + 1) + stringPhase;
        }

        private void initReinforcePhase()
        {
            allowedReinforcements = 15; ////magic number here
            label1.Text = "Reinforcements left: " + allowedReinforcements;
            save.Enabled = true;
            reset.Enabled = true;
            fortify.Enabled = false;
            resetFortify.Enabled = false;
            this.phase = 0;
            setPlayerPhaseLabel();
        }

        private void initAttackPhase()
        {
            save.Enabled = false;
            reset.Enabled = false;
            attack.Enabled = true;
            endAttack.Enabled = true;
            this.phase = 1;
            setPlayerPhaseLabel();
        }

        private void initFortifyPhase()
        {
            attack.Enabled = false;
            endAttack.Enabled = false;
            fortify.Enabled = true;
            resetFortify.Enabled = true;
            this.phase = 2;
            setPlayerPhaseLabel();
        }

        public void save_Click(object sender, EventArgs e)
        {
            if (game.remainingReinforcements() == 0)
            {

                game.saveReinforcements();
                for (int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].Text = game.getTerritories()[i].getNumTroops().ToString();
                }

                label1.Text = "";



            }

        }


        private void reset_Click(object sender, EventArgs e)
        {
            //foreach (Territory t in territories)
            //{
            //    allowedReinforcements += t.getTemporaryReinforcements();
            //    t.resetReinforcements();
            //}
            //label1.Text = "Reinforcements left: " + allowedReinforcements.ToString();
            //for (int i = 0; i < buttons.Count; i++)
            //{
            //    buttons[i].Text = game.getTerritories()[i].getNumTroops().ToString();
            //}

            this.game.resetClick(this.buttons, label1);
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
            clickTerritory(3, MadagascarButton);
        }

        

        public void button7_Click(object sender, EventArgs e)
        {
            clickTerritory(0, NorthAfricaButton);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            clickTerritory(1, CongoButton);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            clickTerritory(2, SouthAfricaButton);

        }
        private void button10_Click(object sender, EventArgs e)
        {
            clickTerritory(4, EastAfricaButton);

        }
        private void button11_Click(object sender, EventArgs e)
        {
            clickTerritory(5, EgyptButton);

        }
        private void button12_Click(object sender, EventArgs e)
        {
            clickTerritory(7, ArgentinaButton);

        }
        private void button13_Click(object sender, EventArgs e)
        {
            clickTerritory(8, PeruButton);

        }
        private void button14_Click(object sender, EventArgs e)
        {
            clickTerritory(6, BrazilButton);

        }
        private void button15_Click(object sender, EventArgs e)
        {
            clickTerritory(9, VenezuelaButton);

        }
        private void button16_Click(object sender, EventArgs e)
        {
            clickTerritory(10, CentralAmericaButton);

        }
        private void button17_Click(object sender, EventArgs e)
        {
            clickTerritory(12, WesternUSButton);

        }
        private void button18_Click(object sender, EventArgs e)
        {
            clickTerritory(11, EasternUSButton);

        }
        private void button19_Click(object sender, EventArgs e)
        {
            clickTerritory(13, AlbertaButton);

        }
        private void button20_Click(object sender, EventArgs e)
        {
            clickTerritory(14, AlaskaButton);

        }
        private void button21_Click(object sender, EventArgs e)
        {
            clickTerritory(16, NorthwestTerritoryButton);

        }
        private void button22_Click(object sender, EventArgs e)
        {
            clickTerritory(18, OntarioButton);

        }
        private void button23_Click(object sender, EventArgs e)
        {
            clickTerritory(17, QuebecButton);

        }
        private void button24_Click(object sender, EventArgs e)
        {
            clickTerritory(15, GreenlandButton);

        }
        private void button25_Click(object sender, EventArgs e)
        {
            clickTerritory(20, IcelandButton);

        }
        private void button26_Click(object sender, EventArgs e)
        {
            clickTerritory(19, GreatBritainButton);

        }
        private void button27_Click(object sender, EventArgs e)
        {
            clickTerritory(23, WestEuropeButton);

        }
        private void button28_Click(object sender, EventArgs e)
        {
            clickTerritory(24, ScandinaviaButton);

        }
        private void button29_Click(object sender, EventArgs e)
        {
            clickTerritory(21, NorthEuropeButton);

        }
        private void button30_Click(object sender, EventArgs e)
        {
            clickTerritory(22, SouthEuropeButton);

        }
        private void button31_Click(object sender, EventArgs e)
        {
            clickTerritory(25, UkraineButton);

        }
        private void button32_Click(object sender, EventArgs e)
        {
            clickTerritory(37, UralButton);

        }
        private void button33_Click(object sender, EventArgs e)
        {
            clickTerritory(35, MiddleEastButton);

        }
        private void button34_Click(object sender, EventArgs e)
        {
            clickTerritory(32, AfghanistanButton);

        }
        private void button35_Click(object sender, EventArgs e)
        {
            clickTerritory(33, IndiaButton);

        }
        private void button36_Click(object sender, EventArgs e)
        {
            clickTerritory(26, ChinaButton);

        }
        private void button37_Click(object sender, EventArgs e)
        {
            clickTerritory(30, SiberiaButton);

        }
        private void button38_Click(object sender, EventArgs e)
        {
            clickTerritory(31, YakutskButton);

        }
        private void button39_Click(object sender, EventArgs e)
        {
            clickTerritory(28, KamchatkaButton);

        }
        private void button40_Click(object sender, EventArgs e)
        {
            clickTerritory(27, IrkutskButton);

        }
        private void button41_Click(object sender, EventArgs e)
        {
            clickTerritory(29, MongoliaButton);

        }
        private void button42_Click(object sender, EventArgs e)
        {
            clickTerritory(36, SiamButton);

        }
        private void button43_Click(object sender, EventArgs e)
        {
            clickTerritory(40, IndonesiaButton);

        }
        private void button44_Click(object sender, EventArgs e)
        {
            clickTerritory(41, NewGuineaButton);

        }
        private void button45_Click(object sender, EventArgs e)
        {
            clickTerritory(39, WestAustraliaButton);

        }
        private void button46_Click(object sender, EventArgs e)
        {
            clickTerritory(38, EastAustraliaButton);

        }
        private void button47_Click(object sender, EventArgs e)
        {
            clickTerritory(34, JapanButton);

        }

        #endregion
        private void label1_Click(object sender, EventArgs e)
        {

        }







    }
}
