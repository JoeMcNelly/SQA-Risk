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
        private int currentPlayerIndex;
        private int gamePhase;  // 0 = reinforce, 1 = attack, 2 = fortify
        private int reinforcements;
        private List<Player> players;
        private Map map;
        private Territory source = new Territory();
        private Territory dest = new Territory();

        //These 2 variables should become depreciated when Map.cs is full implemented
        private List<Territory> territories;
        //private Dictionary<String, Territory> map;
        

        public Game()
        {
            
            this.players = new List<Player>();
            this.currentPlayerIndex = 0;
            this.numOfPlayers = 6; //hard coded
            this.gamePhase = 0;
            this.reinforcements = generateReinforcements();
            this.map = new Map(global::Risk.Properties.Resources.Map); 

            //because we hard code players we also need a hard coded creation of a list of them
            for (int i = 0; i < numOfPlayers; i++)
            {
                String name = "Player " + (i + 1);
                Player player = new Player(name, i); // For testing purposes; remove boolean later
                this.players.Add(player);
            }

            //These 2 variables should become depreciated when Map.cs is full implemented
            //this.map = new Dictionary<String, Territory>();
            //this.territories = new List<Territory>();

            #region territories 
            //territories.Add(new Territory("Africa", "North Africa", 0));
            //territories.Add(new Territory("Africa", "Congo", 0));
            //territories.Add(new Territory("Africa", "South Africa", 0));
            //territories.Add(new Territory("Africa", "Madagascar", 0));
            //territories.Add(new Territory("Africa", "East Africa", 0));
            //territories.Add(new Territory("Africa", "Egypt", 0));
            //6

            //territories.Add(new Territory("South America", "Brazil", 1));
            //territories.Add(new Territory("South America", "Argentina", 1));
            //territories.Add(new Territory("South America", "Peru", 1));
            //territories.Add(new Territory("South America", "Venezuela", 1));
            //4

            //territories.Add(new Territory("North America", "Central America", 2));
            //territories.Add(new Territory("North America", "Eastern US", 2));
            //territories.Add(new Territory("North America", "Western US", 2));
            //territories.Add(new Territory("North America", "Alberta", 2));
            //territories.Add(new Territory("North America", "Alaska", 2));
            //territories.Add(new Territory("North America", "Greenland", 2));
            //territories.Add(new Territory("North America", "Northwest Territory", 2));
            //territories.Add(new Territory("North America", "Quebec", 2));
            //territories.Add(new Territory("North America", "Ontario", 2));
            //9

            //territories.Add(new Territory("Europe", "Great Britain", 3));
            //territories.Add(new Territory("Europe", "Iceland", 3));
            //territories.Add(new Territory("Europe", "North Europe", 3));
            //territories.Add(new Territory("Europe", "South Europe", 3));
            //territories.Add(new Territory("Europe", "West Europe", 3));
            //territories.Add(new Territory("Europe", "Scandinavia", 3));
            //territories.Add(new Territory("Europe", "Ukraine", 3));
            //7

            //territories.Add(new Territory("Asia", "China", 4));
            //territories.Add(new Territory("Asia", "Irkutsk", 4));
            //territories.Add(new Territory("Asia", "Kamchatka", 4));
            //territories.Add(new Territory("Asia", "Mongolia", 4));
            //territories.Add(new Territory("Asia", "Siberia", 4));
            //territories.Add(new Territory("Asia", "Yakutsk", 4));
            //territories.Add(new Territory("Asia", "Afghanistan", 4));
            //territories.Add(new Territory("Asia", "India", 4));
            //territories.Add(new Territory("Asia", "Japan", 4));
            //territories.Add(new Territory("Asia", "Middle-East", 4));
            //territories.Add(new Territory("Asia", "Siam", 4));
            //territories.Add(new Territory("Asia", "Ural", 4));
            //12

            //territories.Add(new Territory("Australia", "East Australia", 5));
            //territories.Add(new Territory("Australia", "West Australia", 5));
            //territories.Add(new Territory("Australia", "Indonesia", 5));
            //territories.Add(new Territory("Australia", "New-Guinea", 5));
            //4
            #endregion
            #region set territory owner, hard code
            this.map.getTerritory("North Africa").setOwner(0);
            this.map.getTerritory("Congo").setOwner(0);
            this.map.getTerritory("South Africa").setOwner(0);
            this.map.getTerritory("Madagascar").setOwner(0);
            this.map.getTerritory("East Africa").setOwner(0);
            this.map.getTerritory("Egypt").setOwner(0);
            //6

            this.map.getTerritory("Brazil").setOwner(1);
            this.map.getTerritory("Argentina").setOwner(1);
            this.map.getTerritory("Peru").setOwner(1);
            this.map.getTerritory("Venezuela").setOwner(1);
            //4

            this.map.getTerritory("Central America").setOwner(2);
            this.map.getTerritory("Eastern United States").setOwner(2);
            this.map.getTerritory("Western United States").setOwner(2);
            this.map.getTerritory("Alberta").setOwner(2);
            this.map.getTerritory("Alaska").setOwner(2);
            this.map.getTerritory("Greenland").setOwner(2);
            this.map.getTerritory("Northwest Territory").setOwner(2);
            this.map.getTerritory("Quebec").setOwner(2);
            this.map.getTerritory("Ontario").setOwner(2);
            //9

            this.map.getTerritory("Great Britain").setOwner(3);
            this.map.getTerritory("Iceland").setOwner(3);
            this.map.getTerritory("Northern Europe").setOwner(3);
            this.map.getTerritory("Southern Europe").setOwner(3);
            this.map.getTerritory("Western Europe").setOwner(3);
            this.map.getTerritory("Scandinavia").setOwner(3);
            this.map.getTerritory("Ukraine").setOwner(3);
            //7

            this.map.getTerritory("China").setOwner(4);
            this.map.getTerritory("Irkutsk").setOwner(4);
            this.map.getTerritory("Kamchatka").setOwner(4);
            this.map.getTerritory("Mongolia").setOwner(4);
            this.map.getTerritory("Siberia").setOwner(4);
            this.map.getTerritory("Yakutsk").setOwner(4);
            this.map.getTerritory("Afghanistan").setOwner(4);
            this.map.getTerritory("India").setOwner(4);
            this.map.getTerritory("Japan").setOwner(4);
            this.map.getTerritory("Middle East").setOwner(4);
            this.map.getTerritory("Siam").setOwner(4);
            this.map.getTerritory("Ural").setOwner(4);
            //12

            this.map.getTerritory("Eastern Australia").setOwner(5);
            this.map.getTerritory("Western Australia").setOwner(5);
            this.map.getTerritory("Indonesia").setOwner(5);
            this.map.getTerritory("New Guinea").setOwner(5);
            //4
            #endregion
            #region set players territory lists
            this.players[0].AddTerritory(this.map.getTerritory("North Africa"));
            this.players[0].AddTerritory(this.map.getTerritory("Congo"));
            this.players[0].AddTerritory(this.map.getTerritory("South Africa"));
            this.players[0].AddTerritory(this.map.getTerritory("Madagascar"));
            this.players[0].AddTerritory(this.map.getTerritory("East Africa"));
            this.players[0].AddTerritory(this.map.getTerritory("Egypt"));
            //6

            this.players[1].AddTerritory(this.map.getTerritory("Brazil"));
            this.players[1].AddTerritory(this.map.getTerritory("Argentina"));
            this.players[1].AddTerritory(this.map.getTerritory("Peru"));
            this.players[1].AddTerritory(this.map.getTerritory("Venezuela"));
            //4

            this.players[2].AddTerritory(this.map.getTerritory("Central America"));
            this.players[2].AddTerritory(this.map.getTerritory("Eastern United States"));
            this.players[2].AddTerritory(this.map.getTerritory("Western United States"));
            this.players[2].AddTerritory(this.map.getTerritory("Alberta"));
            this.players[2].AddTerritory(this.map.getTerritory("Alaska"));
            this.players[2].AddTerritory(this.map.getTerritory("Greenland"));
            this.players[2].AddTerritory(this.map.getTerritory("Northwest Territory"));
            this.players[2].AddTerritory(this.map.getTerritory("Quebec"));
            this.players[2].AddTerritory(this.map.getTerritory("Ontario"));
            //9

            this.players[3].AddTerritory(this.map.getTerritory("Great Britain"));
            this.players[3].AddTerritory(this.map.getTerritory("Iceland"));
            this.players[3].AddTerritory(this.map.getTerritory("Northern Europe"));
            this.players[3].AddTerritory(this.map.getTerritory("Southern Europe"));
            this.players[3].AddTerritory(this.map.getTerritory("Western Europe"));
            this.players[3].AddTerritory(this.map.getTerritory("Scandinavia"));
            this.players[3].AddTerritory(this.map.getTerritory("Ukraine"));
            //7

            this.players[4].AddTerritory(this.map.getTerritory("China"));
            this.players[4].AddTerritory(this.map.getTerritory("Irkutsk"));
            this.players[4].AddTerritory(this.map.getTerritory("Kamchatka"));
            this.players[4].AddTerritory(this.map.getTerritory("Mongolia"));
            this.players[4].AddTerritory(this.map.getTerritory("Siberia"));
            this.players[4].AddTerritory(this.map.getTerritory("Yakutsk"));
            this.players[4].AddTerritory(this.map.getTerritory("Afghanistan"));
            this.players[4].AddTerritory(this.map.getTerritory("India"));
            this.players[4].AddTerritory(this.map.getTerritory("Japan"));
            this.players[4].AddTerritory(this.map.getTerritory("Middle East"));
            this.players[4].AddTerritory(this.map.getTerritory("Siam"));
            this.players[4].AddTerritory(this.map.getTerritory("Ural"));
            //12

            this.players[5].AddTerritory(this.map.getTerritory("Eastern Australia"));
            this.players[5].AddTerritory(this.map.getTerritory("Western Australia"));
            this.players[5].AddTerritory(this.map.getTerritory("Indonesia"));
            this.players[5].AddTerritory(this.map.getTerritory("New Guinea"));
            //4
            #endregion
        }

  
        public Game(int numOfPlayers)
        {
            this.players = new List<Player>();
            this.map = new Map(global::Risk.Properties.Resources.Map); 
            this.numOfPlayers = numOfPlayers;
            this.reinforcements = generateReinforcements();
            this.currentPlayerIndex = 0;

            for (int i = 0; i < numOfPlayers; i++)
            {
                String name = "Player " + i;
                Player player = new Player(name, i); // For testing purposes; remove boolean later
                this.players.Add(player);
            }
        }

       
        public void saveReinforcements(Player player) 
        {
            foreach (Territory t in player.getTerritories())
            {
                //this.map.getMap()[t.getName()].saveTroops(); //may be able to replace with just t.saveTroops();
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
            this.source.saveTroops();
            this.dest.saveTroops();
            this.source = new Territory();
            this.dest = new Territory();
            nextGamePhase();
            nextPlayer();
        }

        public void resetFortify()
        {
            if (!this.dest.getName().Equals("") && !this.source.getName().Equals(""))
            {
                this.source.resetReinforcements();
                this.dest.resetReinforcements();
                this.source = new Territory();
                this.dest = new Territory();
            }
        }

        public void nextPlayer() 
        {
            currentPlayerIndex++;
            currentPlayerIndex = currentPlayerIndex % this.numOfPlayers;
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

        public List<Player> getPlayers() 
        {
            return this.players;
        }

        public int getPhase()
        {
            return this.gamePhase;
        }

        public Player getCurrentPlayer()
        {
            return players[this.currentPlayerIndex];
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

        public void clickTerritory(Territory current)
        {
            switch (this.gamePhase)
            {
                case 0:
                   if (reinforcements > 0 && current.getOwner() == currentPlayerIndex)
                    {
                        current.addTroops();
                        this.reinforcements--;
            
                    }
                    break;
                case 1:
                    //do attacking things
                    break;
                case 2:
                    if (this.source.getName().Equals(""))
                    {
                        this.source = current;
                        Console.WriteLine("Hey asshole, you selected the first territory!");
                        Console.WriteLine("You selected: " + this.source.getName());
                    }
                    else if (this.dest.getName().Equals("") && current != this.source)
                    {
                        this.dest = current;
                        Console.WriteLine("Hey dumbass, you selected your destination territory!");
                        Console.WriteLine("You selected: " + this.dest.getName());
                    }
                    else
                    {
                        //if (this.map.IsInPath(this.source.getName(), this.dest.getName(), this.currentPlayer)
                        //    && this.source == current 
                        //    && (this.source.getNumTroops() + this.source.getTemporaryReinforcements() > 1))
                        if (this.dest.getName().Equals(current.getName()) 
                            && this.source.getNumTroops() + this.source.getTemporaryReinforcements() > 1) 
                        {
                            this.source.decTroops();
                            this.dest.addTroops();
                            Console.WriteLine("----TESTING VALUES----");
                            Console.WriteLine("Number of troops in source: "
                                + (this.source.getNumTroops() + this.source.getTemporaryReinforcements()));
                            Console.WriteLine("Number of troops in destination: " 
                                + (this.dest.getNumTroops() + this.dest.getTemporaryReinforcements()));
                        }
                    }

                    break;
            }
        }

        public void resetClick(Player player)
        {
            foreach (Territory t in player.getTerritories())
            {
                 reinforcements += t.getTemporaryReinforcements();
                 t.resetReinforcements();
            }
           
        }

        
    }
}
