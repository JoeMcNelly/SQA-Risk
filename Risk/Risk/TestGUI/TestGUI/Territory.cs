using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGUI
{
    public class Territory
    {
        private List<Territory> adjacencies;
        private int troops;
        private int tempTroops = 0;
        private String cont;
        private String terrName;
        private int owner;

        public Territory(String cont, String terrName, int owner)
        {
            this.cont = cont;
            this.terrName = terrName;
            this.troops = 0;
            this.owner = owner;
        }

        public int getOwner(){
            return owner;
        }
        public void setOwner(int newOwner)
        {
            this.owner = newOwner;
        }


        public String getName()
        {
            return terrName;
        }

        public int getTemporaryReinforcements()
        {
            return tempTroops;
        }
        public void setAdjancencies()
        {
            //TODO: Should probably use a StringReader or something to auto-initialize 
            //everything at the beginning of a game. Or something like that. 
        }
        
        public void addTroops()  
        {
            this.tempTroops ++;
        }

        public void saveTroops()
        {
            this.troops+=this.tempTroops;
            this.tempTroops = 0;
        }

        public int getNumTroops()
        {
            return this.troops;
        }

        public void resetReinforcements()
        {
            this.tempTroops = 0;
        }
    }

}
