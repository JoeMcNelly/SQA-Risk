using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGUI
{
    public class Territory
    {
        private List<String> adjacencies;
        private int troops;
        private int tempTroops = 0;
        private String cont;
        private String terrName;

        public Territory()
        {

        }

        public Territory(String cont, String terrName)
        {
            this.adjacencies = new List<String>();
            this.cont = cont;
            this.terrName = terrName;
            this.troops = 0;
        }

        public void setAdjancencies()
        {
            //TODO: Should probably use a StringReader or something to auto-initialize 
            //everything at the beginning of a game. Or something like that. 
        }
        public void addAdjancent(String territoryName)
        {
            adjacencies.Add(territoryName);
        }
        
        public void addTroops(int troop)  
        {
            this.troops += troop;
        }

        public int getNumTroops()
        {
            return this.troops;
        }

        public void resetTroops(int troops)
        {
            this.tempTroops = 0;
        }
        public String getContinent() 
        {
            return this.cont;
        }
        public String getName()
        {
            return this.terrName;
        }
        public List<String> getAdjancencies()
        {
            return adjacencies;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Territory temp = (Territory)obj;

            //not finished, needs to check adjacencies and troop count
            return (this.terrName.Equals(temp.getName())) && (this.cont.Equals(temp.getContinent())) && (this.adjacencies.SequenceEqual(temp.getAdjancencies()));
        }


    }

}
