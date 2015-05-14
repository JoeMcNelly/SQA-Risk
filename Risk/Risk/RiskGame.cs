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
    public partial class RiskGame : Form
    {

        
        Dictionary<String, Button> buttons;
        List<PictureBox> displayedHand;
        Game game;
        Button src;
        Button dest;
        Territory srcT;
        Territory destT;
        List<PictureBox> selectedCards;
        List<Color> colors;

        public RiskGame(int numPlayers)
        {
            InitializeComponent();
            this.game = new Game(numPlayers); // Hard coding in 6 players for now

            this.displayedHand = new List<PictureBox>();
            #region cards
            this.displayedHand.Add(pictureBox2);
            this.displayedHand.Add(pictureBox3);
            this.displayedHand.Add(pictureBox4);
            this.displayedHand.Add(pictureBox5);
            this.displayedHand.Add(pictureBox6);
            #endregion

            save.Enabled = false;
            reset.Enabled = false;
            attack.Enabled = false;
            endAttack.Enabled = false;
            fortify.Enabled = false;
            resetFortify.Enabled = false;
            TransportButton.Enabled = false;
            TroopsToMove.Enabled = false;
            this.src = null;
            this.dest = null;
            selectedCards = new List<PictureBox>();
            colors = new List<Color>();
            colors.Add(Color.LimeGreen);
            colors.Add(Color.Red);
            colors.Add(Color.DarkGoldenrod);
            colors.Add(Color.DarkOrchid);
            colors.Add(Color.HotPink);
            colors.Add(Color.Cyan);
            initReinforcePhase();

            #region buttons
            buttons = new Dictionary<String, Button>();

            //6
            buttons.Add(NorthAfrica.Name, NorthAfrica);
            buttons.Add(Congo.Name, Congo);
            buttons.Add(SouthAfrica.Name, SouthAfrica);
            buttons.Add(Madagascar.Name, Madagascar);
            buttons.Add(EastAfrica.Name, EastAfrica);
            buttons.Add(Egypt.Name, Egypt);

            //4
            buttons.Add(Brazil.Name, Brazil);
            buttons.Add(Argentina.Name, Argentina);
            buttons.Add(Peru.Name, Peru);
            buttons.Add(Venezuela.Name, Venezuela);

            //9
            buttons.Add(CentralAmerica.Name, CentralAmerica);
            buttons.Add(EasternUnitedStates.Name, EasternUnitedStates);
            buttons.Add(WesternUnitedStates.Name, WesternUnitedStates);
            buttons.Add(Alberta.Name, Alberta);
            buttons.Add(Alaska.Name, Alaska);
            buttons.Add(Greenland.Name, Greenland);
            buttons.Add(NorthwestTerritory.Name, NorthwestTerritory);
            buttons.Add(Quebec.Name, Quebec);
            buttons.Add(Ontario.Name, Ontario);

            //7
            buttons.Add(GreatBritain.Name, GreatBritain);
            buttons.Add(Iceland.Name, Iceland);
            buttons.Add(NorthernEurope.Name, NorthernEurope);
            buttons.Add(SouthernEurope.Name, SouthernEurope);
            buttons.Add(WesternEurope.Name, WesternEurope);
            buttons.Add(Scandinavia.Name, Scandinavia);
            buttons.Add(Ukraine.Name, Ukraine);

            //12
            buttons.Add(China.Name, China);
            buttons.Add(Irkutsk.Name, Irkutsk);
            buttons.Add(Kamchatka.Name, Kamchatka);
            buttons.Add(Mongolia.Name, Mongolia);
            buttons.Add(Siberia.Name, Siberia);
            buttons.Add(Yakutsk.Name, Yakutsk);
            buttons.Add(Afghanistan.Name, Afghanistan);
            buttons.Add(India.Name, India);
            buttons.Add(Japan.Name, Japan);
            buttons.Add(MiddleEast.Name, MiddleEast);
            buttons.Add(Siam.Name, Siam);
            buttons.Add(Ural.Name, Ural);

            //4
            buttons.Add(EasternAustralia.Name, EasternAustralia);
            buttons.Add(WesternAustralia.Name, WesternAustralia);
            buttons.Add(Indonesia.Name, Indonesia);
            buttons.Add(NewGuinea.Name, NewGuinea);
            #endregion

            List<Territory> tempTerritoryList = game.getMap().GetMapAsList();
            List<Button> tempButtonList = buttons.Values.ToList();

            for (int i = 0; i < buttons.Count; i++)
            {
                tempButtonList[i].Text = tempTerritoryList[i].getNumTroops().ToString();
            }
            label1.Text = "Choose a Territory";
            DieRollLabel.Text = "";
        }

        private void disableAttack()
        {
            attack.Enabled = false;
        }
        private void enableAttack()
        {
            attack.Enabled = true;
        }
        
        private void clickTerritory(Button button)
        {
            DieRollLabel.Text = "";
            Territory current = this.game.getMap().getTerritory(button.Name);
            this.game.clickTerritory(current);

            if (game.getInitPhase())
            {
                label2.Text = game.getCurrentPlayer().playerName;
                label2.ForeColor = colors[game.getCurrentPlayer().playerNumber];
            }
            else
            {
                if (game.isLastInit())
                {
                    foreach (var b in buttons)
                    {
                        Territory t = game.getMap().getTerritory(b.Key);
                        b.Value.Text = (t.getTemporaryReinforcements() + t.getNumTroops()) + "";
                    }
                }
                setPlayerPhaseLabel();
            }


                switch (this.game.getPhase())
                {
                    case 0:
                        button.Text = (current.getTemporaryReinforcements() + current.getNumTroops()) + "";
                        if (game.getInitPhase())
                        {
                            label1.Text = "Choose a Territory";
                        }
                        else
                        {
                            label1.Text = "Reinforcements left: " + this.game.getReinforcements().ToString();
                        }


   
                                         
                        break;
                    case 1:
                        //attack button/label things
                        TransportButton.Enabled = false;
                        TroopsToMove.Enabled = false;
                        string srcString = game.getSource().getName();
                        if (srcString == "")
                            srcString = "?";
                        string destString = game.getDest().getName();
                        if (destString == "")
                            destString = "?";
                        if (!game.getSource().canAttack(game.getDest()))
                        {
                            label1.ForeColor = Color.Red;
                            disableAttack();
                        }
                            
                        else
                        {
                            label1.ForeColor = Color.Black;
                            enableAttack();
                        }
                        label1.Text = srcString + " vs: " + destString;
                        break;
                    case 2:
                            srcString = game.getSource().getName();
                                if (srcString == "")
                                    srcString = "?";
                                destString = game.getDest().getName();
                                if (destString == "")
                                    destString = "?";
                                label1.Text = srcString + " --> " + destString;
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
                updateColors();
        }

        private void setPlayerPhaseLabel()
        {
            var stringPhase = "";
            switch (game.getPhase())
            {
                case 0:
                    if(!game.getInitPhase())
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
            label2.ForeColor = colors[game.getCurrentPlayer().playerNumber];
        }

        private void updateColors()
        {
            foreach(KeyValuePair<String, Button> kv in buttons)
            {
                Territory terr = this.game.getMap().getTerritory(kv.Key);
                int owner = terr.getOwner();
                if(owner != -1)
                {
                    kv.Value.BackColor = colors[owner];
                }
            }
  
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
            tradeIn.Enabled = false;
            setPlayerPhaseLabel();
            resetSrcAndDest();
            loadPlayerCards();
            Check5Cards();
        }

        private void initAttackPhase()
        {
            TroopsToMove.Enabled = false;
            TransportButton.Enabled = false;
            save.Enabled = false;
            reset.Enabled = false;
            attack.Enabled = false;
            endAttack.Enabled = true;
            tradeIn.Enabled = false;
            setPlayerPhaseLabel();
            label1.Text = "? vs: ?";
            label1.ForeColor = Color.Red;
            //DieRollLabel.Text = "";
        }

        private void initFortifyPhase()
        {
            attack.Enabled = false;
            endAttack.Enabled = false;
            fortify.Enabled = true;
            resetFortify.Enabled = true;
            tradeIn.Enabled = false;
            setPlayerPhaseLabel();
            label1.Text = "? --> ?";
            label1.ForeColor = Color.Black;
            DieRollLabel.Text = "";
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
            Territory srcTerr = this.game.getSource();
            Territory destTerr = this.game.getDest();
            int initialPlayer = destTerr.getOwner();
            if (srcTerr.canAttack(destTerr))
            {
                this.game.rollDice();
                this.game.attack();
                updateColors();
                DieRollLabel.Text = "Attacker has rolled: " + game.getAttackerRolls() + Environment.NewLine + "Defender has rolled: " + game.getDefenderRolls();
                Button srcButt = this.buttons[srcTerr.getName()];
                srcButt.Text = (srcTerr.getTemporaryReinforcements() + srcTerr.getNumTroops()) + "";


                Button destButt = this.buttons[destTerr.getName()];
                destButt.Text = (destTerr.getTemporaryReinforcements() + destTerr.getNumTroops()) + "";

                int postPlayer = destTerr.getOwner();
                if (initialPlayer != postPlayer && srcTerr.getNumTroops() > 1)
                {
                    TroopsToMove.Enabled = true;
                    TransportButton.Enabled = true;
                    attack.Enabled = false;
                    label1.Text = "Enter number of troops to move";
                }
                else if (srcTerr.getNumTroops() <= 1)
                {
                    initAttackPhase();
                    this.game.resetSrcAndDest();
                }
                if (this.game.isOver())
                {
                    this.Close();
                }
            }
           
        }
        private void TransportButton_click(object sender, EventArgs e)
        {
            DieRollLabel.Text = "";
            if (game.move(TroopsToMove.Text))
            {
                Territory srcTerr = this.game.getSource();
                Territory destTerr = this.game.getDest();
                Button srcButt = this.buttons[srcTerr.getName()];
                srcButt.Text = (srcTerr.getTemporaryReinforcements() + srcTerr.getNumTroops()) + "";


                Button destButt = this.buttons[destTerr.getName()];
                destButt.Text = (destTerr.getTemporaryReinforcements() + destTerr.getNumTroops()) + "";
                initAttackPhase();
                this.game.resetSrcAndDest();
            }
        }

        private void endAttack_click(object sender, EventArgs e)
        {
            DieRollLabel.Text = "";
            game.endAttack();
            updatePhaseButtons();
            TroopsToMove.Enabled = false;
            TransportButton.Enabled = false;
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
            if (this.srcT != null && this.destT != null)
            {
                this.src.Text = this.srcT.getNumTroops() + "";
                this.dest.Text = this.destT.getNumTroops() + "";
                this.src = null;
                this.dest = null;
            }
        }

        private void loadPlayerCards()
        {
            resetHand();
            List<Card> hand = game.getCurrentPlayer().getHand().Values.ToList();
            for(int i=0; i < hand.Count; i++)
            {
                this.displayedHand[i].BackgroundImage = hand[i].GetImage();
                this.displayedHand[i].Tag = hand[i].GetTerritoryName();
                this.displayedHand[i].Cursor = Cursors.Hand;
            }
        }


        private void endTurn_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
       
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ToggleCardSelect(pictureBox2);
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ToggleCardSelect(pictureBox3);
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ToggleCardSelect(pictureBox4);
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            ToggleCardSelect(pictureBox5);
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            ToggleCardSelect(pictureBox6);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        #region Territory Buttons
        private void button1_Click(object sender, EventArgs e)
        {
            clickTerritory(Madagascar);
        }

        

        public void button7_Click(object sender, EventArgs e)
        {
            clickTerritory(NorthAfrica);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            clickTerritory(Congo);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            clickTerritory(SouthAfrica);

        }
        private void button10_Click(object sender, EventArgs e)
        {
            clickTerritory(EastAfrica);

        }
        private void button11_Click(object sender, EventArgs e)
        {
            clickTerritory(Egypt);

        }
        private void button12_Click(object sender, EventArgs e)
        {
            clickTerritory(Argentina);

        }
        private void button13_Click(object sender, EventArgs e)
        {
            clickTerritory(Peru);

        }
        private void button14_Click(object sender, EventArgs e)
        {
            clickTerritory(Brazil);

        }
        private void button15_Click(object sender, EventArgs e)
        {
            clickTerritory(Venezuela);

        }
        private void button16_Click(object sender, EventArgs e)
        {
            clickTerritory(CentralAmerica);

        }
        private void button17_Click(object sender, EventArgs e)
        {
            clickTerritory(WesternUnitedStates);

        }
        private void button18_Click(object sender, EventArgs e)
        {
            clickTerritory(EasternUnitedStates);

        }
        private void button19_Click(object sender, EventArgs e)
        {
            clickTerritory(Alberta);

        }
        private void button20_Click(object sender, EventArgs e)
        {
            clickTerritory(Alaska);

        }
        private void button21_Click(object sender, EventArgs e)
        {
            clickTerritory(NorthwestTerritory);

        }
        private void button22_Click(object sender, EventArgs e)
        {
            clickTerritory(Ontario);

        }
        private void button23_Click(object sender, EventArgs e)
        {
            clickTerritory(Quebec);

        }
        private void button24_Click(object sender, EventArgs e)
        {
            clickTerritory(Greenland);

        }
        private void button25_Click(object sender, EventArgs e)
        {
            clickTerritory(Iceland);

        }
        private void button26_Click(object sender, EventArgs e)
        {
            clickTerritory(GreatBritain);

        }
        private void button27_Click(object sender, EventArgs e)
        {
            clickTerritory(WesternEurope);

        }
        private void button28_Click(object sender, EventArgs e)
        {
            clickTerritory(Scandinavia);

        }
        private void button29_Click(object sender, EventArgs e)
        {
            clickTerritory(NorthernEurope);

        }
        private void button30_Click(object sender, EventArgs e)
        {
            clickTerritory(SouthernEurope);

        }
        private void button31_Click(object sender, EventArgs e)
        {
            clickTerritory(Ukraine);

        }
        private void button32_Click(object sender, EventArgs e)
        {
            clickTerritory(Ural);

        }
        private void button33_Click(object sender, EventArgs e)
        {
            clickTerritory(MiddleEast);

        }
        private void button34_Click(object sender, EventArgs e)
        {
            clickTerritory(Afghanistan);

        }
        private void button35_Click(object sender, EventArgs e)
        {
            clickTerritory(India);

        }
        private void button36_Click(object sender, EventArgs e)
        {
            clickTerritory(China);

        }
        private void button37_Click(object sender, EventArgs e)
        {
            clickTerritory(Siberia);

        }
        private void button38_Click(object sender, EventArgs e)
        {
            clickTerritory(Yakutsk);

        }
        private void button39_Click(object sender, EventArgs e)
        {
            clickTerritory(Kamchatka);

        }
        private void button40_Click(object sender, EventArgs e)
        {
            clickTerritory(Irkutsk);

        }
        private void button41_Click(object sender, EventArgs e)
        {
            clickTerritory(Mongolia);

        }
        private void button42_Click(object sender, EventArgs e)
        {
            clickTerritory(Siam);

        }
        private void button43_Click(object sender, EventArgs e)
        {
            clickTerritory(Indonesia);

        }
        private void button44_Click(object sender, EventArgs e)
        {
            clickTerritory(NewGuinea);

        }
        private void button45_Click(object sender, EventArgs e)
        {
            clickTerritory(WesternAustralia);

        }
        private void button46_Click(object sender, EventArgs e)
        {
            clickTerritory(EasternAustralia);

        }
        private void button47_Click(object sender, EventArgs e)
        {
            clickTerritory(Japan);

        }

        #endregion
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void RemoveFromSelected(PictureBox picBox)
        {
            selectedCards.Remove(picBox);
            picBox.Image = null;
            picBox.Refresh();
            tradeIn.Enabled = false;
        }
        private void AddToSelected(PictureBox picBox)
        {
            Queue<PictureBox> temp = new Queue<PictureBox>(selectedCards);
            temp.Enqueue(picBox);
            if (temp.Count == 4)
            {
                PictureBox tempimage = temp.Dequeue();
                tempimage.Image = null;
                tempimage.Refresh();
            }
            picBox.Image = Risk.Properties.Resources.HIGHLIGHT;
            this.selectedCards = temp.ToList();
            if(selectedCards.Count == 3)
            {
                Dictionary<String, Card> handCheck = game.getCurrentPlayer().getHand();
                if(game.canTradeIn(handCheck[selectedCards[0].Tag.ToString()], handCheck[selectedCards[1].Tag.ToString()], handCheck[selectedCards[2].Tag.ToString()]))
                {
                    //activate the tradein button
                    tradeIn.Enabled = true;
                }
                else
                {
                    tradeIn.Enabled = false; 
                }
            }
        }

        private void ToggleCardSelect(PictureBox picBox)
        {
            if (game.getPhase() == 0)
            {
                if (picBox.Tag != null)
                {
                    if (selectedCards.Contains(picBox))
                    {
                        RemoveFromSelected(picBox);

                    }
                    else
                    {
                        AddToSelected(picBox);
                    }
                }
            }
        }

        //this is the turn in button, I know its a crap name.
        private void button1_Click_1(object sender, EventArgs e)
        {

            Dictionary<String, Card> playerHand = game.getCurrentPlayer().getHand();
            game.getBonusReinforcementsFromCards(playerHand[selectedCards[0].Tag.ToString()], playerHand[selectedCards[1].Tag.ToString()], playerHand[selectedCards[2].Tag.ToString()]);
            selectedCards.Clear();
            loadPlayerCards();
            tradeIn.Enabled = false;
            label1.Text = "Reinforcements left: " + game.getReinforcements();
            save.Enabled = true;
        }

        private void resetHand()
        {
            foreach(PictureBox card in displayedHand)
            {
                card.BackgroundImage = Risk.Properties.Resources.back;
                card.Tag = null;
                card.Image = null;
                card.Cursor = Cursors.Arrow;
                card.Refresh();

            }
        }

        //if a player has 5 cards they HAVE to turn in 3
        private void Check5Cards()
        {
            if (game.getCurrentPlayer().getHand().Count == 5)
            {
                //probably should make a label that says why this is disabled.
                save.Enabled = false;
            }
        }

        public List<Player> getPlayers()
        {
            return this.game.getPlayers();
        }





    }
}
