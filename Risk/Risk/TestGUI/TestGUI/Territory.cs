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

        public Territory()
        {
            adjacencies = new List<Territory>();
            troops = 0;
            cont = "";
            terrName = "";
            owner = -1;
        }
        public Territory(String cont, String terrName, int owner)
        {
            this.cont = cont;
            this.terrName = terrName;
            this.troops = 0;
            this.owner = owner;
            this.adjacencies = new List<Territory>();
        }

        public Territory (String cont, String terrName) 
        {
            this.cont = cont;
            this.terrName = terrName;
            this.troops = 0;
            this.owner = -1;
            this.adjacencies = new List<Territory>();
        }

        public Territory(String cont, String terrName, List<Territory> adjacencyList)
        {
            this.adjacencies = adjacencyList;
            this.cont = cont;
            this.terrName = terrName;
            this.troops = 0;
            this.owner = -1;
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
        public void setAdjacencyList(List<Territory> adjacencies)
        {
            this.adjacencies = adjacencies;
        }
        public void addAdjancent(Territory territory)
        {
            adjacencies.Add(territory);
        }
        
        public void addTroops()  
        {
            this.tempTroops++;
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
        public String getContinent() 
        {
            return this.cont;
        }
        public List<Territory> getAdjancencies()
        {
            return adjacencies;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Territory temp = (Territory)obj;

            bool names = (this.terrName.Equals(temp.getName()));
            bool cont = (this.cont.Equals(temp.getContinent()));
            bool troops = (this.getNumTroops() == temp.getNumTroops());
            bool adjacencies = (this.adjacencies.GetHashCode() == temp.getAdjancencies().GetHashCode());
            bool owner = (this.owner == temp.getOwner());

            return names && cont && troops && adjacencies && owner;
        }

        public override string ToString()
        {
            if (this.troops == 0 && this.cont == "" && this.terrName == "" && this.adjacencies.Count == 0 && this.owner == -1)
                return "Empty";

            string adjacent = "";
            foreach (Territory adj in this.adjacencies)
            {
                adjacent += adj.terrName + "; ";
            }


            return "Continent: "+ this.cont +
                   "\nTerritory: "+ this.terrName +
                   "\nTroops: "+this.troops+
                   "\nOwner: "+this.owner+
                   "\nAdjacencies: "+ adjacent;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + cont.GetHashCode();
                hash = hash * 23 + terrName.GetHashCode();
                hash = hash * 23 + troops.GetHashCode();
                hash = hash * 23 + adjacencies.GetHashCode();
                hash = hash * 23 + owner.GetHashCode();

                return hash;
            }
        }


    }

}
