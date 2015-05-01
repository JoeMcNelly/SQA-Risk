using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risk
{
    public class Player
    {
        //NOTE: may want to change List<Territory> to a Dictionary
        private List<Territory> territories;
        private List<Card> cards;
        public bool inPlay = true;
        public bool winner = false;
        public String playerName;
        public int playerNumber;
        public List<Card> hand;

        #region Constructors
        public Player(String name, int number)
        {
            this.hand = new List<Card>();
            this.territories = new List<Territory>();
            this.playerName = name;
            this.playerNumber = number;
        }

        public Player(String name,int number, List<Territory> territories)
        {
            this.hand = new List<Card>();
            this.territories = territories;
            this.playerName = name;
            this.playerNumber = number;
        }
        #endregion

        #region Getters
        public List<Territory> getTerritories()
        {
            return this.territories;
        }
        #endregion

        #region Used Methods
        public void AddTerritory(Territory territory)
        {
            this.territories.Add(territory);
        }
        #endregion

        #region Method Stubs
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
        #endregion

    }
}
