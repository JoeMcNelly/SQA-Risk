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

        public Territory()
        {

        }

        public Territory(String cont, String terrName)
        {
            this.cont = cont;
            this.terrName = terrName;
            this.troops = 0;
        }

        public void setAdjancencies()
        {
            //TODO: Should probably use a StringReader or something to auto-initialize 
            //everything at the beginning of a game. Or something like that. 
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
    }

}
