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
        private bool canSetSrc = false;
        private bool canSetDst = false;
        private Stack<Card> deck;
        private List<Card> discardPile = new List<Card>(); // discard pile will always be empty at the start of any game
        private int bonusReinforcements = 2;

        //These 2 variables should become depreciated when Map.cs is full implemented
        private List<Territory> territories;
        //private Dictionary<String, Territory> map;

        #region Constructors
        public Game()
        {
            this.deck = new Stack<Card>();
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
            this.deck = new Stack<Card>();
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
        #endregion

        #region Game Phase Methods
        public void nextPlayer() 
        {
            currentPlayerIndex++;
            currentPlayerIndex = currentPlayerIndex % this.numOfPlayers;

            // check for cards here

            this.reinforcements = generateReinforcements();
        }
        public int generateReinforcements()
        {
            return 40 - 5 * (Math.Abs(2 - this.numOfPlayers));
        }

        public void nextGamePhase()
        {
            gamePhase++;
            if (gamePhase == 3)
            {
                gamePhase = 0;

            }
                
        }
        #endregion

        #region Card Methods
        // return values are for testing purposes; will modify public fields and be all void returns when finished

        public void initializeDeck() // except for this one.
        {
            for (int i = 0; i < 42; i++)
            {
                this.deck.Push(new Card(new Territory("Cont", "Terr" + i), "Infantry"));
                // Adds mock territories "Terr0" thru "Terr41" all with type Infantry.
                // Can add method to randomize troop type later for testing purposes.
            }
        } 

        public Card drawCard(Player curr)
        {
            return this.deck.Pop();
        }

        public Boolean cardTurnIn(Player curr)
        {
            return true;
            // not yet implemented
        }

        public List<Card> shuffleDeck() 
        {
            return null;
            // not yet implemented
        }


        #endregion


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

        #region Fortify Methods
        public void endFortify()
        {
            this.source.saveTroops();
            this.dest.saveTroops();
            this.source = new Territory();
            this.dest = new Territory();
            this.canSetSrc = false;
            this.canSetDst = false;
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
        #endregion


        #region getters and setters
        public Stack<Card> getDeck()
        {
            return this.deck;
        }

        public List<Card> getDiscard()
        {
            return this.discardPile;
        }

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

        public Map getMap()
        {
            return this.map;
        }
        #endregion

        public void addTerritoryToMap(Territory terr)
        {
            this.map.Add(terr.getName(), terr);
        }


        public bool isOver()
        {
            return true;

        }

        #region GUI-related Methods
        public bool canSetSource()
        {
            return this.canSetSrc;
        }
        public bool canSetDestination()
        {
            return this.canSetDst;
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
                    if (current.getOwner() != getCurrentPlayer().playerNumber)
                    {
                        Console.WriteLine("You selected something that isn't yours. Shame.");
                    }
                    else if (this.source.getName().Equals(""))
                    {
                        this.source = current;
                        this.canSetSrc = true;
                        Console.WriteLine("Hey asshole, you selected the first territory!");
                        Console.WriteLine("You selected: " + this.source.getName());
                    }
                    else if (this.dest.getName().Equals("") && current != this.source)
                    {
                        this.dest = current;
                        this.canSetDst = true;
                        Console.WriteLine("Hey dumbass, you selected your destination territory!");
                        Console.WriteLine("You selected: " + this.dest.getName());
                    }

                    else
                    {
                        //if (this.map.IsInPath(this.source.getName(), this.dest.getName(), this.currentPlayerIndex)
                        //    && this.source == current
                        //    && (this.source.getNumTroops() + this.source.getTemporaryReinforcements() > 1))
                            if (this.dest.getName().Equals(current.getName())
                                && this.source.getNumTroops() + this.source.getTemporaryReinforcements() > 1) 
                        {
                            this.source.decTroops();
                            this.dest.addTroops();
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
        #endregion 

    }
}
