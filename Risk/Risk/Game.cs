using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
namespace TestGUI
{
    //Move all dis to RiskGame.cs at some point
    public class Game
    {

        private int numOfPlayers;
        private int currentPlayer;
        private int gamePhase;  // 0 = reinforce, 1 = attack, 2 = fortify
        private int reinforcements;
        private List<Territory> territories;
        private Dictionary<String, Territory> map;

        private List<Player> players;

        public Game()
        {
            this.map = new Dictionary<String, Territory>();
            this.players = new List<Player>();
            this.currentPlayer = 0;
            this.numOfPlayers = 2;
            this.gamePhase = 0;
            this.reinforcements = 15;
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

        public Game(int numOfPlayers)
        {

            this.numOfPlayers = numOfPlayers;
            this.map = new Dictionary<String, Territory>();
            this.players = new List<Player>();

            for (int i = 0; i < numOfPlayers; i++)
            {
                String name = "Player " + i;
                Player player = new Player(name, true); // For testing purposes; remove boolean later
                this.players.Add(player);
            }
        }

        public int getPhase() 
        {
            return 0;
        }

        public List<Territory> getTerritories()
        {
            return this.territories;
        }

        public List<Player> getPlayers() 
        {
            return this.players;
        }

        public void addTerritoryToMap(Territory terr)
        {
            this.map.Add(terr.getName(), terr);
        }

        public Dictionary<String, Territory> getMapList()
        {
            return this.map;
        }

        //public void reinforce(Player player, int allowedTroops)
        //{
        //    List<int> troops = new List<int>();
        //    for (int i = 0; i < player.getTerritories().Count; i++)
        //    {
        //        troops.Add(0);
        //    }


        //}

        public bool isOver()
        {
            return true;

        }

        public void clickTerritory(int index, Button button, Label label)
        {
            Territory current = this.territories[index];
            switch (this.gamePhase)
            {
                case 0:
                    if (reinforcements > 0 && current.getOwner() == currentPlayer)
                    {
                        current.addTroops();
                        reinforcements--;
                        button.Text = (current.getTemporaryReinforcements() + current.getNumTroops()) + "";
                        label.Text = "Reinforcements left: " + reinforcements.ToString();
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

        public void resetClick(List<Button> buttons, Label label)
        {
            foreach (Territory t in territories)
            {
                reinforcements += t.getTemporaryReinforcements();
                t.resetReinforcements();
            }
            label.Text = "Reinforcements left: " + reinforcements.ToString();
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Text = this.getTerritories()[i].getNumTroops().ToString();
            }
        }

        public Dictionary<String, Territory> makeMapFromXML(string xml)
        {
            if (xml.Equals(""))
                return null;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            foreach (XmlNode node in doc.DocumentElement.SelectNodes("territory"))
            {
                String name = node.SelectSingleNode("name").InnerText;
                String continent = node.SelectSingleNode("continent").InnerText;

                Territory terr = new Territory(continent, name);
                
                addTerritoryToMap(terr);
            }

            foreach (XmlNode node in doc.DocumentElement.SelectNodes("territory"))
            {
                if (node.SelectSingleNode("adjacent") != null)
                {
                    String adjacencies = node.SelectSingleNode("adjacent").InnerText;
                    string[] adjacenciesList = adjacencies.Split(':');
                    List<Territory> adjacencyList = new List<Territory>();

                    foreach (String adjName in adjacenciesList)
                    {
                        adjacencyList.Add(this.map[adjName]);
                    }
                    this.map[node.SelectSingleNode("name").InnerText].setAdjacencyList(adjacencyList);

                }
            }
            
            return this.map;
        }
    }
}
