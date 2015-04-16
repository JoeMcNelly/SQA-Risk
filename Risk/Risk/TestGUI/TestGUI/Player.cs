using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGUI
{
    public class Player
    {
        private List<Territory> territories;
        private List<Card> cards;
        public bool inPlay = true;
        public bool winner = false;
        public String playerName;

        public Player()
        {
            
        }

        public Player(String name, Boolean whatever) // Constructor strictly for testing
        {
            this.territories = new List<Territory>();
            this.playerName = name;
            for (int i = 0; i < 42; i++)
            {
                this.territories.Add(new Territory());
            }
        }

        public List<Territory> getTerritories()
        {
            return this.territories;
        }

        public void finalize(List<int> troops)
        {
           
        }
        public void takeTurn()
        {

        }
        public void redeemCards()
        {

        }
        public void incReinforce()
        {

        }
        public void decReinforce()
        {

        }
        void attack(Territory t1, Territory t2)
        {

        }
        public void fortify(Territory source, Territory dest, int numOfTroops)
        {

        }

    }
}
