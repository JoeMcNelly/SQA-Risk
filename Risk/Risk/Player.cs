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

 
        public Dictionary<String, Card> getHand()
        {
            return this.cards;
        }
        //public List<Card> playerHandToList()
        //{
        //    return this.cards.Values.ToList();
        //}
        public void addCard(Card card)
        {
            this.cards.Add(card.GetTerritoryName(), card);
        }

        public bool ownsAll()
        {
            return this.territories.Count == 42;
        }
    }
}
