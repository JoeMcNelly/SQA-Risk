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
        private bool initPhase = true;
        private bool initReinforce = true;
        private bool cardDrawn = false;
        private int initReinforceCounter = 0;
        private bool gameOver = false;
        Player neutralPlayer = new Player("Neutral", -1);
        private int numberOfInitialTerritories;
        //These 2 variables should become depreciated when Map.cs is full implemented
        //private List<Territory> territories;
        //private Dictionary<String, Territory> map;
        private int[] setValues = { 2, 4, 7, 10, 13, 17, 21, 25, 30 };
        private int GoldenCavalry = 0;
        Random dice = new Random();
        List<int> attackerRolls = new List<int>();
        List<int> defenderRolls = new List<int>();
        int playersLeft;

        public Game() : this(6)
        {
            //not used
            #region set territory owner, hard code
             /*
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
              */ 
            #endregion

            

            //not used
            #region set hands
            //this.players[0].addCard(new Card(0, "Afghanistan", Risk.Properties.Resources.AFGHANISTAN));
            //this.players[0].addCard(new Card(0, "Alaska", Risk.Properties.Resources.ALASKA));
            //this.players[0].addCard(new Card(0, "Alberta", Risk.Properties.Resources.ALBERTA));

            //this.players[0].addCard(new Card(0, "Argentina", Risk.Properties.Resources.ARGENTINA));
            //this.players[0].addCard(new Card(2, "Brazil", Risk.Properties.Resources.BRAZIL));
            //this.players[1].addCard(new Card(1, "Central America", Risk.Properties.Resources.CENTRAL_AMERICA));

            //this.players[2].addCard(new Card(1, "China", Risk.Properties.Resources.CHINA));
            //this.players[2].addCard(new Card(1, "Congo", Risk.Properties.Resources.CONGO));
            //this.players[2].addCard(new Card(2, "East Africa", Risk.Properties.Resources.EAST_AFRICA));

            //this.players[3].addCard(new Card(0, "Eastern Australia", Risk.Properties.Resources.EASTERN_AUSTRALIA));
            //this.players[3].addCard(new Card(2, "Eastern United States", Risk.Properties.Resources.EASTERN_UNITED_STATES));
            //this.players[3].addCard(new Card(0, "Egypt", Risk.Properties.Resources.EGYPT));

            //this.players[4].addCard(new Card(1, "Great Britain", Risk.Properties.Resources.GREAT_BRITAIN));
            //this.players[4].addCard(new Card(1, "Greenland", Risk.Properties.Resources.GREENLAND));
            //this.players[4].addCard(new Card(1, "Iceland", Risk.Properties.Resources.ICELAND));

            //this.players[5].addCard(new Card(1, "India", Risk.Properties.Resources.INDIA));
            //this.players[5].addCard(new Card(1, "Indonesia", Risk.Properties.Resources.INDONESIA));
            //this.players[5].addCard(new Card(0, "Irkutsk", Risk.Properties.Resources.IRKUTSK));
            #endregion
          
        }

  
        public Game(int numOfPlayers)
        {
            this.deck = new Stack<Card>();
            this.players = new List<Player>();
            this.map = new Map(global::Risk.Properties.Resources.Map); 
            this.numOfPlayers = numOfPlayers;
            this.reinforcements = initialReinforcements();
            this.currentPlayerIndex = 0;
            numberOfInitialTerritories = numOfPlayers*7;
            this.playersLeft = numOfPlayers;
            for (int i = 0; i < numOfPlayers; i++)
            {
                String name = "Player " + (i + 1);
                Player player = new Player(name, i); // For testing purposes; remove boolean later
                this.players.Add(player);
            }

            
            foreach (Territory terr in map.GetMapAsList())
            {
                terr.setOwner(-1);
                neutralPlayer.AddTerritory(terr);
            }
            initializeDeck();
            

        }
        public string getAttackerRolls()
        {
            return "5 : 3 : 1";
        }
        public string getDefenderRolls()
        {
            return this.defenderRolls.ToString();
        }
        public bool isLastInit()
        {
            return this.numberOfInitialTerritories == 0;
        }
        public void saveReinforcements(Player player) 
        {
            foreach (Territory t in player.getTerritories())
            {
                //this.map.getMap()[t.getName()].saveTroops(); //may be able to replace with just t.saveTroops();
                t.saveTroops();
            }
            if (initReinforce)
            {
                this.reinforcements=initialReinforcements();
                nextPlayer();
                initReinforceCounter++;
                if (initReinforceCounter == this.numOfPlayers)
                {
                    initReinforce = false;
                    this.reinforcements = generateReinforcements();
                }
            }

            else
                nextGamePhase();
                
        }

        public void endAttack()
        {
            resetSrcAndDest();
            nextGamePhase();
        }
        public void resetSrcAndDest()
        {
            this.source = new Territory();
            this.dest = new Territory();
        }
        public void endFortify()
        {
            this.source.saveTroops();
            this.dest.saveTroops();
            resetSrcAndDest();
            this.canSetSrc = false;
            this.canSetDst = false;
            nextPlayer();
            drawCard();
            nextGamePhase();
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
            if (getCurrentPlayer().isEliminated() && !getInitPhase())
                nextPlayer();
            else
                this.cardDrawn = false;
            
        }
        public int initialReinforcements()
        {
            
            return (40 - 5 * (Math.Abs(2 - this.numOfPlayers)))-7;
        }
        public int generateReinforcements()
        {
            return getTerritoryBonus() + getContinentBonus();
        }
        public int getTerritoryBonus()
        {
            int number = (int)Math.Floor((Double)getCurrentPlayer().getTerritories().Count / 3);
            if (number < 3)
                return 3;
            return number;
        }

        public int getContinentBonus()
        {
            int total = 0;
            List<Territory> current = getCurrentPlayer().getTerritories();
            List<Territory> southAmerica = this.map.GetTerritoriesByContinent("South America");
            List<Territory> australia = this.map.GetTerritoriesByContinent("Australia");
            List<Territory> northAmerica = this.map.GetTerritoriesByContinent("North America");
            List<Territory> africa = this.map.GetTerritoriesByContinent("Africa");
            List<Territory> europe = this.map.GetTerritoriesByContinent("Europe");
            List<Territory> asia = this.map.GetTerritoriesByContinent("Asia");

            bool allSouthAmerica = true;
            foreach (Territory t in southAmerica)
            {
                if (!current.Contains(t))
                {
                    allSouthAmerica = false;
                }
            }

            bool allAustralia = true;
            foreach (Territory t in australia)
            {
                if (!current.Contains(t))
                {
                    allAustralia = false;
                }
            }

            bool allNorthAmerica = true;
            foreach (Territory t in northAmerica)
            {
                if (!current.Contains(t))
                {
                    allNorthAmerica = false;
                }
            }
            bool allEurope = true;
            foreach (Territory t in europe)
            {
                if (!current.Contains(t))
                {
                    allEurope = false;
                }
            }

            bool allAsia = true;
            foreach (Territory t in asia)
            {
                if (!current.Contains(t))
                {
                    allAsia = false;
                }
            }

            bool allAfrica = true;
            foreach (Territory t in africa)
            {
                if (!current.Contains(t))
                {
                    allAfrica = false;
                }
            }

           

            
            if (allSouthAmerica)
            {
                total += 2;
            }
            if (allAustralia)
            {
                total += 2;
            }
            if (allNorthAmerica)
            {
                total += 5;
            }
            if (allEurope)
            {
                total += 5;
            }
            if (allAsia)
            {
                total += 7;
            }
            if (allAfrica)
            {
                total += 3;
            }

            return total;
        }
        //For testing only
        public void turnOffInit()
        {
            this.initPhase = false;
            this.initReinforce = false;
        }

        public void nextGamePhase()
        {
            gamePhase++;
            if (gamePhase == 3)
            {
                gamePhase = 0;
                this.reinforcements = generateReinforcements();
            }
                
        }
        public bool move(String stringNumToMove)
        {
            int numToMove;
            try
            {
                numToMove = int.Parse(stringNumToMove);
                Territory src = getSource();
                Territory dst = getDest();
                if (src.getNumTroops() <= numToMove)
                    return false;
                for (int i = 0; i < numToMove; i++)
                {
                    src.decTroops();
                    dst.addTroops();
                }
                src.saveTroops();
                dst.saveTroops();
                return true;
            }
            catch
            {
                return false;
            }



        }

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
            return this.gameOver;

        }
        public bool canSetSource()
        {
            return this.canSetSrc;
        }
        public bool canSetDestination()
        {
            return this.canSetDst;
        }
        public Territory getSource()
        {
            return source;
        }
        public Territory getDest()
        {
            return dest;
        }
        private void clickInitTerritory(Territory current){
            Console.WriteLine(current.getOwner());
            Console.WriteLine(current.getNumTroops());
            if (current.getOwner() == -1)
            {
                current.setOwner(currentPlayerIndex);
                players[currentPlayerIndex].AddTerritory(current);
                current.addTroops();
                current.saveTroops();
                nextPlayer();
                numberOfInitialTerritories--;
                if (numberOfInitialTerritories == 0) {
                    initPhase = false;
                    foreach(Territory t in map.GetMapAsList())
                    {
                        if (t.getOwner() == -1)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                t.addTroops();
                            }
                            t.saveTroops();
                        }
                    }
                }
            }
        }
        public bool getInitPhase()
        {
            return this.initPhase;
        }
        public void clickTerritory(Territory current)
        {
            if (initPhase)
            {
                clickInitTerritory(current);
            }
            else
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
                        
                        
                        if(current.getOwner() == getCurrentPlayer().playerNumber)
                        {
                            this.source = current;
                        }
                        else
                        {
                            this.dest = current;
                        }
                        break;
                    case 2:
                        if (current.getOwner() != getCurrentPlayer().playerNumber)
                        {
                            Console.WriteLine("You selected something that isn't yours.");
                        }
                        else if (this.source.getName().Equals(""))
                        {
                            this.source = current;
                            this.canSetSrc = true;
                        }
                        else if (this.dest.getName().Equals("") && current != this.source)
                        {
                            this.dest = current;
                            this.canSetDst = true;
                        }

                        else
                        {
                            if (this.map.IsInPath(this.source.getName(), this.dest.getName(), this.currentPlayerIndex)
                                && this.dest == current
                                && (this.source.getNumTroops() + this.source.getTemporaryReinforcements() > 1))
                            //if (this.dest.getName().Equals(current.getName())
                            //    && this.source.getNumTroops() + this.source.getTemporaryReinforcements() > 1)
                            {
                                this.source.decTroops();
                                this.dest.addTroops();
                            }
                        }

                        break;
                }
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

        public int getBonusReinforcementsFromCards(Card c1, Card c2, Card c3)
        {

            int addedReinforcements = 0;

            if(canTradeIn(c1, c2, c3))
            {
                addedReinforcements = this.setValues[GoldenCavalry];
                this.reinforcements += addedReinforcements;
                AdvanceGoldenCavalry();

                //discarding cards
                discardSet(c1, c2, c3);

                //depending on what we decide, this is where the discard action would happen
            }

            return addedReinforcements;
        }

        public void AdvanceGoldenCavalry()
        {
            if (GoldenCavalry < setValues.Length - 2)
                GoldenCavalry++;
        }


        public bool canTradeIn(Card c1, Card c2, Card c3)
        {
            int c1t = c1.GetTroopType();
            int c2t = c2.GetTroopType();
            int c3t = c3.GetTroopType();

            return (c1t == c2t && c1t == c3t && c2t == c3t) || (c1t != c2t && c1t != c3t && c2t != c3t);
        }

        public void discardSet(Card c1, Card c2, Card c3)
        {
            Dictionary<String, Card> playerHand = getCurrentPlayer().getHand();
            //moving cards to what ever discard thing we decide to do
            discardPile.Add(c1);
            discardPile.Add(c2);
            discardPile.Add(c3);
            //actual removal of the cards from the hand
            playerHand.Remove(c1.GetTerritoryName());
            playerHand.Remove(c2.GetTerritoryName());
            playerHand.Remove(c3.GetTerritoryName());
        }

        public void ShuffleDeck(List<Card> cards)
        {
            Random rng = new Random();
            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = cards[k];
                cards[k] = cards[n];
                cards[n] = value;
            }
        }



        public void drawCard()
        {
            if (!this.cardDrawn)
            {
                this.getCurrentPlayer().addCard(this.deck.Pop());
                this.cardDrawn = true;
            }
        }

        public void discardToDeck()
        {
            ShuffleDeck(this.discardPile);
            this.deck = new Stack<Card>(this.discardPile);
        }

        public void initializeDeck()
        {
            List<Card> toBeDeck = new List<Card>();


            toBeDeck.Add(new Card(0, "Afghanistan", Risk.Properties.Resources.AFGHANISTAN));
            toBeDeck.Add(new Card(0, "Alaska", Risk.Properties.Resources.ALASKA));
            toBeDeck.Add(new Card(0, "Alberta", Risk.Properties.Resources.ALBERTA));
            toBeDeck.Add(new Card(0, "Argentina", Risk.Properties.Resources.ARGENTINA));
            toBeDeck.Add(new Card(2, "Brazil", Risk.Properties.Resources.BRAZIL));
            toBeDeck.Add(new Card(1, "Central America", Risk.Properties.Resources.CENTRAL_AMERICA));
            toBeDeck.Add(new Card(1, "China", Risk.Properties.Resources.CHINA));
            toBeDeck.Add(new Card(1, "Congo", Risk.Properties.Resources.CONGO));
            toBeDeck.Add(new Card(2, "East Africa", Risk.Properties.Resources.EAST_AFRICA));
            toBeDeck.Add(new Card(0, "Eastern Australia", Risk.Properties.Resources.EASTERN_AUSTRALIA));
            toBeDeck.Add(new Card(2, "Eastern United States", Risk.Properties.Resources.EASTERN_UNITED_STATES));
            toBeDeck.Add(new Card(0, "Egypt", Risk.Properties.Resources.EGYPT));
            toBeDeck.Add(new Card(1, "Great Britain", Risk.Properties.Resources.GREAT_BRITAIN));
            toBeDeck.Add(new Card(1, "Greenland", Risk.Properties.Resources.GREENLAND));
            toBeDeck.Add(new Card(0, "Iceland", Risk.Properties.Resources.ICELAND));
            toBeDeck.Add(new Card(0, "India", Risk.Properties.Resources.INDIA));
            toBeDeck.Add(new Card(1, "Indonesia", Risk.Properties.Resources.INDONESIA));
            toBeDeck.Add(new Card(0, "Irkutsk", Risk.Properties.Resources.IRKUTSK));
            toBeDeck.Add(new Card(0, "Japan", Risk.Properties.Resources.JAPAN));
            toBeDeck.Add(new Card(1, "Kamchatka", Risk.Properties.Resources.KAMCHATKA));
            toBeDeck.Add(new Card(0, "Madagascar", Risk.Properties.Resources.MADAGASCAR));
            toBeDeck.Add(new Card(2, "Middle East", Risk.Properties.Resources.MIDDLE_EAST));
            toBeDeck.Add(new Card(2, "Mongolia", Risk.Properties.Resources.MONGOLIA));
            toBeDeck.Add(new Card(1, "New Guinea", Risk.Properties.Resources.NEW_GUINEA));
            toBeDeck.Add(new Card(0, "North Africa", Risk.Properties.Resources.NORTH_AFRICA));
            toBeDeck.Add(new Card(1, "Northern Europe", Risk.Properties.Resources.NORTHERN_EUROPE));
            toBeDeck.Add(new Card(2, "Northwest Territory", Risk.Properties.Resources.NORTHWEST_TERRITORY));
            toBeDeck.Add(new Card(1, "Ontario", Risk.Properties.Resources.ONTARIO));
            toBeDeck.Add(new Card(1, "Peru", Risk.Properties.Resources.PERU));
            toBeDeck.Add(new Card(2, "Quebec", Risk.Properties.Resources.QUEBEC));
            toBeDeck.Add(new Card(2, "Scandinavia", Risk.Properties.Resources.SCANDINAVIA));
            toBeDeck.Add(new Card(2, "Siam", Risk.Properties.Resources.SIAM));
            toBeDeck.Add(new Card(2, "Siberia", Risk.Properties.Resources.SIBERIA));
            toBeDeck.Add(new Card(2, "South Africa", Risk.Properties.Resources.SOUTH_AFRICA));
            toBeDeck.Add(new Card(1, "Southern Europe", Risk.Properties.Resources.SOUTHERN_EUROPE));
            toBeDeck.Add(new Card(2, "Ukraine", Risk.Properties.Resources.UKRAINE));
            toBeDeck.Add(new Card(1, "Ural", Risk.Properties.Resources.URAL));
            toBeDeck.Add(new Card(2, "Venezuela", Risk.Properties.Resources.VENEZUELA));
            toBeDeck.Add(new Card(2, "Western Australia", Risk.Properties.Resources.WESTERN_AUSTRALIA));
            toBeDeck.Add(new Card(0, "Western Europe", Risk.Properties.Resources.WESTERN_EUROPE));
            toBeDeck.Add(new Card(0, "Western United States", Risk.Properties.Resources.WESTERN_UNITED_STATES));
            toBeDeck.Add(new Card(1, "Yakutsk", Risk.Properties.Resources.YAKUTSK));
            ShuffleDeck(toBeDeck);
            this.deck = new Stack<Card>(toBeDeck);

        }

        public void rollDice()
        {
            attackerRolls = new List<int>();
            defenderRolls = new List<int>();
            int numOfAttackers = Math.Min(3, this.source.getNumTroops() - 1);
            int numOfDefenders = Math.Min(2, this.dest.getNumTroops());
            for (int i = 0; i < numOfAttackers; i++)
            {
                attackerRolls.Add(dice.Next(1, 6));
            }
            for (int i = 0; i < numOfDefenders; i++)
            {
                defenderRolls.Add(dice.Next(1, 6));
            }
            attackerRolls = attackerRolls.OrderByDescending(x => x).ToList();
            defenderRolls = defenderRolls.OrderByDescending(x => x).ToList();
        }

        public void attack()
        {

            int numOfAttackers = Math.Min(3, this.source.getNumTroops() - 1);
            int numOfDefenders = Math.Min(2, this.dest.getNumTroops());
            bool lostATroop = false;
            int destOwner = this.dest.getOwner();
            int srcOwner = this.source.getOwner();
            for (int i = 0; i < Math.Min(numOfAttackers, numOfDefenders); i++)
            {
                if (attackerRolls[i] > defenderRolls[i]) // attacker's highest roll is higher than defender's highest
                {
                    this.dest.decTroops();
                    this.dest.saveTroops();
                    
                    
                    if (destOwner != -1)
                    {
                        this.players[destOwner].incLostTroops();
                    }

                    this.players[srcOwner].incTroopsKilled();

                    if (this.dest.getNumTroops() == 0) // attacker has defeated last army in defender's territory
                    {
                        this.dest.setOwner(srcOwner);
                        this.getCurrentPlayer().getTerritories().Add(dest);
                        
                        this.players[srcOwner].incTerritoriesConquered();
                        if (destOwner != -1)
                        {
                            this.players[destOwner].incTerritoriesLost();
                            this.players[destOwner].getTerritories().Remove(dest);
                            //remove me
                            //this.players[srcOwner].AddTerritory(dest);
                            
                            if (this.players[destOwner].getTerritories().Count == 0) // player eliminated
                            {
                                this.playersLeft--;
                            }
                            
                        }
                        checkEndGame();

                        int numToMove = numOfAttackers;
                        if (lostATroop)
                        {
                            numToMove--;
                        }
                        for (int j = 0; j < numToMove; j++) //added -i for if this is on the second roll
                        {
                            this.source.decTroops();
                            this.dest.addTroops();
                        }
                        this.source.saveTroops();
                        this.dest.saveTroops();

                        //player took a territory so add a card to their hand
                        drawCard();
                    }
                }
                else //attacker's highest roll is less than or equal to defender's highest
                {
                    lostATroop = true;
                    this.source.decTroops();
                    this.source.saveTroops();
                    this.players[srcOwner].incLostTroops();
                    if (destOwner != -1)
                    {
                        this.players[destOwner].incTroopsKilled();
                    }
                }

            }
        }
        
        public void checkEndGame()
        {
            if (this.playersLeft == 1)
            {
                this.gameOver = true;
            }
        }
        
    }
}
