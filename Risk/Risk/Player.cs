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
        private Dictionary<String,Card> cards;
        public bool inPlay = true;
        public bool winner = false;
        public String playerName;
        public int playerNumber;

        public Player()
        {
            
        }

        public Player(String name, Boolean whatever) // Constructor strictly for testing
        {
            this.territories = new List<Territory>();
            this.cards = new Dictionary<String, Card>();
            this.playerName = name;
            for (int i = 0; i < 42; i++)
            {
                this.territories.Add(new Territory());
            }
        }

        public Player(String name, int number)
        {
            this.territories = new List<Territory>();
            this.cards = new Dictionary<String, Card>();
            this.playerName = name;
            this.playerNumber = number;
        }

        public Player(String name,int number, List<Territory> territories)
        {
            this.territories = territories;
            this.cards = new Dictionary<String, Card>();
            this.playerName = name;
            this.playerNumber = number;
        }

        public List<Territory> getTerritories()
        {
            return this.territories;
        }

        public void AddTerritory(Territory territory)
        {
            this.territories.Add(territory);
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
        public Dictionary<String, Card> getHand()
        {
            return this.cards;
        }
        public void addCard(Card card)
        {
            this.cards.Add(card.GetTerritoryName(), card);
        }
    }
}
