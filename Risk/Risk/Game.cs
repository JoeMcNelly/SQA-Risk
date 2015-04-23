using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Risk
{
    public class Game
    {

        private int numOfPlayers;
        private int currentPlayer;
        private int gamePhase;  // 0 = reinforce, 1 = attack, 2 = fortify
        private int reinforcements;
        private List<Player> players;
        private Map map;

        //These 2 variables should become depreciated when Map.cs is full implemented
        private List<Territory> territories;
        //private Dictionary<String, Territory> map;
        

        public Game()
        {
            
            this.players = new List<Player>();
            this.currentPlayer = 0;
            this.numOfPlayers = 6; //hard coded
            this.gamePhase = 0;
            this.reinforcements = generateReinforcements();
            this.map = new Map(global::Risk.Properties.Resources.Map); //probably shouldn't pass in the path right here but that can be changed easily

            //These 2 variables should become depreciated when Map.cs is full implemented
            //this.map = new Dictionary<String, Territory>();
            this.territories = new List<Territory>();

            #region territories 
            territories.Add(new Territory("Africa", "North Africa", 0));
            territories.Add(new Territory("Africa", "Congo", 0));
            territories.Add(new Territory("Africa", "South Africa", 0));
            territories.Add(new Territory("Africa", "Madagascar", 0));
            territories.Add(new Territory("Africa", "East Africa", 0));
            territories.Add(new Territory("Africa", "Egypt", 0));
            //6

            territories.Add(new Territory("South America", "Brazil", 1));
            territories.Add(new Territory("South America", "Argentina", 1));
            territories.Add(new Territory("South America", "Peru", 1));
            territories.Add(new Territory("South America", "Venezuela", 1));
            //4

            territories.Add(new Territory("North America", "Central America", 2));
            territories.Add(new Territory("North America", "Eastern US", 2));
            territories.Add(new Territory("North America", "Western US", 2));
            territories.Add(new Territory("North America", "Alberta", 2));
            territories.Add(new Territory("North America", "Alaska", 2));
            territories.Add(new Territory("North America", "Greenland", 2));
            territories.Add(new Territory("North America", "Northwest Territory", 2));
            territories.Add(new Territory("North America", "Quebec", 2));
            territories.Add(new Territory("North America", "Ontario", 2));
            //9

            territories.Add(new Territory("Europe", "Great Britain", 3));
            territories.Add(new Territory("Europe", "Iceland", 3));
            territories.Add(new Territory("Europe", "North Europe", 3));
            territories.Add(new Territory("Europe", "South Europe", 3));
            territories.Add(new Territory("Europe", "West Europe", 3));
            territories.Add(new Territory("Europe", "Scandinavia", 3));
            territories.Add(new Territory("Europe", "Ukraine", 3));
            //7

            territories.Add(new Territory("Asia", "China", 4));
            territories.Add(new Territory("Asia", "Irkutsk", 4));
            territories.Add(new Territory("Asia", "Kamchatka", 4));
            territories.Add(new Territory("Asia", "Mongolia", 4));
            territories.Add(new Territory("Asia", "Siberia", 4));
            territories.Add(new Territory("Asia", "Yakutsk", 4));
            territories.Add(new Territory("Asia", "Afghanistan", 4));
            territories.Add(new Territory("Asia", "India", 4));
            territories.Add(new Territory("Asia", "Japan", 4));
            territories.Add(new Territory("Asia", "Middle-East", 4));
            territories.Add(new Territory("Asia", "Siam", 4));
            territories.Add(new Territory("Asia", "Ural", 4));
            //12

            territories.Add(new Territory("Australia", "East Australia", 5));
            territories.Add(new Territory("Australia", "West Australia", 5));
            territories.Add(new Territory("Australia", "Indonesia", 5));
            territories.Add(new Territory("Australia", "New-Guinea", 5));
            //4
            #endregion


        }

  
        public Game(int numOfPlayers) : this()
        {
            
            this.numOfPlayers = numOfPlayers;

            for (int i = 0; i < numOfPlayers; i++)
            {
                String name = "Player " + i;
                Player player = new Player(name, true); // For testing purposes; remove boolean later
                this.players.Add(player);
            }
        }

       
        public void saveReinforcements() 
        {
            foreach (Territory t in territories)
            {
                t.saveTroops();
                
            }      
                
            nextGamePhase();
                
        }

        public void endAttack()
        {
            nextGamePhase();
        }
        public void endFortify()
        {
            nextGamePhase();
            nextPlayer();
        }
        public void nextPlayer() 
        {
            currentPlayer++;
            currentPlayer = currentPlayer % this.numOfPlayers;
            this.reinforcements = generateReinforcements();
        }
        public int generateReinforcements()
        {
            return 15;
        }

        public void nextGamePhase()
        {
            gamePhase++;
            if (gamePhase == 3)
                gamePhase = 0;
        }

        #region getters and setters
        public int getReinforcements()
        {
            return reinforcements;
        }

        public List<Territory> getTerritories()
        {
            return this.territories;
        }

        public List<Player> getPlayers() 
        {
            return this.players;
        }

        public int getPhase()
        {
            return this.gamePhase;
        }

        public int getCurrentPlayer()
        {
            return this.currentPlayer;
        }
        #endregion

        public void addTerritoryToMap(Territory terr)
        {
            this.map.Add(terr.getName(), terr);
        }
        public Map getMap()
        {
            return this.map;
        }

        public bool isOver()
        {
            return true;

        }

        public void clickTerritory(int index)
        {
            Territory current = this.territories[index];
            switch (this.gamePhase)
            {
                case 0:
                   if (reinforcements > 0 && current.getOwner() == currentPlayer)
                    {
                        current.addTroops();
                        reinforcements--;
            
                    }
                    break;
                case 1:
                    //do attacking things
                    break;
                case 2:
                    //do fortify things
                    break;
            }
        }

        public void resetClick()
        {
            foreach (Territory t in territories)
            {
                 reinforcements += t.getTemporaryReinforcements();
                 t.resetReinforcements();
            }
           
        }

        
    }
}
