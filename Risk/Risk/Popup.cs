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
            return numPlayers;
        }
 

        private void button1_Click(object sender, EventArgs e)
        {
            String content = textBox1.Text;
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
        }
    }
}
