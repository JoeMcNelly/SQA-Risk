using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Risk
{
    public class Card
    {
        private Territory terr;
        private String troopType;
        private String filepath;

        public Card() // default constructor; hard-coded values are all placeholders
        {
            this.terr = new Territory("TestTerr", "TestCont");
            this.troopType = "Infantry";
            this.filepath = "";

        }

        public Card(Territory terr, String type)
        {
            this.terr = terr;
            this.troopType = type;
        }


        #region getters
        public Territory getTerritory()
        {
            return this.terr;
        }

        public String getTroopType()
        {
            return this.troopType;
        }

        public String getFilePath()
        {
            return this.filepath;
        }
        #endregion 

        #region setters

        #endregion

    }
}
