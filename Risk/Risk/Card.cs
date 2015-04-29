using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Risk
{
    public class Card
    {
        private String terr;
        private int troopType;
        private Image cardPic;

        public Card()
        {
            this.terr = "";
            this.troopType = -1;
            this.cardPic = global::Risk.Properties.Resources.Blank;
        }
    }
}
