using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Risk
{
    public partial class ColorSelect : Form
    {
        private List<Color> colorList = new List<Color>();

        public ColorSelect()
        {
            InitializeComponent();

            colorList.Add(System.Drawing.Color.Red);
            colorList.Add(System.Drawing.Color.Blue);
            colorList.Add(System.Drawing.Color.Green);
            colorList.Add(System.Drawing.Color.Yellow);
            colorList.Add(System.Drawing.Color.Orange);
            colorList.Add(System.Drawing.Color.Purple);
            colorList.Add(System.Drawing.Color.Black);
            colorList.Add(System.Drawing.Color.Silver);
            colorList.Add(System.Drawing.Color.Gold);
            colorList.Add(System.Drawing.Color.Violet);

            this.ColorBox.DataSource = colorList;
        }

        private void ColorSelect_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void select(object sender, EventArgs e)
        {
            this.Close();
            
        }

        public Color getColor()
        {
            return (Color)this.ColorBox.SelectedItem;
        }
    }
}
