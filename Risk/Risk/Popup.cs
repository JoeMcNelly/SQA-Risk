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
    public partial class Popup : Form
    {
       
        private int numPlayers;
        private List<Color> playerColors = new List<Color>();
        private List<String> playerNames = new List<String>();
        public Popup()
        {
            InitializeComponent();
        }

        private void Popup_Load(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        public int getNumPlayers()
        {
            return this.numPlayers;
        }

        public List<Color> getPlayerColors()
        {
            return this.playerColors;
        }

        public List<String> getPlayerNames()
        {
            return this.playerNames;
        }
 

        private void button1_Click(object sender, EventArgs e)
        {
            

            //code to set the number of players and start game
            /*
            String content = comboBox1.Text;
            Console.WriteLine(content);
            if(content.Equals("2"))
            {
                Console.WriteLine("is 2");
                numPlayers = 2;
                this.Close();
            }
            else if (content.Equals("3"))
            {
                numPlayers = 3;
                this.Close();
            }
            else if (content.Equals("4"))
            {
                numPlayers = 4;
                this.Close();
            }
            else if (content.Equals("5"))
            {
                numPlayers = 5;
                this.Close();
            }
            else if (content.Equals("6"))
            {
                numPlayers = 6;
                this.Close();
            }
             */
        }

        //sets both label for player name and color as visible
        //sets the player option controls for the number of players selected
        //makes the back button active
        //makes the player number selector inactive
        private void displayPlayerOptions()
        {
            this.back.Enabled = true;
            this.comboBox1.Enabled = false;


            this.label2.Visible = true;
            this.label3.Visible = true;
            if(numPlayers >= 2)
            {
                this.playerOneName.Visible = true;
                this.playerTwoName.Visible = true;

                this.playerOneColor.Visible = true;
                this.playerTwoColor.Visible = true;
            }
            if(numPlayers >= 3)
            {
                this.playerThreeName.Visible = true;
                this.playerThreeColor.Visible = true;
            }
            if (numPlayers >= 4)
            {
                this.playerFourName.Visible = true;
                this.playerFourColor.Visible = true;
            }
            if (numPlayers >= 5)
            {
                this.playerFiveName.Visible = true;
                this.playerFiveColor.Visible = true;
            }
            if (numPlayers >= 6)
            {
                this.playerSixName.Visible = true;
                this.playerSixColor.Visible = true;
            }



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        #region Color Select Functions
        private void playerOneChooseColor(object sender, EventArgs e)
        {
            ColorSelect c = new ColorSelect();
            c.ShowDialog(this);
            this.playerOneColor.BackColor = c.getColor();
        }

        private void playerTwoChooseColor(object sender, EventArgs e)
        {
            ColorSelect c = new ColorSelect();
            c.ShowDialog(this);
            this.playerTwoColor.BackColor = c.getColor();
        }

        private void playerThreeChooseColor(object sender, EventArgs e)
        {
            ColorSelect c = new ColorSelect();
            c.ShowDialog(this);
            this.playerThreeColor.BackColor = c.getColor();
        }

        private void playerFourChooseColor(object sender, EventArgs e)
        {
            ColorSelect c = new ColorSelect();
            c.ShowDialog(this);
            this.playerFourColor.BackColor = c.getColor();
        }

        private void playerFiveChooseColor(object sender, EventArgs e)
        {
            ColorSelect c = new ColorSelect();
            c.ShowDialog(this);
            this.playerFiveColor.BackColor = c.getColor();
        }

        private void playerSixChooseColor(object sender, EventArgs e)
        {
            ColorSelect c = new ColorSelect();
            c.ShowDialog(this);
            this.playerSixColor.BackColor = c.getColor();
        }
        #endregion
        private void ClickBack(object sender, EventArgs e)
        {
            this.back.Enabled = false;
            this.comboBox1.Enabled = true;

            this.label2.Visible = false;
            this.label3.Visible = false;

            this.playerOneName.Visible = false;
            this.playerTwoName.Visible = false;

            this.playerOneColor.Visible = false;
            this.playerTwoColor.Visible = false;

            this.playerThreeName.Visible = false;
            this.playerThreeColor.Visible = false;

            this.playerFourName.Visible = false;
            this.playerFourColor.Visible = false;

            this.playerFiveName.Visible = false;
            this.playerFiveColor.Visible = false;

            this.playerSixName.Visible = false;
            this.playerSixColor.Visible = false;

            this.NextButton.Text = "Next";
            this.NextButton.Click += new System.EventHandler(Next);
            this.NextButton.Click -= new System.EventHandler(Start);

        }

        private void Next(object sender, EventArgs e)
        {
            try
            {
                numPlayers = Convert.ToInt32(comboBox1.Text);
            }
            catch (FormatException i)
            {
                return;
            }
            displayPlayerOptions();
            this.NextButton.Text = "Start";
            this.NextButton.Click -= new System.EventHandler(Next);
            this.NextButton.Click += new System.EventHandler(Start);
        }

        private void Start(object sender, EventArgs e)
        {
            #region set Colors
            this.playerColors.Add(this.playerOneColor.BackColor);
            this.playerColors.Add(this.playerTwoColor.BackColor);
            this.playerColors.Add(this.playerThreeColor.BackColor);
            this.playerColors.Add(this.playerFourColor.BackColor);
            this.playerColors.Add(this.playerFiveColor.BackColor);
            this.playerColors.Add(this.playerSixColor.BackColor);
            #endregion

            #region set Names
            this.playerNames.Add(this.playerOneName.Text);
            this.playerNames.Add(this.playerTwoName.Text);
            this.playerNames.Add(this.playerThreeName.Text);
            this.playerNames.Add(this.playerFourName.Text);
            this.playerNames.Add(this.playerFiveName.Text);
            this.playerNames.Add(this.playerSixName.Text);
            #endregion

            this.Close();
        }

        
    }
}
