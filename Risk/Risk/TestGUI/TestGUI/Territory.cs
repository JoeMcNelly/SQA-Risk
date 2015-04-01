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
        private String cont;
        private String terrName;

        public Territory()
        {

        }
        
        public void addTroops(int troop)  
        {
            this.troops += troop;
        }

        public int getNumTroops()
        {
            return this.troops;
        }
    }

}
