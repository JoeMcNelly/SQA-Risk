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
    public partial class RiskGame : Form
    {

        
        Dictionary<String, Button> buttons;
        Game game;
        Button src;
        Button dest;
        Territory srcT;
        Territory destT;

        public RiskGame()
        {
            InitializeComponent();
            this.game = new Game(); // Hard coding in 6 players for now



            save.Enabled = false;
            reset.Enabled = false;
            attack.Enabled = false;
            endAttack.Enabled = false;
            fortify.Enabled = false;
            resetFortify.Enabled = false;
            this.src = null;
            this.dest = null;
            initReinforcePhase();

            #region buttons
            buttons = new Dictionary<String, Button>();

            //6
            buttons.Add(NorthAfricaButton.Name, NorthAfricaButton);
            buttons.Add(CongoButton.Name, CongoButton);
            buttons.Add(SouthAfricaButton.Name, SouthAfricaButton);
            buttons.Add(MadagascarButton.Name, MadagascarButton);
            buttons.Add(EastAfricaButton.Name, EastAfricaButton);
            buttons.Add(EgyptButton.Name, EgyptButton);

            //4
            buttons.Add(BrazilButton.Name, BrazilButton);
            buttons.Add(ArgentinaButton.Name, ArgentinaButton);
            buttons.Add(PeruButton.Name, PeruButton);
            buttons.Add(VenezuelaButton.Name, VenezuelaButton);

            //9
            buttons.Add(CentralAmericaButton.Name, CentralAmericaButton);
            buttons.Add(EasternUSButton.Name, EasternUSButton);
            buttons.Add(WesternUSButton.Name, WesternUSButton);
            buttons.Add(AlbertaButton.Name, AlbertaButton);
            buttons.Add(AlaskaButton.Name, AlaskaButton);
            buttons.Add(GreenlandButton.Name, GreenlandButton);
            buttons.Add(NorthwestTerritoryButton.Name, NorthwestTerritoryButton);
            buttons.Add(QuebecButton.Name, QuebecButton);
            buttons.Add(OntarioButton.Name, OntarioButton);

            //7
            buttons.Add(GreatBritainButton.Name, GreatBritainButton);
            buttons.Add(IcelandButton.Name, IcelandButton);
            buttons.Add(NorthEuropeButton.Name, NorthEuropeButton);
            buttons.Add(SouthEuropeButton.Name, SouthEuropeButton);
            buttons.Add(WestEuropeButton.Name, WestEuropeButton);
            buttons.Add(ScandinaviaButton.Name, ScandinaviaButton);
            buttons.Add(UkraineButton.Name, UkraineButton);

            //12
            buttons.Add(ChinaButton.Name, ChinaButton);
            buttons.Add(IrkutskButton.Name, IrkutskButton);
            buttons.Add(KamchatkaButton.Name, KamchatkaButton);
            buttons.Add(MongoliaButton.Name, MongoliaButton);
            buttons.Add(SiberiaButton.Name, SiberiaButton);
            buttons.Add(YakutskButton.Name, YakutskButton);
            buttons.Add(AfghanistanButton.Name, AfghanistanButton);
            buttons.Add(IndiaButton.Name, IndiaButton);
            buttons.Add(JapanButton.Name, JapanButton);
            buttons.Add(MiddleEastButton.Name, MiddleEastButton);
            buttons.Add(SiamButton.Name, SiamButton);
            buttons.Add(UralButton.Name, UralButton);

            //4
            buttons.Add(EastAustraliaButton.Name, EastAustraliaButton);
            buttons.Add(WestAustraliaButton.Name, WestAustraliaButton);
            buttons.Add(IndonesiaButton.Name, IndonesiaButton);
            buttons.Add(NewGuineaButton.Name, NewGuineaButton);
            #endregion

            List<Territory> tempTerritoryList = game.getMap().GetMapAsList();
            List<Button> tempButtonList = buttons.Values.ToList();

            for (int i = 0; i < buttons.Count; i++)
            {
                tempButtonList[i].Text = tempTerritoryList[i].getNumTroops().ToString();
            }
            label1.Text = "Choose your territories";
        }


        
        private void clickTerritory(Button button)
        {
            
            Territory current = this.game.getMap().getTerritory(button.Name);
            this.game.clickTerritory(current);

            if (game.getInitPhase())
            {
                label2.Text = game.getCurrentPlayer().playerName;
            }
            else
            {
                setPlayerPhaseLabel();
            }


                switch (this.game.getPhase())
                {
                    case 0:
                        button.Text = (current.getTemporaryReinforcements() + current.getNumTroops()) + "";
                        label1.Text = "Reinforcements left: " + this.game.getReinforcements().ToString();
                        break;
                    case 1:
                        //attack button/label things
                        break;
                    case 2:
                        if (this.src == null && game.canSetSource())
                        {
                            this.src = button;
                            this.srcT = current;

                        }
                        else if (this.dest == null && current != this.srcT && game.canSetDestination())
                        {
                            this.dest = button;
                            this.destT = current;
                        }
                        else
                        {
                            if (srcT == null || destT == null)
                            {
                                break;
                            }
                            if (current == this.srcT)
                            {
                                break;
                            }
                            if (this.destT.getName() == current.getName())
                            {
                                this.src.Text = (srcT.getTemporaryReinforcements() + srcT.getNumTroops()) + "";
                                this.dest.Text = (destT.getTemporaryReinforcements() + destT.getNumTroops()) + "";

                            }
                        }
                        break;
                }
            
        }

        private void setPlayerPhaseLabel()
        {
            var stringPhase = "";
            switch (game.getPhase())
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
            label2.Text = game.getCurrentPlayer().playerName + stringPhase;
        }

        private void resetSrcAndDest()
        {
            this.dest = null;
            this.src = null;
            this.srcT = null;
            this.destT = null;
        }

        private void initReinforcePhase()
        {
            
            label1.Text = "Reinforcements left: " + game.getReinforcements();
            save.Enabled = true;
            reset.Enabled = true;
            fortify.Enabled = false;
            resetFortify.Enabled = false;
            setPlayerPhaseLabel();
            resetSrcAndDest();
        }

        private void initAttackPhase()
        {
            save.Enabled = false;
            reset.Enabled = false;
            attack.Enabled = true;
            endAttack.Enabled = true;
            setPlayerPhaseLabel();
        }

        private void initFortifyPhase()
        {
            attack.Enabled = false;
            endAttack.Enabled = false;
            fortify.Enabled = true;
            resetFortify.Enabled = true;
            setPlayerPhaseLabel();
        }

        public void save_Click(object sender, EventArgs e)
        {
            if (game.getReinforcements() == 0)
            {
                game.saveReinforcements(game.getCurrentPlayer());
                List<Territory> listToUpdate = game.getCurrentPlayer().getTerritories();
                for (int i = 0; i < listToUpdate.Count; i++)
                {
                    buttons[listToUpdate[i].getName()].Text = listToUpdate[i].getNumTroops().ToString();
                }

                label1.Text = "";

                updatePhaseButtons();

            }

        }
        private void updatePhaseButtons()
        {
            switch (game.getPhase())
            {
                case 0:
                    initReinforcePhase();
                    break;
                case 1:
                    initAttackPhase();
                    break;
                case 2:
                    initFortifyPhase();
                    break;
            }
        }


        private void reset_Click(object sender, EventArgs e)
        {
            this.game.resetClick(game.getCurrentPlayer());

            label1.Text = "Reinforcements left: " + game.getReinforcements().ToString();
            List<Territory> listToUpdate = game.getCurrentPlayer().getTerritories();
            for (int i = 0; i < listToUpdate.Count; i++)
            {
                buttons[listToUpdate[i].getName()].Text = listToUpdate[i].getNumTroops().ToString();
            }
        }

        private void attack_click(object sender, EventArgs e)
        {
            
        }

        private void endAttack_click(object sender, EventArgs e)
        {
            game.endAttack();
            updatePhaseButtons();
        }

        private void fortify_click(object sender, EventArgs e)
        {
            game.endFortify();
            this.src = null;
            this.dest = null;
            updatePhaseButtons();
        }

        private void resetFortify_click(object sender, EventArgs e)
        {
            game.resetFortify();
            this.src.Text = this.srcT.getNumTroops() + "";
            this.dest.Text = this.destT.getNumTroops() + "";
            this.src = null;
            this.dest = null;
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
            clickTerritory(MadagascarButton);
        }

        

        public void button7_Click(object sender, EventArgs e)
        {
            clickTerritory(NorthAfricaButton);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            clickTerritory(CongoButton);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            clickTerritory(SouthAfricaButton);

        }
        private void button10_Click(object sender, EventArgs e)
        {
            clickTerritory(EastAfricaButton);

        }
        private void button11_Click(object sender, EventArgs e)
        {
            clickTerritory(EgyptButton);

        }
        private void button12_Click(object sender, EventArgs e)
        {
            clickTerritory(ArgentinaButton);

        }
        private void button13_Click(object sender, EventArgs e)
        {
            clickTerritory(PeruButton);

        }
        private void button14_Click(object sender, EventArgs e)
        {
            clickTerritory(BrazilButton);

        }
        private void button15_Click(object sender, EventArgs e)
        {
            clickTerritory(VenezuelaButton);

        }
        private void button16_Click(object sender, EventArgs e)
        {
            clickTerritory(CentralAmericaButton);

        }
        private void button17_Click(object sender, EventArgs e)
        {
            clickTerritory(WesternUSButton);

        }
        private void button18_Click(object sender, EventArgs e)
        {
            clickTerritory(EasternUSButton);

        }
        private void button19_Click(object sender, EventArgs e)
        {
            clickTerritory(AlbertaButton);

        }
        private void button20_Click(object sender, EventArgs e)
        {
            clickTerritory(AlaskaButton);

        }
        private void button21_Click(object sender, EventArgs e)
        {
            clickTerritory(NorthwestTerritoryButton);

        }
        private void button22_Click(object sender, EventArgs e)
        {
            clickTerritory(OntarioButton);

        }
        private void button23_Click(object sender, EventArgs e)
        {
            clickTerritory(QuebecButton);

        }
        private void button24_Click(object sender, EventArgs e)
        {
            clickTerritory(GreenlandButton);

        }
        private void button25_Click(object sender, EventArgs e)
        {
            clickTerritory(IcelandButton);

        }
        private void button26_Click(object sender, EventArgs e)
        {
            clickTerritory(GreatBritainButton);

        }
        private void button27_Click(object sender, EventArgs e)
        {
            clickTerritory(WestEuropeButton);

        }
        private void button28_Click(object sender, EventArgs e)
        {
            clickTerritory(ScandinaviaButton);

        }
        private void button29_Click(object sender, EventArgs e)
        {
            clickTerritory(NorthEuropeButton);

        }
        private void button30_Click(object sender, EventArgs e)
        {
            clickTerritory(SouthEuropeButton);

        }
        private void button31_Click(object sender, EventArgs e)
        {
            clickTerritory(UkraineButton);

        }
        private void button32_Click(object sender, EventArgs e)
        {
            clickTerritory(UralButton);

        }
        private void button33_Click(object sender, EventArgs e)
        {
            clickTerritory(MiddleEastButton);

        }
        private void button34_Click(object sender, EventArgs e)
        {
            clickTerritory(AfghanistanButton);

        }
        private void button35_Click(object sender, EventArgs e)
        {
            clickTerritory(IndiaButton);

        }
        private void button36_Click(object sender, EventArgs e)
        {
            clickTerritory(ChinaButton);

        }
        private void button37_Click(object sender, EventArgs e)
        {
            clickTerritory(SiberiaButton);

        }
        private void button38_Click(object sender, EventArgs e)
        {
            clickTerritory(YakutskButton);

        }
        private void button39_Click(object sender, EventArgs e)
        {
            clickTerritory(KamchatkaButton);

        }
        private void button40_Click(object sender, EventArgs e)
        {
            clickTerritory(IrkutskButton);

        }
        private void button41_Click(object sender, EventArgs e)
        {
            clickTerritory(MongoliaButton);

        }
        private void button42_Click(object sender, EventArgs e)
        {
            clickTerritory(SiamButton);

        }
        private void button43_Click(object sender, EventArgs e)
        {
            clickTerritory(IndonesiaButton);

        }
        private void button44_Click(object sender, EventArgs e)
        {
            clickTerritory(NewGuineaButton);

        }
        private void button45_Click(object sender, EventArgs e)
        {
            clickTerritory(WestAustraliaButton);

        }
        private void button46_Click(object sender, EventArgs e)
        {
            clickTerritory(EastAustraliaButton);

        }
        private void button47_Click(object sender, EventArgs e)
        {
            clickTerritory(JapanButton);

        }

        #endregion
        private void label1_Click(object sender, EventArgs e)
        {

        }







    }
}
