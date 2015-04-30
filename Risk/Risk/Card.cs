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

        public Card(int troopType, string territoryName, Bitmap cardPic)
        {
            this.terr = territoryName;
            this.troopType = troopType;
            this.cardPic = cardPic;
        }

        public int GetTroopType()
        {
            return this.troopType;
        }

        public String GetTerritoryName()
        {
            return this.terr;
        }

        public Image GetImage()
        {
            return this.cardPic;
        }
    }
}
