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

            colorList.Add(Color.LimeGreen);
            colorList.Add(Color.Red);
            colorList.Add(Color.DarkGoldenrod);
            colorList.Add(Color.DarkOrchid);
            colorList.Add(Color.HotPink);
            colorList.Add(Color.Cyan);

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
